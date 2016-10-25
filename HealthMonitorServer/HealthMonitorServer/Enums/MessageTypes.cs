using System.ComponentModel;

namespace HealthMonitorServer.Enums
{
    public enum MessageTypes
    {
        [Description("Running processes information")]
        ProcessesInformation,

        [Description("Warning highload")]
        CpuHighload,

        [Description("Warning low memory")]
        LowMemory
    }
}
