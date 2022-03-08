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

        public void AddObject(TileObject anObject)
        {
            if (!CanAddObject(anObject))
            {
                throw new ArgumentException("Cannot add object " + anObject + " to the tile " + this);
            }
            objects.Add(anObject);
        }

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

        //TODO: Make Test
        public bool RemoveAllObjects(Type type)
        {
            bool removed = false;
            foreach (TileObject obj in objects)
            {
                if (obj.GetType() == type)
                {
                    removed = objects.Remove(obj);
                }
            }
            return removed;
        }

        //TODO: Make Test
        public void RemoveAllObjects()
        {
            foreach (TileObject obj in objects.ToList())
            {
                objects.Remove(obj);
            }
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
