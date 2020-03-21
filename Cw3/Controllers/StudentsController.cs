using System;
using System.Linq;
using Cw3.DAL;
using Cw3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers {
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase {
        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService) {
            _dbService = dbService;
        }
        
        [HttpGet]
        public IActionResult GetStudents() {
            return Ok(_dbService.GetStudents());
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id) {
            if (_dbService.GetStudents().Any(student => student.Id == id)) {
                return Ok(_dbService.GetStudents().First(student => student.Id == id));
            }
            return NotFound("Nie znaleziono studenta");
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student) {
            if (_dbService.GetStudents().Any(fromStudents => fromStudents.Id == student.Id)) {
                return Ok("Student z takim Id już istnieje");
            }
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            //_dbService.GetStudents().Concat(new []{student});
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