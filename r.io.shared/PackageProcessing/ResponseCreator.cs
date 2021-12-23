using r.io.shared.Services;

namespace r.io.shared.PackageProcessing
{
    public abstract class ResponseCreator
    {
        protected GameServiceCollection gameServices;

        public GameServiceCollection GameServices { set => gameServices = value; }
        public abstract char Type { get; }

        protected abstract void Configure(UdpPackage pack, params object[] @params);
        
        public UdpPackage Create(params object[] @params)
        {
            UdpPackage pack = new();
            Configure(pack, @params);
            pack.Type = Type;
            return pack;
        }
    }
}
