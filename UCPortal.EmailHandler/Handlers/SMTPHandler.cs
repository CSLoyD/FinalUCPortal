using MimeKit;
using MailKit.Net.Smtp;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UCPortal.EmailHandler.EmailConfig;
using UCPortal.EmailHandler.EmailTemplate;

namespace UCPortal.EmailHandler.Handlers
{
    public class SMTPHandler : ISMTPHandler
    {
		private readonly Regex RegexStripHtml = new Regex("<[^>]*>", RegexOptions.Compiled);

		private SMTPEmailConfig _emailConfiguration;

		private EmailTemplates _emailTemplates;

		public SMTPHandler(EmailTemplates emailTemplates, SMTPEmailConfig emailConfiguration)
		{
			_emailConfiguration = emailConfiguration;
			_emailTemplates = emailTemplates;
		}
		public bool SendEmail(EmailDetails emailDetails, int type)
		{
			var message = new MimeMessage();
			
			message.To.Add(new MailboxAddress(emailDetails.To.Name, emailDetails.To.Address));
			message.From.Add(new MailboxAddress(
				"University of Cebu",
				"noreply@smtp.uc.edu.ph"));

			message.Subject = "Welcome to University of Cebu!";

			string content = string.Empty;

			if (type == (int)Enums.EmailType.VERIFICATIONCODE)
			{
				content = _emailTemplates.VerificationCode;
			}
			else if (type == (int)Enums.EmailType.RESETPASSWORD)
			{
				content = _emailTemplates.ResetPassword;
			}
			else if (type == (int)Enums.EmailType.OFFICIALENROLLMENT)
			{
				content = _emailTemplates.OfficialEnrolled;
			}

			foreach (var spefics in emailDetails.SpecificInfo)
			{
				content = content.Replace(spefics.Key, spefics.Value);
				message.Subject = message.Subject.Replace(spefics.Key, spefics.Value);
			}

			var bodyBuilder = GenerateBodyBuilder(content);

			message.Body = bodyBuilder.ToMessageBody();

			//Be careful that the SmtpClient class is the one from Mailkit not the framework!
			using (var emailClient = new SmtpClient())
			{
				//The last parameter here is to use SSL (Which you should!)
				emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);

				//Remove any OAuth functionality as we won't be using it. 
				emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

				emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

				emailClient.Send(message);

				emailClient.Disconnect(true);
			}

			return true;

		}

		public bool SendResetPassword(EmailDetails emailDetails)
		{
			var message = new MimeMessage();

			message.To.Add(new MailboxAddress(emailDetails.To.Name, emailDetails.To.Address));
			message.From.Add(new MailboxAddress(
				"University of Cebu",
				"no-reply.banilad@uc.edu.ph"));

			message.Subject = "Welcome to University of Cebu!";

			var content = _emailTemplates.ResetPassword;
			foreach (var spefics in emailDetails.SpecificInfo)
			{
				content = content.Replace(spefics.Key, spefics.Value);
				message.Subject = message.Subject.Replace(spefics.Key, spefics.Value);
			}

			var bodyBuilder = GenerateBodyBuilder(content);

			message.Body = bodyBuilder.ToMessageBody();

			//Be careful that the SmtpClient class is the one from Mailkit not the framework!
			using (var emailClient = new SmtpClient())
			{
				//The last parameter here is to use SSL (Which you should!)
				emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);

				//Remove any OAuth functionality as we won't be using it. 
				emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

				emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

				emailClient.Send(message);

				emailClient.Disconnect(true);
			}

			return true;

		}
		public BodyBuilder GenerateBodyBuilder(string body)
		{
			var bodyBuilder = new BodyBuilder();

			bodyBuilder.HtmlBody = body;
			bodyBuilder.TextBody = StripHtml(bodyBuilder.HtmlBody);

			return bodyBuilder;
		}
		private string StripHtml(string html)
		{
			return string.IsNullOrWhiteSpace(html) ? string.Empty : RegexStripHtml.Replace(html, string.Empty).Trim();
		}
	}

	public class EmailAddress
	{
		public string Name { get; set; }
		public string Address { get; set; }
	}
	public class EmailDetails
	{
		public EmailDetails()
		{
			SpecificInfo = new Dictionary<string, string>();
		}
		public EmailAddress To { get; set; }
		public Dictionary<string, string> SpecificInfo { get; set; }
	}


}
