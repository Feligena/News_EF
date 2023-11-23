using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HW_EF.Models
{
    public class Person
    {
        public int Id { get; set; }
        
        [Required, MaxLength(20), MinLength(2)]
        public string Name { get; set; }

        [Required, MaxLength(20), MinLength(2)]
        public string Surname { get; set; }

        [Required, MaxLength(10), MinLength(4)]
        public string Nickname { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
        public ICollection<Post> UserPosts { get; set; }

        public bool IsAdmin { get; set; } = false;

        public Person()
        {
            UserPosts = new List<Post>();
        }
    }
}
