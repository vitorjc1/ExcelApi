using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capgemini.PMESP.SpreadsheetImport.Db;
using Capgemini.PMESP.SpreadsheetImport.Dtos;
using Capgemini.PMESP.SpreadsheetImport.Models;
using Capgemini.PMESP.SpreadsheetImport.Services.Interfaces;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;

namespace Capgemini.PMESP.SpreadsheetImport.Services
{
    public class ImportService : IImportService
    {
        private readonly DataContext _context;

        public ImportService(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<ImportResponse> CreateImportAsync(IFormFile file)
        {
            var products = new List<Product>();
            var errors = new List<ImportError>();

            try
            {
                using (XLWorkbook wb = new XLWorkbook(file.OpenReadStream()))
                {
                    var worksheet = wb.Worksheet(1);

                    bool isValid = ValidateSpreadsheet(worksheet, products, errors);

                    if (isValid)
                    {
                        int importId = await SaveImportAsync(products);
                        return new ImportResponse { ImportId = importId, Success = true };
                    }
                    else
                    {
                        return new ImportResponse { Success = false, Errors = errors };
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Extensão de arquivo não suportada");
            }
        }

        private async Task<int> SaveImportAsync(List<Product> products)
        {
            var import = new Import { Date = DateTime.Now };

            _context.Imports.Add(import);

            await _context.SaveChangesAsync();

            foreach (var product in products)
            {
                product.Import = import;

                _context.Products.Add(product);

                await _context.SaveChangesAsync();
            }

            return import.Id;
        }

        private bool ValidateSpreadsheet(IXLWorksheet ws, List<Product> products, List<ImportError> errors)
        {
            // number of rows used (empty rows aren't considered)
            int rows = ws.RowsUsed().Count();

            // i = 2 --> first row after header
            for (int i = 2; i <= rows; i++)
            {
                int empty = 0;
                DateTime date = DateTime.Now;
                string name = null;
                int amount = 0;
                decimal unitPrice = 0;

                for (int j = 'A'; j <= 'D'; j++)
                {
                    // string value of one cell
                    string stringValue = ws.Cell(Convert.ToChar(j) + i.ToString()).Value.ToString();

                    if (string.IsNullOrEmpty(stringValue)) empty++;

                    switch (j)
                    {
                        case 'A':
                            try
                            {
                                date = Convert.ToDateTime(stringValue);

                                if (date <= DateTime.Today)
                                {
                                    errors.Add(new ImportError(i.ToString(), Convert.ToChar(j) + i.ToString(), "O campo Data de Entrega não pode ser menor ou igual que o dia atual."));
                                }
                            }
                            catch (Exception)
                            {
                                errors.Add(new ImportError(i.ToString(), Convert.ToChar(j) + i.ToString(), "Formato de data inválido."));
                            }
                            break;

                        case 'B':
                            if (stringValue.Length <= 50 && stringValue.Length > 0)
                            {
                                name = stringValue;
                            }
                            else
                            {
                                errors.Add(new ImportError(i.ToString(), Convert.ToChar(j) + i.ToString(), "O campo Nome do Produto não pode exceder 50 caractéres."));
                            }
                            break;

                        case 'C':
                            try
                            {
                                amount = Convert.ToInt32(stringValue);
                                if (amount <= 0)
                                {
                                    errors.Add(new ImportError(i.ToString(), Convert.ToChar(j) + i.ToString(), "O campo Quantidade não pode ser menor ou igual a zero."));
                                }
                            }
                            catch (Exception)
                            {
                                errors.Add(new ImportError(i.ToString(), Convert.ToChar(j) + i.ToString(), "Formato inválido para o campo Quantidade."));
                            }
                            break;

                        case 'D':
                            try
                            {
                                unitPrice = Convert.ToDecimal(stringValue);
                                unitPrice = Math.Round(unitPrice, 2);
                                if (unitPrice <= 0)
                                {
                                    errors.Add(new ImportError(i.ToString(), Convert.ToChar(j) + i.ToString(), "O campo Valor Unitário não pode ser menor ou igual a zero."));
                                }
                            }
                            catch (Exception)
                            {
                                errors.Add(new ImportError(i.ToString(), Convert.ToChar(j) + i.ToString(), "Formato inválido para o campo Valor Unitário."));
                            }
                            break;
                    }
                }
                if (errors.Count == 0)
                {
                    products.Add(new Product(date, name, amount, unitPrice));
                }

                // check if row is empty and increment row amount
                if (empty == 4)
                {
                    rows++;
                }
            }

            if (products.Count > 0 && errors.Count == 0)
            {
                return true;
            }

            return false;
        }
    }
}