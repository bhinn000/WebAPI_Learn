using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Learn.Models;

namespace WebAPI_Learn.Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;
        public StudentRepository(CollegeDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateStudentAsync(StudentData student)
        {
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
        }
            
        public async Task<int> DeleteAsync(StudentData student)
        {
             _dbContext.Students.Remove(student);
            return  await _dbContext.SaveChangesAsync();
        }

        public async Task<List<StudentData>> GetAllAsync()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<StudentData> GetByIDAsync(int id)
        {
            return await _dbContext.Students.Where(n => n.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateStudentAsync(StudentDTO studentDTO)
        {
            StudentData existingStudent=await _dbContext.Students.Where(s => s.ID == studentDTO.ID).FirstOrDefaultAsync();
            existingStudent.StudentName = studentDTO.StudentName;
            return await _dbContext.SaveChangesAsync();
        }
    }
}
