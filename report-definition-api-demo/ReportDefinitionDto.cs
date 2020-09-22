using report_definition_api_demo.Services.Report;
using System.Text.Json.Serialization;

namespace report_definition_api_demo
{
    public class ReportDefinitionDto
    {
        [JsonConverter(typeof(IntToStringConverter))]
        public int Id { get; set; }
        public string ReportIdent { get; set; }
        public string Name { get; set; }
        public string Engine { get; set; }
        public string Definition { get; set; }
    }
}
