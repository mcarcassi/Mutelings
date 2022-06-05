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
            return Sense(senseRange, tile => tile.Contains(typeof(Plant)));
        }

        public List<WorldTile> Sense(int senseRange, Predicate<WorldTile> condition)
        {
            List<WorldTile> sensedTiles = new List<WorldTile>();
            List<WorldTile> visited = new List<WorldTile>();
            List<WorldTile> fringes = new List<WorldTile>();
            List<WorldTile> neighbors = new List<WorldTile>();
            List<WorldTile> nextLayer = new List<WorldTile>();
            visited.Add(Position);
            fringes.Add(Position);
            if (condition.Invoke(Position))
            {
                sensedTiles.Add(Position);
            }
            for (int i = 0; i < senseRange; i++)
            {
                foreach (WorldTile fringeTile in fringes)
                {
                    neighbors = fringeTile.GetNeighbors();
                    foreach (WorldTile tile in neighbors)
                    {
                        if (!visited.Contains(tile))
                        {
                            if (condition.Invoke(tile))
                            {
                                sensedTiles.Add(tile);
                            }
                            nextLayer.Add(tile);
                            visited.Add(tile);
                        }
                    }
                }
                fringes.RemoveAll(x => true);
                fringes.AddRange(nextLayer);
                nextLayer.RemoveAll(x => true);

            }

            return sensedTiles;
        }

        public bool IsReachable(int senseRange, WorldTile goal)
        {
            if (goal.DistanceFrom(Position) > senseRange)
            {
                throw new ArgumentException("Cannot find a path to a tile that cannot be sensed");
            }
            if (goal.DistanceFrom(Position) == 0)
            {
                return true;
            }
            List<WorldTile> visited = new List<WorldTile>();
            List<WorldTile> fringes = new List<WorldTile>();
            List<WorldTile> neighbors = new List<WorldTile>();
            List<WorldTile> nextLayer = new List<WorldTile>();
            visited.Add(Position);
            fringes.Add(Position);

            while (fringes.Count > 0)
            {
                foreach (WorldTile fringeTile in fringes)
                {
                    neighbors = fringeTile.GetNeighbors();
                    foreach (WorldTile tile in neighbors)
                    {
                        if (tile.Equals(goal))
                        {
                            return true;
                        }
                        if (!visited.Contains(tile) && tile.TerrainType.IsPassable && tile.DistanceFrom(Position) <= senseRange)
                        {
                            nextLayer.Add(tile);
                            visited.Add(tile);
                        }
                    }
                }
                fringes.RemoveAll(x => true);
                fringes.AddRange(nextLayer);
                nextLayer.RemoveAll(x => true);
            }
            return false;

        }
    }
}
