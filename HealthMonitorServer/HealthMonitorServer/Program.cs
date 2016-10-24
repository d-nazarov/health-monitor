using System;
using System.Configuration;

namespace HealthMonitorServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new HealthMonitorServer(ConfigurationSettings.AppSettings["Host"], ConfigurationSettings.AppSettings["Port"]);

            Console.ReadLine();
        }
    }
}
