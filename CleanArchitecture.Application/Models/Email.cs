﻿
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Application.Models
{
    public class Email
    {
        public string To { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

    }
    public class EmailToSendDto
    {
        [EmailAddress]
        public string EmailFrom { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public List<To> EmailsTo { get; set; } = new List<To>();
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool Html { get; set; } = false;
        public string HtmlBody { get; set; } = string.Empty;

        public class To
        {
            public string DisplayName { get; set; } = string.Empty;

            [EmailAddress]
            public string Email { get; set; } = string.Empty;
        }
    }
}
