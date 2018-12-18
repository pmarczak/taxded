using System;
using TaxDeductionReporting.Models.Calendar;

namespace TaxDeductionReporting.Models
{
	using System.Collections.Generic;

	public class ReportViewModel
	{
		public DateTime Date { get; set; }
		public CalendarViewModel Calendar { get; set; }
		public IList<TicketViewModel> Tickets { get; set; }
	}
}
