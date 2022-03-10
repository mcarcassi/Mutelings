using System;
using System.Collections.Generic;

namespace Assets.GameLogic
{
    public class PlantType
    {
        public string Name { get; }
        public List<TerrainType> HabitableTerrain { get; }
        public bool IsPassible { get; }
        public bool CanReproduce { get; }
        public bool CanDie { get; }
        public int MaxResourceCount { get; }


        public PlantType()
        {

        }
    }
}

