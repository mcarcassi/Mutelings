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
                    tiles[x, y] = new WorldTile();
                }
            }
        }

        public WorldTile GetTileAt(int x, int y)
        {
            return tiles[x, y];
        }
    }
    
}
