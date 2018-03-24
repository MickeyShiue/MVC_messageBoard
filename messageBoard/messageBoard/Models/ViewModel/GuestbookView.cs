using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace messageBoard.Models.ViewModel
{
    public class GuestbookView
    {
        public string Search { get; set; }
        public List<Guestbooks> DataList { get; set; }
    }
}