using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioEdCRUD.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public SubjectService(IRepositoryWrapper repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async ValueTask<PagedList<Subject>> GetSubjectsAsync(SubjectParameters subjectParameters)
        {
            var subjects = await _repository.Subject.GetSubjectsAsync(subjectParameters);

            var subjectList = PagedList<Subject>.ToPagedList(subjects.AsQueryable(),
                 subjectParameters.PageNumber,
                 subjectParameters.PageSize);

            return subjectList;
        }

        public async ValueTask<SubjectDto> GetSubjectByIdAsync(int Id)
        {
            var subject = await _repository.Subject.GetSubjectByIdAsync(Id);

            if (subject == null)
            {
                _logger.LogError($"subject with id:{Id}, not found.");
                return null;
            }

            var subjectResult = _mapper.Map<SubjectDto>(subject);
            _logger.LogInfo($"Returned subject with id: {Id}");
            return subjectResult;
        }

        public async ValueTask<SubjectDto> CreateSubjectAsync(Subject subject)
        {
            await _repository.Subject.CreateSubjectAsync(subject);

            var createdsubject = _mapper.Map<SubjectDto>(subject);

            _logger.LogInfo($"Subject created with id: {createdsubject.SubjectId}");

            return createdsubject;
        }

        public async Task<bool> UpdateSubjectAsync(int Id, SubjectPut subject)
        {
            var subjectEntity = await _repository.Subject.GetSubjectByIdAsync(Id);

            if (subjectEntity == null)
            {
                _logger.LogError($"Subject with id: {Id}, not found in db.");
                return false;
            }

            var updatedSubject = _mapper.Map(subject, subjectEntity);

            await _repository.Subject.UpdateSubjectAsync(updatedSubject);

            return true;
        }

        public async Task<bool> DeleteSubjectAsync(int Id)
        {
            var subject = await _repository.Subject.GetSubjectByIdAsync(Id);

            if (subject == null)
            {
                _logger.LogError($"subject with id: {Id}, not found in db.");
                return false;
            }

            await _repository.Subject.DeleteSubjectAsync(subject);

            return true;
        }
    }
}
