using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{

    public class Plant : TileObject
    {
        public PlantType PlantType { get; }
        private List<Resource> _resources;

        public Plant(PlantType plantType)
        {
            PlantType = plantType;
            _resources = new List<Resource>();
        }

        public Plant()
        {

        }

        public void GrowResource()
        {
            _resources.Add(new Resource(PlantType.ResourceType));
        }

        public List<Resource> GetResources()
        {
            return _resources;
        }

        public int ResourceCount()
        {
            return _resources.Count;
        }
    }
}
