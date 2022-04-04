using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
    public class Muteling : TileObject
    {
        private static Random rand = new Random();
        private int _energy = 50;

        public void Move(Direction dir)
        {
            Position.GetNextTile(dir).AddObject(this);
        }

        public override void AdvanceTime()
        {
            _energy -= 5;
            List<Direction> allowedDirections = new List<Direction>();
            if (Position.ContainsFood())
            {
                Eat();
                return;
            }
            foreach (Direction dir in Enum.GetValues(typeof(Direction)).Cast<Direction>())
            {
                WorldTile tile = Position.GetNextTile(dir);
                if (tile != null && tile.CanAddObject(this))
                {
                    allowedDirections.Add(dir);
                }
            }
            if (allowedDirections.Count == 0)
            {
                return;
            }

            int intId = rand.Next(0, allowedDirections.Count);
            Move(allowedDirections[intId]);

            if (_energy >= 100)
            {
                Position.AddObject(new Egg());
                _energy -= 75;
            }

            if (_energy <= 0)
            {
                Position = null;
            }
        }

        public void Eat()
        {
            Resource foodEaten = Position.GetFirstFood();
            if (foodEaten == null)
            {
                throw new InvalidOperationException("There is no food on this tile.");
            }
            Position.RemoveObject(foodEaten);
            _energy += 30;
        }

        public List<WorldTile> SenseFood(int senseRange)
        {
            int x;
            int y;
            List<WorldTile> foodTiles = new List<WorldTile>();
            //Loop to iterate through all tiles
            for (int q = -senseRange; q <= senseRange; q++)
            {
                for (int r = -senseRange; r <= senseRange; r++)
                {
                    for (int s = -senseRange; s <= senseRange; s++)
                    {
                        //Checks if tile exists
                        if(q + r + s == 0)
                        {
                            //Convert QRS to XY. Perhaps can have better function later
                            x = q + (r - (r % 2)) / 2;
                            y = -r;
                            //Checking if tile has a plant
                            if (Position.GetWorld().GetTileAt(x, y).Contains(typeof(Plant)))
                            {
                                foodTiles.Add(Position.GetWorld().GetTileAt(x, y));
                            }
                        }
                    }
                }
            }
            return foodTiles;
        }
    }
}
