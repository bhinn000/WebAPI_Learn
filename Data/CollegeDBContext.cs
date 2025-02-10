using Microsoft.EntityFrameworkCore;
using WebAPI_Learn.Data.Config;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI_Learn.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
        { 
               
        }
        DbSet<Student> Students { get; set; } // for table Student of database CollegeDB (CollegeDBContext)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //using this , dbcontext will know about configuration file 
            modelBuilder.ApplyConfiguration(new StudentConfig());//adding configuration to db context

            //modelBuilder.Entity<Student>().HasData(new List<Student>() { 
            //    new Student{ID=1 , StudentName="Bhintuna" ,Email="b@gmail.com" , Address="Nepal"} ,
            //    new Student{ID=2 , StudentName="Shakya" ,Email="b@gmail.com" , Address="Nepal2"}
            //});

            //modifying schema using EF code first method
            //now this is done in configuration StudentConfig.cs
            //modelBuilder.Entity<Student>(entity =>
            //{
            //    entity.Property(n => n.StudentName).IsRequired();
            //    entity.Property(n => n.StudentName).IsRequired();
            //    entity.Property(n => n.Address).IsRequired(false).HasMaxLength(500);
            //    entity.Property(n => n.Email).IsRequired(false).HasMaxLength(500);
            //});
        }
    }
}

