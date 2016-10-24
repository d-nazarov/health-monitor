using HealthMonitorServer.Enums;

namespace HealthMonitorServer.Models
{
    public class MonitoringServiceMessage<T>
    {
        public MessageTypes MessageType { get; set; }

        public T Data { get; set; }
    }
}
