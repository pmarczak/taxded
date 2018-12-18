using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using okta_aspnetcore_mvc_example.Models;

namespace okta_aspnetcore_mvc_example.Controllers
{
	using Atlassian.Jira;

	public class HomeController : Controller
	{
		private const string Password = "";

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

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


		[Authorize]
		public IActionResult Profile()
		{
			return View(HttpContext.User.Claims);
		}

		public IActionResult Jira()
		{
			Jira jiraClient = Atlassian.Jira.Jira.CreateRestClient("https://jira.kcura.com", "avichay.kardash", Password);

			string jql = "assignee was currentUser()";// AFTER startOfMonth(-1) BEFORE startOfMonth() AND (status != Closed  OR status changed to closed AFTER startOfMonth(-1) )";

			JiraUser user = jiraClient.Users.GetUserAsync("avichay.kardash").Result;

			IPagedQueryResult<Issue> result = jiraClient.Issues.GetIssuesFromJqlAsync(new IssueSearchOptions(jql)).Result;

			return View();
		}
	}
}
