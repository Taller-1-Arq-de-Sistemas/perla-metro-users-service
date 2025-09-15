using PerlaMetroUsersService.Data;
using Microsoft.EntityFrameworkCore;
using PerlaMetroUsersService.Repositories;
using PerlaMetroUsersService.Repositories.Interfaces;
using PerlaMetroUsersService.Services.Interfaces;
using PerlaMetroUsersService.Services;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;
using Prometheus;
using PerlaMetroUsersService.Util;
using PerlaMetroUsersService.Middlewares;

namespace PerlaMetroUsersService.Extensions
{
    public static class AppServiceExtensions
    {
        public const string AllowAllCorsPolicy = "AllowAll";
        public const string RestrictedCorsPolicy = "Restricted";

        // Presentation layer/service registrations for the web app
        public static void AddWebApp(this IServiceCollection services, IConfiguration config)
        {
            // MVC + lowercase route tokens
            services
                .AddControllers(options =>
                {
                    options.Conventions.Add(
                        new RouteTokenTransformerConvention(new LowercaseParameterTransformer()));
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    // Unify automatic 400 responses (model validation) with our ProblemDetails style
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var problem = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Title = "Validation failed",
                            Detail = "One or more validation errors occurred.",
                            Instance = context.HttpContext.Request.Path,
                            Type = "https://httpstatuses.io/400"
                        };
                        problem.Extensions["traceId"] = context.HttpContext.TraceIdentifier;

                        return new BadRequestObjectResult(problem)
                        {
                            ContentTypes = { "application/json" }
                        };
                    };
                });

            // Output caching registration
            services.AddOutputCache(options =>
            {
                // Avoid stale API responses for mutable resources; opt-in per-endpoint in future
                options.AddBasePolicy(builder => builder.NoCache());
            });

            // Health checks (readiness: DB)
            services.AddHealthChecks()
                .AddCheck<Health.DataContextHealthCheck>("database", tags: ["ready"]);

            // CORS policies (AllowAll for dev, Restricted for prod)
            AddCorsPolicies(services, config);
        }

        // Compose the middleware pipeline in the right order
        public static WebApplication UseWebApp(this WebApplication app)
        {
            app.UseOutputCache();

            app.UseProblemDetailsExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.MapDocumentation();
                app.UseCors(AllowAllCorsPolicy);
            }
            else
            {
                app.UseCors(RestrictedCorsPolicy);
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseHttpMetrics();
            app.MapMetrics("/metrics");
            app.MapHealthEndpoints();
            app.MapControllers();

            return app;
        }
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            AddServices(services);
            AddOpenApiMapper(services);
            AddDbContext(services, config);
            AddUnitOfWork(services);
            AddHttpContextAccessor(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddSingleton<IClockService, SystemClockService>();
            services.AddSingleton<IPasswordHasherService, BCryptPasswordHasherService>();
            services.AddSingleton<IJwtService, JwtService>();
        }

        private static void AddOpenApiMapper(IServiceCollection services)
        {
            // Explorer and OpenAPI document generation
            services.AddEndpointsApiExplorer();
            services.AddOpenApi();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration config)
        {
            var connectionUrl = config.GetConnectionString("AppDb") ??
                throw new InvalidOperationException("Connection string 'AppDb' not found.");

            services.AddDbContext<DataContext>(opt =>
                opt.UseNpgsql(connectionUrl, npgsql =>
                {
                    // Enable transient failure retries for DB connectivity
                    npgsql.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorCodesToAdd: null);
                }));
        }

        private static void AddUnitOfWork(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddCorsPolicies(IServiceCollection services, IConfiguration config)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowAllCorsPolicy, policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });

                var origins = config.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
                if (origins.Length == 0)
                {
                    // Provide a conservative default to avoid failures in prod if not configured
                    origins = ["http://localhost", "http://127.0.0.1"];
                }

                options.AddPolicy(RestrictedCorsPolicy, policy =>
                {
                    policy.WithOrigins(origins)
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
        }

        private static void AddHttpContextAccessor(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
        }

        public static void MapDocumentation(this WebApplication app)
        {
            // Serve OpenAPI JSON at /openapi/v1.json and Scalar UI at /scalar
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options.Title = "Perla Metro Users Service";
            });
        }

        public static void MapHealthEndpoints(this WebApplication app)
        {
            // Liveness: basic process up check (no external deps)
            app.MapHealthChecks("/healthz", new HealthCheckOptions
            {
                Predicate = _ => false,
                ResponseWriter = WriteBasicLiveness
            });

            // Readiness: checks tagged as "ready" (e.g., DB)
            app.MapHealthChecks("/readyz", new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("ready"),
                ResponseWriter = WriteDetailedReadiness
            });

            // Combined detailed health (all checks)
            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = WriteDetailedReadiness
            });
        }

        private static Task WriteBasicLiveness(HttpContext context, HealthReport report)
        {
            context.Response.ContentType = "application/json";
            var payload = JsonSerializer.Serialize(new
            {
                status = "ok",
                uptime = (DateTimeOffset.UtcNow - System.Diagnostics.Process.GetCurrentProcess().StartTime.ToUniversalTime()).TotalSeconds
            });
            return context.Response.WriteAsync(payload);
        }

        private static Task WriteDetailedReadiness(HttpContext context, HealthReport report)
        {
            context.Response.ContentType = "application/json";
            var payload = JsonSerializer.Serialize(new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(e => new
                {
                    name = e.Key,
                    status = e.Value.Status.ToString(),
                    description = e.Value.Description,
                    duration = e.Value.Duration.TotalMilliseconds
                }),
                totalDuration = report.TotalDuration.TotalMilliseconds
            });
            return context.Response.WriteAsync(payload);
        }
    }
}
