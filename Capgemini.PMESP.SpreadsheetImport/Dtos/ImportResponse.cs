using System.Collections.Generic;

namespace Capgemini.PMESP.SpreadsheetImport.Dtos
{
    public class ImportResponse
    {
        public int ImportId { get; set; }
        public bool Success { get; set; }
        public List<ImportError> Errors { get; set; }
    }
}