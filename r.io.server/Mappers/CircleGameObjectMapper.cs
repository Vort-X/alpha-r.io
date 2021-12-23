using r.io.model.GameEntities;
using r.io.shared.UdpGraph;

namespace r.io.server.Mappers
{
    internal static class CircleGameObjectMapper
    {
        public static CircleGameObjectNode ToNode(this CircleGameObject entity)
        {
            var node = new CircleGameObjectNode
            {
                X = entity.x,
                Y = entity.y,
                Radius = entity.radius,
            };
            return node;
        }
    }
}
