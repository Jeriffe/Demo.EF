using System.Collections.Generic;

namespace Demo.EF.Entities
{
    public class Student
    {
        public Student()
        {
            Courses = new List<Course>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }

}
