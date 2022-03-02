using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
    public class WorldTile
    {
        private List<TileObject> objects = new List<TileObject>();
        public WorldTile()
        {

        }

        public void AddObject(TileObject anObject)
        {
            if (!CanAddObject(anObject))
            {
                throw new ArgumentException("Cannot add object " + anObject + " to the tile " + this);
            }
            objects.Add(anObject);
        }

        public bool CanAddObject(TileObject anObject)
        {
            // TODO: consistency checks
            return true;
        }

        public List<TileObject> GetTileObjects()
        {
            return objects;
        }
    }
}
