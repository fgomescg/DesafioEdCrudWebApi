using Entities.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using Xunit.Priority;
using Moq;
using Contracts;
using DesafioEdCRUD.Controllers;
using AutoMapper;

namespace DesafioEdCRUD.Unit.Tests.Controller
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class BookControllerUnitTest
    {
       
        //private Mock<IBookRepository> bookControllerMock;
        private Mock<IRepositoryWrapper> repoWrapperMock;
        private Mock<ILoggerManager> logManagerMock;
        private Mock<IMapper> mapperMock;
       

        public BookControllerUnitTest()
        {
            repoWrapperMock = new Mock<IRepositoryWrapper>();
            logManagerMock = new Mock<ILoggerManager>();
            mapperMock = new Mock<IMapper>();            
        }

        /*
                [Fact, Priority(1)]
                public async Task GetAllBooks_ReturnsOkResponse()
                {

                }

                [Fact, Priority(2)]
                public async Task GetBookById_Should_ReturnsOkResponse()
                {

                }

                [Fact, Priority(3)]
                public async Task UpdateBook_Should_ReturnsNoContentResponse()
                {

                }

                [Fact, Priority(4)]
                public async Task DeleteBook_Should_ReturnsNoContentResponse()
                {

                }*/
    }
}
