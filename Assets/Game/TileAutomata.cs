using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using Assets.GameLogic;

public class TileAutomata : MonoBehaviour
{
    [Range(0,100)]
    public int initChance;

    [Range(1, 8)]
    public int birthLimit;

    [Range(1, 8)]
    public int deathLimit;

    [Range(1, 10)]
    public int numR;

    private World world;
    public Vector3Int tmapSize;

    public Tilemap mutelingMap;
    public Tilemap plantMap;
    public Tilemap terrainMap;
    public Tile plantTile;
    public Tile grassTile;
    public Tile waterTile;
    public Tile mutelingTile;

    int width;
    int height;

    public void generateWorld()
    {
        width = tmapSize.x;
        height = tmapSize.y;

        world = WorldGenerator.GenerateRandomWorld(width, height);
    }

    public void updateWorld()
    {
        clearMap(false);
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                WorldTile currentTile = world.GetTileAt(width - x - 1, height - y - 1);
                if (currentTile.TerrainType.Name.Equals("Grassland"))
                {
                    terrainMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), grassTile);
                } else
                {
                    terrainMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), waterTile);
                }
                foreach (var tileObject in currentTile.GetTileObjects())
                {
                    if (tileObject is Plant)
                    {
                        plantMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), plantTile);
                    }
                    if (tileObject is Muteling)
                    {
                        mutelingMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), mutelingTile);
                    }
                }
            }
        }

    }

    public int [,] genTilePos(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        int neighb;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighb = 0;
                foreach(var b in myB.allPositionsWithin)
                {
                    if(b.x == 0 && b.y == 0)
                    {
                        continue;
                    }
                    if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y+b.y < height)
                    {
                        neighb += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        neighb++;
                    }
                }

                if (oldMap[x, y] == 1)
                {
                    if (neighb < deathLimit)
                    {
                        newMap[x, y] = 0;
                    }
                    else
                    {
                        newMap[x, y] = 1;
                    }
                }
                if(oldMap[x,y] == 0)
                {
                    if (neighb > birthLimit)
                    {
                        newMap[x, y] = 1;
                    }
                    else
                    {
                        newMap[x, y] = 0;
                    }
                }

            }
        }
        return newMap;

    }



    


    // Update is called once per frame
    void Update()
    {
        if (world == null)
        {
            generateWorld();
            updateWorld();
        }

        if (Input.GetMouseButtonDown(0))
        {
            world.AdvanceTime();
            updateWorld();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 5; i++)
            {
                world.AdvanceTime();
                updateWorld();
                
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            clearMap(true);
            world = null;
        }

    }

    public void clearMap(bool complete)
    {
        plantMap.ClearAllTiles();
        terrainMap.ClearAllTiles();
        mutelingMap.ClearAllTiles();
    }
}
