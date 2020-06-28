using System;

namespace DatabaseContext.Models
{
    public class EkudosModel
    {
        public Guid Id { get; set; }
        public string Who { get; set; }
        public string Donator { get; set; }
        public string ForWhat { get; set; }
        public string Giphy { get; set; }
        public DateTime When { get; set; }

    }
}
