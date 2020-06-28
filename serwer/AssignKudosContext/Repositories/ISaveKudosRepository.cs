using AssignKudosContext.Entities;

namespace AssignKudosContext.Repositories
{
    public interface ISaveKudosRepository
    {
        bool SaveKudos(Kudos kudos);
    }
}
