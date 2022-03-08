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


}
