using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using csharpexam.Models;
using Microsoft.EntityFrameworkCore;

namespace csharpexam.Controllers
{
    public class HomeController : Controller
    {
    
        private MyContext _context { get; set; }
        private User GetUser()
        {
          return _context.users.FirstOrDefault(u => u.userID == HttpContext.Session.GetInt32("userID"));
        }

        public HomeController(MyContext context)
        {
            _context = context;
        }
        [HttpGet("signin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("home")]
        public IActionResult Dashboard()
        {
            User current = GetUser();
            if(current == null)
            {
                return Redirect("/signin");
            }
            ViewBag.User = current;
            List<Gathering> allevents = _context.gatherings
                .Include(m => m.coordinator)
                .Include(m => m.participants)
                .ThenInclude(u => u.attendee)
                .Where(m => m.date >= DateTime.Now)
                .OrderBy(m => m.date)
                .ToList();
            return View("dashboard", allevents);
        }

        [HttpGet("new")]
        public IActionResult NewPage()
        {
            User current = GetUser();
            if(current == null)
            {
                return Redirect("/signin");
            }
            ViewBag.User = current;
            return View("new");
        }

        [HttpGet("activity/{gatheringID}")]
        public IActionResult Details(int gatheringID)
        {
            User current = GetUser();
            if(current == null)
            {
                return Redirect("/signin");
            }
            ViewBag.User = current;
            Gathering theevent = _context.gatherings
                .Include(w => w.participants)
                .ThenInclude(a => a.attendee)
                .Include(w =>w.coordinator)
                .FirstOrDefault(w => w.gatheringID == gatheringID);
            ViewBag.User = current;
            return View("details", theevent);
        }

        [HttpPost("register")]
        public IActionResult Register(User newuser)
        {
            if(ModelState.IsValid)
            {
                if(_context.users.FirstOrDefault(u => u.email == newuser.email) != null)
                {
                  // Manually add a ModelState error to the Email field, with provided
                  // error message
                  ModelState.AddModelError("Email", "Email already in use!");
                  // You may consider returning to the View at this point
                  return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newuser.password = Hasher.HashPassword(newuser, newuser.password);
                _context.users.Add(newuser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("userID", newuser.userID);
                
                return RedirectToAction("Dashboard");
            
            }
            return View("Index");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUser login)
        {
            if(ModelState.IsValid)
            {
                User gettinguser = _context.users.FirstOrDefault(u=> u.email == login.loginemail);
                if(gettinguser == null)
                {
                    ModelState.AddModelError("loginemail", "Invalid Email/Password");
                    return View("Index");
                }
                var hash = new PasswordHasher<LoginUser>();
                var result = hash.VerifyHashedPassword(login, gettinguser.password, login.loginpassword);
                if(result == 0)
                {
                  ModelState.AddModelError("loginpassword", "Invalid Email/Password");
                  return View("Index");
                }
                HttpContext.Session.SetInt32("userID", gettinguser.userID);
                return RedirectToAction("Dashboard");

            }
            return View("Index");
            
        }
        [HttpPost("postevent")]
        public IActionResult PostEvent(Gathering newevent)
        {
            User current = GetUser();
            if (current == null)
            {
                return Redirect ("/signin");
            }
            if(ModelState.IsValid)
            {
                newevent.userID = current.userID;
                _context.gatherings.Add(newevent);
                _context.SaveChanges();
                return RedirectToAction("Details", newevent);


            }
            return View("new");
        }
        [HttpGet("{gatheringID}/delete")]
        public IActionResult Delete(int gatheringID)
        {
            User current = GetUser();
            if (current == null)
            {
                return RedirectToAction("Index");
            }
            Gathering todelete = _context.gatherings
                .FirstOrDefault(w => w.gatheringID == gatheringID);
                _context.gatherings.Remove(todelete);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
        }
        [HttpGet("{gatheringID}/rsvp")]
        public IActionResult RSVP(int gatheringID)
        {
            User current = GetUser();
            if (current == null)
            {
                return RedirectToAction("Index");
            }
            Association rsvp = new Association();
            rsvp.gatheringID = gatheringID;
            rsvp.userID = current.userID;
            _context.associations.Add(rsvp);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet("{gatheringID}/unrsvp")]
        public IActionResult UNRSVP(int gatheringID)
        {
            User current = GetUser();
            if (current == null)
            {
                return RedirectToAction("Index");
            }
            Association unrsvp = _context.associations.SingleOrDefault(r => r.gatheringID == gatheringID && r.userID== current.userID);
            unrsvp.gatheringID = gatheringID;
            unrsvp.userID = current.userID;
            _context.associations.Remove(unrsvp);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
     
        




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
