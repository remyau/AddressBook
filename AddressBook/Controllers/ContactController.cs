using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using AddressBook.ViewModels;

namespace AddressBook.Controllers
{
   
    public class ContactController : Controller
    {
        private AddressBookEntities addressDBContext;

        public ContactController()
        {
            addressDBContext = new AddressBookEntities();
        }

        protected override void Dispose(bool disposing)
        {
            addressDBContext.Dispose();
        }

        // GET: Contact        
        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var contact = await Task.Run(()=> addressDBContext.Contacts);
            return View(contact);
        }

        [HttpGet]        
        public ActionResult Create()
        {
            //Countries cs = new Countries();
            ContactViewModel contactViewModel = new ContactViewModel
            {
                //CountryLists = cs.GetAllCountryNames()
            };
            return View(contactViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                var contact = new Contact
                {
                    FirstName = contactViewModel.FirstName,
                    LastName = contactViewModel.LastName,
                    PhoneNumber = contactViewModel.PhoneNumber,
                    City = contactViewModel.City,
                    Province = contactViewModel.Province,
                    PostalCode = contactViewModel.PostalCode,
                    Country = contactViewModel.Country
                };
                await Task.Run(() => addressDBContext.Contacts.Add(contact));
                await addressDBContext.SaveChangesAsync();
                return RedirectToAction("Index", "Contact");
            }

            return View();
        }

        //[Route()]
        public async Task<ActionResult> Show(int Id)
        {
            if (Id != 0)
            {
                var contact = await addressDBContext.Contacts.FindAsync(Id);
                return View(contact);
            }

            return RedirectToAction("Index", "Error");
        }

        public async Task<ActionResult> Delete(int Id)
        {
            var contact = await addressDBContext.Contacts.FindAsync(Id);
            if (contact != null)
            {
                await Task.Run(() => addressDBContext.Contacts.Remove(contact));
                await addressDBContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Contact");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int Id)
        {
            var contact = await addressDBContext.Contacts.FindAsync(Id);
            var contactViewModel = new ContactViewModel
            {
                Id=contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNumber = contact.PhoneNumber,
                City = contact.City,
                Province = contact.Province,
                PostalCode = contact.PostalCode,
                Country = contact.Country
            };
            return View(contactViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ContactViewModel contact)
        {
            if (contact.Id == 0)
            {
                return View(contact);
            }
            var _contact = await Task.Run(()=> addressDBContext.Contacts.SingleOrDefault(m=> m.Id==contact.Id));
            if (ModelState.IsValid)
            {
                _contact.FirstName = contact.FirstName;
                _contact.LastName = contact.LastName;
                _contact.PhoneNumber = contact.PhoneNumber;
                _contact.City = contact.City;
                _contact.Province = contact.Province;
                _contact.PostalCode = contact.PostalCode;
                _contact.Country = contact.Country;
               // await Task.Run(()=> addressDBContext.Contacts.Attach(_contact));
                await addressDBContext.SaveChangesAsync();

                return RedirectToAction("Index", "Contact");
            }

            return View(contact);
        }
    }
}