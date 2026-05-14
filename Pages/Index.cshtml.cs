using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BudgetTrackerWeb.Pages;

public class IndexModel : PageModel
{
    public List<string> Descriptions = new List<string>();
    public List<double> Amounts = new List<double>();
    public List<string> Dates = new List<string>();

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
        }
    }
}
