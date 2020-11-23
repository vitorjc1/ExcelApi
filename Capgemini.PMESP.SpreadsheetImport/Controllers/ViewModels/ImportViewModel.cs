using System;
using System.Collections.Generic;
using System.Linq;
using Capgemini.PMESP.SpreadsheetImport.Models;

namespace Capgemini.PMESP.SpreadsheetImport.Controllers.ViewModels
{
    public class ImportViewModel
    {
        public ImportViewModel() {}

        public ImportViewModel(Import import, bool loadList) 
        {
            Id = import.Id;
            Date = import.Date;
            Amount = import.Products.Count;
            ClosestDeliveryDate = import.Products.Min(i => i.Date);
            Total = import.Products.Sum(s => s.Amount * s.UnitPrice);

            if (loadList)
            {
                Products = import.Products.Select(s => new ProductViewModel(s)).ToList();
            }
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public DateTime ClosestDeliveryDate { get; set; }
        public decimal Total { get; set; }
        public List<ProductViewModel> Products  { get; set; }
    }   
}