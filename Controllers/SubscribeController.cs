using RestSharp;
using RestSharp.Authenticators;
using ShauliFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShauliFinalProject.Controllers
{
    public class SubscribeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Subscribe
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string FullName, string Email)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri(@"https://api.mailgun.net/v3");
            client.Authenticator =
                   new HttpBasicAuthenticator("api",
                                              "key-2a10cb17779b8b8de0f764425dee04c1");
            RestRequest request = new RestRequest();
//            request.AddParameter("domain",
//                                "sandboxac7df206acbb4633b6ed384af4f9c0ef.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Shauly <shauly@notreply>");
            request.AddParameter("to", FullName + " <" + Email + ">");
            request.AddParameter("subject", "Hello " + FullName);
            request.AddParameter("text", "Congratulations, you successfuly subscribed to Shauly blog posts");
            request.Method = Method.POST;

            IRestResponse response = client.Execute(request);

            string responseString = "Error while subscribe by email";

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                responseString = "Operation completed successfuly";
            }

            return Json(responseString);
        }
    }
}