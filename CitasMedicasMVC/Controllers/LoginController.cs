using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CitasMedicasMVC.Controllers
{
  public class LoginController : Controller
  {
    private citas_medicasEntities2 db = new citas_medicasEntities2();

    // GET: Login
    public ActionResult Index()
    {
      return View(new LoginModel());
    }


    /*
    public ActionResult Login()
    {
      Session["user_name"] = null;
      Session["user_profile"] = null;
      // colocar la logica para borrar la session
      return View();
    }
    */
    [HttpPost]
   // [ValidateAntiForgeryToken]
    public ActionResult Index([Bind(Include = "usuario,password")] LoginModel _usuario)
    {
      // ViewBag.message = "Error ";

      if (!ModelState.IsValid)
        return View(_usuario);
      var ob = db.usuarios.Where(a => a.usuario.Equals(_usuario.usuario))
      .FirstOrDefault();
      if (ob != null)
      {
        Session["_user_id"] = ob.id;
        Session["user_name"] = ob.usuario.ToString();
        Session["user_profile"] = ob.perfil.ToString();
        return RedirectToAction("Index", "Home");
      }
      return View(_usuario);
    }



    // [ValidateAntiForgeryToken]
    public ActionResult Exit()
    {
      // ViewBag.message = "Error ";


        Session["user_name"] = null;
        Session["user_profile"] =null ;
        return RedirectToAction("Index", "Login");

    }
  }
}