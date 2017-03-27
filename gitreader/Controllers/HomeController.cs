using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gitreader.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // repo data
            //string data = new gitreader.data.GitHubReader().GetRepoData("calvinirwin");
            //ViewBag.Message = data;

            //commit data
            //string data = new gitreader.data.GitHubReader().GetCommitData("calvinirwin", "bb-mm-webhook");
            //ViewBag.Message = data;


            //get repos
            string data = new gitreader.data.GitHubReader().CloneRepos("calvinirwin");
            ViewBag.Message = data;


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}