using AutoMapper;
using Contracts;
using DesafioEdCRUD.Services;
using Entities.DTO;
using Entities.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DesafioEdCRUD.Unit.Tests.Services
{
    public class SubjectServiceTest
    {
        private readonly SubjectService SubjectService;
        private readonly Mock<IRepositoryWrapper> repoWrapperMock = new Mock<IRepositoryWrapper>();
        private readonly Mock<ILoggerManager> loggerMock = new Mock<ILoggerManager>();
        private readonly List<Subject> _SubjectList;

        public SubjectServiceTest()
        {
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            _SubjectList = new List<Subject>()
            {
                new Subject(){ SubjectId = new Random().Next(1, int.MaxValue), Description = "Subject Test 1"},
                new Subject(){ SubjectId = new Random().Next(1, int.MaxValue), Description = "Subject Test 2"},
                new Subject(){ SubjectId = new Random().Next(1, int.MaxValue), Description = "Subject Test 3"},
                new Subject(){ SubjectId = new Random().Next(1, int.MaxValue), Description = "Subject Test 4"},
                new Subject(){ SubjectId = new Random().Next(1, int.MaxValue), Description = "Subject Test 5"}
            };

            SubjectService = new SubjectService(repoWrapperMock.Object, loggerMock.Object, mapper);
        }

        [Fact]
        public async Task GetSubjectsAsync_ShouldReturnPagedListSubjects()
        {
            //Arrange   
            var parameters = new SubjectParameters() { PageNumber = 0, PageSize = 10 };

            var pagedSubjectsMock = PagedList<Subject>.ToPagedList(_SubjectList.AsQueryable(), 0, 10);

            repoWrapperMock.Setup(p => p.Subject.GetSubjectsAsync(parameters))
                .ReturnsAsync(pagedSubjectsMock);

            //Act
            var pagedListSubject = await SubjectService.GetSubjectsAsync(parameters);

            //Assert
            Assert.Equal(pagedSubjectsMock.TotalCount, pagedListSubject.TotalCount);
            Assert.Equal(pagedSubjectsMock.TotalPages, pagedListSubject.TotalPages);
            Assert.Equal(pagedSubjectsMock.CurrentPage, pagedListSubject.CurrentPage);
            Assert.Equal(pagedSubjectsMock.PageSize, pagedListSubject.PageSize);
            Assert.Equal(pagedSubjectsMock.Count, pagedListSubject.Count);
        }

        [Fact]
        public async Task CreateSubjectAsync_ShouldReturnSubjectDto_WhenSubjectCreated()
        {
            //Arrange                        
            var SubjectDescription = "Subject Description created";
            var Subject = new Subject() { SubjectId = new Random().Next(1, int.MaxValue), Description = SubjectDescription };

            repoWrapperMock.Setup(p => p.Subject.CreateSubjectAsync(Subject));

            //Act
            var SubjectDto = await SubjectService.CreateSubjectAsync(Subject);

            //Assert
            Assert.Equal(SubjectDto.Description, SubjectDescription);
        }

        [Fact]
        public async Task GetSubjectByIdAsync_ShouldReturnSubject_WhenSubjectExists()
        {
            //Arrange            
            var SubjectId = new Random().Next(1, int.MaxValue);
            var SubjectDescription = "SubjectDescription Test";
            var Subject = new Subject() { SubjectId = SubjectId, Description = SubjectDescription };

            repoWrapperMock.Setup(p => p.Subject.GetSubjectByIdAsync(SubjectId))
                .ReturnsAsync(Subject);

            //Act
            var SubjectDto = await SubjectService.GetSubjectByIdAsync(SubjectId);

            //Assert
            Assert.Equal(SubjectId, SubjectDto.SubjectId);
            Assert.Equal(SubjectDescription, SubjectDto.Description);
        }

        [Fact]
        public async Task GetSubjectByIdAsync_ShouldReturnNothing_WhenSubjectNotExists()
        {
            //Arrange                      
            repoWrapperMock.Setup(p => p.Subject.GetSubjectByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var SubjectId = new Random().Next(1, int.MaxValue);
            var Subject = await SubjectService.GetSubjectByIdAsync(SubjectId);

            //Assert
            Assert.Null(Subject);
        }

        [Fact]
        public async Task UpdateSubjectAsync_ShouldReturnTrue_WhenSubjectUpdated()
        {
            //Arrange                        
            var SubjectId = new Random().Next(1, int.MaxValue);
            var SubjectDescription = "Subject Description created";
            var Subject = new Subject() { SubjectId = new Random().Next(1, int.MaxValue), Description = SubjectDescription };


            repoWrapperMock.Setup(p => p.Subject.GetSubjectByIdAsync(SubjectId))
                .ReturnsAsync(Subject);

            var SubjectForUpdated = new SubjectPut() { Description = "Subject Description Update" };
            repoWrapperMock.Setup(p => p.Subject.UpdateSubjectAsync(Subject));

            //Act
            var isSubjectUpdated = await SubjectService.UpdateSubjectAsync(SubjectId, SubjectForUpdated);

            //Assert
            Assert.True(isSubjectUpdated);
        }

        [Fact]
        public async Task UpdateSubjectAsync_ShouldReturnFalse_WhenSubjectNotFound()
        {
            //Arrange  
            var SubjectId = new Random().Next(1, int.MaxValue);
            var SubjectForUpdated = new SubjectPut() { Description = "Subject Description Update" };
            repoWrapperMock.Setup(p => p.Subject.GetSubjectByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var isSubjectUpdated = await SubjectService.UpdateSubjectAsync(SubjectId, SubjectForUpdated);

            //Assert
            Assert.False(isSubjectUpdated);
        }

        [Fact]
        public async Task DeleteSubjectAsync_ShouldReturnTrue_WhenSubjectDeleted()
        {
            //Arrange                        
            var SubjectId = new Random().Next(1, int.MaxValue);
            var SubjectDescription = "Subject test title";
            var Subject = new Subject() { SubjectId = SubjectId, Description = SubjectDescription };

            repoWrapperMock.Setup(p => p.Subject.GetSubjectByIdAsync(SubjectId))
                .ReturnsAsync(Subject);

            repoWrapperMock.Setup(p => p.Subject.DeleteSubjectAsync(Subject));

            //Act
            var isSubjectDeleted = await SubjectService.DeleteSubjectAsync(SubjectId);

            //Assert
            Assert.True(isSubjectDeleted);
        }

        [Fact]
        public async Task DeleteSubjectAsync_ShouldReturnFalse_WhenSubjectNotFound()
        {
            //Arrange  
            var SubjectId = new Random().Next(1, int.MaxValue);

            repoWrapperMock.Setup(p => p.Subject.GetSubjectByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var isBookDeleted = await SubjectService.DeleteSubjectAsync(SubjectId);

            //Assert
            Assert.False(isBookDeleted);
        }
    }
}
