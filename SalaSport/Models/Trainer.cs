using System.ComponentModel.DataAnnotations;

namespace SalaSport.Models
{
    public class Trainer
    {
        public int TrainerId { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, Phone]
        public string Phone { get; set; } = string.Empty;


        public string? UserId { get; set; }
    }
}
