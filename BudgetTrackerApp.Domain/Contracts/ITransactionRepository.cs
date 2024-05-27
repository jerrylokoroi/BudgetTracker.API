using System;
using System.Collections.Generic;
using System.Collections.Generic;

namespace BudgetTrackerApp.Domain.Contracts
{
    public interface ITransactionRepository
    {
        BudgetTransaction GetById(int id);
        List<BudgetTransaction> GetAll();
        void Add(BudgetTransaction transaction);
        void Update(BudgetTransaction transaction);
        void Delete(int id);
    }
}

