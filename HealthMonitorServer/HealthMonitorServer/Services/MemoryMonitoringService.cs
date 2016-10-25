using HealthMonitorServer.Enums;
using HealthMonitorServer.Models;
using HealthMonitorServer.Services.Interfaces;
using Microsoft.VisualBasic.Devices;
using System;
using System.Diagnostics;
using System.Threading;

namespace HealthMonitorServer.Services
{
    public class MemoryMonitoringService : INotificationService<float>
    {
        #region Fields

        private int interval;
        private int memoryPercentTreshold;
        private readonly double highloadTreshold;
        private readonly PerformanceCounter memoryCounter;
        private Timer timer;

        public event EventHandler<MonitoringServiceMessage<float>> NewData;

        #endregion

        public MemoryMonitoringService(int interval = 2000, int memoryPercentTreshold = 10)
        {
            this.interval = interval;
            this.memoryPercentTreshold = memoryPercentTreshold;

            memoryCounter = new PerformanceCounter("Memory", "Available MBytes");
            var computerInfo = new ComputerInfo();
            highloadTreshold = computerInfo.TotalPhysicalMemory / 1024 / 1024 * (ulong)memoryPercentTreshold / 100;

            timer = new Timer(e => CheckAvailableMemory(), null, interval, Timeout.Infinite);
        }

        public void SetInterval(int interval)
        {
            this.interval = interval;
        }

        private void CheckAvailableMemory()
        {
            var availableMemory = memoryCounter.NextValue();

            if (availableMemory < highloadTreshold)
            {
                OnHighLoad(new MonitoringServiceMessage<float> { MessageType = MessageTypes.LowMemory, Data = availableMemory });
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
