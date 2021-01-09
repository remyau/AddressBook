using AddressBook.Models;
using AddressBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace AddressBook.Controllers
{
    public class AccountController : Controller
    {
        private AddressBookEntities addressBookDb;

        public AccountController()
        {
            addressBookDb = new AddressBookEntities();
        }

        protected override void Dispose(bool disposing)
        {
            addressBookDb.Dispose();
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(()=> addressBookDb.Users.Add(user));
                await addressBookDb.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View();
        }

        public ActionResult Login()
        {
            UserViewModel userViewModel = new UserViewModel
            {
               
            };
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserViewModel userViewModel)
        {
           
            if (ModelState.IsValid)
            {
                var _user = await Task.Run(() => addressBookDb.Users
                                                .Where(m => (m.UserName == userViewModel.UserName && m.PasswordHashed == userViewModel.PasswordHashed))
                                                .FirstOrDefault());

                if (_user!=null)
                {
                    Session["userId"] = _user.UserId;
                    Session["userName"] = _user.UserName;
                    return RedirectToAction("Index", "Contact");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Credentials");
            }

            return View(userViewModel);
            
        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}