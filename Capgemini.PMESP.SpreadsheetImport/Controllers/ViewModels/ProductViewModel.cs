using System;
using System.Linq;
using Capgemini.PMESP.SpreadsheetImport.Models;

namespace Capgemini.PMESP.SpreadsheetImport.Controllers.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel() {}

        public ProductViewModel(Product product) 
        {
            Date = product.Date;
            Name = product.Name;
            Amount = product.Amount;
            UnitPrice = product.UnitPrice;
            Total = Amount * UnitPrice;
        }

        public DateTime Date { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }   
}