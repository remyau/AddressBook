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
    }
}