using EmailGateway.Configurations;

namespace EkudosAPI.Configurations
{
    public class SmtpConfiguration : ISmtpConfiguration
    {
        public string Server { get; set; }
        public string User { get; set; }
        public string UserDisplayName { get; set; }
        public string Password { get; set; }
        public string[] DigitalGroup { get; set; }
        public int Port { get; set; }
    }
}
