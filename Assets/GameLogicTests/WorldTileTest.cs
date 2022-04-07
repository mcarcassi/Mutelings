using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.GameLogic;
using System;

public class WorldTileTest
{
    World world = new World(75, 50);


    //Tests CanAddObject Method
    [Test]
    public void CanAddObjectTest()
    {
        WorldTile newSampleTile = new WorldTile(world, 0, 0);
        WorldTile waterTile = new WorldTile(world, 0, 0);
        waterTile.TerrainType = Library.Instance.GetTerrainTypeByName("Water");

        Plant newPlant = new Plant();
        Assert.IsTrue(newSampleTile.CanAddObject(newPlant));

        newSampleTile.AddObject(newPlant);
        Assert.IsFalse(newSampleTile.CanAddObject(new Plant()));

        Muteling newMute = new Muteling();
        Assert.IsTrue(newSampleTile.CanAddObject(newMute));
        newSampleTile.AddObject(newMute);
        Assert.IsFalse(newSampleTile.CanAddObject(new Muteling()));

        Assert.IsFalse(waterTile.CanAddObject(newPlant));
        Assert.IsFalse(waterTile.CanAddObject(newMute));


    }

    [Test]
    public void AddObjectTest()
    {
        WorldTile sampleTile = new WorldTile(world, 0, 0);
        Plant newPlant = new Plant();
        sampleTile.AddObject(newPlant);
        Assert.IsTrue(sampleTile.GetTileObjects()[0] == newPlant);
        Assert.Throws<ArgumentException>(() => sampleTile.AddObject(new Plant()));
    }

    [Test]
    public void ContainsTest()
    {
        WorldTile sampleTile = new WorldTile(world, 0, 0);
        Plant plant1 = new Plant();
        Plant plant2 = new Plant();
        Assert.IsFalse(sampleTile.Contains(plant1));

        sampleTile.AddObject(plant1);
        Assert.IsTrue(sampleTile.Contains(plant1));
        Assert.IsFalse(sampleTile.Contains(plant2));

        Assert.IsTrue(sampleTile.Contains(typeof(Plant)));
        Assert.IsFalse(sampleTile.Contains(typeof(Muteling)));
        sampleTile.AddObject(new Muteling());
        Assert.IsTrue(sampleTile.Contains(typeof(Muteling)));
    }

    [Test]
    public void RemoveSpecificObjectTest()
    {
        WorldTile sampleTile = new WorldTile(world, 0, 0);
        Muteling newMute = new Muteling();
        Plant newPlant = new Plant();
        sampleTile.AddObject(newPlant);
        sampleTile.AddObject(newMute);
        Assert.IsTrue(sampleTile.RemoveObject(newMute));
        Assert.IsFalse(sampleTile.Contains(newMute));
        Assert.IsTrue(sampleTile.Contains(newPlant));
        Assert.IsTrue(sampleTile.RemoveObject(newPlant));
        Assert.IsFalse(sampleTile.Contains(typeof(Plant)));

        Assert.IsFalse(sampleTile.RemoveObject(newPlant));

    }

    [Test]
    public void RemoveAllObjectsTest()
    {
        WorldTile sampleTile = new WorldTile(world, 0, 0);
        Plant newPlant = new Plant();
        Muteling newMuteling = new Muteling();
        sampleTile.AddObject(newPlant);
        sampleTile.AddObject(newMuteling);
        sampleTile.RemoveAllObjects();
        Assert.IsFalse(sampleTile.Contains(newPlant));
        Assert.IsFalse(sampleTile.Contains(newMuteling));

        sampleTile.AddObject(newPlant);
        sampleTile.AddObject(newMuteling);
        sampleTile.RemoveAllObjects(typeof(Plant));
        Assert.IsFalse(sampleTile.Contains(newPlant));
        Assert.IsTrue(sampleTile.Contains(newMuteling));

    }

    [Test]
    public void QRSCoordinatesTest()
    {
        TestQRSNeighbors(world.GetTileAt(4, 4));
        TestQRSNeighbors(world.GetTileAt(4, 3));
        TestQRSNeighbors(world.GetTileAt(3, 4));
        TestQRSNeighbors(world.GetTileAt(3, 3));
    }

    private void TestQRSNeighbors(WorldTile tile)
    {
        Assert.AreEqual(1, tile.GetNextTile(Direction.NE).Q - tile.Q);
        Assert.AreEqual(-1, tile.GetNextTile(Direction.NE).R - tile.R);
        Assert.AreEqual(0, tile.GetNextTile(Direction.NE).S - tile.S);
        Assert.AreEqual(1, tile.GetNextTile(Direction.E).Q - tile.Q);
        Assert.AreEqual(0, tile.GetNextTile(Direction.E).R - tile.R);
        Assert.AreEqual(-1, tile.GetNextTile(Direction.E).S - tile.S);
        Assert.AreEqual(0, tile.GetNextTile(Direction.SE).Q - tile.Q);
        Assert.AreEqual(1, tile.GetNextTile(Direction.SE).R - tile.R);
        Assert.AreEqual(-1, tile.GetNextTile(Direction.SE).S - tile.S);
        Assert.AreEqual(-1, tile.GetNextTile(Direction.SW).Q - tile.Q);
        Assert.AreEqual(1, tile.GetNextTile(Direction.SW).R - tile.R);
        Assert.AreEqual(0, tile.GetNextTile(Direction.SW).S - tile.S);
        Assert.AreEqual(-1, tile.GetNextTile(Direction.W).Q - tile.Q);
        Assert.AreEqual(0, tile.GetNextTile(Direction.W).R - tile.R);
        Assert.AreEqual(1, tile.GetNextTile(Direction.W).S - tile.S);
        Assert.AreEqual(0, tile.GetNextTile(Direction.NW).Q - tile.Q);
        Assert.AreEqual(-1, tile.GetNextTile(Direction.NW).R - tile.R);
        Assert.AreEqual(1, tile.GetNextTile(Direction.NW).S - tile.S);
    }

    [Test]
    public void DistanceTest()
    {
        WorldTile tile = world.GetTileAt(4, 4);
        Assert.AreEqual(0, tile.DistanceFrom(tile));
        Assert.AreEqual(1, tile.DistanceFrom(tile.GetNextTile(Direction.NE)));
        Assert.AreEqual(1, tile.DistanceFrom(tile.GetNextTile(Direction.E)));
        Assert.AreEqual(4, tile.DistanceFrom(tile.GetNextTile(Direction.NE).GetNextTile(Direction.E).GetNextTile(Direction.NE).GetNextTile(Direction.NE)));
        Assert.AreEqual(4, tile.DistanceFrom(tile.GetNextTile(Direction.E).GetNextTile(Direction.SE).GetNextTile(Direction.SE).GetNextTile(Direction.E)));
    }


    [Test]
    public void NextTileTest()
    {
        WorldTile tile = world.GetTileAt(0, 0);
        Assert.AreEqual(world.GetTileAt(0, 1), tile.GetNextTile(Direction.NE));
        Assert.AreEqual(tile, tile.GetNextTile(Direction.NE).GetNextTile(Direction.SW));
        Assert.AreEqual(world.GetTileAt(1, 0), tile.GetNextTile(Direction.E));
        Assert.AreEqual(tile, tile.GetNextTile(Direction.E).GetNextTile(Direction.W));
        Assert.AreEqual(null, tile.GetNextTile(Direction.SE));
        Assert.AreEqual(null, tile.GetNextTile(Direction.SW));
        Assert.AreEqual(null, tile.GetNextTile(Direction.W));
        Assert.AreEqual(null, tile.GetNextTile(Direction.NW));

        tile = world.GetTileAt(2, 2);
        Assert.AreEqual(world.GetTileAt(2, 3), tile.GetNextTile(Direction.NE));
        Assert.AreEqual(tile, tile.GetNextTile(Direction.NE).GetNextTile(Direction.SW));
        Assert.AreEqual(world.GetTileAt(3, 2), tile.GetNextTile(Direction.E));
        Assert.AreEqual(tile, tile.GetNextTile(Direction.E).GetNextTile(Direction.W));
        Assert.AreEqual(world.GetTileAt(2, 1), tile.GetNextTile(Direction.SE));
        Assert.AreEqual(tile, tile.GetNextTile(Direction.SE).GetNextTile(Direction.NW));
        Assert.AreEqual(world.GetTileAt(1, 1), tile.GetNextTile(Direction.SW));
        Assert.AreEqual(tile, tile.GetNextTile(Direction.SW).GetNextTile(Direction.NE));
        Assert.AreEqual(world.GetTileAt(1, 2), tile.GetNextTile(Direction.W));
        Assert.AreEqual(tile, tile.GetNextTile(Direction.W).GetNextTile(Direction.E));
        Assert.AreEqual(world.GetTileAt(1, 3), tile.GetNextTile(Direction.NW));
        Assert.AreEqual(tile, tile.GetNextTile(Direction.NW).GetNextTile(Direction.SE));

        tile = world.GetTileAt(3, 3);
        Assert.AreEqual(world.GetTileAt(4, 4), tile.GetNextTile(Direction.NE));
        Assert.AreEqual(tile, tile.GetNextTile(Direction.NE).GetNextTile(Direction.SW));
        Assert.AreEqual(world.GetTileAt(4, 3), tile.GetNextTile(Direction.E));
        Assert.AreEqual(tile, tile.GetNextTile(Direction.E).GetNextTile(Direction.W));
        Assert.AreEqual(world.GetTileAt(4, 2), tile.GetNextTile(Direction.SE));
        Assert.AreEqual(tile, tile.GetNextTile(Direction.SE).GetNextTile(Direction.NW));
        Assert.AreEqual(world.GetTileAt(3, 2), tile.GetNextTile(Direction.SW));
        Assert.AreEqual(tile, tile.GetNextTile(Direction.SW).GetNextTile(Direction.NE));
        Assert.AreEqual(world.GetTileAt(2, 3), tile.GetNextTile(Direction.W));
        Assert.AreEqual(tile, tile.GetNextTile(Direction.W).GetNextTile(Direction.E));
        Assert.AreEqual(world.GetTileAt(3, 4), tile.GetNextTile(Direction.NW));
        Assert.AreEqual(tile, tile.GetNextTile(Direction.NW).GetNextTile(Direction.SE));
    }

    [Test]
    public void GetNeighborsTest()
    {
        WorldTile newSampleTile1 = new WorldTile(world, 2, 2);
        List<WorldTile> neighbors = newSampleTile1.GetNeighbors();
        Assert.AreEqual(newSampleTile1.GetNextTile(Direction.E), neighbors[0]);
        Assert.AreEqual(newSampleTile1.GetNextTile(Direction.NE), neighbors[1]);
        Assert.AreEqual(newSampleTile1.GetNextTile(Direction.NW), neighbors[2]);
        Assert.AreEqual(newSampleTile1.GetNextTile(Direction.W), neighbors[3]);
        Assert.AreEqual(newSampleTile1.GetNextTile(Direction.SW), neighbors[4]);
        Assert.AreEqual(newSampleTile1.GetNextTile(Direction.SE), neighbors[5]);

        WorldTile newSampleTile2 = new WorldTile(world, 0, 0);
        neighbors = newSampleTile2.GetNeighbors();
        Assert.AreEqual(newSampleTile2.GetNextTile(Direction.E), neighbors[0]);
        Assert.AreEqual(newSampleTile2.GetNextTile(Direction.NE), neighbors[1]);
        Assert.AreEqual(null, neighbors[2]);
        Assert.AreEqual(null, neighbors[3]);
        Assert.AreEqual(null, neighbors[4]);
        Assert.AreEqual(null, neighbors[5]);

        WorldTile newSampleTile3 = new WorldTile(world, 74, 0);
        neighbors = newSampleTile3.GetNeighbors();
        Assert.AreEqual(null, neighbors[0]);
        Assert.AreEqual(newSampleTile3.GetNextTile(Direction.NE), neighbors[1]);
        Assert.AreEqual(newSampleTile3.GetNextTile(Direction.NW), neighbors[2]);
        Assert.AreEqual(newSampleTile3.GetNextTile(Direction.W), neighbors[3]);
        Assert.AreEqual(null, neighbors[4]);
        Assert.AreEqual(null, neighbors[5]);

        WorldTile newSampleTile4 = new WorldTile(world, 74, 49);
        neighbors = newSampleTile4.GetNeighbors();
        Assert.AreEqual(null, neighbors[0]);
        Assert.AreEqual(null, neighbors[1]);
        Assert.AreEqual(null, neighbors[2]);
        Assert.AreEqual(newSampleTile4.GetNextTile(Direction.W), neighbors[3]);
        Assert.AreEqual(newSampleTile4.GetNextTile(Direction.SW), neighbors[4]);
        Assert.AreEqual(null, neighbors[5]);

        WorldTile newSampleTile5 = new WorldTile(world, 0, 49);
        neighbors = newSampleTile5.GetNeighbors();
        Assert.AreEqual(newSampleTile5.GetNextTile(Direction.E), neighbors[0]);
        Assert.AreEqual(null, neighbors[1]);
        Assert.AreEqual(null, neighbors[2]);
        Assert.AreEqual(null, neighbors[3]);
        Assert.AreEqual(newSampleTile5.GetNextTile(Direction.SW), neighbors[4]);
        Assert.AreEqual(newSampleTile5.GetNextTile(Direction.SE), neighbors[5]);
    }

    [Test]
    public void GetResourcesTest()
    {
        WorldTile newSampleTile = new WorldTile(world, 0, 0);
        Resource res1 = new Resource(Library.Instance.GetResourceTypeByName("Redberry"));
        Resource res2 = new Resource(Library.Instance.GetResourceTypeByName("Redberry"));
        Assert.IsEmpty(newSampleTile.GetResources());
        newSampleTile.AddObject(res1);
        Assert.AreEqual(res1, newSampleTile.GetResources()[0]);
        newSampleTile.AddObject(res2);
        Assert.AreEqual(res2, newSampleTile.GetResources()[1]);
        
    }

    [Test]
    public void ContainsFoodTest()
    {
        WorldTile newSampleTile = new WorldTile(world, 0, 0);
        Assert.IsFalse(newSampleTile.ContainsFood());
        Resource res1 = new Resource(Library.Instance.GetResourceTypeByName("Redberry"));
        newSampleTile.AddObject(res1);
        Assert.IsTrue(newSampleTile.ContainsFood());
    }




}
