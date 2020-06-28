using AssignKudosContext.Gateways;
using EmailGateway.Configurations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace EmailGateway.AssignKudos
{
    public class EmailSenderService : ISendNotificationsService
    {
        SmtpClient _client;
        string _respondingEmail;
        string _displayNameResponder;
        string[] _responderGroup;
        EmailCheckerService _emailCheckerService;

        public EmailSenderService(ISmtpConfiguration configuration, EmailCheckerService emailCheckerService)
        {
            _client = new SmtpClient(configuration.Server);
            _client.UseDefaultCredentials = false;
            _client.Credentials = new NetworkCredential(configuration.User, configuration.Password);
            _client.Port = configuration.Port;
            _client.EnableSsl = true;

            _respondingEmail = configuration.User;
            _displayNameResponder = configuration.UserDisplayName;
            _responderGroup = configuration.DigitalGroup.Take(4).ToArray();

            _emailCheckerService = emailCheckerService;
        }

        private void BroadcastAssigningKudosForAll(string whom, string forWhat, string whoFrom)
        {
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_respondingEmail, _displayNameResponder);
            foreach (var userFromGroup in _responderGroup)
            {
                mailMessage.To.Add(userFromGroup);
            }

            mailMessage.IsBodyHtml = true;
            string body = string.Empty;
            string[] lines = File.ReadAllLines(@"wwwroot\Templates\EmailForAll.htm");
            foreach(var line in lines)
            {
                body += line;
            }
            body = body.Replace("{{ForWhat}}", forWhat ?? "Special kudos for you");
            body = body.Replace("{{Whom}}", whom ?? "for Mr. Somebody");
            body = body.Replace("{{WhoFrom}}", whoFrom ?? "we don't know");
            mailMessage.Subject = "Kudos for " + whom ?? "you";
            
            Attachment att = new Attachment(@"wwwroot\Templates\EmailForAll_files\image001.jpg");
            att.ContentDisposition.Inline = true;
            mailMessage.Attachments.Add(att);

            mailMessage.AlternateViews.Add(getEmbeddedImage(body, @"wwwroot\Templates\EmailForAll_files\image001.jpg"));
            //mailMessage.Body = body;
            _client.Send(mailMessage);
        }

        private AlternateView getEmbeddedImage(string body, string filePath)
        {
            LinkedResource res = new LinkedResource(filePath, MediaTypeNames.Image.Jpeg);
            res.ContentId = Guid.NewGuid().ToString();
            string htmlBody = body.Replace("[magicEggs]", res.ContentId);
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(res);
            return alternateView;
        }

        private void BroadcastAssigningKudosForRecipents(string whom, string forWhat, string whoFrom, IList<string> emailsWhom)
        {
            if (emailsWhom == null || emailsWhom.Count() == 0)
            {
                return;
            }


            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_respondingEmail, _displayNameResponder);

            foreach (var emailWhom in emailsWhom)
            {
                mailMessage.To.Add(emailWhom);
            }

            mailMessage.IsBodyHtml = true;
            string body = string.Empty;
            string[] lines = File.ReadAllLines(@"wwwroot\Templates\EmailForRecipent.htm");
            foreach (var line in lines)
            {
                body += line;
            }
            body = body.Replace("{{ForWhat}}", forWhat ?? "Special kudos for you");
            body = body.Replace("{{WhoFrom}}", whoFrom ?? "we don't know");
            mailMessage.Subject = "Kudos for " + whom ?? "you";

            Attachment att = new Attachment(@"wwwroot\Templates\EmailForRecipent_files\image001.jpg");
            att.ContentDisposition.Inline = true;
            mailMessage.Attachments.Add(att);

            mailMessage.AlternateViews.Add(getEmbeddedImage(body, @"wwwroot\Templates\EmailForRecipent_files\image001.jpg"));
            //mailMessage.Body = body;
            _client.Send(mailMessage);
        }

        public void BroadcastAssigningKudos(string whom, string forWhat, string whoFrom)
        {
            if (_emailCheckerService.IsEmailInText(whom))
            {
                // Now we can send email to all people in rockwool domain
                BroadcastAssigningKudosForRecipents(whom, forWhat, whoFrom, _emailCheckerService.GetRockwoolEmails(whom));
            }
            else if (_emailCheckerService.IsBeginningEmailInText(whom))
            {
                BroadcastAssigningKudosForRecipents(whom, forWhat, whoFrom, _emailCheckerService.GetEmailsFromListByBeginning(whom));
            }

            BroadcastAssigningKudosForAll(whom, forWhat, whoFrom);
        }
    }
}
