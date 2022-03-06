using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
    public class TerrainType
    {
        public string Name { get; }
        public bool CanHavePlant { get; }
        public bool IsPassable { get; }

        public TerrainType(string name, bool canHavePlant, bool isPassable)
        {
            Name = name;
            CanHavePlant = canHavePlant;
            IsPassable = isPassable;
        }
    }
}
