using System.ComponentModel.DataAnnotations;

namespace Cw3.DTOs.Requests {
    public class EnrollPromotionsRequest {
        [Required]
        [MaxLength(100)]
        public string Studies { get; set; }
        [Required]
        public int Semester { get; set; }
    }
}