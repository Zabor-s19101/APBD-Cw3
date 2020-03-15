using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers {
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase {
        [HttpGet]
        public string GetStudent() {
            return "Mateusz s19101";
        }
    }
}