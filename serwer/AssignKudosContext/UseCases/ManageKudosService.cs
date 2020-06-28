using AssignKudosContext.Entities;
using AssignKudosContext.Gateways;
using AssignKudosContext.Repositories;

namespace AssignKudosContext.UseCases
{
    public class ManageKudosService : IManageKudosService
    {
        ISaveKudosRepository _kudosRepository;
        ISendNotificationsService _sendNotificationsService;

        public ManageKudosService(
            ISaveKudosRepository kudosRepository,
            ISendNotificationsService sendNotificationsService
        )
        {
            _kudosRepository = kudosRepository;
            _sendNotificationsService = sendNotificationsService;
        }

        public bool AddKudos(Kudos kudos, Donator donator, Recipient recipient)
        {
            kudos.Donator = donator;
            kudos.Recipient = recipient;

            _kudosRepository.SaveKudos(kudos);
            _sendNotificationsService.BroadcastAssigningKudos(recipient.Name, kudos.ForWhat, donator.Name);

            return true;
        }
    }
}
