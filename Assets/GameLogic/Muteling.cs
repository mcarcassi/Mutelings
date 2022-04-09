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
            List<WorldTile> foodTiles = new List<WorldTile>();
            List<WorldTile> visited = new List<WorldTile>();
            List<WorldTile> fringes = new List<WorldTile>();
            List<WorldTile> neighbors = new List<WorldTile>();
            List<WorldTile> nextLayer = new List<WorldTile>();
            visited.Add(Position);
            fringes.Add(Position);
            if (Position.Contains(typeof(Plant)))
            {
                foodTiles.Add(Position);
            }
            for (int i = 0; i < senseRange; i++)
            {
                foreach (WorldTile fringeTile in fringes)
                {
                    neighbors = fringeTile.GetNeighbors();
                    foreach (WorldTile tile in neighbors)
                    {
                        if(!visited.Contains(tile))
                        {
                            if (tile.Contains(typeof(Plant)))
                            { 
                                foodTiles.Add(tile);
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
            
            return foodTiles;
        }


        //public List<WorldTile> SenseFood(int senseRange)
        //{
        //    List<WorldTile> foodTiles = new List<WorldTile>();
        //    WorldTile currentTile = Position;
        //    Direction currentDirection = Direction.SW;
        //    if (currentTile.Contains(typeof(Plant)))
        //    {
        //        WorldTile newTile = new WorldTile(currentTile.GetWorld(), currentTile.X, currentTile.Y);
        //        foodTiles.Add(newTile);
        //    }
        //    for (int i = 0; i < senseRange; i++)
        //    {
        //        for (int j = 0; j < 6; j++)
        //        {
        //            if (j == 0)
        //            {
        //                currentDirection = Direction.E;
        //            }
        //            else if (j == 1)
        //            {
        //                currentDirection = Direction.NE;
        //            }
        //            else if (j == 2)
        //            {
        //                currentDirection = Direction.NW;
        //            }
        //            else if (j == 3)
        //            {
        //                currentDirection = Direction.W;
        //            }
        //            else if (j == 4)
        //            {
        //                currentDirection = Direction.SW;
        //            }
        //            else if (j == 5)
        //            {
        //                currentDirection = Direction.SE;
        //            }

        //            for (int k = 0; k < currentTile.DistanceFrom(Position); k++)
        //            {

        //                if (currentTile.Contains(typeof(Plant)))
        //                {
        //                    WorldTile newTile = new WorldTile(currentTile.GetWorld(), currentTile.X, currentTile.Y);
        //                    foodTiles.Add(newTile);
        //                }
        //                currentTile = currentTile.GetNextTile(currentDirection);
        //            }
        //        }
        //        currentTile = currentTile.GetNextTile(Direction.SW);
        //    }
        //    return foodTiles;
        //}




        //public List<WorldTile> SenseFood(int senseRange)
        //{
        //    int x;
        //    int y;
        //    List<WorldTile> foodTiles = new List<WorldTile>();
        //    //Loop to iterate through all tiles
        //    for (int q = Position.Q-senseRange; q <= Position.Q + senseRange; q++)
        //    {
        //        for (int r = Position.R-senseRange; r <= Position.R + senseRange; r++)
        //        {
        //            for (int s = Position.S-senseRange; s <= Position.S + senseRange; s++)
        //            {
        //                //Checks if tile exists
        //                if(q + r + s == 0 && r <= 0 && q >= 0)
        //                {
        //                    //Convert QRS to XY. Perhaps can have better function later
        //                    x = q + (r - (r % 2)) / 2;
        //                    y = -r;
        //                    //Checking if tile has a plant
        //                    if (Position.GetWorld().GetTileAt(x, y).Contains(typeof(Plant)))
        //                    {
        //                        foodTiles.Add(Position.GetWorld().GetTileAt(x, y));
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return foodTiles;
        //}
    }
}
