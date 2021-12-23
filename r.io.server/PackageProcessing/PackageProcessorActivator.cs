using r.io.shared.PackageProcessing;
using r.io.shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace r.io.server.PackageProcessing
{
    internal static class PackageProcessorActivator
    {
        public static List<RequestHandler> GetRequestHandlers(GameServiceCollection gameServices)
        {
            var rhs = GetT<RequestHandler>();
            rhs.ForEach(rh => rh.GameServices = gameServices);
            return rhs;
        }

        public static List<ResponseCreator> GetResponseCreators(GameServiceCollection gameServices)
        {
            var rcs = GetT<ResponseCreator>();
            rcs.ForEach(rc => rc.GameServices = gameServices);
            return rcs;
        }

        private static List<T> GetT<T>()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.BaseType != null && t.BaseType.Equals(typeof(T)));
            return types.Select(t => (T)Activator.CreateInstance(t)).ToList();
        }
    }
}
