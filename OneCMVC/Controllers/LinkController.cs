using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OneCMVC.Models;

namespace OneCMVC.Controllers
{
    public class LinkController : Controller
    {
        private OneCMVCEntities1 db = new OneCMVCEntities1();

        //POST: /Link/
        public ActionResult Index(string searchString, int categoryID = 0)
        {
            //1. Tao danh sách danh muc hiển thị ở giao diện thông qua DropDownList
            var categories = from c in db.Categories select c;
            ViewBag.categoryID = new SelectList(categories, "CategooryID", "CategoryName");
            
            //2.tao lien ket giua 2 bang category va link bang menh de join
            var links = from l in db.Links
                        join c in db.Categories on l.CategoryID equals c.CategoryID
                        select new { l.LinkID, l.LinkName, l.LinkURL, l.LinkDescription, l.CategoryID, l.CategoryName};
            
            //3. tim kiem chuoi truy van 
            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.LinkName.Contains(searchString));    
            }

            //4. tim kiem theo categoyID
            if(categoryID != 0)
            {
                links = links.Where(x => x.CategoryID == categoryID);
            }

            //5. chuyen doi ket qua tu var sang danh sach  List<Link>
            List<Link> listLinks = new List<Link>();
            foreach (var item in links)
            {
                Link temp = new Link();
                temp.CategoryID = item.CategoryID;
                temp.CategoryName = item.CategoryName;
                temp.LinkDescription = item.LinkDescription;
                temp.LinkID = item.LinkID;
                temp.LinkName = item.LinkName;
                temp.LinkURL = item.LinkURL;
                listLinks.Add(temp);
            }
            return View(links);
        }

        // GET: Link/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = db.Links.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // GET: Link/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Link/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LinkID,LinkName,LinkURL,LinkDescription,CategoryID")] Link link)
        {
            if (ModelState.IsValid)
            {
                db.Links.Add(link);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(link);
        }

        // GET: Link/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = db.Links.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // POST: Link/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LinkID,LinkName,LinkURL,LinkDescription,CategoryID")] Link link)
        {
            if (ModelState.IsValid)
            {
                db.Entry(link).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(link);
        }

        // GET: Link/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = db.Links.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // POST: Link/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Link link = db.Links.Find(id);
            db.Links.Remove(link);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
