using Fleck;
using HealthMonitorServer.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthMonitorServer
{
    public class HealthMonitorServer
    {
        private List<IWebSocketConnection> connections = new List<IWebSocketConnection>();
        private object sync = new object();

        private readonly WebSocketServer server;

        public HealthMonitorServer(string host, string port)
        {
            server = new WebSocketServer(string.Format("ws://{0}:{1}", host, port));

            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    lock (sync)
                    {
                        connections.Add(socket);
                    }
                };
                socket.OnClose = () =>
                {
                    lock (sync)
                    {
                        connections.Remove(socket);
                    }
                };
            });

            var processService = new DataProcessingService();
            processService.NewBroadcastMessage += ProcessService_NewBroadcastMessage;
        }

        private void ProcessService_NewBroadcastMessage(object sender, string e)
        {
            lock (sync)
            {
                Parallel.ForEach(connections, socket =>
                {
                    socket.Send(e);
                });
            }
        }
    }
}
