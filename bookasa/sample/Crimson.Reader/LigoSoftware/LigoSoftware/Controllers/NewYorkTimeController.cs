using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace LigoSoftware.Controllers
{
    public class NewYorkTimesController : Controller
    {
        public ActionResult BestSellerLists()
        {
            ViewData["BestSellerLists"] = new NewYorkTimesServices.NewYorkTimesClient().BestSellerLists();

            return View();
        }

        //
        // GET: /NewYorkTime/Details/5

        public void Details(Guid id)
        {
            Response.Write("<H1>Coming Soon New York Times Best Sellers</H1>");
        }

        //
        // GET: /NewYorkTime/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /NewYorkTime/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /NewYorkTime/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /NewYorkTime/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
