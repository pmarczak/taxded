using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using TaxDeductionReporting.Models;
using TaxDeductionReporting.Models.Calendar;

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
				Date = reportDate,
				Calendar = CreateCalendar(reportDate)
			};

			return View(model);
		}

		private CalendarViewModel CreateCalendar(DateTime date)
		{
			var days = new List<CalendarDayViewModel>();

			DateTime currentDate = date;
			int weekIndex = 0;

			while (currentDate.Month == date.Month)
			{
				if (IsWorkDay(currentDate.DayOfWeek))
				{
					days.Add(new CalendarDayViewModel
					{
						DayOfWeek = (int)currentDate.DayOfWeek,
						DayOfMonth = currentDate.Day,
						WeekIndex = weekIndex
					});
				}

				currentDate = currentDate.AddDays(1);

				if (currentDate.DayOfWeek == DayOfWeek.Monday && days.Count > 0)
				{
					weekIndex++;
				}
			}

			var calendarViewModel = new CalendarViewModel
			{
				Days = days
			};			
		
			return calendarViewModel;
		}

		private bool IsWorkDay(DayOfWeek dayOfWeek)
		{
			return dayOfWeek != DayOfWeek.Saturday && dayOfWeek != DayOfWeek.Sunday;
		}
    }
}