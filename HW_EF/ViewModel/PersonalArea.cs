using HW_EF.Models;

namespace HW_EF.ViewModel
{
    public class PersonalArea
    {
        public Person User { get; set; }
        public IEnumerable<Post>? Posts { get; set; }
    }
}
