using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using messageBoard.Models;
namespace messageBoard.Service
{
    public class GuestbooksDBService
    {
        private MyGuestbookEntities db;

        public GuestbooksDBService()
        {
            db = new MyGuestbookEntities();
        }

        public List<Guestbooks> GetGuestbooks()
        {
            return db.Guestbooks.ToList();
        }

        public void CreateGuestbooks(Guestbooks guestbooks)
        {
            guestbooks.CreateTime = DateTime.Now;
            db.Guestbooks.Add(guestbooks);
            SaveChanges();
        }

        private void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}