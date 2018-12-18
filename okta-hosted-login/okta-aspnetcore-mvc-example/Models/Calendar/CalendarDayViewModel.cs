namespace TaxDeductionReporting.Models.Calendar
{
	public class CalendarDayViewModel
	{
		public int WeekIndex { get; set; }
		public int DayOfWeek { get; set; }
		public int DayOfMonth { get; set; }
		public CalendarDayType Type { get; set; } = CalendarDayType.Work;
	}
}
