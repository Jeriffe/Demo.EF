using Demo.EF.Entities;
using Demo.EF.Infrastructure;
using Demo.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            // WithDbContextDirectly();

            WithUnitOfWork();
        }

        private static void WithUnitOfWork()
        {
            var context = new StudentDbContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);

            var repository = new StudentRepository();
            repository.UnitOfWork = unitOfWork;

            var student =repository.GetByKey(1);

            Assert.AreEqual(1, student.ID);
        }

        private static void WithDbContextDirectly()
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
