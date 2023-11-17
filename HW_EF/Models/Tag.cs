namespace HW_EF.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<PostsTags> PostsTags { get; set; }
    }
}
