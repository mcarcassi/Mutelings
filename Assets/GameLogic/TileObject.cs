using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
    public abstract class TileObject
    {
        private WorldTile _position;
        public WorldTile Position
        {
            get => _position;
            
            set
            {
                if (_position != null)
                {
                    _position.RemoveObject(this);
                }
                _position = value;
            }
        }

        public virtual void AdvanceTime()
        {
            // Does nothing
        }
    }
}
