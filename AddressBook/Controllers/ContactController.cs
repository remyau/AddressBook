using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

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
        //[Route ("Contact")]
        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var contact = await Task.Run(()=> addressDBContext.Contacts);
            return View(contact);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
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
            return View(contact);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Contact contact)
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