using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RCCodeTest.Dal;
using RCCodeTest.Models;
using RCCodeTest.Models.Plugins;

namespace RCCodeTest.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductDataAjaxHandler(jQueryDataTableParamModel param)
        {
            System.Collections.Generic.IEnumerable<Models.Product> results = null;

            if (Session["ProductResultSet"] == null)
            {
                ProductDAL ProdDal = new ProductDAL();
                results = ProdDal.GetProducts();

                Session["ProductResultSet"] = results;
            }
            else
            {
                results = ((System.Collections.Generic.IEnumerable<Models.Product>)Session["ProductResultSet"]).ToList();
            }

            IEnumerable<Models.Product> filteredResults;

            if (!String.IsNullOrEmpty(param.sSearch))
            {
                filteredResults = results.Where(x => (!String.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(param.sSearch.ToLower())) ||
                                                     (!String.IsNullOrEmpty(x.ProductNumber) && x.ProductNumber.ToLower().Contains(param.sSearch.ToLower())) ||
                                                     (!String.IsNullOrEmpty(x.Color) && x.Color.ToLower().Contains(param.sSearch.ToLower())) ||
                                                     (!String.IsNullOrEmpty(x.SafetyStockLevel.ToString()) && x.SafetyStockLevel.ToString().Contains(param.sSearch.ToLower())) ||
                                                     (!String.IsNullOrEmpty(x.ReorderPoint.ToString()) && x.ReorderPoint.ToString().Contains(param.sSearch.ToLower())) ||
                                                     (!String.IsNullOrEmpty(x.StandardCost.ToString()) && x.StandardCost.ToString().Contains(param.sSearch.ToLower())) ||
                                                     (!String.IsNullOrEmpty(x.ListPrice.ToString()) && x.ListPrice.ToString().Contains(param.sSearch.ToLower()))).ToList();
            }
            else
            {
                filteredResults = results.ToList();
            }

            var sortColumnIndex = string.IsNullOrEmpty(Request["iSortCol_0"]) ? 0 : Convert.ToInt32(Request["iSortCol_0"]);
            Func<Models.Product, string> orderingFunction = (x => sortColumnIndex == 0 ? x.Name :
                                                                sortColumnIndex == 1 ? x.ProductNumber :
                                                                sortColumnIndex == 2 ? x.Color :
                                                                sortColumnIndex == 3 ? x.SafetyStockLevel.ToString() :
                                                                sortColumnIndex == 4 ? x.ReorderPoint.ToString() :
                                                                sortColumnIndex == 5 ? x.StandardCost.ToString() :
                                                                x.ListPrice.ToString());

            var sortDirection = string.IsNullOrEmpty(Request["sSortDir_0"]) ? "asc" : Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredResults = filteredResults.OrderBy(orderingFunction).ToList();
            else
                filteredResults = filteredResults.OrderByDescending(orderingFunction).ToList();

            var displayResults = param.iDisplayLength == -1 ? filteredResults.ToList() : filteredResults.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = results.ToList().Count(),
                iTotalDisplayRecords = results.ToList().Count(),
                iDisplayLength = param.iDisplayLength,
                aaData = displayResults.ToList()
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string UpdateData(int id, string value, int? rowId, int? columnPosition, int? columnId, string columnName)
        {
            try
            {
                ProductDAL ProdDal = new ProductDAL();
                ProdDal.UpdateProduct(id, columnName, value);
                Session["ProductResultSet"] = null;

                return value;
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public int AddData(string ProductId, string Name, string ProductNumber, string SafetyStockLevel, string ReorderPoint, string StandardCost, string ListPrice)
        {   /*
            var companies = DataRepository.GetCompanies();
            if (companies.Any(c => c.Name.ToLower().Equals(name.ToLower())))
            {
                Response.Write("Company with the name '" + name + "' already exists");
                Response.StatusCode = 404;
                Response.End();
                return -1;
            }

            var company = new Company();
            company.Name = name;
            company.Address = address;
            company.Town = town;
            companies.Add(company);
            return company.ID;
            */
            return 0;
        }
    }
}
