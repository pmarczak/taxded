namespace TaxDeductionReporting.API
{
	using System;
	using System.Collections.Generic;
	using Microsoft.AspNetCore.Mvc;
	using Models.Calendar;

	[Route("api/[controller]")]
	[ApiController]
	public class CalendarController : ControllerBase
	{
		// api/calendar
		public ActionResult Calendar()
		{
			DateTime date = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(-1);

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

			return Ok(calendarViewModel);
		}

		private static bool IsWorkDay(DayOfWeek dayOfWeek)
		{
			return dayOfWeek != DayOfWeek.Saturday && dayOfWeek != DayOfWeek.Sunday;
		}
	}
}