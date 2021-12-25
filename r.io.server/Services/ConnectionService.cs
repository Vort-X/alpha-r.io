﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace r.io.server.Services
{
    class ConnectionService
    {
        private readonly List<UserConnection> connectedUsers;

        public ConnectionService()
        {
            connectedUsers = new();
        }

        public IReadOnlyList<UserConnection> Connected => connectedUsers;
        public IReadOnlyList<UserConnection> Timeouted => connectedUsers.Where(x => x.Timeouted).ToList();

        public UserConnection Add(IPEndPoint endpoint)
        {
            UserConnection conn = new() { EndPoint = endpoint };
            connectedUsers.Add(conn);
            return conn;
        }

        public UserConnection Get(IPEndPoint endpoint)
        {
            return connectedUsers.Find(x => x.EndPoint.Equals(endpoint));
        }

        public void Remove(IPEndPoint endpoint)
        {
            connectedUsers.RemoveAll(x => x.EndPoint.Equals(endpoint));
        }

        public void RemoveTimeouted()
        {
            connectedUsers.RemoveAll(x => x.Timeouted);
        }
    }
}
