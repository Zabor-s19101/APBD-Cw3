using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers {
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase {
        [HttpGet]
        public string GetStudents(string orderBy) {
            if (orderBy == null) {
                return "Mateusz s19101";
            }
            return $"Mateusz s19101 sortowanie={orderBy}";
        }

        [HttpGet("{id}")]
        public string GetStudent(int id) {
            if (id == 1) {
                return "Mati";
            }
            if (id == 2) {
                return "Zabor";
            }
            return "Not found";
        }
    }
}