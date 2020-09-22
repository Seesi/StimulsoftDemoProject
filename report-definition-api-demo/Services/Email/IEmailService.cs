using System.Threading.Tasks;

namespace report_definition_api_demo.Services.Report.Email
{
    public interface IEmailService
    {
        Task SendEmail(string subject, string to, string message, string html, string attachment, string fileName);
    }
}
