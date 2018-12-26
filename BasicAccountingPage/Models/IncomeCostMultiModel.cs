using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAccountingPage.Models
{
    public class IncomeCostMultiModel
    {
        private readonly TestDBEntities db = new TestDBEntities();
        public IncomeCostMultiModel()
        {
            
        }
        public Income income { get; set; }
        public Cost cost { get; set; }
        public List<Income> incomeList { get; set; }
        public List<Cost> costList { get; set; }
        public List<Balance> balance { get; set; }
        public class Balance
        {
            public long TotalIncome { get; set; }
            public long TotalCost { get; set; }
            public long CurrentBalance { get; set; }
        }
    }
}