using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using okta_aspnetcore_mvc_example.Models;

namespace okta_aspnetcore_mvc_example.Controllers
{
	using System;
	using System.Net;
	using Newtonsoft.Json;

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
			const string uri = "https://jira.kcura.com/rest/api/latest/search?jql=assignee=currentUser()&fields=id,key";

			using (var w = new WebClient())
			{
				string jsonData = string.Empty;

				w.Headers.Add("Content-Type", "application/json");
				jsonData = w.DownloadString(uri);


				
				//return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<T>(jsonData) : new T();
			}

			return View();
		}
	}
}
