using r.io.model.GameEntities;
using System.Collections.Generic;

namespace r.io.model.Services.Abstract
{
    public interface PlayerService
    {
        void Move(PlayerCircle player, double x, double y);
        void TryEat(CircleGameObject player);
        List<AreaPart> getGameAreasAround(double x, double y);
        AreaPart getAreaPart(double x, double y);
    }
}
