namespace TaxDeductionReporting.Controllers
{
	using Microsoft.AspNetCore.Authentication.Cookies;
	using Microsoft.AspNetCore.Mvc;
	using System.Dynamic;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Authentication.OpenIdConnect;
	using Microsoft.AspNetCore.Authorization;
	using Models;
	using Okta.Sdk;

	public class AccountController : Controller
	{
		private readonly IOktaClient _oktaClient;

		public AccountController(IOktaClient oktaClient)
		{
			_oktaClient = oktaClient;
		}

		public IActionResult Login()
		{
			if (!HttpContext.User.Identity.IsAuthenticated)
			{
				return Challenge(OpenIdConnectDefaults.AuthenticationScheme);
			}

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public IActionResult Logout()
		{
			return new SignOutResult(new[] { CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme });
		}

		[Authorize]
		public async Task<IActionResult> Me()
		{
			var username = User.Claims
				.FirstOrDefault(x => x.Type == "preferred_username")
				?.Value;

			var viewModel = new MeViewModel
			{
				Username = username,
				SdkAvailable = _oktaClient != null
			};

			if (!viewModel.SdkAvailable)
			{
				return View(viewModel);
			}

			if (!string.IsNullOrEmpty(username))
			{
				var user = await _oktaClient.Users.GetUserAsync(username);
				dynamic userInfoWrapper = new ExpandoObject();
				userInfoWrapper.Profile = user.Profile;
				userInfoWrapper.PasswordChanged = user.PasswordChanged;
				userInfoWrapper.LastLogin = user.LastLogin;
				userInfoWrapper.Status = user.Status.ToString();
				viewModel.UserInfo = userInfoWrapper;

				viewModel.Groups = (await user.Groups.ToList().ConfigureAwait(false)).Select(g => g.Profile.Name).ToArray();
			}

			return View(viewModel);
		}
	}
}