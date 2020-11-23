using Capgemini.PMESP.SpreadsheetImport.Models;
using Microsoft.EntityFrameworkCore;

namespace Capgemini.PMESP.SpreadsheetImport.Db
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Import> Imports { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}