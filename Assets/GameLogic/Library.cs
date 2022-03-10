using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
    public class Library
    {
        public List<TerrainType> TerrainTypes { get; }
        public TerrainType DefaultTerrainType { get; }
        public List<PlantType> PlantTypes { get;}


        public Library()
        {
            TerrainTypes = new List<TerrainType>();
            TerrainTypes.Add(new TerrainType("Water", false, false));
            TerrainTypes.Add(new TerrainType("Grassland", true, true));

            DefaultTerrainType = TerrainTypes[1];
        }

        public static Library Instance { get; }

        static Library()
        {
            Instance = new Library();
        }
    }
}
