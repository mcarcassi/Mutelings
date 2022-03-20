using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.GameLogic;

public class LibraryTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void GetTerrainTypeByNameTest()
    {
        Assert.IsNotNull(Library.Instance.GetTerrainTypeByName("Water"));
        Assert.AreEqual("Water", Library.Instance.GetTerrainTypeByName("Water").Name);

        Assert.IsNull(Library.Instance.GetTerrainTypeByName("Hello"));
    }

}
