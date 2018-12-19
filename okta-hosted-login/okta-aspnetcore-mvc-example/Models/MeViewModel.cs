using System.Collections.Generic;

namespace TaxDeductionReporting.Models
{
	public class MeViewModel
	{
		public string Username { get; set; }

		public bool SdkAvailable { get; set; }

		public dynamic UserInfo { get; set; }

		public IEnumerable<string> Groups { get; set; }
	}
}
