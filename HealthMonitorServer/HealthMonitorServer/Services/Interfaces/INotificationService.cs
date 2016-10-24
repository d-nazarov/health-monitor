using HealthMonitorServer.Models;
using System;

namespace HealthMonitorServer.Services.Interfaces
{
    public interface INotificationService<T>
    {
        /// <summary>
        /// Sets notification service check interval
        /// </summary>
        /// <param name="interval">New interval in milliseconds</param>
        void SetInterval(int interval);

        /// <summary>
        /// New data event from notification service
        /// </summary>
        event EventHandler<MonitoringServiceMessage<T>> NewData;
    }
}
