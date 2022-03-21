using System;

namespace Assets.GameLogic
{
    public class Resource : TileObject
    {
        ResourceType ResourceType { get; }

        public Resource(ResourceType resourceType)
        {
            ResourceType = resourceType;
        }

        public Resource()
        {

        }
    }
}

