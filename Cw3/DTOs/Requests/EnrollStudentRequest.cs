using System;
using System.ComponentModel.DataAnnotations;

namespace Cw3.DTOs.Requests {
    public class EnrollStudentRequest {
        [Required]
        [RegularExpression("^s\\d+$")]
        public string IndexNumber { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [MaxLength(100)]
        public string Studies { get; set; }
    }
}