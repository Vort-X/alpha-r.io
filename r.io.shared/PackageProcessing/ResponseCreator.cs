using r.io.shared.Services;
using System.Threading.Tasks;

namespace r.io.shared.PackageProcessing
{
    public abstract class ResponseCreator
    {
        protected GameServiceCollection gameServices;

        public GameServiceCollection GameServices { set => gameServices = value; }
        public abstract int Priority { get; }
        public abstract char Type { get; }

        public abstract Task[] Broadcast();
    }
}
