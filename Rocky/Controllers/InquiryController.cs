using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class InquiryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public InquiryController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public IActionResult Index()
        {
            IEnumerable<InquiryDetails> i=_db.InquiryDetails.ToList();
            IEnumerable<Product> p = _db.Product.ToList();
            ViewBag.product = p;
            ViewBag.inquiry = i;
            return View();
        }
        public IActionResult Status(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Update(InquiryDetails s)
        {
            InquiryDetails i = _db.InquiryDetails.FirstOrDefault(each => each.Id==s.Id);
            i.status = s.status;
            _db.InquiryDetails.Update(i);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
