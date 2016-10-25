using HealthMonitorServer.Enums;
using HealthMonitorServer.Models;
using HealthMonitorServer.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Threading;

namespace HealthMonitorServer.Services
{
    public class CPUMonitoringService : INotificationService<float>
    {
        #region Fields

        private int interval;
        private int highloadTreshold = 80;
        private readonly PerformanceCounter cpuUsageCounter;
        private Timer timer;

        public event EventHandler<MonitoringServiceMessage<float>> NewData;

        #endregion

        public CPUMonitoringService(int interval = 2000, int highloadTreshold = 80)
        {
            this.interval = interval;
            this.highloadTreshold = highloadTreshold;
            cpuUsageCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            timer = new Timer(e => CheckCPUUsage(), null, interval, Timeout.Infinite);
        }

        public void SetInterval(int interval)
        {
            this.interval = interval;
        }

        private void CheckCPUUsage()
        {
            var cpuUsage = cpuUsageCounter.NextValue();

            if (cpuUsage > highloadTreshold)
            {
                OnHighLoad(new MonitoringServiceMessage<float> { MessageType = MessageTypes.CpuHighload, Data = cpuUsage });
            }

            timer.Change(interval, Timeout.Infinite);
        }

        virtual protected void OnHighLoad(MonitoringServiceMessage<float> message)
        {
            var handler = Volatile.Read(ref NewData);

            if (handler != null)
                handler(this, message);
        }
    }
}
