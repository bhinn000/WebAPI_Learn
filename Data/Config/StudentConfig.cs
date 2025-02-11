using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPI_Learn.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<StudentData>
    {
        public void Configure(EntityTypeBuilder<StudentData> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.ID);//make primary key to ID
            builder.Property(x => x.ID).UseIdentityColumn(); // auto increment new entities 
            builder.Property(n => n.StudentName).IsRequired();
            builder.Property(n => n.Address).IsRequired(false).HasMaxLength(500);
            builder.Property(n => n.Email).IsRequired(false).HasMaxLength(500);//will allow Null value

            builder.HasData(new List<StudentData>() {
                new StudentData{ID=1 , StudentName="Bhintuna" ,Email="b@gmail.com" , Address="Nepal"} ,
                new StudentData{ID=2 , StudentName="Shakya" ,Email="b@gmail.com" , Address="Nepal2"}
            });

        }
    }
}
    