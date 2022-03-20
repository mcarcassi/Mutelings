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
        WorldTile newSampleTile = new WorldTile();
        Plant newPlant = new Plant();
        Assert.IsTrue(newSampleTile.CanAddObject(newPlant));

        newSampleTile.AddObject(newPlant);
        Assert.IsFalse(newSampleTile.CanAddObject(new Plant()));

        Muteling newMute = new Muteling();
        Assert.IsTrue(newSampleTile.CanAddObject(newMute));
        newSampleTile.AddObject(newMute);
        Assert.IsFalse(newSampleTile.CanAddObject(new Muteling()));


    }

    [Test]
    public void AddObjectTest()
    {
        WorldTile sampleTile = new WorldTile();
        Plant newPlant = new Plant();
        sampleTile.AddObject(newPlant);
        Assert.IsTrue(sampleTile.GetTileObjects()[0] == newPlant);
        Assert.Throws<ArgumentException>(() => sampleTile.AddObject(new Plant()));
    }

    [Test]
    public void ContainsTest()
    {
        WorldTile sampleTile = new WorldTile();
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
        WorldTile sampleTile = new WorldTile();
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
        WorldTile sampleTile = new WorldTile();
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


}
