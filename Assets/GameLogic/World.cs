using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
    public class World
    {
        public int XSize { get; }
        public int YSize { get; }
        public WorldTile[,] tiles { get; }

        public World(int xSize, int ySize)
        {
            XSize = xSize;
            YSize = ySize;
            tiles = new WorldTile[xSize, ySize];
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    tiles[x, y] = new WorldTile(this, x, y);
                }
            }
        }

        public WorldTile GetTileAt(int x, int y)
        {
            if (x <0 || x >= XSize || y < 0 || y >= YSize)
            {
                return null;
            }
            return tiles[x, y];
        }

        public void AdvanceTime()
        {
            List<TileObject> allObjects = new List<TileObject>();
            foreach (WorldTile tile  in tiles)
            {
                allObjects.AddRange(tile.GetTileObjects());
            }

            // TODO: Decide order

            // Call AdvanceTime on all objects
            foreach (TileObject obj in allObjects)
            {
                obj.AdvanceTime();
            }
        }
    }
    
}
