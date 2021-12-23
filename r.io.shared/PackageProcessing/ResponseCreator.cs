namespace r.io.shared.PackageProcessing
{
    public abstract class ResponseCreator
    {
        public abstract char Type { get; }

        public abstract UdpPackage Create();
    }
}
