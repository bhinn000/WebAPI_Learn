using Microsoft.AspNetCore.Mvc;
using WebAPI_Learn.Models;

namespace WebAPI_Learn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status404NotFound)]//list all possible responses so that we can get documented response
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public class StudentsController : ControllerBase
    {
        //****HTTP GET
        [HttpGet] //get all students 
        public IEnumerable<Student> GetMoreStudentName()
        {
            return CollegeRepository.Students;
        }

        [HttpGet("All" , Name = "GetAllStudentName")] //get all students
        public IEnumerable<StudentDTO> GetAllStudentName()
        {
            //return CollegeRepository.Students;
            //business logic level which will convert the data from dll , use dto concept here
            //var StudentDTO = new List<StudentDTO>();
            var StudentDTO1 = CollegeRepository.Students.Select(s => new StudentDTO() //now convert student list to studentDto list
            {
                ID=s.ID,
                Roll=s.Roll,
                Symbol=s.Symbol,
                StudentName=s.StudentName,
            });
            return StudentDTO1;
            
        }

        [HttpGet("{sym:alpha}", Name = "GetAStudentBySymbol")] //https://localhost:7226/api/Students/GetAStudentBySymbol?symbol1=A10
        public Student GetAStudentBySymbol(string symbol1)
        {
            return CollegeRepository.Students.Where(n => n.Symbol == symbol1).FirstOrDefault();
        }

        [HttpGet("{id:int}", Name = "GetAStudentByID")] //take id of type integer
        public ActionResult<StudentDTO> GetAStudentByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest();//400 --client error ; 500 --server error
            }
            var student = CollegeRepository.Students.Where(n => n.ID == id).FirstOrDefault();
            if (student == null)
            {
                return NotFound("The student is not there");//404--not found
            }
            var studentDTO = new StudentDTO()
            {
                ID = student.ID,
                Roll=student.Roll,
                Symbol=student.Symbol,
                StudentName=student.StudentName,
            };
            return Ok(studentDTO); //200
        }

        [HttpDelete("{id}", Name = "DeleteAStudentByID")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public bool DeleteStudent(int id)
        {
            var student = CollegeRepository.Students.Where(n => n.ID == id).FirstOrDefault();
            CollegeRepository.Students.Remove(student);
            return true;
        }

        //**HTTP POST
        [HttpPost("create")] //api/Students/create
        public ActionResult<Student> CreateStudent([FromBody] StudentDTO studentDTO) //creating 'Student' from 'StudentDTO'
        {
            if (studentDTO == null) 
            { 
                return BadRequest();
            }
            if (studentDTO.Roll < 0)
            {
                ModelState.AddModelError("Roll number error", "Roll number cant be negtive");
                return BadRequest(ModelState);
            }
            int newID = CollegeRepository.Students.LastOrDefault().ID + 1;
            Student student = new Student
            {
                ID=newID,
                StudentName= studentDTO.StudentName,
                Roll= studentDTO.Roll,
                Symbol= studentDTO.Symbol,
            };
            studentDTO.ID =student.ID;
            CollegeRepository.Students.Add(student);
            //return Ok(student);
            return CreatedAtRoute("GetAStudentByID", new { id = studentDTO.ID} , studentDTO);//give the url for newly created Student , 201 
        }

        [HttpPut("update")]
        ///api/Students/update
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<Student> UpdateStudent([FromBody] StudentDTO studentDTO)
        {
            if(studentDTO == null)
            {
                return BadRequest();
            }
            var existingStudent= CollegeRepository.Students.Where(s=>s.ID==studentDTO.ID).FirstOrDefault();
            existingStudent.StudentName=studentDTO.StudentName;
            return NoContent();
        }
    }
}
