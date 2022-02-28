using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
    public class WorldGenerator
    {
        public static World GenerateRandomWorld(int xSize, int ySize)
        {
            World world = new World(xSize, ySize);
            Random rand = new Random();

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (rand.Next(0, 4) == 0)
                    {
                        world.GetTileAt(x, y).addObject(new Plant());
                    }
                }
            }

            return world;
        }
    }
}
