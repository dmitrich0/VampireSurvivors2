using NUnit.Framework;
using System;
using System.Windows;

namespace VampireSurvivors2
{
    [TestFixture]
    internal class GameTests
    {
        [Test]
        public void HighHPTest()
        {
            var world = new WorldModel(1920, 1080, 30);
            world.Player.GetHP(1000);
            if (world.Player.Health != 100)
                throw new Exception();
        }

        [Test]
        public void AddToMonsterListTest()
        {
            var world = new WorldModel(1920, 1080, 30);
            world.Monsters.Add(new Bee(world, new System.Drawing.PointF(100, 200)));
            if (world.Monsters.Count != 1)
                throw new Exception();
        }

        [Test]
        public void PlayerMoveTest()
        {
            var world = new WorldModel(1920, 1080, 30);
            var pos = new Vector(world.Player.Position.X, world.Player.Position.Y);
            var vector = new Vector(10, 20);
            vector.Normalize();
            world.Player.Move(vector);
            if (Math.Abs(world.Player.Position.X - pos.X + vector.X * world.Player.Speed) > 10
                || Math.Abs(world.Player.Position.Y - pos.Y + vector.Y * world.Player.Speed) > 10)
                throw new Exception();
        }

        [Test]
        public void GetXPTest()
        {
            var world = new WorldModel(1920, 1080, 30);
            world.Player.GetXP((int)world.Player.XPToNextLevel);
            if (world.Player.Level != 2)
                throw new Exception();
        }

        [Test]
        public void MonsterMoveTest()
        {
            var world = new WorldModel(1920, 1080, 30);
            world.Monsters.Add(new Bat(world, new System.Drawing.PointF(200, 300)));
            var monster = world.Monsters[0];
            var distance = new Vector(world.Player.Position.X - monster.Position.X,
                world.Player.Position.Y - monster.Position.Y).Length;
            var pos = new Vector(monster.Position.X, monster.Position.Y);
            monster.Move();
            var newDistance = new Vector(world.Player.Position.X - monster.Position.X,
                world.Player.Position.Y - monster.Position.Y).Length;
            if (distance <= newDistance)
                throw new Exception();
        }
    }
}
