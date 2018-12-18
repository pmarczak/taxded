namespace TaxDeductionReporting.Models.Calendar
{
	public class CalendarDayViewModel
	{
		public int Day { get; set; }
		public CalendarDayType Type { get; set; } = CalendarDayType.Work;
	}
}
