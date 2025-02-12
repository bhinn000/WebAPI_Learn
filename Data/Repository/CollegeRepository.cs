
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace WebAPI_Learn.Data.Repository
{
    public class CollegeRepository<T> : ICollegeRepository<T> where T : class
    {
        private readonly CollegeDBContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public CollegeRepository(CollegeDBContext dbContext, DbSet<T> dbSet)
        {
            _dbContext = dbContext;
            _dbSet = dbSet;
        }
        public async Task CreateStudentAsync(T dbRecord)
        {
            await _dbSet.AddAsync(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Expression<Func<T, bool>> filter,int id)
        {
            //var student = await _dbSet.Where(n => n.ID == id).FirstOrDefaultAsync();
            var student = await _dbSet.Where(filter).FirstOrDefaultAsync();
            _dbSet.Remove(student);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIDAsync(Expression<Func<T,bool>> filter,int id)
        {
            //return await _dbSet.Where(n => n.ID == id).FirstOrDefaultAsync();
            return await _dbSet.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateStudentAsync(Expression<Func<T, bool>> filter,T dto) 
        {
            //StudentData existingStudent = await _dbContext.Students.Where(s => s.ID == studentDTO.ID).FirstOrDefaultAsync();
            T existingStudent = await _dbSet.Where(filter).FirstOrDefaultAsync();
            var studentNameProperty = typeof(T).GetProperty("StudentName");
            if (studentNameProperty == null)
            {
                throw new Exception("StudentName property not found.");
            }
            studentNameProperty.SetValue(existingStudent, studentNameProperty.GetValue(dto));
            return await _dbContext.SaveChangesAsync();
        }
    }
}
