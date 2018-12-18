using System;
using Microsoft.AspNetCore.Mvc;
using TaxDeductionReporting.Models;

namespace TaxDeductionReporting.Controllers
{
    public class ReportsController : Controller
    {
		public IActionResult Create()
		{
			DateTime now = DateTime.UtcNow;
			DateTime reportDate = new DateTime(now.Year, now.Month, 1).AddMonths(-1);

			var model = new ReportViewModel
			{
				Date = reportDate
			};

			return View(model);
		}
    }
}