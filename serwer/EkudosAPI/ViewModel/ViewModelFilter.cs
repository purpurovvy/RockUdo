using AssignKudosContext.Repositories.Parameters;
using System.Collections.Generic;

namespace EkudosAPI.ViewModel
{
    public class ViewModelFilter : IFilter
    {
        string sort { get; set; }
        public string Sort {
            get
            {
                if (string.IsNullOrEmpty(sort))
                {
                    sort = "When";
                }

                return sort;
            }

            set { sort = value; }
        }
        public Order Order { get; set; }
        public List<(string name, string value)> FilterList { get; set; }
        public int HowMany { get; set; }
        public int From { get; set; }
    }
}
