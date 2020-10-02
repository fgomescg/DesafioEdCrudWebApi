﻿using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ISubjectRepository : IRepositoryBase<Subject>
    {
        ValueTask<PagedList<Subject>> GetSubjects(SubjectParameters subjectParameters);       
        ValueTask<Subject> GetSubjectById(int subjectId);
        void CreateSubject(Subject subject);
        void UpdateSubject(Subject subject);
        void DeleteSubject(Subject subject);
    }
}
