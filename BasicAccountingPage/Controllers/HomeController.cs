using BasicAccountingPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BasicAccountingPage.Controllers
{
    public class HomeController : Controller
    {
        private static TestDBEntities db = new TestDBEntities();

        IncomeCostMultiModel model = new IncomeCostMultiModel();
        IncomeCostMultiModel.Balance bal = new IncomeCostMultiModel.Balance();
        public ActionResult Index()
        {
            List<Income> incomeList = new List<Income>();
            List<Cost> costList = new List<Cost>();
            List<IncomeCostMultiModel.Balance> balanceList = new List<IncomeCostMultiModel.Balance>();

            incomeList = db.Incomes.Where(x => x.Id > 0).ToList();
            costList = db.Costs.Where(x => x.Id > 0).ToList();
            if (incomeList==null)
            {
                model.incomeList = new List<Income>();
            }
            else
            {
                model.incomeList = incomeList;
                foreach (var income in incomeList)
                {
                    bal.TotalIncome = bal.TotalIncome + income.Amount;
                }
            }
            if (costList == null)
            {
                model.costList = new List<Cost>();
            }
            else
            {
                model.costList = costList;
                foreach (var cost in costList)
                {
                    bal.TotalCost = bal.TotalCost + cost.Amount;
                }
            }
            bal.CurrentBalance = bal.TotalIncome - bal.TotalCost;
            balanceList.Add(bal);
            model.balance = balanceList;

            return View(model);
        }

        public ActionResult AddIncome(Income income)
        {
            if (ModelState.IsValid)
            {
                db.Incomes.Add(income);
                db.SaveChanges();
            }

            return RedirectToAction("Index","Home");
        }
        public ActionResult AddCost(Cost cost)
        {
            if (ModelState.IsValid)
            {
                db.Costs.Add(cost);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Chart()
        {
            bal = getBalance();

            new Chart(width: 800, height: 200)
                .AddSeries(chartType: "column",
                            xValue: new[] { "Income", "Cost", "Balance" },
                            yValues: new[] { bal.TotalIncome, bal.TotalCost, bal.CurrentBalance }).Write("png");
            return null;
        }
        public IncomeCostMultiModel.Balance getBalance()
        {
            List<Income> incomeList = db.Incomes.Where(x => x.Id > 0).ToList();
            List<Cost> costList = db.Costs.Where(x => x.Id > 0).ToList();
            foreach (var income in incomeList)
            {
                bal.TotalIncome = bal.TotalIncome + income.Amount;
            }
            foreach (var cost in costList)
            {
                bal.TotalCost = bal.TotalCost + cost.Amount;
            }
            bal.CurrentBalance = bal.TotalIncome - bal.TotalCost;

            return bal;
        }
    }
}