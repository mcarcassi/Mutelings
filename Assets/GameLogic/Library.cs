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

        public List<ResourceType> ResourceTypes { get; }


        public Library()
        {
            TerrainTypes = new List<TerrainType>();
            TerrainTypes.Add(new TerrainType("Water", false, false));
            TerrainTypes.Add(new TerrainType("Grassland", true, true));
            DefaultTerrainType = TerrainTypes[1];

            ResourceTypes = new List<ResourceType>();
            ResourceTypes.Add(new ResourceType("Fruit", true));

            PlantTypes = new List<PlantType>();
            PlantTypes.Add(new PlantType("Berry Bush", new List<TerrainType>()
            {
                GetTerrainTypeByName("Grassland")
            },
                true, false, false, 10, GetResourceTypeByName("Fruit")));

        }

        public TerrainType GetTerrainTypeByName(String str)
        {
            return TerrainTypes.Find(x => x.Name.Equals(str));
        }

        public PlantType GetPlantTypeByName(String str)
        {
            return PlantTypes.Find(x => x.Name.Equals(str));
        }

        public ResourceType GetResourceTypeByName(String str)
        {
            return ResourceTypes.Find(x => x.Name.Equals(str));
        }

        public static Library Instance { get; }

        static Library()
        {
            Instance = new Library();
        }
    }
}
