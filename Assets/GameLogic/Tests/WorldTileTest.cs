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

        Muteling mute1 = new Muteling();
        Assert.IsTrue(newSampleTile.CanAddObject(mute1));
        newSampleTile.AddObject(mute1);
        Assert.IsFalse(newSampleTile.CanAddObject(new Muteling()));


    }

    [Test]
    public void AddObjectTest()
    {
        WorldTile sampleTile = new WorldTile();
        sampleTile.AddObject(new Plant());
        Assert.IsTrue(sampleTile.GetTileObjects()[0] is Plant);
        Assert.Throws<ArgumentException>(() => sampleTile.AddObject(new Plant()));
    }

    [Test]
    public void RemoveObjectTest()
    {
        WorldTile sampleTile = new WorldTile();
        //Basic Check
        Muteling mute1 = new Muteling();
        sampleTile.AddObject(new Plant());
        sampleTile.AddObject(mute1);
        sampleTile.RemoveObject(mute1);
        //sampleTile.RemoveAllObjects(typeof(Plant));
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
