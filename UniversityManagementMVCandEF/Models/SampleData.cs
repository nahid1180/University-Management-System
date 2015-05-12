using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UniversityManagementMVCandEF.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<DiuDBContext>
    {
        protected override void Seed(DiuDBContext context)
        {
            var semesters = new List<Semester>
                {
                    new Semester{Name = "1.1"},
                    new Semester{Name = "1.2"},
                    new Semester{Name = "2.1"},
                    new Semester{Name = "2.1"},
                    new Semester{Name = "3.1"},
                    new Semester{Name = "3.2"},
                    new Semester{Name = "4.1"},
                    new Semester{Name = "4.2"}
                };

            var designations = new List<Designation>
                {
                  
                    new Designation{Name = "Department Head"},
                    new Designation{Name = "Professor"},
                    new Designation{Name = "Associate Professor"},
                    new Designation{Name = "Lecturer"}
                };

            new List<ClassRoom>
                {
                    new ClassRoom{RoomNo = "7A01"},
                    new ClassRoom{RoomNo = "7A02"},
                    new ClassRoom{RoomNo = "7B01"},
                    new ClassRoom{RoomNo = "7B02"},
                    new ClassRoom{RoomNo = "5A01"},
                     new ClassRoom{RoomNo = "5B01"}
                }.ForEach(c => context.ClassRooms.Add(c));
            new List<GradeLetter>
                {
                    new GradeLetter{Name="A+"},
                    new GradeLetter{Name="A"},
                    new GradeLetter{Name="A-"},
                    new GradeLetter{Name="B+"},
                    new GradeLetter{Name="B"},
                    new GradeLetter{Name="B-"},
                    new GradeLetter{Name="C"},
                    new GradeLetter{Name="D"},
                    new GradeLetter{Name = "F"}
                }.ForEach(g => context.GradeLetters.Add(g));

            var departments = new List<Department>
                {
                    new Department{Code = "CSE",Name = "Computer Science $ Engineering"},
                    new Department{Code = "EEE",Name = "Electrical and Electronic Engineering "},
                    new Department{Code = "CE",Name = "Civil Engineering"},
                    new Department{Code = "IPE",Name = "Industrial and Production Engineering"},
                    new Department{Code = "BBA",Name = "Business Administration"}
                };

            new List<Day>
                {
                    new Day{Name = "Saturday"},
                    new Day{Name = "Sunday"},
                    new Day{Name = "Monday"},
                    new Day{Name = "Tuesday"},
                    new Day{Name = "Wednesday"},
                    new Day{Name = "Thursday"},
                   
                }.ForEach(d => context.Days.Add(d));
            new List<Course>
                {
                    new Course{Name = "C",Code ="OOP1" ,Credit = 4, Department = departments[0], Semester = semesters[0],Description = "Departmental Course"},
                    new Course{Name = "C#",Code = "OOP2",Credit = 3,Department = departments[0], Semester = semesters[1],Description = "Departmental Course "},
                    new Course{Name = "java",Code = "OOP3",Credit = 3,Department = departments[0], Semester = semesters[2],Description = "Departmental Course "},
                    new Course{Name = "SQL",Code ="DataBase1" ,Credit = 3,Department = departments[0], Semester = semesters[3],Description = "Departmental Course "},
                    new Course{Name = "Oracle",Code = "Database2",Credit = 2,Department = departments[0], Semester = semesters[4], Description = "Departmental Course"}
                    
                }.ForEach(c => context.Courses.Add(c));

            new List<Teacher>
                {
                    new Teacher{Name = "Umme Zakia", Address = "Dhaka", CreditToBeTaken = 10, RemainingCredit = 10,Department = departments[0], ContractNo = 0167,Email = "zakia@gmail.com",Designation = designations[1] },
                    new Teacher{Name = "Shuvo", Address = "Hatirjheel", CreditToBeTaken = 6,RemainingCredit = 6,Department = departments[0],ContractNo = 0191,Email = "shuvo@gmail.com",Designation = designations[1] },
                    new Teacher{Name = "Wasi", Address = "Panthpoth", CreditToBeTaken = 7,RemainingCredit = 7,Department = departments[1],ContractNo = 0171,Email = "wasi@gmail.com",Designation = designations[2] },
                    new Teacher{Name = "shobnom ", Address = "Mirpur", CreditToBeTaken = 8,RemainingCredit = 8,Department = departments[2],ContractNo = 0151,Email = "shobnom@gmail.com",Designation = designations[3] }
                }.ForEach(t => context.Teachers.Add(t));

            new List<Student>
                {
                    new Student{Name = "Nahid",Email = "nahid@gmail.com",ContactNo = 0191,Department = departments[0],RegNo = "CSE2014.001",Date =Convert.ToDateTime("2014-12-20 00:00:00.000")},
                    new Student{Name = "Saif",Email = "saif@gmail.com",ContactNo = 0167,Department = departments[0],RegNo = "CSE2014.002",Date =Convert.ToDateTime("2014-12-20 00:00:00.000")},
                    new Student{Name = "Showmik",Email = "showmik@gmail.com",ContactNo = 0152,Department = departments[1],RegNo = "EEE2014.001",Date =Convert.ToDateTime("2014-12-20 00:00:00.000")},
                    new Student{Name = "Shanta",Email = "shanta@gmail.com",ContactNo = 0181,Department = departments[1],RegNo = "EEE2014.002",Date =Convert.ToDateTime("2014-12-20 00:00:00.000")}
                }.ForEach(s => context.Students.Add(s));
        }
    }
}