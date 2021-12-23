using r.io.model.Services.Abstract;
using r.io.server.Constants;
using r.io.shared;
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

        public GameFactory GameFactory { get; internal set; }
        public GameLoopManager GameLoopManager
        {
            get => gameLoopManager;
            internal set
            {
                value.RoundEnded += OnRoundEnded;
                gameLoopManager = value;
            }
        }
        public GameService GameService => GameLoopManager.gameService;
        public PlayerService PlayerService => GameLoopManager.playerService;

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
                Task[] ta = connectedUsers.Select(u => Task.Run(() =>
                {
                    //TODO: refactor this as UdpPackageCreator class or smth
                    //Types of UdpPackage responses:
                    //-[n]earby GameAreas
                    //-round [e]nd and player top
                    //-[t]imeout
                    //TODO: find other types
                    UdpPackage pack = new();
                    pack.Type = 'n';
                    //pack["areas"] = PlayerService.getGameAreasAround(u.Player.x, u.Player.y);

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
                Task.WaitAll(ta);
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
                    //TODO: refactor this
                    var request = queue.Dequeue();
                    var pack = serializer.Deserialize(request.Buffer);

                    if (pack.Type == 'c')
                    {
                        UserConnection conn = new()
                        { 
                            EndPoint = request.RemoteEndPoint,
                        };
                        connectedUsers.Add(conn);
                    }

                    connectedUsers.Find(u => u.EndPoint.Equals(request.RemoteEndPoint))?.UpdateLastPing();
                    //Types of UdpPackage requests:
                    //-[c]onnect to game
                    //-[m]ove by vector (or angle)
                    //-[d]isconnect from game
                    //-[e]mpty pack (to avoid timeout)
                    //TODO: find other types
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
