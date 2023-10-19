using CleanArchitecture.Application.Models.EmailModels;

namespace CleanArchitecture.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(Email email);
    }


}
