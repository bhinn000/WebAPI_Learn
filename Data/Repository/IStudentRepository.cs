using WebAPI_Learn.Models;

namespace WebAPI_Learn.Data.Repository
{
    //we had put all the routes inside controllers , which is not good practice
    //its better to have repository to communicate with database 
    //and controller communicates with repository
    public interface IStudentRepository
    {
        //we have to return the type that we have received from db
        Task<List<StudentData>> GetAllAsync();
        Task<StudentData> GetByIDAsync(int id);
        Task<int> DeleteAsync(int id);
        Task CreateStudentAsync(StudentData student);
        Task<int> UpdateStudentAsync(StudentDTO studentDTO);
    }
}
