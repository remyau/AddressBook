using AddressBook.Models;
using AddressBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            var userRegisterViewModel = new UserRegisterViewModel { };
             
            return View(userRegisterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserRegisterViewModel userRegister)
        {
            userRegister.ErrMsag = "";
            if (ModelState.IsValid)
            {
                string password = Convert.ToBase64String(CalculateSHA256(userRegister.Password));
                User user = new User { 
                    FirstName=userRegister.FirstName,
                    LastName=userRegister.LastName,
                    UserName=userRegister.UserName,
                    PasswordHashed=password
                };
                //user.PasswordHashed = password;
                var userExist = await Task.Run(() => addressBookDb.Users.Where(m => m.UserName == userRegister.UserName ||
                                                                                    m.PasswordHashed == password)
                                                                        .FirstOrDefault());
                if (userExist!=null)
                {
                    userRegister.ErrMsag = "User Name or Password already exist!";
                    return View(userRegister);
                }
                await Task.Run(() => addressBookDb.Users.Add(user));
                await addressBookDb.SaveChangesAsync();
                userRegister.UserId = user.UserId;
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserViewModel userViewModel)
        {

            if (ModelState.IsValid)
            {
                string password = Convert.ToBase64String(CalculateSHA256(userViewModel.PasswordHashed));
                var _user = await Task.Run(() => addressBookDb.Users
                                                .Where(m => (m.UserName == userViewModel.UserName && m.PasswordHashed == password))
                                                .FirstOrDefault());

                if (_user != null)
                {
                    Session["userId"] = _user.UserId;
                    Session["userName"] = _user.UserName;
                    return RedirectToAction("Index", "Contact");
                }
                else
                {
                    userViewModel.ErrMsg = "Invalid Credentials! User Name or Password does not exist";
                }
            }
            else
            {
                userViewModel.ErrMsg="Invalid Credentials";
            }

            return View(userViewModel);

        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }


        public byte[] CalculateSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] hashvalue;
            UTF8Encoding objUtf8 = new UTF8Encoding();
            hashvalue = sha256.ComputeHash(objUtf8.GetBytes(str));

            return hashvalue;
        }
    }
}