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

        public List<Guestbooks> GetGuestbooks(string Search)
        {
            List<Guestbooks> SearchData = new List<Guestbooks>();

            if (string.IsNullOrEmpty(Search))
            {
                SearchData = db.Guestbooks.ToList();
            }
            else
            {
                SearchData = db.Guestbooks.Where(p => p.Content.Contains(Search) ||
                                                      p.Name.Contains(Search)||
                                                      p.Reply.Contains(Search)).ToList();
            }

            return SearchData.ToList();
        }

        public void CreateGuestbooks(Guestbooks guestbooks)
        {
            guestbooks.CreateTime = DateTime.Now;
            db.Guestbooks.Add(guestbooks);
            SaveChanges();
        }

        public void UpdateGuestbook(Guestbooks guestbooks)
        {
            Guestbooks Data = db.Guestbooks.FirstOrDefault(p => p.Id == guestbooks.Id);
            if (Data != null)
            {
                Data.Name = guestbooks.Name;
                Data.Content = guestbooks.Content;
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("資料庫無此筆資料", string.Format("id:{0}", guestbooks.Id));
            }
        }

        public void ReplyGuestbooks(Guestbooks guestbooks)
        {
            Guestbooks Data = db.Guestbooks.FirstOrDefault(p => p.Id == guestbooks.Id);
            if (Data != null)
            {
                Data.Reply = guestbooks.Reply;
                Data.ReplyTime = DateTime.Now;
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("資料庫無此筆資料", string.Format("id:{0}", guestbooks.Id));
            }
        }

        public bool CheckUpdate(int id)
        {
            Guestbooks Data = db.Guestbooks.Find(id);
            return (Data != null && Data.ReplyTime == null);        
        }

        public Guestbooks GetDataById(int id)
        {
            return db.Guestbooks.FirstOrDefault(p => p.Id == id);
        }

        private void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}