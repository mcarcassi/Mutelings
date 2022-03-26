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

    [Test]
    public void GetResourceTypeByNameTest()
    {
        Assert.IsNotNull(Library.Instance.GetResourceTypeByName("Redberry"));
        Assert.AreEqual("Redberry", Library.Instance.GetResourceTypeByName("Redberry").Name);

        Assert.IsNull(Library.Instance.GetResourceTypeByName("Hello"));
    }

    [Test]
    public void GetPlantTypeByNameTest()
    {
        Assert.IsNotNull(Library.Instance.GetPlantTypeByName("Redberry Bush"));
        Assert.AreEqual("Redberry Bush", Library.Instance.GetPlantTypeByName("Redberry Bush").Name);

        Assert.IsNull(Library.Instance.GetPlantTypeByName("Hello"));
    }

}
