using AssignKudosContext.Entities;
using AssignKudosContext.Repositories.Parameters;
using System.Linq;

namespace AssignKudosContext.Repositories
{
    public interface IGetKudosRepository
    {
        IQueryable<Kudos> SimpleList(IFilter filter);
    }
}
