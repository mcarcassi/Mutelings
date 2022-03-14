using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
    public class Muteling : TileObject
    {
        public void Move(Direction dir)
        {
            Position.GetNextTile(dir).AddObject(this);
        }

        public override void AdvanceTime()
        {
            //List<Direction> allowedDirections = new List<Direction>();
            //foreach (Direction dir in Enum.GetValues(typeof(Direction)).Cast<Direction>())
            //{
            //    WorldTile tile = Position.GetNextTile(dir);
            //    if (tile != null && tile.CanAddObject(this))
            //    {
            //        allowedDirections.Add(dir);
            //    }
            //}

            //if (allowedDirections.Count == 0)
            //{
            //    return;
            //}

            //Random rand = new Random();
            //int intId = rand.Next(0, allowedDirections.Count);
            //            Move(allowedDirections[intId]);

            WorldTile tile = Position.GetNextTile(Direction.NE);
            if (tile != null && tile.CanAddObject(this))
            {
                Move(Direction.NE);
            }
        }
    }
}
