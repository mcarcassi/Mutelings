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
        private int _growthCount = 0;
        public int GrowthStage { get; set; }

        public Plant(PlantType plantType)
        {
            PlantType = plantType;
            _resources = new List<Resource>();
            GrowthStage = 1;
        }

        public Plant()
        {
            PlantType = Library.Instance.GetPlantTypeByName("Berry Bush");
            _resources = new List<Resource>();
            GrowthStage = 1;
        }

        public void GrowResource()
        {
            Resource resource = new Resource(PlantType.ResourceType);
            _resources.Add(resource);
            Position.AddObject(resource);
            UpdateGrowth();
        }

        public void UpdateGrowth()
        {
            if(this.ResourceCount() > PlantType.GrowthStages)
            {
                GrowthStage = PlantType.GrowthStages;
            }
            else
            {
                GrowthStage = (this.ResourceCount() + 1);
            }
        }

        public List<Resource> GetResources()
        {
            return _resources;
        }

        public int ResourceCount()
        {
            return _resources.Count;
        }

        public bool RemoveResource(Resource resource)
        {
            return _resources.Remove(resource);
        }

        public override void AdvanceTime()
        {
            if (_growthCount > 5)
            {
                GrowResource();
                _growthCount = 0;
            }
            else
            {
                _growthCount++;
            }
            
        }


    }
}
