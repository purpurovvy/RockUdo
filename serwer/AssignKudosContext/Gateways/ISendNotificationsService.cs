namespace AssignKudosContext.Gateways
{
    public interface ISendNotificationsService
    {
        void BroadcastAssigningKudos(string whom, string forWhat, string whoFrom);
    }
}
