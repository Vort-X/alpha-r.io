using System.Collections.Generic;

namespace r.io.shared.Services
{
    public class GameServiceCollection
    {
        private readonly List<object> gameServices = new();

        public void Add<T>(T service)
        {
            gameServices.Add(service);
        }

        public T Get<T>()
        {
            return (T)gameServices.Find(x => x is T);
        }
    }
}
