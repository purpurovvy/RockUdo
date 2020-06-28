using AssignKudosContext.Dto;
using AssignKudosContext.Repositories.Parameters;
using System.Collections.Generic;

namespace AssignKudosContext.UseCases
{
    public interface IReaderKudosService
    {
        IEnumerable<KudosSimpleData> GetKudosSimpleList(IFilter filter);
    }
}
