using System;

namespace Assets.GameLogic
{
    public class Resource : TileObject
    {
        public ResourceType ResourceType { get; }

        public Resource(ResourceType resourceType)
        {
            ResourceType = resourceType;
        }
    }
}

