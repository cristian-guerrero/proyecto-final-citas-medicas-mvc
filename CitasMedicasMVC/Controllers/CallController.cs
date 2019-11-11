using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using RestSharp;
using Newtonsoft.Json;

namespace CitasMedicasMVC.Controllers
{
  public class CallController : Controller
  {
    // GET: Call
    public ActionResult Index()
    {
      var citas = callApiAsync();
      return View(citas);
    }

    private List<citas> callApiAsync()
    {
      string url = "http://rest.proyecto/";

      var client = new RestClient(url);
      var request = new RestRequest("api/Citas", Method.GET);
      request.AddHeader("Content-Type", "application/json");
      // execute the request
      IRestResponse response = client.Execute(request);
      var content = response.Content;
      var result = JsonConvert.DeserializeObject<List<citas>>(content);
      return result ?? new List<citas>();
    }
  }
  }