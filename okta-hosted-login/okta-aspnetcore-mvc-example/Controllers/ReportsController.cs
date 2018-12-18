namespace TaxDeductionReporting.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Globalization;
	using Microsoft.AspNetCore.Mvc;
	using Models;
	using Models.Calendar;
	using Atlassian.Jira;

	public class ReportsController : Controller
	{
		public string Password = "Idontlikepasswords123";

		public IActionResult Create()
		{
			DateTime now = DateTime.UtcNow;
			DateTime reportDate = new DateTime(now.Year, now.Month, 1).AddMonths(-1);

			var model = new ReportViewModel
			{
				Date = reportDate,
				Calendar = CreateCalendar(reportDate)
				Tickets = FetchIssues().ToList()
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

		private IEnumerable<TicketViewModel> FetchIssues()
		{
			Jira jiraClient = Jira.CreateRestClient("https://jira.kcura.com", "avichay.kardash", Password);

			string jql = "assignee was currentUser() AFTER startOfMonth(-1) BEFORE startOfMonth() AND (status != Closed  OR status changed to closed AFTER startOfMonth(-1) )";

			JiraUser user = jiraClient.Users.GetUserAsync("avichay.kardash").Result;

			return jiraClient.Issues.GetIssuesFromJqlAsync(new IssueSearchOptions(jql)).Result.Select(a => new TicketViewModel { Title = a.Summary, Description = a.Description });
		}
	}
}