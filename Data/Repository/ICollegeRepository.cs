using System.Linq.Expressions;
using WebAPI_Learn.Models;

// this is for common reposito
namespace WebAPI_Learn.Data.Repository
{
    public interface ICollegeRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIDAsync(Expression<Func<T, bool>> filter);
        Task<int> DeleteAsync(Expression<Func<T, bool>> filter);
        Task CreateStudentAsync(T dbRecord);
        Task<int> UpdateStudentAsync(Expression<Func<T, bool>> filter); //update logic is wrong
    }
}
