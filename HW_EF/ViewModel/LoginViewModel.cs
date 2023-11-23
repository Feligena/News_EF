using System.ComponentModel.DataAnnotations;

namespace HW_EF.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(3)]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
