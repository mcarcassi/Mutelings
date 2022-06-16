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

            if (Position.ContainsFood())
            {
                Eat();
                return;
            }
            //List<Direction> allowedDirections = new List<Direction>();
            //foreach (Direction dir in Enum.GetValues(typeof(Direction)).Cast<Direction>())
            //{
            //    WorldTile tile = Position.GetNextTile(dir);
            //    if (tile != null && tile.CanAddObject(this))
            //    {
            //        allowedDirections.Add(dir);
            //    }
            //}
            //if (allowedDirections.Count == 0)
            //{
            //    return;
            //}

            //int intId = rand.Next(0, allowedDirections.Count);
            //Move(allowedDirections[intId]);
            if (CanMove())
            {
                Direction dir;
                dir = DecideMove();
                Move(dir);
            }

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

        public double FoodScore(int senseRange, int numSteps, Direction dir)
        {
            double weight;
            double score = 0;
            // TODO Homogenize check for muteling on tile 
            if (!Position.GetNextTile(dir).CanAddObject(this))
            {
                throw new ArgumentException("Cannot traverse this tile");
            }
            List<WorldTile> visited = new List<WorldTile>();
            List<WorldTile> fringes = new List<WorldTile>();
            List<WorldTile> neighbors = new List<WorldTile>();
            List<WorldTile> nextLayer = new List<WorldTile>();
            visited.Add(Position);
            visited.Add(Position.GetNextTile(dir));
            fringes.Add(Position.GetNextTile(dir));
            if (Position.GetNextTile(dir).Contains(typeof(Plant)))
            {
                score += Position.GetNextTile(dir).GetPlant().ResourceCount();
            }

            for (int i = 2; i <= numSteps; i++)
            {
                weight = 1.0 / i;
                foreach (WorldTile fringeTile in fringes)
                {
                    neighbors = fringeTile.GetNeighbors();
                    foreach (WorldTile tile in neighbors)
                    {
                        // TODO Homogenize check for muteling on tile 
                        if (!visited.Contains(tile) && tile.TerrainType.IsPassable && tile.DistanceFrom(Position) <= senseRange)
                        {
                            nextLayer.Add(tile);
                            visited.Add(tile);
                            if (tile.Contains(typeof(Plant)))
                            {
                                score += weight*tile.GetPlant().ResourceCount();
                            }
                        }
                    }
                }
                fringes.RemoveAll(x => true);
                fringes.AddRange(nextLayer);
                nextLayer.RemoveAll(x => true);

            }
            return score;
        }
        //TODO: Write test
        public bool CanMove()
        {
            foreach (Direction dir in Enum.GetValues(typeof(Direction)).Cast<Direction>())
            {
                WorldTile tile = Position.GetNextTile(dir);
                if (tile != null && tile.CanAddObject(this))
                {
                    return true;
                }
            }
            return false;
        }
        public Direction DecideMove()
        {
            List<Direction> allowedDirections = new List<Direction>();
            Direction bestDirection = new Direction();
            double bestScore = -1;
            double currentScore;
            foreach (Direction dir in Enum.GetValues(typeof(Direction)).Cast<Direction>())
            {
                WorldTile tile = Position.GetNextTile(dir);
                if (tile != null && tile.CanAddObject(this))
                {
                    allowedDirections.Add(dir);
                }
            }
            if (this.CanMove() == false)
            {
                throw new ArgumentException("Trying to decide movement when no available directions");
            }

            foreach (Direction dir in allowedDirections)
            {
                currentScore = this.FoodScore(3, 3, dir);
                if (currentScore > bestScore)
                {
                    bestScore = currentScore;
                    bestDirection = dir;
                }
                if(currentScore == bestScore)
                {
                    if(rand.Next(0,2) == 0)
                    {
                        bestDirection = dir;
                    }
                }
            }
            return bestDirection;
        }

        public override String ToString()
        {
            return "Muteling at (" + Position.X + "," + Position.Y + ")";
        }
    }
}
