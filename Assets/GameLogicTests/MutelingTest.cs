using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.GameLogic;


public class MutelingTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void MoveETest()
    {
        World world = new World(5, 5);
        Muteling mute = new Muteling();
        world.GetTileAt(2, 2).AddObject(mute);
        mute.Move(Direction.E);
        Assert.AreEqual(world.GetTileAt(3, 2), mute.Position);
    }

    [Test]
    public void MoveWTest()
    {
        World world = new World(5, 5);
        Muteling mute = new Muteling();
        world.GetTileAt(2, 2).AddObject(mute);
        mute.Move(Direction.W);
        Assert.AreEqual(world.GetTileAt(1, 2), mute.Position);
    }

    [Test]
    public void MoveNETest()
    {
        World world = new World(5, 5);
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
        World world = new World(5, 5);
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
        World world = new World(5, 5);
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
        World world = new World(5, 5);
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
        World world = new World(5, 5);
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
        Assert.AreEqual(plant1.Position, foodTiles1[0]);
        Assert.AreEqual(plant3.Position, foodTiles1[1]);
        Assert.AreEqual(plant2.Position, foodTiles1[2]);

        Assert.AreEqual(2, foodTiles2.Count);
        Assert.AreEqual(plant1.Position, foodTiles1[0]);
        Assert.AreEqual(plant3.Position, foodTiles1[1]);
    }


}
