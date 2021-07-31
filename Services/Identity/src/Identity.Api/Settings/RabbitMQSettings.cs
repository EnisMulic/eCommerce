namespace Identity.Api.Settings
{
    public class RabbitMQSettings
    {
        public const string RabbitMQ = "RabbitMQ";

        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RetryCount { get; set; }
    }
}
