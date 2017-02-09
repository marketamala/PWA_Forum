using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class SearchingController : Controller
    {
        public ActionResult Search()
        {
            SearchModel model = new SearchModel();
            return View(model);
        }

        public ActionResult _SearchItem()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult _SearchItem(SearchModel model)
        {
            return PartialView(model);
        }
    }

    public class SearchModel
    {
        public string search { get; set; }
    }
}