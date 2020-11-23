namespace Capgemini.PMESP.SpreadsheetImport.Dtos
{
    public class ImportError
    {
        public ImportError(string row, string cell, string error)
        {
            Row = row;
            Cell = cell;
            Error = error;
        }
        public string Row { get; set; }
        public string Cell { get; set; }
        public string Error { get; set; }
    }
}