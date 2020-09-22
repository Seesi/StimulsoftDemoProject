using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace report_definition_api_demo.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReportDefinitionDto>> GetAllAsync()
        {
            var list = await _context.ReportDefinitions.ToListAsync();
            return list.Select(index => new ReportDefinitionDto()
            {
                Id = index.Id,
                Definition = index.Definition,
                Name = index.Name,
                Engine = index.Engine,
                ReportIdent = index.ReportIdentitty.ToString(),
            });
        }

        public async Task<ReportDefinitionDto> GetReportAsync(int id)
        {
            var report = await _context.ReportDefinitions.FirstOrDefaultAsync(m => m.Id == id);
            if (report != null)
            {
                return new ReportDefinitionDto()
                {
                    Id = report.Id,
                    Definition = report.Definition,
                    Name = report.Name,
                    Engine = report.Engine,
                    ReportIdent = report.ReportIdentitty.ToString(),
                };
            }
            else throw new ArgumentException($"No Report matche id {id}");
        }

        public async Task SaveReportDefinitionAsync(ReportDefinitionDto reportDefinitionDto)
        {
            await CreateOrUpdateReportDefinition(reportDefinitionDto);
        }

        private async Task CreateOrUpdateReportDefinition(ReportDefinitionDto reportDefinitionDto)
        {
            if (reportDefinitionDto != null)
            {
                if (reportDefinitionDto.Id != default)
                {
                    if (await _context.ReportDefinitions.AnyAsync(m => m.Id == reportDefinitionDto.Id))
                    {
                        var reportToBeUpdated = await _context.ReportDefinitions.FirstAsync(m => m.Id == reportDefinitionDto.Id);
                        reportToBeUpdated.Name = reportDefinitionDto.Name;
                        reportToBeUpdated.Definition = reportDefinitionDto.Definition;
                        reportToBeUpdated.ReportIdentitty = Guid.Parse(reportDefinitionDto.ReportIdent);
                        _context.ReportDefinitions.Update(reportToBeUpdated);
                    }
                    else throw new ArgumentException($"Cannot update report that doesn't exist in database, Report Id: {reportDefinitionDto.Id}");
                }
                else
                {
                    _context.ReportDefinitions.Add(new ReportDefinition()
                    {
                        ReportIdentitty = Guid.Parse(reportDefinitionDto.ReportIdent),
                        Definition = reportDefinitionDto.Definition,
                        Engine = reportDefinitionDto.Engine,
                        Name = reportDefinitionDto.Name
                    });
                }
                await _context.SaveChangesAsync();
            }
            else
                throw new ArgumentException($"Report Definition cannot be empty");
        }
    }
    public class IntToStringConverter : JsonConverter<int>
    {
        public override int Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
                if (Utf8Parser.TryParse(span, out int number, out int bytesConsumed) && span.Length == bytesConsumed)
                {
                    return number;
                }

                if (int.TryParse(reader.GetString(), out number))
                {
                    return number;
                }
            }

            return reader.GetInt32();
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
