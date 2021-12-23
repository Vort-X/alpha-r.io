using r.io.model.Services.Abstract;
using r.io.server.Constants;
using r.io.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace r.io.server
{
    class RequestProcessor
    {
        private readonly List<UserConnection> connectedUsers;
        private readonly RequestQueue queue;
        private GameLoopManager gameLoopManager;

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
                connectedUsers.RemoveAll(u => u.Timeout);
                Task[] ta = connectedUsers.Select(u => Task.Run(() =>
                {
                    //TODO: refactor this as UdpPackageCreator class
                    //Types of UdpPackage responses:
                    //-[tick] nearby GameAreas
                    //-[end] round end and player top
                    //TODO: find other types
                    UdpPackage pack = new();
                    pack.Type = "tick";
                    pack["areas"] = PlayerService.getGameAreasAround(u.Player.x, u.Player.y);
                    //TODO: serialization
                    Send?.Invoke(u.EndPoint, Array.Empty<byte>());
                })).ToArray();
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
            new Timer(_ => Broadcast(), null, 0, 1000 / Server.TicksPerSecond);
        }

        private void StartProcess()
        {
            //???????????????
            while (true)
            {
                //TODO: Dequeue and process
                //Types of UdpPackage requests:
                //-[connect] to game
                //-[move] by vector or angle
                //-[disconnect] from game
                //TODO: find other types
            }
        }
    }
}
