namespace PerlaMetroUsersService.Util;

/// <summary>
/// Transforms route parameters to lowercase.
/// </summary>
public sealed class LowercaseParameterTransformer : IOutboundParameterTransformer
{
    /// <summary>
    /// Transforms the given value to lowercase invariant.
    /// </summary>
    /// <param name="value">The value to transform.</param>
    /// <returns>The transformed value.</returns>
    public string? TransformOutbound(object? value)
        => value?.ToString()?.ToLowerInvariant();
}
