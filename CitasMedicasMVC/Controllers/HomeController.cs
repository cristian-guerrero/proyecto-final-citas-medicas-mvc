using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CitasMedicasMVC.Controllers
{
    public class HomeController : Controller
    {
    public ActionResult Index()
        {

      if(notSession())
      return RedirectToAction("Index", "Login");

      return View();
        }

        public ActionResult About()
        {
      if (notSession())
        return RedirectToAction("Index", "Login");
      ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
      if (notSession())
        return RedirectToAction("Index", "Login");
      ViewBag.Message = "Your contact page.";

            return View();
        }



    private bool notSession()
    {
      return Session["user_name"] == null && Session["user_profile"] == null;
    
      
    }


  }
}