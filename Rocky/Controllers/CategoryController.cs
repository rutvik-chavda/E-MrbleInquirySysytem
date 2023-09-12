using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Models;

namespace Rocky.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Category> objList = _db.Category;
            return View(objList);
        }


        //GET - CREATE
        public IActionResult Create()
        {
            ViewBag.applicatio = "a";
            return View();
        }


        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                Category c = _db.Category.FirstOrDefault(each => each.Name.ToLower() == obj.Name.ToLower());
                if (c==null)
                {
                    _db.Category.Add(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.applicatio = "ab";
            return View(obj);

        }


        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Category.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            ViewBag.applicatio = "a";
            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
                {
                    var a = _db.Category.FirstOrDefault(each => each.Name.ToLower() == obj.Name.ToLower());
                if (a != null)
                {
                    if (a.Id == obj.Id)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    if (a == null)
                    {
                        _db.Category.Update(obj);
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            ViewBag.applicatio = "abc";
            return View(obj);

        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Category.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Category.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
                _db.Category.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            

        }

    }
}
