using System.Threading.Tasks;
using Capgemini.PMESP.SpreadsheetImport.Dtos;
using Microsoft.AspNetCore.Http;

namespace Capgemini.PMESP.SpreadsheetImport.Services.Interfaces
{
    public interface IImportService
    {
        Task<ImportResponse> CreateImportAsync(IFormFile file);
    }
}