using InventorySystem.Infrastructure.DrivenAdapters.sqlServer.context;

namespace InventorySystem.Infrastructure.DrivenAdapters;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Creating Database...");
        var dataContext = new DataContext();
        dataContext.Database.EnsureCreated();
        Console.WriteLine("Database ready");
        Console.ReadKey();
    }
}