﻿using System;
using System.Diagnostics;
using r.io.model.Services.Abstract;
using r.io.server.Services;
using r.io.shared;
using r.io.shared.PackageProcessing;
using r.io.shared.Services;
using r.io.shared.UdpGraph;
using System.Net.Sockets;
using System.Linq;

namespace r.io.server.PackageProcessing
{
    class ConnectionRequestHandler : RequestHandler
    {
        private ConnectionService connectionService;
        private GameLoopManager gameLoopManager;

        public override char Type => 'c';

        public override GameServiceCollection GameServices 
        {
            set 
            {
                connectionService = value.Get<ConnectionService>();
                gameLoopManager = value.Get<GameLoopManager>();
            }
        }

        public override void Handle(UdpReceiveResult result, UdpPackage pack)
        {
            if (pack.Node is not UsernameNode) return;
            var username = (pack.Node as UsernameNode).Username;
            if (string.IsNullOrEmpty(username)) return;
            var actualName = username;
            for (int i = 0; i < 6; i++)
            {
                bool nameTaken = connectionService.Connected.Any(conn => conn.Player.name == actualName);
                if (nameTaken) actualName = $"{username}{i}";
                else if (nameTaken && i == 6) return;
                else break;
            }
            var conn = connectionService.Add(result.RemoteEndPoint);
            conn.Player = gameLoopManager.gameService.RegisterPlayer(actualName);
            conn.UpdateLastPing();
        }
    }
}
