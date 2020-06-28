using System;

namespace AssignKudosContext.Entities
{
    public class Kudos
    {
        public Guid Id { get; set; }
        public string ForWhat { get; set; }
        public DateTime When { get; set; }
        public Donator Donator { get; set; }
        public Recipient Recipient { get; set; }
        public string Giphy { get; set; }
    }
}
