using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
    public class World
    {
        private int xSize;
        private int ySize;
        private WorldTile[,] tiles;

        public World(int xSize, int ySize)
        {
            this.xSize = xSize;
            this.ySize = ySize;
            tiles = new WorldTile[xSize,ySize];
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
            return xSize;
        }

        public int GetYSize()
        {
            return ySize;
        }
        public WorldTile GetTileAt(int x, int y)
        {
            return tiles[x, y];
        }
    }
}
