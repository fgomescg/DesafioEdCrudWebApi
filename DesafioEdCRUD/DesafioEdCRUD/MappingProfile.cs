using AutoMapper;
using Entities.DTO;
using Entities.Models;

namespace DesafioEdCRUD
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<Author, AuthorDto>();
            CreateMap<Subject, SubjectDto>();            
            CreateMap<BookPut, Book>();            
            CreateMap<AuthorPut, Author>();            
            CreateMap<SubjectPut, Subject>();
        }
    }
}
