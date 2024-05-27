using System.Collections.Generic;
using System.Linq;
using BudgetTrackerApp.Domain;
using BudgetTrackerApp.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BudgetTrackerApp.Data.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BudgetTrackerDbContext _context;

        public TransactionRepository(BudgetTrackerDbContext context)
        {
            _context = context;
        }

        public BudgetTransaction GetById(int id)
        {
            return _context.BudgetTransactions.FirstOrDefault(t => t.Id == id);
        }

        public List<BudgetTransaction> GetAll()
        {
            return _context.BudgetTransactions.ToList();
        }

        public void Add(BudgetTransaction transaction)
        {
            _context.BudgetTransactions.Add(transaction);
            _context.SaveChanges();
        }

        public void Update(BudgetTransaction transaction)
        {
            _context.BudgetTransactions.Update(transaction);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var transaction = GetById(id);
            if (transaction != null)
            {
                _context.BudgetTransactions.Remove(transaction);
                _context.SaveChanges();
            }
        }
    }
}



