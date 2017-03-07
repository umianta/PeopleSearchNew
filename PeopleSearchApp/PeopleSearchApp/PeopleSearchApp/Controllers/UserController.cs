using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PeopleSearchApp.DataContext;
using PeopleSearchApp.Models;
using PeopleSearchApp.Repository;
using System.IO;

namespace PeopleSearchApp.Controllers
{
    public class UserController : Controller
    {
        private IRepository<User> _user = null;
        public UserController( IRepository<User> userRepo)
        {
            this._user = userRepo;
        }

       
        // GET: User
        public ViewResult PeopleList()
        {
            return View(_user.GetAll());
        }

        public ActionResult UserSearchScreen()
        {
            return View();
        }
        public JsonResult UserSearch(string searchString)
        {
            var users = from u in _user.GetAll() select u;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                       || s.FirstName.ToUpper().Contains(searchString.ToUpper())).OrderBy(s=>s.FirstName);
            }
            System.Threading.Thread.Sleep(5000);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _user.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Address,Interests,Age")] User user, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(upload.FileName);
                    string path = Path.Combine(Server.MapPath("~/UserImages"),
                                               fileName);
                    upload.SaveAs(path);
                    user.PicPath = fileName;
                    
                }
                _user.Insert(user);
                _user.Save();
                return RedirectToAction("PeopleList");
            }
            return View(user);
        }

       

        
        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _user.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //User user = _user.GetById(id);
           _user.Delete(id);
           _user.Save();
            return RedirectToAction("PeopleList");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _user.di();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
