using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgileMetricsIntegrator.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using ExcelDataReader;
using System.Data;

namespace AgileMetricsIntegrator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Chart()
        {
            return View();
        }
        public IActionResult AdminMenu()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile files)
        {           

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var configuration = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true
                }
            };

            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(files.OpenReadStream());

            DataSet ds = excelReader.AsDataSet(configuration);
            DataTable dt = ds.Tables[0];

            var lstColumn = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                lstColumn.Add(row[0].ToString());
            }

            ViewData["a"] = "hola";
            
            HttpContext.Session.SetString("name", "samuel");
            return Json(lstColumn);
        }
    
    }
}

