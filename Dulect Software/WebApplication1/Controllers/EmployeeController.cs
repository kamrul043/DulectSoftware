using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        private AuthenticationEntities db = new AuthenticationEntities();

        // GET: Employee
        public ActionResult Index()
        {
            var tbl_Emp = db.tbl_Emp.Include(t => t.tbl_Dept);
            return View(tbl_Emp.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Emp tbl_Emp = db.tbl_Emp.Find(id);
            if (tbl_Emp == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Emp);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.DeptId = new SelectList(db.tbl_Dept, "DId", "DName");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EId,EName,Salary,Dob,Gender,DeptId")] tbl_Emp tbl_Emp)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Emp.Add(tbl_Emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeptId = new SelectList(db.tbl_Dept, "DId", "DName", tbl_Emp.DeptId);
            return View(tbl_Emp);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Emp tbl_Emp = db.tbl_Emp.Find(id);
            if (tbl_Emp == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeptId = new SelectList(db.tbl_Dept, "DId", "DName", tbl_Emp.DeptId);
            return View(tbl_Emp);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EId,EName,Salary,Dob,Gender,DeptId")] tbl_Emp tbl_Emp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Emp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeptId = new SelectList(db.tbl_Dept, "DId", "DName", tbl_Emp.DeptId);
            return View(tbl_Emp);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Emp tbl_Emp = db.tbl_Emp.Find(id);
            if (tbl_Emp == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Emp);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Emp tbl_Emp = db.tbl_Emp.Find(id);
            db.tbl_Emp.Remove(tbl_Emp);
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
