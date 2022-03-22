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
}
