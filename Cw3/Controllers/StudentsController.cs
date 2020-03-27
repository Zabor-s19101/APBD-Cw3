using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Cw3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers {
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s19101;Integrated Security=True";

        [HttpGet]
        public IActionResult GetStudents() {
            var list = new List<Student>();
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand()) {
                com.Connection = con;
                com.CommandText =
                    "select IndexNumber, FirstName, LastName, convert(varchar, BirthDate, 105) as BirthDate, Name, Semester from Student inner join Enrollment E on Student.IdEnrollment = E.IdEnrollment inner join Studies S on E.IdStudy = S.IdStudy";
                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read()) {
                    list.Add(new Student {
                        IndexNumber = dr["IndexNumber"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        BirthDate = dr["BirthDate"].ToString(),
                        StudyName = dr["Name"].ToString(),
                        Semester = int.Parse(dr["Semester"].ToString())
                    });
                }
            }
            return Ok(list);
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(string indexNumber) {
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand()) {
                com.Connection = con;
                com.CommandText = "select Student.IndexNumber, Student.FirstName, Student.LastName, Studies.Name, convert(varchar, Enrollment.StartDate, 105) as StartDate, Enrollment.Semester from dbo.Student inner join Enrollment on Enrollment.IdEnrollment = Student.IdEnrollment inner join Studies on Studies.IdStudy = Enrollment.IdStudy where Student.IndexNumber = @index";
                com.Parameters.AddWithValue("index", indexNumber);
                con.Open();
                var dr = com.ExecuteReader();
                if (dr.Read()) {
                    var st = new Student {
                        IndexNumber = dr["IndexNumber"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        StudyName = dr["Name"].ToString(),
                        StartDate = dr["StartDate"].ToString(),
                        Semester = int.Parse(dr["Semester"].ToString())
                    };
                    return Ok(st);
                }
            }
            return NotFound("Nie znaleziono studenta");
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student) {
            //creating student
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id) {
            //searching for student to update
            return Ok("Aktualizacja ukończona");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id) {
            //searching for student to delete
            return Ok("Usuwanie ukończone");
        }
    }
}