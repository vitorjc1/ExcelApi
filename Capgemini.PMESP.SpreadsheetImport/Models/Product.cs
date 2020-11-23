using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capgemini.PMESP.SpreadsheetImport.Models
{
    public class Product
    {
        public Product() { }
        public Product(DateTime date, string name, int amount, decimal unitPrice)
        {
            Date = date;
            Name = name;
            Amount = amount;
            UnitPrice = unitPrice;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "The name must have less than 50 characters")]
        public string Name { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The amount must be greater than zero")]
        public int Amount { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        [ForeignKey("ImportId")]
        public Import Import { get; set; }
    }
}