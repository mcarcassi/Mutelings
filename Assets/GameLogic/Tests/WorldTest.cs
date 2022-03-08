using System.Collections;
using System.Collections.Generic;
using Assets.GameLogic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WorldTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void WorldGetSizeTest()
    {
        World world = new World(50, 75);
        // Use the Assert class to test conditions
        Assert.AreEqual(50, world.XSize);
        Assert.AreEqual(75, world.YSize);
    }


}
