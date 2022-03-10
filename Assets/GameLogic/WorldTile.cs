using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
    public class WorldTile
    {
        public TerrainType TerrainType { get; set; }
        private List<TileObject> objects = new List<TileObject>();

        public WorldTile()
        {
            TerrainType = Library.Instance.DefaultTerrainType;
        }

        /// <summary>
        /// Method <c>AddObject</c> adds an tile object on the tile.
        /// </summary>
        /// <param name="anObject"> the new object being added.</param>
        /// <exception cref="ArgumentException" thrown when object cannot be added.</exception>
        public void AddObject(TileObject anObject)
        {
            if (!CanAddObject(anObject))
            {
                throw new ArgumentException("Cannot add object " + anObject + " to the tile " + this);
            }
            objects.Add(anObject);
        }

        /// <summary>
        /// Method <c>CanAddObject</c> checks to see if object can be added.
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
                return canAddMuteling;
            }
            else
            {
                return true;
            }
        }

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


        public bool RemoveObject(TileObject obj)
        {
            return objects.Remove(obj);
        }

        //TODO: Debug and find issue
        public bool RemoveAllObjects(Type type)
        {
            return objects.RemoveAll(x => x.GetType() == type) != 0;
        }

        //TODO: Debug and find issue
        public void RemoveAllObjects()
        {
            objects.RemoveAll(x => true);
        }

        public bool Contains(TileObject anObject)
        {
            foreach (TileObject obj in objects)
            {
                if(obj == anObject)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Contains(Type type)  {

            foreach (TileObject obj in objects)
            {
                if (obj.GetType() == type)
                {
                    return true;
                }
            }
            return false;
        }

        public List<TileObject> GetTileObjects()
        {
            return objects;
        }
    }
}
