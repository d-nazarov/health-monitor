using HealthMonitorServer.Helpers;
using HealthMonitorServer.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace HealthMonitorServer.Services
{
    public class DataProcessingService
    {
        public event EventHandler<string> NewBroadcastMessage;

        public DataProcessingService()
        {
            var processService = new ProcessMonitoringService();
            processService.NewData += ProcessService_NewData;

            var cpuService = new CPUMonitoringService();
            cpuService.NewData += CpuService_NewData;

            var memoryService = new MemoryMonitoringService();
            memoryService.NewData += MemoryService_NewData;
        }

        private void MemoryService_NewData(object sender, MonitoringServiceMessage<float> e)
        {
            OnNewBroadcastMessage(JsonHelper.GetJsonMessage(e.MessageType, e.Data));
        }

        private void CpuService_NewData(object sender, MonitoringServiceMessage<float> e)
        {
            OnNewBroadcastMessage(JsonHelper.GetJsonMessage(e.MessageType, e.Data));
        }

        private void ProcessService_NewData(object sender, MonitoringServiceMessage<IEnumerable<ProcessInformation>> e)
        {
            OnNewBroadcastMessage(JsonHelper.GetJsonMessage(e.MessageType, e.Data));
        }

        virtual protected void OnNewBroadcastMessage(string message)
        {
            var handler = Volatile.Read(ref NewBroadcastMessage);

            if (handler != null)
                handler(this, message);
        }
    }
}
