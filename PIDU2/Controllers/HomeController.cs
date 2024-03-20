using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using PIDU2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PIDU2.Controllers
{
    public class HomeController : Controller
    
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Kutse()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 10 ? "Tere hommikust" : "Tere päevast";
            ViewBag.Message = "Ootan teid peole! Tule kindlasti!";
            return View();
        }
        [Authorize]
        public ActionResult Roll()
        {
            IList<string> roles = new List<string> { "Roll ei ole maaratud" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                                        .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);

            if (user != null)
                roles = userManager.GetRoles(user.Id);


            return View(roles);
        }
        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }
        [HttpGet]
        public ActionResult Ankeet()
        {
            return View();
        }
        public ViewResult Ankeet(Guest guest)
        {
            E_mail(guest);
            if (ModelState.IsValid)
            {
                db.Guests.Add(guest);
                db.SaveChanges();
                return View("Thanks", guest); }
            else
            { return View(); }
        }
       
        
        public void E_mail(Guest guest)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "dyer7347@gmail.com";
                WebMail.Password = "rbxr nele shsg zxib";
                WebMail.From = "dyer7347@gmail.com";
                WebMail.Send(guest.Email, "Vastus kutsele", guest.Name + " vastas " + ((guest.WillAttend ?? false) ?
                    "tuleb peole" : "ei tule peole"));

                
            }
            catch (Exception)
            {
                ViewBag.Message = "Mul on kahjulti saa kirja saada!";
            }

        }
        PeoContext dabik = new PeoContext();
        [Authorize]
        public ActionResult Peod()
        {
            IEnumerable<Peo> Peod = dabik.Peod;
            return View(Peod);
        }

        GuestContext db= new GuestContext();
        //[Authorize]
        [Authorize]
        public ActionResult Guests()
        {
            IEnumerable<Guest> guests= db.Guests;
            return View(guests);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        

        [HttpGet]
        public ActionResult crit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult crit(Peo Peod)
        {
            dabik.Peod.Add(Peod);
            dabik.SaveChanges();
            return RedirectToAction("Peod");

        }
        [HttpPost]
        public ActionResult Create(Guest guest)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        
        public ActionResult Delete(int id)
        {
            Guest g = db.Guests.Find(id);
            
                if (g == null)
                {
                    return HttpNotFound();
                }
                return View(g);
            
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            db.Guests.Remove(g);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Guest guest)
        {
            db.Entry(guest).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        [HttpGet]
        public ActionResult Accept()
        {
            IEnumerable<Guest> guests = db.Guests.Where(g => (bool)g.WillAttend);
            return View(guests);
        }
       
    }

}
