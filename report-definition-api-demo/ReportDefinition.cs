using System;

namespace report_definition_api_demo
{
    public class ReportDefinition
    {
        public int Id { get; set; }
        public Guid ReportIdentitty { get; set; }
        public string Name { get; set; }
        public string Engine { get; set; }
        public string Definition { get; set; }
        public string Permission { get; set; }
    }
}
