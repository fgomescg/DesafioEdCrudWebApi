using Contracts;
using Entities;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IBookRepository _book;
        private IAuthorRepository _author;
        private ISubjectRepository _subject;

        public IBookRepository Book
        {
            get
            {
                if (_book == null)
                {
                    _book = new BookRepository(_repoContext);
                }

                return _book;
            }
        }

        public IAuthorRepository Author
        {
            get
            {
                if (_author == null)
                {
                    _author = new AuthorRepository(_repoContext);
                }

                return _author;
            }
        }

        public ISubjectRepository Subject
        {
            get
            {
                if (_subject == null)
                {
                    _subject = new SubjectRepository(_repoContext);
                }

                return _subject;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }

    }
}