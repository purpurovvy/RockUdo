using System.Collections.Generic;
using System.Linq;
using AssignKudosContext.Dto;
using AssignKudosContext.Repositories;
using AssignKudosContext.Repositories.Parameters;

namespace AssignKudosContext.UseCases
{
    public class ReaderKudosService : IReaderKudosService
    {
        IGetKudosRepository _readerKudosRepository;
        public ReaderKudosService(IGetKudosRepository readerKudosRepository)
        {
            _readerKudosRepository = readerKudosRepository;
        }

        public IEnumerable<KudosSimpleData> GetKudosSimpleList(IFilter filter)
        {
            return _readerKudosRepository
                .SimpleList(filter)
                .Select(kudos => new KudosSimpleData
                {
                    Id = kudos.Id,
                    Whom = kudos.Recipient.Name,
                    WhoFrom = kudos.Donator.Name,
                    ForWhat = kudos.ForWhat,
                    When = kudos.When,
                    Giphy = kudos.Giphy
                });
        }
    }
}
