using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
    /// <summary>
    /// Class <c>WorldTile</c> represents a tile that is generated in the World class.
    /// Tiles can contain objects, such as plants, mutelings, and resources. Only one plant and
    /// one muteling may exist on a tile at any time, although there may be multiple resources.
    /// </summary>
    public class WorldTile
    {
        public TerrainType TerrainType { get; set; }
        private List<TileObject> _objects = new List<TileObject>();
        private World _world;
        public int X { get; }
        public int Y { get; }

        /// <summary>
        /// Default constructor that creates a tile of default terrain type.
        /// </summary>
        public WorldTile(World world, int x, int y)
        {
            _world = world;
            X = x;
            Y = y;
            TerrainType = Library.Instance.DefaultTerrainType;
        }

        //public WorldTile()
        //{
        //    // TODO This needs to be removed
        //    TerrainType = Library.Instance.DefaultTerrainType;
        //}

        ///// <summary>
        ///// Constructor that allows custom terrain type.
        ///// </summary>
        ///// <param name="terrainType">The type of terrain for the tile.</param>
        //public WorldTile(TerrainType terrainType)
        //{
        //    TerrainType = terrainType;
        //}

        public WorldTile GetNextTile(Direction dir)
        {
            switch(dir)
            {
                case Direction.E:
                    return _world.GetTileAt(X + 1, Y);
                case Direction.W:
                    return _world.GetTileAt(X - 1, Y);
                case Direction.NE:
                    return _world.GetTileAt(X + (Y % 2), Y + 1);
                case Direction.SE:
                    return _world.GetTileAt(X + (Y % 2), Y - 1);
                case Direction.NW:
                    return _world.GetTileAt(X + (Y % 2) - 1, Y + 1);
                case Direction.SW:
                    return _world.GetTileAt(X + (Y % 2) - 1, Y - 1);
            }
            return null;
        }

        /// <summary>
        /// Method <c>AddObject</c> adds an tile object on the tile.
        /// </summary>
        /// <param name="anObject"> the new object being added.</param>
        /// <exception cref="ArgumentException"> Thrown when object cannot be added.</exception>
        public void AddObject(TileObject anObject)
        {
            if (!CanAddObject(anObject))
            {
                throw new ArgumentException("Cannot add object " + anObject + " to the tile " + this);
            }
            _objects.Add(anObject);
            anObject.Position = this;
        }

        /// <summary>
        /// Method <c>CanAddObject</c> checks to see if object can be added to the tile.
        /// </summary>
        /// <param name="anObject"> the object to be checked.</param>
        /// <returns>
        /// True if object can be added and false otherwise.
        /// </returns>
        public bool CanAddObject(TileObject anObject)
        {
            bool canAddPlant = true;
            bool canAddMuteling = true;

            if (this.Contains(typeof(Plant)))
            {
                canAddPlant = false;
            }
            if (this.Contains(typeof(Muteling)))
            {
                canAddMuteling = false;
            }

            if (anObject is Plant)
            {
                if (!TerrainType.CanHavePlant)
                {
                    return false;
                }
                return canAddPlant;
            }
            else if (anObject is Muteling)
            {
                if (!TerrainType.IsPassable)
                {
                    return false;
                }
                return canAddMuteling;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Method <c>CanAddObject</c> checks to see if TileObject type can be added to the tile.
        /// </summary>
        /// <param name="type"> the TileObject type to be checked</param>
        /// <returns>
        /// True if TileObject type can be added and false otherwise.
        /// </returns>
        public bool CanAddObject(Type type)
        {
            bool canAddPlant = true;
            bool canAddMuteling = true;

            if (this.Contains(typeof(Plant)))
            {
                canAddPlant = false;
            }
            if (this.Contains(typeof(Muteling)))
            {
                canAddMuteling = false;
            }

            if (type == typeof(Plant))
            {
                if (!TerrainType.CanHavePlant)
                {
                    return false;
                }
                return canAddPlant;
            }
            else if (type == typeof(Muteling))
            {
                return canAddMuteling;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Method <c>RemoveObject</c> removes an object from the tile.
        /// </summary>
        /// <param name="obj"> the object to be removed.</param>
        /// <returns>
        /// True if object was successfully removed and false otherwise.
        /// </returns>
        public bool RemoveObject(TileObject obj)
        {
            bool removed = _objects.Remove(obj);
            if (removed)
            {
                obj.Position = null;
            }
            return removed;
        }

        /// <summary>
        /// Method <c>RemoveAllObjects</c> removes all TileObjects of a specific type from the tile.
        /// </summary>
        /// <param name="type"> the TileObject type to be removed.</param>
        /// <returns>
        /// True if at least one object is removed and false otherwise.
        /// </returns>
        public bool RemoveAllObjects(Type type)
        {
            return RemoveAllObjects(x => x.GetType() == type);
        }

        public bool RemoveAllObjects(Predicate<TileObject> condition)
        {
            List<TileObject> toBeRemoved = _objects.FindAll(condition);
            foreach (TileObject tileObject in toBeRemoved)
            {
                RemoveObject(tileObject);
            }
            return toBeRemoved.Count != 0;
        }

        /// <summary>
        /// Method <c>RemoveAllObjects</c> removes all objects from the tile.
        /// </summary>
        public void RemoveAllObjects()
        {
            RemoveAllObjects(x => true);
        }

        /// <summary>
        /// Method <c>Contains</c> checks tile for a given object.
        /// </summary>
        /// <param name="anObject"> object to be checked.</param>
        /// <returns>
        /// True if object is on tile and false if not.
        /// </returns>
        public bool Contains(TileObject anObject)
        {
            foreach (TileObject obj in _objects)
            {
                if(obj == anObject)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method <c>Contains</c> checks tile for given TileObject type.
        /// </summary>
        /// <param name="type"> TileObject type to be checked.</param>
        /// <returns>
        /// True if an object of TileObject type is on tile and false if not. 
        /// </returns>
        public bool Contains(Type type)  {

            foreach (TileObject obj in _objects)
            {
                if (obj.GetType() == type)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Resource> GetResources()
        {
            List<Resource> resources = new List<Resource>();
            foreach (TileObject obj in _objects)
            {
                if(obj.GetType() == typeof(Resource))
                {
                    resources.Add((Resource)obj);
                }
            }
            return resources;
        }

        public bool ContainsFood()
        {
            List<Resource> resources = GetResources();
            foreach (Resource resource in resources)
            {
                if (resource.ResourceType.IsFood)
                {
                    return true;
                }
            }
            return false;
        }

        public Resource GetFirstFood()
        {
            List<Resource> resources = GetResources();
            if (!ContainsFood())
            {
                return null;
            }
            foreach (Resource resource in resources)
            {
                if (resource.ResourceType.IsFood)
                {
                    return resource;
                }
            }
            return null;
        }



        /// <summary>
        /// Method <c>GetTileObjects</c> gives all objects on the tile.
        /// </summary>
        /// <returns>A list with all the objects.</returns>
        public List<TileObject> GetTileObjects()
        {
            return _objects;
        }
    }
}
