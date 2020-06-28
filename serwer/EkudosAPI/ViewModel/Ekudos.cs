using System.Text.Encodings.Web;
using System.Web;

namespace EkudosAPI.ViewModel
{
    public class Ekudos
    {
        string _whom;
        string _whoFrom;
        string _description;
        string _giphy;

        public string Whom {
            get
            {
                return HttpUtility.HtmlEncode(_whom).Replace("&#243;", "ó");
            }
            set { _whom = value; }
        }

        public string WhoFrom
        {
            get
            {
                return HttpUtility.HtmlEncode(_whoFrom).Replace("&#243;", "ó");
            }
            set { _whoFrom = value; }
        }

        public string Description
        {
            get
            {
                return HttpUtility.HtmlEncode(_description).Replace("&#243;", "ó");
            }
            set { _description = value; }
        }
        public string Giphy
        {
            get
            {
                return HttpUtility.HtmlEncode(_giphy);
            }
            set { _giphy = value; }
        }
    }

  
}
