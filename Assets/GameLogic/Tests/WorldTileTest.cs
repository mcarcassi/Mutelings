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
    WorldTile sampleTile = new WorldTile();

    //Tests CanAddObject Method
    [Test]
    public void CanAddObjectTest()
    {
        Assert.IsTrue(sampleTile.CanAddObject(new Plant()));

        sampleTile.AddObject(new Plant());
        Assert.IsFalse(sampleTile.CanAddObject(new Plant()));
        Assert.IsTrue(sampleTile.CanAddObject(new Muteling()));

        sampleTile.AddObject(new Muteling());
        Assert.IsFalse(sampleTile.CanAddObject(new Muteling()));


    }

}
