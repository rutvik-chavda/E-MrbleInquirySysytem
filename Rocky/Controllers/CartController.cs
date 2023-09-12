using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Models;
using Rocky.Models.ViewModels;
using Rocky.Utility;

namespace Rocky.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
       
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;
        private readonly icountrepo _count;

        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }
        public CartController(ApplicationDbContext db,IWebHostEnvironment webHostEnvironment,IEmailSender emailSender,icountrepo count)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
            _count = count;
        }
        public IActionResult Index()
        {
            IEnumerable<InquiryCart> shoppingCartList = (from a in _db.InquiryCarts
                     where a.User == User.Identity.Name
                     select a);
            

            List<int> prodInCart = shoppingCartList.Select(i => i.Produtid).ToList();
            IEnumerable<Product> prodList = _db.Product.Where(u => prodInCart.Contains(u.Id));

            return View(prodList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {

            return RedirectToAction(nameof(Summary));
        }

        
        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
          

            IEnumerable<InquiryCart> shoppingCartList = (from a in _db.InquiryCarts
                                                         where a.User == User.Identity.Name
                                                         select a);


            List<int> prodInCart = shoppingCartList.Select(i => i.Produtid).ToList();
            IEnumerable<Product> prodList = _db.Product.Where(u => prodInCart.Contains(u.Id));

          

            ProductUserVM = new ProductUserVM()
            {
                ApplicationUser = _db.ApplicationUser.FirstOrDefault(u => u.Id == claim.Value),
                ProductList = prodList.ToList()
            };


            return View(ProductUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(ProductUserVM ProductUserVM)
        {
            
            for(var i=0;i< ProductUserVM.ProductList.Count();i++ )
            {
                Product p = _db.Product.FirstOrDefault(each => each.Id == ProductUserVM.ProductList[i].Id);
                InquiryDetails details = new InquiryDetails
                {
                    Name = ProductUserVM.ApplicationUser.FullName,
                    Phone=ProductUserVM.ApplicationUser.PhoneNumber,
                    Email=ProductUserVM.ApplicationUser.Email,
                    Produtid=ProductUserVM.ProductList[i].Id,
                    price=p.Price,
                    user=ProductUserVM.ApplicationUser.Email,
                    Datetime= DateTime.Now,
                    status="Inprocess"
                    


            };
                _db.InquiryDetails.Add(details);
                _db.SaveChanges();
                InquiryCart inquiryCart = _db.InquiryCarts.FirstOrDefault(each => each.Produtid == ProductUserVM.ProductList[i].Id && each.User == ProductUserVM.ApplicationUser.Email);
                _db.InquiryCarts.Remove(inquiryCart);
                _db.SaveChanges();
                int num = _count.getcount();
                _count.addcount(num-1);
                    }
          

            return RedirectToAction(nameof(InquiryConfirmation));
        }
        public IActionResult InquiryConfirmation()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult Remove(int id)
        {
            InquiryCart i= _db.InquiryCarts.FirstOrDefault(each => each.Produtid==id && each.User== User.Identity.Name);
            _db.InquiryCarts.Remove(i);
            _db.SaveChanges();
            int io = _count.getcount();
            _count.addcount(io - 1);
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //session exsits
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            shoppingCartList.Remove(shoppingCartList.FirstOrDefault(u => u.ProductId == id));
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }
    }
}
