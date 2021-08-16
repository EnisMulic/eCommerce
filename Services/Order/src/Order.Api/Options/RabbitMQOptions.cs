namespace Order.Api.Options
{
    public class RabbitMQOptions
    {
        public const string RabbitMQ = "RabbitMQ";

        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RetryCount { get; set; }
    }
}
