using System;

namespace AssignKudosContext.Dto
{
    public class KudosSimpleData
    {
        public Guid Id { get; set; }
        public string Whom { get; set; }
        public string WhoFrom { get; set; }
        public string ForWhat { get; set; }
        public DateTime When { get; set; }
        public string Giphy { get; set; }
    }
}
