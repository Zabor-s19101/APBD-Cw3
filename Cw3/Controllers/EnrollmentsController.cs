using System;
using System.Data.SqlClient;
using System.Globalization;
using Cw3.DTOs.Requests;
using Cw3.DTOs.Responses;
using Cw3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers {
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase {
        private readonly IStudentsDbService _service;

        public EnrollmentsController(IStudentsDbService service) {
            _service = service;
        }
        
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request) {
            EnrollStudentResponse response = null;
            try {
                response = _service.EnrollStudent(request);
            } catch (SqlException e) {
                return NotFound(e.Message);
            } catch (Exception e) {
                return BadRequest("Studia nie istnieją");
            }
            return Created(nameof(response), response);
        }
        
        [HttpPost("promotions")]
        public IActionResult EnrollPromotions(EnrollPromotionsRequest request) {
            EnrollStudentResponse response = null;
            try {
                response = _service.EnrollPromotions(request);
            } catch (SqlException e) {
                return NotFound(e.Message);
            }
            return Created(nameof(response), response);
        }
    }
}