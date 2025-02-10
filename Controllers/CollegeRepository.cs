//in memory , by default this application have following data
using WebAPI_Learn.Models;

namespace WebAPI_Learn.Controllers
{
    public static class CollegeRepository
    {
        public static List<StudentModel> Students {  get; set; } = new List<StudentModel>
            {
                new StudentModel{ID=01 , StudentName = "Ram" , Address= "USA", Email="ram@g.co"},
                new StudentModel{ID=02 , StudentName = "Shyam" , Address = "UK", Email="shyam@g.co"},
            };

    }
}
