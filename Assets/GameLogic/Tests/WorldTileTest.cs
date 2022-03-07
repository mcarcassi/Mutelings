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
        //TODO: The object added should be the same as the one tested to be added
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
    public void RemoveSpecificObjectTest()
    {
        WorldTile sampleTile = new WorldTile();
        //Basic Check
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

    //sampleTile.RemoveAllObjects(typeof(Plant));
}
