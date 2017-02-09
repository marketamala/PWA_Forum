using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Results;
using App.Models;

namespace App.Controllers
{
    public class AccountManageController : Controller
    {
        public static Accounts actUser;

        public ActionResult Register()
        {
            if (actUser != null)
            {
                Session.Clear();
                actUser = null;
            }
            ModelState.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Register(Accounts account)
        {
            bool userExists;
            if (ModelState.IsValid)
            {
                userExists = new DataCollection().Accounts.Any(u => u.UserName == account.UserName);
                if (userExists)
                {
                    ModelState.AddModelError(string.Empty, "Uživatel se jménem " + account.UserName + " již existuje!");
                    return View();
                }
                else
                {
                    var r = new AccountsController().PostAccounts(account);
                    return RedirectToAction("Login");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Správně vyplňte všechny požadované údaje!");
                return View();
            }
        }

        public ActionResult Login()
        {
            if (actUser != null)
            {
                Session.Clear();
                actUser = null;
            }
            ModelState.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Login(Accounts account)
        {
            var user = new DataCollection().Accounts.FirstOrDefault(u => u.UserName == account.UserName && u.Password == account.Password);
            if (user != null)
            {
                Session["UserID"] = user.UserID;
                Session["UserName"] = user.UserName;
                actUser = user;   
                return RedirectToAction("Loggedin");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Špatné uživatelské jméno nebo heslo");
                return View();
            }
        }

        public ActionResult Loggedin()
        {
            if (actUser != null)
                return View(actUser);
            else return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            actUser = null;
            return RedirectToAction("Login");
        }

        public ActionResult EditUser()
        {
            if (actUser != null)
                return View(actUser);
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult EditUser(Accounts account)
        {
            if (ModelState.IsValid)
            {
                var r = new AccountsController().PutAccounts(account.UserID, account);
                ModelState.Clear();
                actUser = account;
                return RedirectToAction("Loggedin");
            }
            else
            {
                ModelState.AddModelError("", "Změny nebyly uloženy!");
                return View(account);
            }
        }

        public ActionResult ChangePsw()
        {
            if (actUser != null)
                return View(actUser);
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult ChangePsw(Accounts account)
        {
            if ((ModelState.IsValid) && (account.Password != null) && (account.Password == account.ConfirmPasswd))
            {
                var r = new AccountsController().PutAccounts(account.UserID, account);
                actUser = account;
                ModelState.Clear();
                return RedirectToAction("Loggedin");
            }
            else
            {
                ModelState.AddModelError("", "Změny nebyly uloženy. Nesprávné zadání údajů.");
                return View(actUser);
            }
        }

        public ActionResult DeleteUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteUser(Accounts account)
        {
            if (Session["UserID"] != null)
            {
                if (actUser.Password == account.Password)
                {
                    var r = new AccountsController().DeleteAccounts(actUser.UserID);
                    ModelState.AddModelError("", "Uživatel " + actUser + " byl odstraněn.");
                    Session.Clear();
                    actUser = null;
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    ModelState.AddModelError("", "Je nutné zadat správné heslo!");
                    return View();
                }
            }
            else return RedirectToAction("Login");
        }
    }
}