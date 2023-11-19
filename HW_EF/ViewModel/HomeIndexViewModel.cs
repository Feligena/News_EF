using HW_EF.Models;

namespace HW_EF.ViewModel
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Post>? Posts { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
