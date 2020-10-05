using AutoMapper;
using Entities.DTO;
using Entities.Models;
using System.Linq;

namespace DesafioEdCRUD
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AllowNullCollections = false;

            CreateMap<Book, BookDto>()
                .ForMember(p => p.Authors,
                            opt => opt.MapFrom(x =>
                                x.BookAuthors.Select(y => y.Author)))
                .ForMember(p => p.Subjects,
                            opt => opt.MapFrom(x =>
                                x.BookSubjects.Select(y => y.Subject)));                
            CreateMap<Author, AuthorDto>();
            CreateMap<Subject, SubjectDto>();            
            CreateMap<BookPut, Book>();            
            CreateMap<AuthorPut, Author>();            
            CreateMap<SubjectPut, Subject>();
        }
    }
}
