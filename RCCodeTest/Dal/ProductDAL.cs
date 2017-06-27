using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RCCodeTest.Models;

namespace RCCodeTest.Dal
{
    public class ProductDAL
    {
        /// <summary>
        /// UpdateProduct
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Models.Product> GetProducts()
        {
            using (var productContext = new ProductDataContext())
            {
                return (from p in productContext.Products
                        select new Models.Product
                        {
                            ProductID = p.ProductID,
                            Color = p.Color,
                            ListPrice = p.ListPrice,
                            Name = p.Name,
                            ProductNumber = p.ProductNumber,
                            ReorderPoint = p.ReorderPoint,
                            SafetyStockLevel = p.SafetyStockLevel,
                            StandardCost = p.StandardCost
                        }).ToList();

            }
        }

        /// <summary>
        /// UpdateProduct
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="columnName"></param>
        /// <param name="newValue"></param>
        public void UpdateProduct(int productId, string columnName, string newValue)
        {
            using (var productContext = new ProductDataContext())
            {
                decimal number;
                Int16 shortNumber;
                var productToUpdate = (from p in productContext.Products
                                       where p.ProductID == productId
                                       select p).FirstOrDefault();

                if (productToUpdate != null)
                {
                    switch (columnName)
                    {
                        case "Color":
                            productToUpdate.Color = newValue;
                            break;
                        case "List Price":
                            productToUpdate.ListPrice = (Decimal.TryParse(newValue, out number) ? Convert.ToDecimal(newValue) : productToUpdate.ListPrice);
                            break;
                        case "Name":
                            productToUpdate.Name = newValue;
                            break;
                        case "Product Number":
                            productToUpdate.ProductNumber = newValue;
                            break;
                        case "Reorder Point":
                            productToUpdate.ReorderPoint = (Int16.TryParse(newValue, out shortNumber) ? Convert.ToInt16(newValue) : productToUpdate.ReorderPoint);
                            break;
                        case "Safety Stock Level":
                            productToUpdate.SafetyStockLevel = (Int16.TryParse(newValue, out shortNumber) ? Convert.ToInt16(newValue) : productToUpdate.SafetyStockLevel);
                            break;
                        case "Standard Cost":
                            productToUpdate.StandardCost = (Decimal.TryParse(newValue, out number) ? Convert.ToDecimal(newValue) : productToUpdate.StandardCost);
                            break;
                        default:
                            break;

                    }

                    productContext.SubmitChanges();
                }
            }
        }


        /// <summary>
        /// AddProduct
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="productNumber"></param>
        /// <param name="safetyStockLevel"></param>
        /// <param name="reorderPoint"></param>
        /// <param name="standardCost"></param>
        /// <param name="listPrice"></param>
        public void AddProduct(string name, string color, string productNumber, string safetyStockLevel, string reorderPoint, string standardCost, string listPrice)
        {
            using (var productContext = new ProductDataContext())
            {
                Decimal number;
                Int16 shortNumber;

                Product newProduct = new Product
                {
                    Color = color,
                    ListPrice = (Decimal.TryParse(listPrice, out number) ? Convert.ToDecimal(listPrice) : Decimal.MinValue),
                    Name = name,
                    ProductNumber = productNumber,
                    ReorderPoint = (Int16.TryParse(reorderPoint, out shortNumber) ? Convert.ToInt16(reorderPoint) : Int16.MinValue),
                    SafetyStockLevel = (Int16.TryParse(safetyStockLevel, out shortNumber) ? Convert.ToInt16(safetyStockLevel) : Int16.MinValue),
                    StandardCost = (Decimal.TryParse(standardCost, out number) ? Convert.ToDecimal(standardCost) : Decimal.MinValue),
                    ModifiedDate = DateTime.Now,
                    SellStartDate = DateTime.Now,
                    FinishedGoodsFlag = true,
                    MakeFlag = true,
                    rowguid = Guid.NewGuid() 
                };

                productContext.Products.InsertOnSubmit(newProduct);
                productContext.SubmitChanges();
            }
        }
    }
}