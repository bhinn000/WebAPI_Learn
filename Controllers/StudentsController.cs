using Microsoft.AspNetCore.Mvc;
using WebAPI_Learn.Models;
using WebAPI_Learn.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebAPI_Learn.Data.Repository;


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
        private readonly IMapper _mapper;
        //private readonly IStudentRepository _studentRepository;
        private readonly ICollegeRepository<StudentData> _studentRepository;
        public StudentsController(ILogger<StudentsController> logger,IMapper mapper , ICollegeRepository<StudentData> studentRepository, CollegeDBContext dbContext)
        {
            //_myLoggings= myLoggings;// injecting services in controller (using D.I)
            //_myLoggings = LogToDB(); //withoug using D.I.
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
            _studentRepository = studentRepository;
        }
        //****HTTP GET
        [HttpGet] //get all students 
        public IEnumerable<StudentData> GetMoreStudentName()
        {
            return _dbContext.Students;
        }

        [HttpGet("All" , Name = "GetAllStudentName")] //get all students
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetAllStudentNameAsync()
        {
            //_myLoggings.Log("My message");// injecting services in controller
                _logger.LogInformation("All the students have been fetched");
            //return CollegeRepository.Students;
            //business logic level which will convert the data from dll , use dto concept here

            var students = await _studentRepository.GetAllAsync();

            var StudentDTO1 = _mapper.Map<List<StudentDTO>>(students);

            return Ok(StudentDTO1);            
        }

        //[HttpGet("{sym:alpha}", Name = "GetAStudentBySymbol")] //https://localhost:7226/api/Students/GetAStudentBySymbol?symbol1=A10
        //public Student GetAStudentBySymbol(string symbol1)
        //{
        //    return CollegeRepository.Students.Where(n => n.Symbol == symbol1).FirstOrDefault();
        //}

        [HttpGet("{id:int}", Name = "GetAStudentByID")] //take id of type integer
        public async Task<ActionResult<StudentDTO>> GetAStudentByIDAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();//400 --client error ; 500 --server error
            }
            var student =await _studentRepository.GetByIDAsync(n => n.ID == id);
            if (student == null)
            {
                _logger.LogWarning($"The id {id} doesnt exist");
                return NotFound("The student is not there");//404--not found
            }
            //var studentDTO = new StudentDTO()
            //{
            //    ID = student.ID,
            //    Email = student.Email,
            //    Address = student.Address,
            //    StudentName =student.StudentName,
            //};

            var studentDTO=_mapper.Map<StudentDTO>(student);
            return Ok(studentDTO); //200
        }

        [HttpDelete("{id}", Name = "DeleteAStudentByID")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<bool> DeleteStudent(int id)
        {
            await _studentRepository.DeleteAsync(n => n.ID == id);
            return true;
        }
            //to only keep related to controller , removing database logic from controller
        //**HTTP POST
        [HttpPost("create")] //api/Students/create
        public async Task<ActionResult<StudentData>> CreateStudent([FromBody] StudentDTO studentDTO) //creating 'Student' from 'StudentDTO'
        {
            if (studentDTO == null || studentDTO.ID < 0) 
            {
                _logger.LogError("You have to provide the correct model ");
                return BadRequest();
            }

            var student = _mapper.Map<StudentData>(studentDTO); //this needs both class to have same named variables
            await _studentRepository.CreateStudentAsync(student);
            //return Ok(student);
            return CreatedAtRoute("GetAStudentByID", new { id = studentDTO.ID} , studentDTO);//give the url for newly created Student , 201 
        }

        [HttpPut("update")]
        ///api/Students/update
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<StudentModel>> UpdateStudent([FromBody] StudentDTO studentDTO, int id)
        {
            //StudentDTO studentDTOController = studentDTO;
            if(studentDTO == null)
            {
                return BadRequest();
            }

            var existingStudent = await _studentRepository.GetByIDAsync(n => n.ID == id); // Ensure this method is available
            if (existingStudent == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            await _studentRepository.UpdateStudentAsync(s => s.ID == studentDTO.ID, studentDTO);
            return NoContent();
        }
    }
}
