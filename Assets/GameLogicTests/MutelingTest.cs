using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.GameLogic;
using System;

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
        // G G G G P2
        //  G G G G G
        // G G M1:P1 G G
        //  G P3 G G G
        // M2 G G G G
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

    [Test]
    public void IsReachableTest()
    {
        // G P4 G G G
        //  W P3 W G G
        // P1 W W P2 G
        //  W G G G G
        // G G M G G
        World world = new World(5, 5);

        world.GetTileAt(0, 1).TerrainType = Library.Instance.GetTerrainTypeByName("Water");
        world.GetTileAt(1, 2).TerrainType = Library.Instance.GetTerrainTypeByName("Water");
        world.GetTileAt(2, 2).TerrainType = Library.Instance.GetTerrainTypeByName("Water");
        world.GetTileAt(0, 3).TerrainType = Library.Instance.GetTerrainTypeByName("Water");
        world.GetTileAt(2, 3).TerrainType = Library.Instance.GetTerrainTypeByName("Water");

        Muteling mute = new Muteling();
        Plant plant1 = new Plant(Library.Instance.GetPlantTypeByName("Redberry Bush"));
        Plant plant2 = new Plant(Library.Instance.GetPlantTypeByName("Redberry Bush"));
        Plant plant3 = new Plant(Library.Instance.GetPlantTypeByName("Redberry Bush"));
        Plant plant4 = new Plant(Library.Instance.GetPlantTypeByName("Redberry Bush"));
        world.GetTileAt(2, 0).AddObject(mute);

        world.GetTileAt(0, 2).AddObject(plant1);
        world.GetTileAt(3, 2).AddObject(plant2);
        world.GetTileAt(1, 3).AddObject(plant3);
        world.GetTileAt(1, 4).AddObject(plant4);

        Assert.IsFalse(mute.IsReachable(3, plant1.Position));
        Assert.IsTrue(mute.IsReachable(3, plant2.Position));
        Assert.IsFalse(mute.IsReachable(3, plant3.Position));
        Assert.Throws<ArgumentException>(() => mute.IsReachable(3, plant4.Position));

        // G P4 G G G
        //  W P3 W G G
        // P1 W W P2 G
        //  W G M G G
        // G G G G G
        mute.Move(Direction.NE);
        Assert.IsFalse(mute.IsReachable(3, plant1.Position));
        Assert.IsTrue(mute.IsReachable(3, plant2.Position));
        Assert.IsTrue(mute.IsReachable(3, plant3.Position));
        Assert.IsTrue(mute.IsReachable(3, plant4.Position));

        //Check if detect objects on same tile as reachable
        mute.Move(Direction.NE);
        Assert.IsTrue(mute.IsReachable(3, plant2.Position));

    }

    [Test]
    public void PlantScoreTest()
    {
        // G G G P P
        //  G G P P P
        // G G M P P
        //  G G W G G
        // G G G G P
        World world = new World(5, 5);
        world.GetTileAt(2, 1).TerrainType = Library.Instance.GetTerrainTypeByName("Water");

        world.GetTileAt(4, 0).AddObject(new Plant());
        world.GetTileAt(3, 2).AddObject(new Plant());
        world.GetTileAt(4, 2).AddObject(new Plant());
        world.GetTileAt(2, 3).AddObject(new Plant());
        world.GetTileAt(3, 3).AddObject(new Plant());
        world.GetTileAt(4, 3).AddObject(new Plant());
        world.GetTileAt(3, 4).AddObject(new Plant());
        world.GetTileAt(4, 4).AddObject(new Plant());

        Muteling mute = new Muteling();
        world.GetTileAt(2, 2).AddObject(mute);

        // 1*1 + 3*0.5 + 4*0.333 = 3.833
        Assert.AreEqual(3.833, mute.PlantScore(3, 3, Direction.E));
        // 1*1 + 3*0.5 + 3*0.333 = 3.5
        Assert.AreEqual(3.5, mute.PlantScore(3, 3, Direction.NE));
        // 0*1 + 1*0.5 + 3*0.333 = 1.5
        Assert.AreEqual(1.5, mute.PlantScore(3, 3, Direction.NW));
        // 0*1 + 0*0.5 + 1*0.333 = 0.333
        Assert.AreEqual(0.333, mute.PlantScore(3, 3, Direction.W));
        // 0*1 + 0*2 + 0*3 = 0
        Assert.AreEqual(0.0, mute.PlantScore(3, 3, Direction.SW));
        // Invalid move
        Assert.AreEqual(-1.0, mute.PlantScore(3, 3, Direction.SE));


    }
}
