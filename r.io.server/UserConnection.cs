using r.io.model.GameEntities;
using r.io.server.Constants;
using System;
using System.Net;

namespace r.io.server
{
    class UserConnection
    {
        private DateTime lastPing = DateTime.Now;

        public IPEndPoint EndPoint { get; internal set; }
        public PlayerCircle Player { get; internal set; }
        public bool Timeouted => (DateTime.Now - lastPing).TotalMilliseconds > Server.SessionTimeout;

        public void UpdateLastPing()
        {
            lastPing = DateTime.Now;
        }
    }
}
