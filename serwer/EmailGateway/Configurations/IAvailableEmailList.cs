using System.Collections.Generic;

namespace EmailGateway.Configurations
{
    public interface IAvailableEmailList
    {
        IList<string> EmailList { get; }
    }
}
