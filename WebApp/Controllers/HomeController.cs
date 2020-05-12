using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jsreport.AspNetCore;
using jsreport.Types;
using WebApp.Model;
using System.Net.Http;
using jsreport.Client;
using Microsoft.Net.Http.Headers;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {        
        public IActionResult Index()
        {
            return View();
        }

        [MiddlewareFilter(typeof(JsReportPipeline))]
        public IActionResult Invoice()
        {
            HttpContext.JsReportFeature()
                // the normal jsreport base url injection into the html doesn't work properly with docker and asp.net because of port mapping
                // the project typically starts with some http://localhost:1234 url but inside docker the url is http://localhost
                .Configure((req) => req.Options.Base = "http://webapp")
                .Recipe(Recipe.ChromePdf);         

            return View(InvoiceModel.Example());
        }

        [MiddlewareFilter(typeof(JsReportPipeline))]
        public IActionResult Items()
        {
            HttpContext.JsReportFeature()
                .Recipe(Recipe.HtmlToXlsx);               

            return View(InvoiceModel.Example());
        }

        // no need to use razor, you can also create templates inside jsreport http://localhost:5488 and render them by name
        // because the jsreport container data folder is maped volume, you still have everything inside version control
        private IReportingService _reportingService = new ReportingService("http://jsreport:5488");
        public async Task<IActionResult> Orders()
        {
            var result = await _reportingService.RenderByNameAsync("orders-main", null);

            return new FileStreamResult(result.Content, new MediaTypeHeaderValue(result.Meta.ContentType));
        }
    }
}
