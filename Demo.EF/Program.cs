using Demo.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.EF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new StudentDbContext())
            {
                var s = new Student()
                {
                    Name = "Summer",
                    Age = 24,
                    Courses = new List<Course> { new Course { Name = "English" } }
                };

                context.Set<Student>().Add(s);
                context.SaveChanges();
            }
        }
    }
}
