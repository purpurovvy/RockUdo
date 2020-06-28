using System.Collections.Generic;

namespace AssignKudosContext.Repositories.Parameters
{
    public interface IFilter
    {
        int HowMany { get; set; }
        int From { get; set; }
        string Sort { get; set; }
        Order Order { get; set; }
        List<(string name, string value)> FilterList { get; set; }
    }
}
