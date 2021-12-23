using r.io.model.Services;
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
            GameFactoryImpl factory = new();
            GameLoopManagerImpl loop = new(factory);
            l.gameLoopManager = loop;
            l.requestProcessor = processor;
            processor.GameLoopManager = loop;
            processor.GameFactory = factory;
            return l;
        }
    }
}
