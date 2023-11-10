using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace HW_EF.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required, MaxLength(100), MinLength(2)]
        public string Title { get; set; }

        [Required, MaxLength(600), MinLength(3)]
        public string Content { get; set; }

        [Required, MaxLength(250)]
        [Display(Name = "Image ")]
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }

        [Required]
        public string Category { get; set; }

        public int? PersonId { get; set; }
        public Person? Person { get; set; }
    }
}