using System.ComponentModel.DataAnnotations;

namespace HW_EF.ViewModel
{
    public class RegistrationViewModel
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        public string Password { get; set; }

        [Required]
        [MinLength(3)]
        [Compare("Password")]
        public string PasswordAgain { get; set; }

        [Required, MaxLength(20), MinLength(2)]
        public string Name { get; set; }

        [Required, MaxLength(20), MinLength(2)]
        public string Surname { get; set; }

        [Required, MaxLength(10), MinLength(4)]
        public string Nickname { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
