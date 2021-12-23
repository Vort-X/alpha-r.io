﻿using r.io.shared.Services;

namespace r.io.shared.PackageProcessing
{
    public abstract class RequestHandler
    {
        protected GameServiceCollection gameServices;

        public GameServiceCollection GameServices { set => gameServices = value; }
        public abstract char Type { get; }

        public abstract void Handle(UdpPackage pack);
    }
}
