using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
    public class WorldTile
    {
        private List<TileObject> objects = new List<TileObject>();
        public WorldTile()
        {

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

            if (this.ContainsPlant())
            {
                canAddPlant = false;
            }
            if (this.ContainsMuteling())
            {
                canAddMuteling = false;
            }

            if (anObject is Plant)
            {
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

        public void RemoveObject(String str)
        {
            str = str.ToLower();
            if (str.Equals("plant"))
            {
                for(int i = 0; i < objects.Count; i++)
                {
                    if (objects[i] is Plant)
                    {
                        objects.RemoveAt(i);
                    }
                }
            }
            if (str.Equals("muteling"))
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    if (objects[i] is Muteling)
                    {
                        objects.RemoveAt(i);
                    }
                }
            }
        }

        public void RemoveAllObjects()
        {
            foreach(TileObject obj in objects)
            {
                objects.Remove(obj);
            }
        }

        public bool ContainsPlant()
        {
            foreach (TileObject obj in objects)
            {
                if (obj is Plant)
                {
                    return true;
                }
            }
            return false;
        }
        public bool ContainsMuteling()
        {
            foreach (TileObject obj in objects)
            {
                if (obj is Muteling)
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
