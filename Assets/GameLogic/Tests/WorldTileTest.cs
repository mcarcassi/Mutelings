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
        Assert.IsTrue(newSampleTile.CanAddObject(new Plant()));

        newSampleTile.AddObject(new Plant());
        Assert.IsFalse(newSampleTile.CanAddObject(new Plant()));
        Assert.IsTrue(newSampleTile.CanAddObject(new Muteling()));

        newSampleTile.AddObject(new Muteling());
        Assert.IsFalse(newSampleTile.CanAddObject(new Muteling()));


    }

    [Test]
    public void AddObjectTest()
    {
        WorldTile sampleTile = new WorldTile();
        sampleTile.RemoveAllObjects();
        sampleTile.AddObject(new Plant());
        Assert.IsTrue(sampleTile.GetTileObjects()[0] is Plant);
        Assert.Throws<ArgumentException>(() => sampleTile.AddObject(new Plant()));
    }

    [Test]
    public void RemoveObjectTest()
    {
        WorldTile sampleTile = new WorldTile();
        sampleTile.RemoveAllObjects();
        //Basic Check
        sampleTile.AddObject(new Plant());
        sampleTile.AddObject(new Muteling());
        sampleTile.RemoveObject("Muteling");
        Assert.IsFalse(sampleTile.ContainsMuteling());
        Assert.IsTrue(sampleTile.ContainsPlant());
        sampleTile.RemoveObject("Plant");
        Assert.IsFalse(sampleTile.ContainsPlant());

        //Cap and Order Check
        sampleTile.AddObject(new Muteling());
        sampleTile.AddObject(new Plant());
        sampleTile.RemoveObject("PLaNt");
        Assert.IsTrue(sampleTile.ContainsMuteling());
        Assert.IsFalse(sampleTile.ContainsPlant());
        sampleTile.RemoveObject("mUteLinG");
        Assert.IsFalse(sampleTile.ContainsMuteling());

    }
}
