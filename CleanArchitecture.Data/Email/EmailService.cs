using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace CleanArchitecture.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        public EmailSettings emailSettings { get; }
        public ILogger<EmailService> Logger { get; }
        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            this.emailSettings = emailSettings.Value;
            Logger = logger;
        }

        public async Task<bool> SendEmailAsync(Application.Models.Email email)
        {
            var emailRequest = new EmailToSendDto()
            {
                //DisplayName = "CQRS managment",
                //EmailFrom = "cmargokk@hotmail.com",

                DisplayName = emailSettings.FromDisplayName,
                EmailFrom = emailSettings.FromAdress,
                Html = false,
                Subject = email.Subject,
                Message = email.Body,
                EmailsTo = new List<EmailToSendDto.To> {
                    new EmailToSendDto.To
                    {

                        Email = email.To,
                        DisplayName = email.DisplayName,


                    }
                }
            };

            using var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri("https://localhost:7001/api/v1/Email/");

                var requestContent = new StringContent(JsonConvert.SerializeObject(emailRequest), Encoding.UTF8, "application/json");

                var response  = await client.PostAsync("SendEmail", requestContent);

            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            var h = await response.Content.ReadAsStringAsync();
            Logger.LogError("el email no fue enviado con exito");

            return false;

        }
    }
}
