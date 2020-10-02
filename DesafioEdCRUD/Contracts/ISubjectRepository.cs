using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ISubjectRepository : IRepositoryBase<Subject>
    {
        ValueTask<PagedList<Subject>> GetSubjects(SubjectParameters subjectParameters);       
        ValueTask<Subject> GetSubjectById(int subjectId);
        Task CreateSubjectAsync(Subject subject);
        Task UpdateSubjectAsync(Subject subject);
        Task DeleteSubjectAsync(Subject subject);
    }
}
