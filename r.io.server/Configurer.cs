using r.io.model.Services;
using r.io.model.Services.Abstract;
using r.io.server.PackageProcessing;
using r.io.server.Services;
using r.io.shared;
using r.io.shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r.io.server
{
    public static class Configurer
    {
        public static Listener Create()
        {
            Listener l = new();
            GameServiceCollection gameServices = ConfigureGameServices();
            RequestProcessor processor = new();

            l.gameLoopManager = gameServices.Get<GameLoopManager>();
            l.requestProcessor = processor;
            processor.GameServices = gameServices;

            return l;
        }

        private static GameServiceCollection ConfigureGameServices()
        {
            GameServiceCollection gameServices = new();

            GameFactoryImpl factory = new();
            GameLoopManagerImpl loop = new(factory);
            ConnectionService connectionService = new();
            Serializer<UdpPackage> serializer = new();

            gameServices.Add(factory);
            gameServices.Add(loop);
            gameServices.Add(connectionService);
            gameServices.Add(serializer);

            return gameServices;
        }
    }
}
