namespace Demo.EF.Entities
{
    public class Course
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public string Name { get; set; }
        public virtual Student Student { get; set; }
    }
}