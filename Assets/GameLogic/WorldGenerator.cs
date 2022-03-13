﻿using System;
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
                    if (x == 0 && y == 0)
                    {
                        world.GetTileAt(x, y).TerrainType = Library.Instance.DefaultTerrainType;
                        world.GetTileAt(x, y).AddObject(new Plant());
                        continue;
                    }

                    int terrainId = rand.Next(0, Library.Instance.TerrainTypes.Count);
                    world.GetTileAt(x, y).TerrainType = Library.Instance.TerrainTypes[terrainId];

                    Plant plant = new Plant();
                    if (world.GetTileAt(x, y).CanAddObject(plant) && rand.Next(0, 4) == 0)
                    {
                        world.GetTileAt(x, y).AddObject(plant);
                    }
                }
            }

            return world;
        }
    }
}
