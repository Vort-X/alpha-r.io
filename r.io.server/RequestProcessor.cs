using r.io.model.GameEntities;
using r.io.model.Services.Abstract;
using r.io.server.Constants;
using r.io.server.PackageProcessing;
using r.io.server.Services;
using r.io.shared;
using r.io.shared.PackageProcessing;
using r.io.shared.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace r.io.server
{
    class RequestProcessor : IDisposable
    {
        private readonly RequestQueue queue;
        private readonly List<IPEndPoint> tickIPs;

        private Timer broadcastTimer;
        private GameServiceCollection gameServices;
        private List<RequestHandler> requestHandlers;
        private List<ResponseCreator> responseCreators;

        public RequestProcessor()
        {
            queue = new();
            tickIPs = new();
        }

        public GameServiceCollection GameServices
        {
            get => gameServices;
            set
            {
                gameServices = value;
                requestHandlers = PackageProcessorActivator.GetRequestHandlers(gameServices);
                responseCreators = PackageProcessorActivator.GetResponseCreators(gameServices);
            }
        }

        public void AddToQueue(UdpReceiveResult request)
        {
            queue.Enqueue(request);
        }

        private void Broadcast()
        {
            lock (queue)
            {
                if (gameServices.Get<ConnectionService>().Connected.Count == 0) return;
                var responses = responseCreators.OrderBy(rc => rc.Priority)
                    .SelectMany(rc => rc.Broadcast())
                    .ToArray();
                Task.WaitAll(responses);
                tickIPs.Clear();
            }
        }

        private void OnRoundEnded()
        {
            //TODO: 
        }

        public void Start()
        {
            //Processing requests in queue
            new Thread(StartProcess).Start();
            //Sending to clients data every tick
            broadcastTimer = new Timer(_ => Broadcast(), null, 0, 1000 / Server.TicksPerSecond);
        }

        private void StartProcess()
        {
            //???????????????
            while (true)
            {
                //Waiting for elements in queue
                while (queue.Count == 0) ;
                //considering queue.Count > 0
                lock (queue)
                {
                    var s = GameServices.Get<Serializer<UdpPackage>>();
                    var request = queue.Dequeue();
                    //skip handling if client has already sent pack 
                    if (tickIPs.Any(ip => ip == request.RemoteEndPoint)) continue;
                    var pack = s.Deserialize(request.Buffer);
                    requestHandlers.Find(rc => rc.Type == pack.Type)?.Handle(request, pack);
                    tickIPs.Add(request.RemoteEndPoint);
                }
            }
        }

        public void Dispose()
        {
            broadcastTimer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
