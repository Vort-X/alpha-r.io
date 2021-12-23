namespace r.io.shared.PackageProcessing
{
    public abstract class RequestHandler
    {
        public abstract char Type { get; }

        public abstract void Handle(UdpPackage pack);
    }
}
