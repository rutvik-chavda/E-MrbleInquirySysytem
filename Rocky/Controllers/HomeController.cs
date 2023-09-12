using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rocky.Data;
using Rocky.Models;
using Rocky.Models.ViewModels;
using Rocky.Utility;

namespace Rocky.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly icountrepo _icountrepo;
        public HomeController(ILogger<HomeController> logger,icountrepo icountrepo, ApplicationDbContext db)
        {
            _icountrepo = icountrepo;
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Products = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType),
                Categories = _db.Category
            };
            return View(homeVM);
        }

        public IActionResult Details(int id)
        {


            DetailsVM DetailsVM = new DetailsVM()
            {
                Product = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType)
                           .Where(u => u.Id == id).FirstOrDefault(),
                ExistsInCart = false
            };
            var i = _db.InquiryCarts.FirstOrDefault(each => each.Produtid == id && each.User == User.Identity.Name);
            if (i != null)
            {
                ViewBag.addtocart = false;
            }
            else
            {
                ViewBag.addtocart = true;
            }
            if (User.Identity.Name != null)
            {
                ViewBag.login = true;
            }
            else
            {
                ViewBag.login = false;
            }
            return View(DetailsVM);
        }

        [HttpPost,ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
           
            InquiryCart i = new InquiryCart
            {

                Produtid = id,
                User = User.Identity.Name,
                Datetime = DateTime.Now
            };
            _db.InquiryCarts.Add(i);
            _db.SaveChanges();
            int count = _icountrepo.getcount();
            _icountrepo.addcount(count + 1);

            return RedirectToAction(nameof(Index));

           }

        public IActionResult RemoveFromCart(int id)
        {
          

           
            InquiryCart i = _db.InquiryCarts.FirstOrDefault(each => each.Produtid == id && each.User == User.Identity.Name);
            if (i != null)
            {
                _db.InquiryCarts.Remove(i);
                _db.SaveChanges();
                int count = _icountrepo.getcount();
                _icountrepo.addcount(count-1);
            }
          
            return RedirectToAction(nameof(Index));
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
