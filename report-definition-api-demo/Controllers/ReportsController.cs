using Microsoft.AspNetCore.Mvc;
using report_definition_api_demo.Services.Report;
using report_definition_api_demo.Services.Report.Email;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace report_definition_api_demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IEmailService _emailService;

        public ReportsController(IReportService reportService, IEmailService emailService)
        {
            _reportService = reportService;
            _emailService = emailService;
        }

        [HttpGet(nameof(Get))]
        public async Task<IEnumerable<ReportDefinitionDto>> Get()
        {
            return await _reportService.GetAllAsync();
        }
        [HttpGet(nameof(GetReport))]
        public async Task<ReportDefinitionDto> GetReport(int id)
        {
            return await _reportService.GetReportAsync(id);
        }

        [HttpPost(nameof(Post))]
        public async Task Post(ReportDefinitionDto dto)
        {
            await _reportService.SaveReportDefinitionAsync(dto);
        }

        [HttpPost(nameof(SendMail))]
        public async Task SendMail(int reportId, string emailAddress)
        {
            var reportDefinition = await _reportService.GetReportAsync(reportId);
            var newreport = new StiReport();
            var loadedReport = newreport.LoadFromJson(reportDefinition.Definition);
            var renderReport = await loadedReport.RenderAsync();
            string base64ReportString = BuildReport(renderReport);
            string message = "Please find attached a copy of report recently generated with the Stimulsoft Report Design Engine in my Demo app";
            await _emailService.SendEmail("Demo Stimulsoft Report", emailAddress, message, "", base64ReportString, $"{reportDefinition.Name}.pdf");
        }

        private string BuildReport(StiReport report)
        {
            Byte[] bytesArray;
            using (var stream = new MemoryStream())
            {
                report.ExportDocument(StiExportFormat.Pdf, stream);
                bytesArray = stream.ToArray();
            }
            return Convert.ToBase64String(bytesArray);
        }
    }
}
