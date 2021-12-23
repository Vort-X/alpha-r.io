using r.io.model.Services;
using r.io.model.Services.Abstract;
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
            RequestProcessor processor = new();
            GameServiceCollection gameServices = ConfigureGameServices();
            l.gameLoopManager = gameServices.Get<GameLoopManager>();
            l.requestProcessor = processor;
            processor.GameLoopManager = gameServices.Get<GameLoopManager>();
            return l;
        }

        private static GameServiceCollection ConfigureGameServices()
        {
            GameServiceCollection gameServices = new();
            GameFactoryImpl factory = new();
            GameLoopManagerImpl loop = new(factory);
            gameServices.Add(loop);
            gameServices.Add(factory);
            gameServices.Add(loop.gameService);
            gameServices.Add(loop.playerService);
            return gameServices;
        }
    }
}
