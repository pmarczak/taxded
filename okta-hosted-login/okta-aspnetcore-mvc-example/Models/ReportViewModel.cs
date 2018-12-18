using System;
using TaxDeductionReporting.Models.Calendar;

namespace TaxDeductionReporting.Models
{
	public class ReportViewModel
	{
		public DateTime Date { get; set; }
		public CalendarViewModel Calendar { get; set; }
	}
}
