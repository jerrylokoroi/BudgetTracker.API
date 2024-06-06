// DataSeeder.cs
using BudgetTrackerApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace BudgetTrackerApp.Data
{
    public class DataSeeder
    {
        private readonly BudgetTrackerDbContext _context;

        public DataSeeder(BudgetTrackerDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            // Ensure database is created and migrations are applied
            _context.Database.Migrate();

            // Seed data if the table is empty
            if (!_context.BudgetTransactions.Any())
            {
                var transactions = new List<BudgetTransaction>
                {
                    new BudgetTransaction
                    {
                        Description = "Job Salary",
                        Amount = 45000.00m,
                        Type = "Income",
                        Date = new DateTime(2024, 05, 30),
                        Category = "Salary",
                    },

                    new BudgetTransaction
                    {
                        Description = "House Rent",
                        Amount = 6500.00m,
                        Type = "Expense",
                        Date = new DateTime(2024, 05, 30),
                        Category = "Rent",
                    },

                    new BudgetTransaction
                    {
                        Description = "Tomatoes, Onions, Kale, Cabbages, Carrot and Spinach",
                        Amount = 650.00m,
                        Type = "Expense",
                        Date = new DateTime(2024, 05, 30),
                        Category = "Grocery",
                    }
                };

                _context.BudgetTransactions.AddRange(transactions);
                _context.SaveChanges();
            }
        }
    }
}


