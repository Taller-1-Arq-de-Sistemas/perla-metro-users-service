namespace PerlaMetroUsersService.DTOs.Auth
{
    public class RegisterPassengerResponseDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastNames { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = "Passenger";
        public string Token { get; set; } = null!;
    }
}