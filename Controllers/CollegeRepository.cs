//in memory , by default this application have following data
using WebAPI_Learn.Models;

namespace WebAPI_Learn.Controllers
{
    public static class CollegeRepository
    {
        public static List<Student> Students {  get; set; } = new List<Student>
            {
                new Student{ID=01 , StudentName = "Ram" , Roll= 10, Symbol="A10"},
                new Student{ID=02 , StudentName = "Shyam" , Roll = 20, Symbol="A20"},
            };

    }
}
