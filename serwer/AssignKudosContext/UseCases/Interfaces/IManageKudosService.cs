using AssignKudosContext.Entities;

namespace AssignKudosContext.UseCases
{
    public interface IManageKudosService
    {
        bool AddKudos(Kudos kudos, Donator donator, Recipient recipient);
    }
}
