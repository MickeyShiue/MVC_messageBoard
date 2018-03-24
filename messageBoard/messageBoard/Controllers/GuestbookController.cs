using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using messageBoard.Service;
using messageBoard.Models.ViewModel;
using messageBoard.Models;

namespace messageBoard.Controllers
{
    public class GuestbookController : Controller
    {
        private GuestbooksDBService guestbookDBService;

        public GuestbookController()
        {
            guestbookDBService = new GuestbooksDBService();
        }
      
        [HttpGet]
        public ActionResult Index()
        {
            GuestbookView Data = new GuestbookView();
            Data.DataList = guestbookDBService.GetGuestbooks();
            return View(Data);
        }


        public ActionResult CreateMessage()
        {
            return PartialView();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMessage([Bind(Include ="Name,Content")] Guestbooks guestbooks)
        {
            guestbookDBService.CreateGuestbooks(guestbooks);
            return RedirectToAction("Index");
        }
    }
}