﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagementMVCandEF.Models;

namespace UniversityManagementMVCandEF.Controllers
{
    public class ClassRoomAllocationController : Controller
    {
        private DiuDBContext db = new DiuDBContext();

        // GET: /ClassRoomAllocation/
        public ActionResult Index()
        {
            var classroomallocations = db.ClassRoomAllocations.Include(c => c.ClassRoom).Include(c => c.Course).Include(c => c.Day).Include(c => c.Department);
            return View(classroomallocations.ToList());
        }

        // GET: /ClassRoomAllocation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoomAllocation classroomallocation = db.ClassRoomAllocations.Find(id);
            if (classroomallocation == null)
            {
                return HttpNotFound();
            }
            return View(classroomallocation);
        }

        // GET: /ClassRoomAllocation/Create
        public ActionResult Create()
        {
            ViewBag.ClassRoomId = new SelectList(db.ClassRooms, "ClassRoomId", "RoomNo");
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code");
            ViewBag.DayId = new SelectList(db.Days, "DayId", "Name");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            return View();
        }

        // POST: /ClassRoomAllocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ClassRoomAllocationId,DepartmentId,CourseId,ClassRoomId,DayId,TimeFrom,TimeTo")] ClassRoomAllocation classroomallocation)
        {
            if (ModelState.IsValid)
            {
                db.ClassRoomAllocations.Add(classroomallocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassRoomId = new SelectList(db.ClassRooms, "ClassRoomId", "RoomNo", classroomallocation.ClassRoomId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", classroomallocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", classroomallocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", classroomallocation.DepartmentId);
            return View(classroomallocation);
        }

        // GET: /ClassRoomAllocation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoomAllocation classroomallocation = db.ClassRoomAllocations.Find(id);
            if (classroomallocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassRoomId = new SelectList(db.ClassRooms, "ClassRoomId", "RoomNo", classroomallocation.ClassRoomId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", classroomallocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", classroomallocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", classroomallocation.DepartmentId);
            return View(classroomallocation);
        }

        // POST: /ClassRoomAllocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ClassRoomAllocationId,DepartmentId,CourseId,ClassRoomId,DayId,TimeFrom,TimeTo")] ClassRoomAllocation classroomallocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classroomallocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassRoomId = new SelectList(db.ClassRooms, "ClassRoomId", "RoomNo", classroomallocation.ClassRoomId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", classroomallocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", classroomallocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", classroomallocation.DepartmentId);
            return View(classroomallocation);
        }

        // GET: /ClassRoomAllocation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoomAllocation classroomallocation = db.ClassRoomAllocations.Find(id);
            if (classroomallocation == null)
            {
                return HttpNotFound();
            }
            return View(classroomallocation);
        }

        // POST: /ClassRoomAllocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassRoomAllocation classroomallocation = db.ClassRoomAllocations.Find(id);
            db.ClassRoomAllocations.Remove(classroomallocation);
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
