using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWarehouse.User;

namespace MvcWarehouse.Controllers
{
    public class StoreController : Controller
    {

        int[] options = {0, 0, 0, 0};
        // GET: Store
        Repository.Repository Rep = new Repository.Repository();


        //The form is filled out in the index page based on options, the variables are matched from the form to Index method here

        public ActionResult Index(string search = null, int checkname = 0, int checkprice = 0, int checkanumber = 0, int checkall = 0)
        {
            IEnumerable<Models.StockItem> result;

            if (search == null)
            {
                result = Rep.Stock();
            }
            else
            {

                options[0] = checkname;
                options[1] = checkprice;
                options[2] = checkanumber;
                options[3] = checkall;


                result = Rep.Search(search, options);
            }
            
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.StockItem item)
        {
            if (ModelState.IsValid)
            {
                Rep.AddItem(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(Rep.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Models.StockItem item)
        {
            if (ModelState.IsValid)
            {
                Rep.Edit(item);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(Rep.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {

                Rep.Remove(id);
                return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(Rep.Find(id));
        }

        [HttpPost]
        public ActionResult Details(Models.StockItem item)
        {

            return View(item);
        }
    
        public ActionResult About()
        {
            ViewBag.Message = "This is a warehouse.";

            return View();
        }

    
        public ActionResult Contact()
        {
            return View();
        }

        
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string email = null, string password = null)
        {
            if (email != null && password != null)
            {
                Rep.AddUser(email, Encryption.Encrypt(password));
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email = null, string password = null)
        {
            if (email != null && password != null)
            {
                Rep.Auth(email, Encryption.Encrypt(password));
            }
            
            return View();
        }
    }
}