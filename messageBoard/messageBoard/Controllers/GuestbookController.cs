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
        public ActionResult Index(string Search)
        {
            GuestbookView Data = new GuestbookView();
            Data.DataList = guestbookDBService.GetGuestbooks(Search);
            return View(Data);
        }


        public ActionResult CreateMessage()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMessage([Bind(Include = "Name,Content")] Guestbooks guestbooks)
        {
            guestbookDBService.CreateGuestbooks(guestbooks);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditMessage(int id)
        {
            Guestbooks Data = guestbookDBService.GetDataById(id);
            return View(Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMessage(int id, [Bind(Include = "Name,Content")]Guestbooks guestbooks)
        {
            if (guestbookDBService.CheckUpdate(id))
            {
                guestbooks.Id = id;
                guestbookDBService.UpdateGuestbook(guestbooks);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Reply(int id)
        {
            Guestbooks Data = guestbookDBService.GetDataById(id);
            return View(Data);
        }

        [HttpPost]
        public ActionResult Reply(int id, Guestbooks guestbooks)
        {
            if (guestbookDBService.CheckUpdate(id))
            {
                guestbooks.Id = id;
                guestbookDBService.ReplyGuestbooks(guestbooks);
            }
            return RedirectToAction("Index");
        }
    }
}
