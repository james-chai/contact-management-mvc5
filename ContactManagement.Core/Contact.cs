using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContactManagement.Core
{
    public class Contact
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [DisplayName("First Name")]
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required, StringLength(50)]
        public string LastName { get; set; }
        [DisplayName("Company Name")]
        public string CompanyName { get; set; } = null;
        [RegularExpression(@"^[0-9]{1,10}$", ErrorMessage = "Mobile must be numeric and up to 10 digits.")]
        [MaxLength(10, ErrorMessage = "Mobile cannot exceed 10 digits.")]
        public string Mobile { get; set; } = null;
        [Required, StringLength(80, ErrorMessage = "Email cannot exceed 80 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$",
        ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
