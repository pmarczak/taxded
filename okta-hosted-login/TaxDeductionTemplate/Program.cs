using Microsoft.Office.Interop.Word;
using System.IO;

namespace TaxDeductionTemplate
{
    class TaxDeductionTemplateFiller
    {
        public class Employee
        {
            public static string EmployeeName { get; set; }
            public static string ReplaceName = "<ImieINazwisko>";
            public static string EmployeePosition { get; set; }
            public static string ReplacePosition = "<Stanowisko>";
            public static string EmployeePeriod { get; set; }
            public static string ReplacePeriod = "<Miesiac/ROK>";
        }
        static void Main()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "Resources", "Tax Deduction - TEMPLATE.docx");
            Application app = new Application();
            Document docx = app.Documents.Open(filePath);

            string findName = Employee.ReplaceName;
            string replacedName = Employee.EmployeeName;
            string findPeriod = Employee.ReplacePeriod;
            string replacedPeriod = Employee.EmployeePeriod;
            string findPositon = Employee.ReplacePosition;
            string replacedPosition = Employee.EmployeePosition;

            Find findText = docx.Application.Selection.Find;
            findText.ClearFormatting();
            findText.Text = findName;
            findText.Replacement.ClearFormatting();
            findText.Replacement.Text = replacedName;

            object repleace = WdReplace.wdReplaceAll;

            findText.Execute(findName, false, true, false, false, false, true, 1, false, replacedName, 2,
            false, false, false, false);
            findText.Execute(findPeriod, false, true, false, false, false, true, 1, false, replacedPeriod, 2,
            false, false, false, false);
            findText.Execute(findPositon, false, true, false, false, false, true, 1, false, replacedPosition, 2,
            false, false, false, false);
            object newTemplateName = Path.Combine(currentDirectory, "Resources", "Tax Deduction - " + Employee.EmployeeName + ".docx");
            docx.Application.ActiveDocument.SaveAs(ref newTemplateName);

        }

    }
}
