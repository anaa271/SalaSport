using System.ComponentModel.DataAnnotations;

namespace SalaSport.Models
{
    public class Member
    {
        public int MemberId { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, Phone]
        public string Phone { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? UserId { get; set; }
    }
}
