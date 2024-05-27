using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class BudgetTrackerDbContextFactory : IDesignTimeDbContextFactory<BudgetTrackerDbContext>
{
    public BudgetTrackerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BudgetTrackerDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BudgetTrackerDB;Trusted_Connection=True;TrustServerCertificate=True");

        return new BudgetTrackerDbContext(optionsBuilder.Options);
    }
}

