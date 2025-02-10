using Microsoft.AspNetCore.Mvc;
using WebAPI_Learn.Models;
using WebAPI_Learn.Data;


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
        //private readonly IMyLoggings _myLoggings;// injecting services in controller
        private readonly ILogger<StudentsController> _logger; // in built logging mechanism ; can only log to debug , console but not to db or text file
        private readonly CollegeDBContext _dbContext;
       public StudentsController(ILogger<StudentsController> logger, CollegeDBContext dbContext)
        {
            //_myLoggings= myLoggings;// injecting services in controller (using D.I)
            //_myLoggings = LogToDB(); //withoug using D.I.
            _logger = logger;
            _dbContext = dbContext;
        }
        //****HTTP GET
        [HttpGet] //get all students 
        public IEnumerable<StudentData> GetMoreStudentName()
        {
            return _dbContext.Students;
        }

        [HttpGet("All" , Name = "GetAllStudentName")] //get all students
        public IEnumerable<StudentDTO> GetAllStudentName()
        {
            //_myLoggings.Log("My message");// injecting services in controller
            _logger.LogInformation("All the students have been fetched");
            //return CollegeRepository.Students;
            //business logic level which will convert the data from dll , use dto concept here
            //var StudentDTO = new List<StudentDTO>();
            var StudentDTO1 = _dbContext.Students.Select(s => new StudentDTO() //now convert student list to studentDto list
            {
                ID=s.ID,
                Email=s.Email,
                Address=s.Address,
                StudentName=s.StudentName,
            }).ToList();
            return StudentDTO1;
            
        }

        //[HttpGet("{sym:alpha}", Name = "GetAStudentBySymbol")] //https://localhost:7226/api/Students/GetAStudentBySymbol?symbol1=A10
        //public Student GetAStudentBySymbol(string symbol1)
        //{
        //    return CollegeRepository.Students.Where(n => n.Symbol == symbol1).FirstOrDefault();
        //}

        [HttpGet("{id:int}", Name = "GetAStudentByID")] //take id of type integer
        public ActionResult<StudentDTO> GetAStudentByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest();//400 --client error ; 500 --server error
            }
            var student = _dbContext.Students.Where(n => n.ID == id).FirstOrDefault();
            if (student == null)
            {
                _logger.LogWarning($"The id {id} doesnt exist");
                return NotFound("The student is not there");//404--not found
            }
            var studentDTO = new StudentDTO()
            {
                ID = student.ID,
                Email = student.Email,
                Address = student.Address,
                StudentName =student.StudentName,
            };
            return Ok(studentDTO); //200
        }

        [HttpDelete("{id}", Name = "DeleteAStudentByID")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public bool DeleteStudent(int id)
        {
            var student = _dbContext.Students.Where(n => n.ID == id).FirstOrDefault();
            _dbContext.Students.Remove(student);
            return true;
        }

        //**HTTP POST
        [HttpPost("create")] //api/Students/create
        public ActionResult<StudentModel> CreateStudent([FromBody] StudentDTO studentDTO) //creating 'Student' from 'StudentDTO'
        {
            if (studentDTO == null) 
            {
                _logger.LogError("You have to provide the model ");
                return BadRequest();
            }
            //if (studentDTO.Roll < 0)
            //{
            //    ModelState.AddModelError("Roll number error", "Roll number cant be negtive");
            //    return BadRequest(ModelState);
            //}

            StudentData student = new StudentData
            {
                StudentName= studentDTO.StudentName,
                Email = studentDTO.Email,
                Address = studentDTO.Address,
                ID=studentDTO.ID
            };

            //studentDTO.ID =student.ID;
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();
            //return Ok(student);
            return CreatedAtRoute("GetAStudentByID", new { id = studentDTO.ID} , studentDTO);//give the url for newly created Student , 201 
        }

        [HttpPut("update")]
        ///api/Students/update
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<StudentModel> UpdateStudent([FromBody] StudentDTO studentDTO)
        {
            if(studentDTO == null)
            {
                return BadRequest();
            }
            var existingStudent= _dbContext.Students.Where(s=>s.ID==studentDTO.ID).FirstOrDefault();
            existingStudent.StudentName=studentDTO.StudentName;
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
