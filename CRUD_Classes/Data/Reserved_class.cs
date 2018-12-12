namespace CRUD_Classes.Models
{
    public class Reserved_class
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public ClassModel Class { get; set; }
    }
}