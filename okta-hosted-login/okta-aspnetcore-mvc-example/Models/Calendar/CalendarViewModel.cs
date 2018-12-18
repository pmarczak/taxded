using System.Collections.Generic;

namespace TaxDeductionReporting.Models.Calendar
{
	public class CalendarViewModel
	{
		public List<CalendarDayViewModel> Days { get; set; } = new List<CalendarDayViewModel>();
	}
}
