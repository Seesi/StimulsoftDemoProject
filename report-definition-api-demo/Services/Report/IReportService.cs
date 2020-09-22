using System.Collections.Generic;
using System.Threading.Tasks;

namespace report_definition_api_demo.Services.Report
{
    public interface IReportService
    {
        Task SaveReportDefinitionAsync(ReportDefinitionDto reportDefinitionDto);
        Task<IEnumerable<ReportDefinitionDto>> GetAllAsync();
        Task<ReportDefinitionDto> GetReportAsync(int id);
    }
}
