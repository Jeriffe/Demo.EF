using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.EF.Entities.Maps
{
    public class CourseMap : EntityTypeConfiguration<Course>
    {
        public CourseMap()
        {
            ToTable("Courses");

            HasKey(d => d.ID);

            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Name)
           .IsRequired()
           .HasMaxLength(50);

            HasRequired(t => t.Student)
           .WithMany(t => t.Courses)
           .HasForeignKey(d => d.StudentID);
        }
    }
}
