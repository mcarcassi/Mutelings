using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.GameLogic;

public class PlantTest
{
    [Test]
    public void GrowResourceTest()
    {
        Plant bush = new Plant(Library.Instance.GetPlantTypeByName("Berry Bush"));
        bush.GrowResource();
        Assert.AreEqual(1, bush.ResourceCount());
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
