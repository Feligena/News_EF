namespace HW_EF.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Post> Posts { get; set; }

        public Category()
        {
            Posts = new List<Post>();
        }
    }
}
