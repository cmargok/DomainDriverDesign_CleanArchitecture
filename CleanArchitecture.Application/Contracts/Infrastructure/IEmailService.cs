using CleanArchitecture.Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(Email email);
    }
    //var email = new EmailToSendDto()
    //{
    //    DisplayName = "CQRS managment",
    //    EmailFrom = "cmargokk@hotmail.com",
    //    Html = false,

    //    Message
    //            EmailsTo = new List<EmailToSendDto.To> {
    //                new EmailToSendDto.To
    //                {

    //                    Email

    //                }
    //            }

    //};

}
