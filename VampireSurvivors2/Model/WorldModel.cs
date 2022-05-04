using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace VampireSurvivors2
{
    internal class WorldModel
    {
        public List<IMonster> Monsters;
        public Player Player;
        public Random Random;
        public Stopwatch Timer;
        public float WorldHeight;
        public float WorldWidth;
        public int SpawnCooldown;
        public int SpawnCooldownRemaining;
        public int MonstersSpawned;
        public List<IEntity> Entities;
        public List<Crystal> Crystals;
        public List<Heart> Hearts;
        public List<RacingSoulBullet> RacingSoulBullets;
        public int HeartChance { get; set; }

        public WorldModel(float width, float height, int spawnCooldown)
        {
            WorldHeight = height;
            WorldWidth = width;
            Monsters = new List<IMonster>();
            Player = new Player(this);
            Random = new Random();
            Timer = new Stopwatch();
            SpawnCooldown = spawnCooldown;
            Timer.Start();
            SpawnCooldownRemaining = SpawnCooldown;
            MonstersSpawned = 0;
            Crystals = new List<Crystal>();
            Hearts = new List<Heart>();
            Entities = new List<IEntity>();
            HeartChance = 30;
        }

        public void SpawnMonster()
        {
            if (SpawnCooldownRemaining != 0)
                SpawnCooldownRemaining--;
            else
            {
                var sideId = Random.Next(0, 4);
                var x = 0f;
                var y = 0f;
                switch (sideId)
                {
                    case 0:
                        y = -10f;
                        x = (float)Random.NextDouble() * WorldWidth;
                        break;
                    case 1:
                        y = (float)Random.NextDouble() * WorldHeight;
                        x = WorldWidth + 10;
                        break;
                    case 2:
                        y = WorldHeight + 10;
                        x = (float)Random.NextDouble() * WorldWidth;
                        break;
                    case 3:
                        y = (float)Random.NextDouble() * WorldHeight;
                        x = -10f;
                        break;
                }
                var monsterPos = new PointF(x, y);
                var monsterId = Random.Next(0, 5);
                if (monsterId != 0)
                    Monsters.Add(new Bat(this, monsterPos));
                else
                    Monsters.Add(new Snake(this, monsterPos));
                SpawnCooldownRemaining = SpawnCooldown;
                MonstersSpawned++;
                if (MonstersSpawned % 30 == 0 && SpawnCooldown != 1)
                    SpawnCooldown--;
            }
        }

        public void MoveMonsters()
        {
            foreach (var monster in Monsters.ToList())
            {
                monster.Move();
                monster.MakeAnim();
            }
        }

        //public void CheckBullets()
        //{
        //    foreach (var weapon in Player.Weapons) 
        //    {
        //        if (weapon is RacingSoulWeapon)
        //        {
        //            var currentWeapon = (RacingSoulWeapon)weapon;
        //            foreach (var bullet in currentWeapon.Bullets)
        //            {
        //                var vector = new Vector(Player.CentralPosition.X - bullet.CentralPosition.X, 
        //                    Player.CentralPosition.Y - bullet.CentralPosition.Y);
        //                if (vector.Length < 5)
        //                {

        //                }
        //            }
        //        }
        //        else
        //            return;
        //    }

        public void CheckEntities()
        {
            foreach (var entity in Entities.ToList())
            {
                var vector = new Vector(Player.CentralPosition.X - entity.CentralPosition.X,
                    Player.CentralPosition.Y - entity.CentralPosition.Y);
                if (vector.Length <= Player.PickupRange)
                {
                    vector.Normalize();
                    entity.Move(vector);
                    vector = new Vector(Player.CentralPosition.X - entity.CentralPosition.X,
                    Player.CentralPosition.Y - entity.CentralPosition.Y);
                    if (vector.Length <= 3)
                    {
                        if (entity is Heart)
                        {
                            Player.GetHP(entity.Value);
                            Hearts.Remove((Heart)entity);
                        }
                        else
                        {
                            Player.GetXP(entity.Value);
                            Crystals.Remove((Crystal)entity);
                        }
                        Entities.Remove(entity);
                    }
                }
            }
        }
    }
}
