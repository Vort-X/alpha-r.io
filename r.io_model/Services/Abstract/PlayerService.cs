using r.io_model.GameEntities;
using System.Collections.Generic;

namespace r.io_model.Services.Abstract
{
    public interface PlayerService
    {
        void Move(PlayerCircle player, double x, double y);
        void TryEat(CircleGameObject player);
        List<AreaPart> getGameAreasAround(double x, double y);
        AreaPart getAreaPart(double x, double y);
    }
}
