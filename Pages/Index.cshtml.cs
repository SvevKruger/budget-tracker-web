using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BudgetTrackerWeb.Pages;

public class IndexModel : PageModel
{
    public List<string> Descriptions = new List<string>();
    public List<double> Amounts = new List<double>();
    public List<string> Dates = new List<string>();

    public double Balance = 0;
    public double TotalIncome = 0;
    public double TotalExpenses = 0;

    private string filepath = "transactions.txt";

    public void OnGet()
    {
        if (System.IO.File.Exists(filepath))
        {
            string[] lines = System.IO.File.ReadAllLines(filepath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                Descriptions.Add(parts[0]);
                Amounts.Add(double.Parse(parts[1]));
                Dates.Add(parts[2]);
            }

            foreach (double amount in Amounts)
            {
                if (amount > 0)
                {
                    TotalIncome += amount;
                }
                else
                {
                    TotalExpenses += amount;
                }
            }

            Balance = TotalIncome + TotalExpenses;
        }
    }

    public IActionResult OnPost(string description, double amount)
    {
        string date = DateTime.Now.ToString("dd/MM/yyyy");
        System.IO.File.AppendAllText(filepath, $"{description}, {amount}, {date}\n");

        return RedirectToPage();
    }

    public IActionResult OnPostDelete(int index)
    {
        if (System.IO.File.Exists(filepath))
        {
            string[] lines = System.IO.File.ReadAllLines(filepath);
            var newLines = lines.Where((line, i) => i != index).ToList();
            System.IO.File.WriteAllLines(filepath, newLines);
        }

        return RedirectToPage();
    }
}
