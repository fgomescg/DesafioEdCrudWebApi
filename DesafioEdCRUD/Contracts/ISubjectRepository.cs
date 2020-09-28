using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ISubjectRepository : IRepositoryBase<Subject>
    {
        Task<Subject[]> GetAllSubjects(SubjectParameters subjectParameters);
        Task<Subject> GetSubjectById(int subjectId);
        void CreateSubject(Subject subject);
        void UpdateSubject(Subject subject);
        void DeleteSubject(Subject subject);
    }
}
