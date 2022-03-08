using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
    public class World
    {
        private int XSize { get; }
        private int YSize { get; }
        private WorldTile[,] tiles { get; }

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

        public int GetXSize()
        {
            return XSize;
        }

        public int GetYSize()
        {
            return YSize;
        }
        public WorldTile GetTileAt(int x, int y)
        {
            return tiles[x, y];
        }
    }
    
}
