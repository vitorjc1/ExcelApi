using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Capgemini.PMESP.SpreadsheetImport.Models
{
    public class Import
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public List<Product> Products { get; set;}
    }
}