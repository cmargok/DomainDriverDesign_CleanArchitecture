using CleanArchitecture.Application.Models;


namespace CleanArchitecture.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(Email email);
    }


}
