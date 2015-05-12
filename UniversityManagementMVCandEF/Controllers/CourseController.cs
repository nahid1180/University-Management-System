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
    public class CourseController : Controller
    {
        private DiuDBContext db = new DiuDBContext();

        // GET: /Course/
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Department).Include(c => c.Semester);
            return View(courses.ToList());
        }

        // GET: /Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: /Course/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "Name");
            return View();
        }

        // POST: /Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CourseId,Code,Name,Credit,Description,DepartmentId,SemesterId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "Name", course.SemesterId);
            return View(course);
        }

        // GET: /Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "Name", course.SemesterId);
            return View(course);
        }

        // POST: /Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CourseId,Code,Name,Credit,Description,DepartmentId,SemesterId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "Name", course.SemesterId);
            return View(course);
        }

        // GET: /Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: /Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
        public JsonResult CheckCourseCode(string code)
        {
            var result = db.Courses.Count(c => c.Code == code) == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckCourseName(string name)
        {
            var result = db.Courses.Count(c => c.Name == name) == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CourseAssign()
        {
            PopulateDropdownList();
            ViewBag.Message = "";
            return View();
        }

        private void PopulateDropdownList()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            ViewBag.TeacherId = new SelectList("", "TeacherId", "Name");
            ViewBag.CourseId = new SelectList("", "CourseId", "Code");
        }

        //
        // POST: /Course/Create
        [HttpPost]
        public ActionResult CourseAssign(Department aDepartment, Course aCourse, Teacher aTeacher)
        {
            PopulateDropdownList();
            if (aDepartment.DepartmentId == 0 || aCourse.CourseId == 0 || aTeacher.TeacherId == 0)
            {
                ViewBag.Message = "All fields are required";
               
                return View();
            }

            aDepartment = db.Departments.Find(aDepartment.DepartmentId);
            aTeacher = db.Teachers.Find(aTeacher.TeacherId);
            aCourse = db.Courses.Find(aCourse.CourseId);

            if (aCourse.Teacher != null)
            {
                PopulateDrodownList(aCourse, aTeacher);
                ViewBag.Message = "This Course already assigned to another teacher ";
                return View();

            }
            aCourse.Teacher = aTeacher;
            aCourse.Department = aDepartment;

            if (!ModelState.IsValid)
            {
                db.Entry(aCourse).State = EntityState.Modified;
                db.SaveChanges();
                aTeacher.RemainingCredit -= aCourse.Credit;
                db.Entry(aTeacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewCourseStatus");
            }
            return View();
        }

        private void PopulateDrodownList(Course aCourse, Teacher aTeacher)
        {
            var teachers = db.Teachers.Where(t => t.DepartmentId == aTeacher.DepartmentId).ToList();
            var courses = db.Courses.Where(s => s.DepartmentId == aCourse.DepartmentId).ToList();
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            ViewBag.TeacherId = new SelectList(teachers, "TeacherId", "Name", aTeacher.Name);
            ViewBag.CourseId = new SelectList(courses, "CourseId", "Code", aCourse.Code);
        }

        public ActionResult SelectDepartmentForTeacher(int? departmentId)
        {
            var teachers = db.Teachers.Where(t => t.DepartmentId == departmentId);
            ViewBag.TeacherId = new SelectList(teachers.ToArray(), "TeacherId", "Name");
            return PartialView("_Teacher", ViewData["TeacherId"]);
        }
        public ActionResult SelectDepartmentForCourse(int? departmentId)
        {
            var courses = db.Courses.Where(s => s.DepartmentId == departmentId);
            ViewBag.CourseId = new SelectList(courses.ToArray(), "CourseId", "Code");
            return PartialView("_Course", ViewData["CourseId"]);
        }
        public ActionResult SelectTeacher(int? teacherId)
        {
            Teacher teacher = db.Teachers.FirstOrDefault(t => t.TeacherId == teacherId);
            return PartialView("_TeacherDetails", teacher);
        }
        public ActionResult SelectCourse(int? courseId)
        {
            Course course = db.Courses.FirstOrDefault(c => c.CourseId == courseId);
            return PartialView("_CourseDetails", course);
        }


        public ActionResult ViewCourseStatus(int? departmentId)
        {
            try
            {
                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
                var model = db.Courses.Include(d => d.Department).Where(c => c.Department.DepartmentId == departmentId).Include(c => c.Semester).Include(c => c.Teacher);
                return View(model.ToList());
            }
            catch
            {
                return View();
            }
        }

        public PartialViewResult FilteredDepartment(int? departmentId)
        {
            var model = db.Courses.Include(d => d.Department).Where(c => c.Department.DepartmentId == departmentId).Include(c => c.Semester).Include(c => c.Teacher);
            return PartialView("_CourseInformation", model.ToList());
        }
    }
}
