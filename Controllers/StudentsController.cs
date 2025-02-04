﻿using Microsoft.AspNetCore.Mvc;
using WebAPI_Learn.Models;

namespace WebAPI_Learn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status404NotFound)]//list all possible responses so that we can get documented response
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
        //[ProducesResponseType(StatusCodes.Status404NotFound)]//list all possible responses so that we can get documented response
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Student> GetAStudentByID(int id)
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
            return Ok(student); //200
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public bool DeleteStudent(int id)
        {
            var student = CollegeRepository.Students.Where(n => n.ID == id).FirstOrDefault();
            CollegeRepository.Students.Remove(student);
            return true;
        }
    }
}
