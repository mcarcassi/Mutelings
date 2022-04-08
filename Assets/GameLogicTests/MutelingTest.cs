using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.GameLogic;


public class MutelingTest
{
    World world = new World(5, 5);
    // A Test behaves as an ordinary method
    [Test]
    public void MoveETest()
    {
        Muteling mute = new Muteling();
        world.GetTileAt(2, 2).AddObject(mute);
        mute.Move(Direction.E);
        Assert.AreEqual(world.GetTileAt(3, 2), mute.Position);
    }

    [Test]
    public void MoveWTest()
    {
        Muteling mute = new Muteling();
        world.GetTileAt(2, 2).AddObject(mute);
        mute.Move(Direction.W);
        Assert.AreEqual(world.GetTileAt(1, 2), mute.Position);
    }

    [Test]
    public void MoveNETest()
    {
        Muteling mute = new Muteling();
        world.GetTileAt(2, 2).AddObject(mute);
        mute.Move(Direction.NE);
        Assert.AreEqual(world.GetTileAt(2, 3), mute.Position);
        mute.Move(Direction.NE);
        Assert.AreEqual(world.GetTileAt(3, 4), mute.Position);

    }

    [Test]
    public void MoveNWTest()
    {
        Muteling mute = new Muteling();
        world.GetTileAt(2, 2).AddObject(mute);
        mute.Move(Direction.NW);
        Assert.AreEqual(world.GetTileAt(1, 3), mute.Position);
        mute.Move(Direction.NW);
        Assert.AreEqual(world.GetTileAt(1, 4), mute.Position);
    }

    [Test]
    public void MoveSETest()
    {
        Muteling mute = new Muteling();
        world.GetTileAt(2, 2).AddObject(mute);
        mute.Move(Direction.SE);
        Assert.AreEqual(world.GetTileAt(2, 1), mute.Position);
        mute.Move(Direction.SE);
        Assert.AreEqual(world.GetTileAt(3, 0), mute.Position);
    }

    [Test]
    public void MoveSWTest()
    {
        Muteling mute = new Muteling();
        world.GetTileAt(2, 2).AddObject(mute);
        mute.Move(Direction.SW);
        Assert.AreEqual(world.GetTileAt(1, 1), mute.Position);
        mute.Move(Direction.SW);
        Assert.AreEqual(world.GetTileAt(1, 0), mute.Position);
    }

    [Test]
    public void SensingTest()
    {
        Muteling mute1 = new Muteling();
        Muteling mute2 = new Muteling();
        Plant plant1 = new Plant(Library.Instance.GetPlantTypeByName("Redberry Bush"));
        Plant plant2 = new Plant(Library.Instance.GetPlantTypeByName("Redberry Bush"));
        Plant plant3 = new Plant(Library.Instance.GetPlantTypeByName("Redberry Bush"));
        world.GetTileAt(2, 2).AddObject(mute1);
        world.GetTileAt(0, 0).AddObject(mute2);

        world.GetTileAt(2, 2).AddObject(plant1);
        world.GetTileAt(4, 4).AddObject(plant2);
        world.GetTileAt(1, 1).AddObject(plant3);
        List<WorldTile> foodTiles1 = mute1.SenseFood(3);
        List<WorldTile> foodTiles2 = mute2.SenseFood(3);

        Assert.AreEqual(3, foodTiles1.Count);
        Assert.AreEqual(2, foodTiles1[0].X);
        Assert.AreEqual(2, foodTiles1[0].Y);
        Assert.AreEqual(1, foodTiles1[1].X);
        Assert.AreEqual(1, foodTiles1[1].Y);
        Assert.AreEqual(4, foodTiles1[2].X);
        Assert.AreEqual(4, foodTiles1[2].Y);

        Assert.AreEqual(2, foodTiles2.Count);
        Assert.AreEqual(1, foodTiles2[0].X);
        Assert.AreEqual(1, foodTiles2[0].Y);
        Assert.AreEqual(2, foodTiles2[1].X);
        Assert.AreEqual(2, foodTiles2[1].Y);




    }


}
