using System;

namespace EkudosAPI.ViewModel
{
    public class EkudosSimple
    {
        public Guid Id { get; set; }
        public string Who { get; set; }
        public string WhoFrom { get; set; }
        public string Title { get; set; }
        public string Avatar { get; set; }
        public DateTime When { get; set; }
        public string Giphy { get; set; }
    }
}
