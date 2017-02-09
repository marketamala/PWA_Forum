using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Models;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }

        public ActionResult _LoadUsers(string search)
        {
            var accounts = new AccountsController().GetAccounts();
            if (search != null)
                accounts = accounts.Where(a => a.UserName.Contains(search));
            if (Session["UserName"] != null)
            {
                var actualU = Session["UserName"].ToString();
                accounts = accounts.Where(a => a.UserName != actualU);
            }
            return PartialView(accounts.ToList());
        }

        public ActionResult ShowUser(Accounts account)
        {
            return View(account);
        }
    }
}
