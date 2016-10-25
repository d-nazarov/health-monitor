using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMonitorServer.Models
{
    public class ProcessInformation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public long MemoryUsage { get; set; }

        public int TotalProcessorTime { get; set; }
    }
}
