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
            CreateStudent();
        }

        private static void CreateStudent()
        {
            using (var context = new StudentDbContext())
            {
                var original = context.Set<Student>().Find(1);
                if (original != null)
                {
                    return;
                }

                var s = new Student()
                {
                    Name = "Summer",
                    Age = 24,
                    Courses = new List<Course> { new Course { Name = "English" },
                     new Course { Name = "C Language Programm Design" }}
                };

                context.Set<Student>().Add(s);
                context.SaveChanges();
            }
        }
    }
}
