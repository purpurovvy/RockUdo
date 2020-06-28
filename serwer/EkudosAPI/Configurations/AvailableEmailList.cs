using EmailGateway.Configurations;
using System.Collections.Generic;

namespace EkudosAPI.Configurations
{
    public class AvailableEmailList : IAvailableEmailList
    {
        string[] _list;
        public AvailableEmailList(string[] list)
        {
            _list = list;
        }
        
        public IList<string> EmailList { get => _list; }
    }
}
