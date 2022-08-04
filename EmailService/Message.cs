﻿using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace EmailService
{
    public class Message
    {
        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(str => new MailboxAddress(str)));
            Subject = subject;
            Content = content;
        }

        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}