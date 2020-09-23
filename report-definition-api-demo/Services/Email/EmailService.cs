using SendGrid;
using SendGrid.Helpers.Errors.Model;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace report_definition_api_demo.Services.Report.Email
{
    public class EmailService : IEmailService
    {
        public async Task SendEmail(string subject, string to, string message, string html, string attachment, string fileName)
        {

            var apiKey = "[Paste You Key or use Environment Variables]";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("seesi@accedegh.net", "seesi@accedegh.net");
            var _to = new EmailAddress(to, to);
            var plainTextContent = message;
            var htmlContent = html;
            var msg = MailHelper.CreateSingleEmail(from, _to, subject, plainTextContent, htmlContent);
            msg.AddAttachment(new Attachment()
            {
                Content = attachment,
                Type = System.Net.Mime.MediaTypeNames.Application.Pdf,
                Filename = fileName
            });
            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
                throw new SendGridInternalException($"Emails sending failed with status code {response.StatusCode}");

        }
    }
}
