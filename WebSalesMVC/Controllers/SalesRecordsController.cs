using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebSalesMVC.Models.Enums;
using WebSalesMVC.Services;

namespace WebSalesMVC.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordsService _salesRecordService;

        public SalesRecordsController(SalesRecordsService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate, SaleStatus status)
        {
            if (!minDate.HasValue && !maxDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
           
            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate, status);

            return View(result);
        }
        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate, SaleStatus status)
        {
            if (!minDate.HasValue && !maxDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate, status);

            return View(result);
        }
    }
}