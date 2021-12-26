using r.io.model.Services;
using r.io.model.Services.Abstract;
using r.io.server.Constants;
using r.io.server.Services;
using r.io.shared;
using r.io.shared.Services;
using System.Net.Sockets;

namespace r.io.server
{
    public static class Configurer
    {
        public static Listener Create()
        {
            UdpClient client = new(Server.Port);
            Listener listener = new(client);
            GameServiceCollection gameServices = ConfigureGameServices();
            RequestProcessor processor = new();

            listener.gameLoopManager = gameServices.Get<GameLoopManager>();
            listener.requestProcessor = processor;
            processor.GameServices = gameServices;
            gameServices.Get<BroadcastService>().Server = client;

            return listener;
        }

        private static GameServiceCollection ConfigureGameServices()
        {
            GameServiceCollection gameServices = new();

            GameFactoryImpl factory = new();
            GameLoopManagerImpl loop = new(factory);
            BroadcastService broadcastService = new();
            ConnectionService connectionService = new(loop);
            Serializer<UdpPackage> serializer = new();

            gameServices.Add(factory);
            gameServices.Add(loop);
            gameServices.Add(broadcastService);
            gameServices.Add(connectionService);
            gameServices.Add(serializer);

            return gameServices;
        }
    }
}
