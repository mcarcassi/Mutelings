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


}
