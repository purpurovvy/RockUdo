namespace EmailGateway.Configurations
{
    public interface ISmtpConfiguration
    {
        string Server { get; set; }
        string User { get; set; }
        string UserDisplayName { get; set; }
        string Password { get; set; }
        string[] DigitalGroup { get; set; }
        int Port { get; set; }
    }
}
