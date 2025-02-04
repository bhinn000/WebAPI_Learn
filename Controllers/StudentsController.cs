using Microsoft.AspNetCore.Mvc;
using WebAPI_Learn.Models;

namespace WebAPI_Learn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        [HttpGet] //get all students 
        public IEnumerable<Student> GetMoreStudentName()
        {
            return CollegeRepository.Students;
        }

        [HttpGet("All" , Name = "GetAllStudentName")] //get all students
        public IEnumerable<Student> GetAllStudentName()
        {
            return CollegeRepository.Students;

        }

        [HttpGet("{sym:alpha}", Name = "GetAStudentBySymbol")] //https://localhost:7226/api/Students/GetAStudentBySymbol?symbol1=A10
        public Student GetAStudentBySymbol(string symbol1)
        {
            return CollegeRepository.Students.Where(n => n.Symbol == symbol1).FirstOrDefault();
        }

        [HttpGet("{id:int}", Name = "GetAStudentByID")] //take id of type integer
        public Student GetAStudentByID(int id)
        {
            return CollegeRepository.Students.Where(n => n.ID == id).FirstOrDefault();
        }

        //***cause route conflict
        //[HttpGet("{id:int}", Name = "GetAStudentByID")] //take id of type integer
        //public Student GetAStudentByID(int id)
        //{
        //return CollegeRepository.Students.Where(n => n.ID == id).FirstOrDefault();
        //}

        //[HttpGet("{id:int}", Name = "GetAStudentByRoll")] //take id of type integer
        //public Student GetAStudentByRoll(int id)
        //{
        //    return CollegeRepository.Students.Where(n => n.ID == id).FirstOrDefault();
        //}

        [HttpDelete("{id}", Name = "DeleteAStudentByID")]
        public bool DeleteStudent(int id)
        {
            var student = CollegeRepository.Students.Where(n => n.ID == id).FirstOrDefault();
            CollegeRepository.Students.Remove(student);
            return true;
        }
    }
}
