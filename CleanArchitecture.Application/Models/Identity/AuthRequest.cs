
namespace CleanArchitecture.Application.Models.Identity
{
    public class AuthRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AuthResponse
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }

    public class JwtSettings 
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string DurationInMinutes { get; set; } = string.Empty;
    }
    public class RegistrationRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegistrarionResponse
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }

    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);

        Task<RegistrarionResponse> Register(RegistrationRequest request);
    }

    public class AuthService : IAuthService
    {
        public Task<AuthResponse> Login(AuthRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<RegistrarionResponse> Register(RegistrationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
