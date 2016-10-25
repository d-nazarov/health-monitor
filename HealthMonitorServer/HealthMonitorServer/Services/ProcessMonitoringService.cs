using HealthMonitorServer.Enums;
using HealthMonitorServer.Models;
using HealthMonitorServer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace HealthMonitorServer.Services
{
    public class ProcessMonitoringService : INotificationService<IEnumerable<ProcessInformation>>
    {
        #region Fields

        private int interval;
        private int count; 
        private Timer timer;

        public event EventHandler<MonitoringServiceMessage<IEnumerable<ProcessInformation>>> NewData;

        #endregion

        public ProcessMonitoringService(int interval = 2000, int count = 10)
        {
            this.interval = interval;
            this.count = count;
            timer = new Timer(e => GetProcesses(), null, interval, Timeout.Infinite);
        }

        public void SetInterval(int interval)
        {
            this.interval = interval;
        }

        private void GetProcesses()
        {
            var processes = Process.GetProcesses()
                .OrderByDescending(p => p.WorkingSet64)
                .Take(count)
                .Select(p => new ProcessInformation
                {
                    Id = p.Id,
                    Name = p.ProcessName,
                    MemoryUsage = p.WorkingSet64,
                    TotalProcessorTime = p.TotalProcessorTime.Milliseconds
                });

            OnNewProcessesList(new MonitoringServiceMessage<IEnumerable<ProcessInformation>> { MessageType = MessageTypes.ProcessesInformation, Data = processes });

            timer.Change(interval, Timeout.Infinite);
        }

        virtual protected void OnNewProcessesList(MonitoringServiceMessage<IEnumerable<ProcessInformation>> message)
        {
            var handler = Volatile.Read(ref NewData);

            if (handler != null)
                handler(this, message);
        }
    }
}
