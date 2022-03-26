using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
    public class Egg : Resource
    {
        private int _counter = 10;

        public Egg() : base(Library.Instance.GetResourceTypeByName("Muteling Egg"))
        {
        }

        public override void AdvanceTime()
        {
            _counter--;
            if (_counter == 0)
            {
                if (Position.CanAddObject(typeof(Muteling)))
                {
                    Position.AddObject(new Muteling());
                    Position = null;
                }
                else
                {
                    _counter++;
                }
            }

        }
    }
}
