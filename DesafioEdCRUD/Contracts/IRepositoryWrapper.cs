using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IBookRepository Book { get; }
        IAuthorRepository Author { get; }
        ISubjectRepository Subject { get; }        
    }
}
