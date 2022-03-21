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

        public Plant(PlantType plantType, List<Resource> resources)
        {
            PlantType = plantType;
            _resources = resources;
        }

        public Plant()
        {

        }
    }
}
