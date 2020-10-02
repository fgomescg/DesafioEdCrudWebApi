using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEdCRUD.Controllers
{
    [ApiController]
    [Route("api/subject")]
    public class SubjectController : ControllerBase
    {
       
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public SubjectController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjects([FromQuery] SubjectParameters subjectParameters)
        {    
            var subjects = await _repository.Subject.GetSubjects(subjectParameters);

            _logger.LogInfo($"Returned {subjects.TotalCount} subjects from database.");

            var subjectsResult = new
            {
                subjects.TotalCount,
                subjects.PageSize,
                subjects.CurrentPage,
                subjects.TotalPages,
                subjects
            };

            return Ok(subjectsResult);
        }


        [HttpGet("{Id}", Name = "SubjectById")]
        public async Task<IActionResult> GetSubjectById(int Id)
        {           
            var subject = await _repository.Subject.GetSubjectById(Id);

            if (subject == null)
            {
                _logger.LogError($"Subject with id:{Id}, not found.");
                return NotFound();
            }
            else
            {
                var subjectResult = _mapper.Map<SubjectDto>(subject);
                _logger.LogInfo($"Returned Subject with id: {Id}");
                return Ok(subjectResult);
            }           
        }

        [HttpPost]
        public IActionResult CreateSubject([FromBody] SubjectForCreateUpdateDto subject)
        {  
            var subjectEntity = _mapper.Map<Subject>(subject);

            _repository.Subject.CreateSubject(subjectEntity);
            _repository.Subject.Save();

            var createdSubject = _mapper.Map<SubjectDto>(subjectEntity);

            return CreatedAtRoute("SubjectById", new { id = createdSubject.SubjectId }, createdSubject);            
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateSubject(int Id, [FromBody] SubjectForCreateUpdateDto subjectObj)
        {  
            var subjectEntity = await _repository.Subject.GetSubjectById(Id);

            if (subjectEntity == null)
            {
                _logger.LogError($"Subject with id: {Id}, not found in db.");
                return NotFound();
            }

            _mapper.Map(subjectObj, subjectEntity);

            _repository.Subject.UpdateSubject(subjectEntity);
            _repository.Subject.Save();

            return NoContent();
           
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteSubject(int Id)
        {            
            var subject = await _repository.Subject.GetSubjectById(Id);

            if (subject == null)
            {
                _logger.LogError($"Subject with id: {Id}, not found in db.");
                return NotFound();
            }

            _repository.Subject.DeleteSubject(subject);
            _repository.Subject.Save();

            return NoContent();                        
        }
    }    
}
