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
        public ResourceType ResourceType { get; }

        public PlantType(string name, List<TerrainType> habitableTerrain, bool isPassible, bool canReproduce, bool canDie, int maxResourceCount, ResourceType resourceType)
        {
            Name = name;
            HabitableTerrain = habitableTerrain;
            IsPassible = isPassible;
            CanReproduce = canReproduce;
            CanDie = canDie;
            MaxResourceCount = maxResourceCount;
            ResourceType = resourceType;
        }
    }
}

