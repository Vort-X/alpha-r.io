using r.io.model.GameEntities;
using r.io.server.Constants;
using System;
using System.Net;

namespace r.io.server
{
    class UserConnection
    {
        private DateTime lastPing;

        public IPEndPoint EndPoint { get; internal set; }
        public PlayerCircle Player { get; internal set; }
        public bool Timeout => (DateTime.Now - lastPing).TotalMilliseconds > Server.SessionTimeout;

        public void UpdateLastPing()
        {
            lastPing = DateTime.Now;
        }
    }
}
