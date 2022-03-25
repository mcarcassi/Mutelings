using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.GameLogic;

public class PlantTest
{

    World world = new World(75, 50);
    [Test]
    public void GrowResourceTest()
    {
        WorldTile newSampleTile = new WorldTile(world, 0, 0);
        Plant bush = new Plant(Library.Instance.GetPlantTypeByName("Berry Bush"));
        newSampleTile.AddObject(bush);
        bush.GrowResource();
        Assert.AreEqual(1, bush.ResourceCount());
        Assert.IsTrue(bush.Position.Contains(typeof(Resource)));
    }

    [Test]
    public void UpdateGrowthTest()
    {
        Plant bush = new Plant(Library.Instance.GetPlantTypeByName("Berry Bush"));
        Assert.AreEqual(1, bush.GrowthStage);
        bush.UpdateGrowth();
        Assert.AreEqual(1, bush.GrowthStage);
        bush.GrowthStage = 3;
        Assert.AreEqual(3, bush.GrowthStage);
        bush.UpdateGrowth();
        Assert.AreEqual(1, bush.GrowthStage);
    }

}
