using r.io.model.Services.Abstract;
using r.io.server.Constants;
using r.io.shared;
using r.io.shared.PackageProcessing;
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
        private readonly List<UserConnection> connectedUsers;
        private readonly RequestQueue queue;
        private readonly Serializer<UdpPackage> serializer;
        private Timer broadcastTimer;
        private GameLoopManager gameLoopManager;

        public RequestProcessor()
        {
            connectedUsers = new List<UserConnection>();
            queue = new RequestQueue();
            serializer = new Serializer<UdpPackage>();
        }

        public List<RequestHandler> RequestHandlers { get; set; }
        public List<ResponseCreator> ResponseCreators { get; set; }

        //TODO: remove this garbage
        public GameLoopManager GameLoopManager
        {
            get => gameLoopManager;
            internal set
            {
                value.RoundEnded += OnRoundEnded;
                gameLoopManager = value;
            }
        }

        public event Action<IPEndPoint, byte[]> Send;

        public void AddToQueue(UdpReceiveResult request)
        {
            queue.Enqueue(request);
        }

        private void Broadcast()
        {
            lock (queue)
            {
                var timeoutedUsers = connectedUsers.Where(x => x.Timeouted).ToList();
                timeoutedUsers.ForEach(x => connectedUsers.Remove(x));
                Task[] tc = connectedUsers.Select(u => Task.Run(() =>
                {
                    //TODO: refactor this as response creators
                    //Types of UdpPackage responses:
                    //-[n]earby GameAreas
                    //-round [e]nd and player top
                    //-[t]imeout
                    //TODO: find other types
                    UdpPackage pack = ResponseCreators.Find(rc => rc.Type == 'n')?.Create(u.Player) ?? new();

                    byte[] data = serializer.Serialize(pack);
                    Send?.Invoke(u.EndPoint, data);
                })).ToArray();
                Task[] tt = timeoutedUsers.Select(u => Task.Run(() =>
                {
                    UdpPackage pack = new();
                    pack.Type = 't';

                    byte[] data = serializer.Serialize(pack);
                    Send?.Invoke(u.EndPoint, data);
                })).ToArray();
                Task.WaitAll(tt);
                Task.WaitAll(tc);
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
                while (queue.Count == 0);
                //considering queue.Count > 0
                lock (queue)
                {
                    var request = queue.Dequeue();
                    var pack = serializer.Deserialize(request.Buffer);
                    //Types of UdpPackage requests:
                    //-[c]onnect to game
                    //-[m]ove by vector (or angle)
                    //-[d]isconnect from game
                    //-[e]mpty pack (to avoid timeout)
                    //TODO: find other types
                    RequestHandlers.Find(rc => rc.Type == pack.Type)?.Handle(pack);
                    //TODO: fix ifs
                    if (pack.Type == 'c') connectedUsers.Add(new() 
                    { 
                        EndPoint = request.RemoteEndPoint, 
                        Player = gameLoopManager.gameService.RegisterPlayer("mihailo") 
                    });
                    if (pack.Type == 'd') connectedUsers.RemoveAll(c => c.EndPoint == request.RemoteEndPoint);
                    connectedUsers.Find(u => u.EndPoint.Equals(request.RemoteEndPoint))?.UpdateLastPing();
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
