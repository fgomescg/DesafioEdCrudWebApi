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
        public async Task<IActionResult> CreateSubject([FromBody] Subject subjectFromBody)
        {  
            await _repository.Subject.CreateSubjectAsync(subjectFromBody);            

            var createdSubject = _mapper.Map<SubjectDto>(subjectFromBody);

            return CreatedAtRoute("SubjectById", new { id = createdSubject.SubjectId }, createdSubject);            
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateSubject(int Id, [FromBody] SubjectForCreateUpdateDto subject)
        {  
            var subjectEntity = await _repository.Subject.GetSubjectById(Id);

            if (subjectEntity == null)
            {
                _logger.LogError($"Subject with id: {Id}, not found in db.");
                return NotFound();
            }            

            var subjectToUpdate = _mapper.Map(subject, subjectEntity);

            await _repository.Subject.UpdateSubjectAsync(subjectToUpdate);
            
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

            await _repository.Subject.DeleteSubjectAsync(subject);            

            return NoContent();                        
        }
    }    
}
