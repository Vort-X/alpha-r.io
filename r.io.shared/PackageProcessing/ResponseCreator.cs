using r.io.shared.Services;
using System.Threading.Tasks;

namespace r.io.shared.PackageProcessing
{
    public abstract class ResponseCreator
    {
        public abstract GameServiceCollection GameServices { set; }
        public abstract int Priority { get; }
        public abstract char Type { get; }

        public abstract Task[] Broadcast();
    }
}
