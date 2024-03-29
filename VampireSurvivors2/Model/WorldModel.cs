﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;
using VampireSurvivors2.Model.Entities;
using VampireSurvivors2.Model.Interfaces;
using VampireSurvivors2.Model.Monsters;
using VampireSurvivors2.Model.Weapons;

namespace VampireSurvivors2.Model
{
    internal class WorldModel
    {
        public List<Monster> Monsters;
        public Player Player;
        private readonly Random random;
        public float WorldHeight;
        public float WorldWidth;
        private int spawnCooldown;
        private int spawnCooldownRemaining;
        public int MonstersSpawned;
        public List<Entity> Entities;
        public List<RacingSoulBullet> RacingSoulBullets;
        public int LastPlayerLevel;
        private readonly int heartChance;
        private readonly int chestChance;
        public List<IWeapon> AllWeapons;

        public WorldModel(float width, float height, int spawnCooldown)
        {
            WorldHeight = height;
            WorldWidth = width;
            Monsters = new List<Monster>();
            Player = new Player(this);
            random = new Random();
            var timer = new Stopwatch();
            timer.Start();
            this.spawnCooldown = spawnCooldown;
            spawnCooldownRemaining = this.spawnCooldown;
            Entities = new List<Entity>();
            RacingSoulBullets = new List<RacingSoulBullet>();
            MonstersSpawned = 0;
            heartChance = 30;
            chestChance = 100;
            LastPlayerLevel = 1;
            AllWeapons = new List<IWeapon>
            { new RacingSoulWeapon(this), new DeathRingWeapon(this),
                new ProtectionBookWeapon() };
        }

        public void SpawnMonster()
        {
            if (spawnCooldownRemaining != 0)
            {
                spawnCooldownRemaining--;
                return;
            }
            var sideId = random.Next(0, 4);
            var x = 0f;
            var y = 0f;
            switch (sideId)
            {
                case 0:
                    y = -10f;
                    x = (float)random.NextDouble() * WorldWidth;
                    break;
                case 1:
                    y = (float)random.NextDouble() * WorldHeight;
                    x = WorldWidth + 10;
                    break;
                case 2:
                    y = WorldHeight + 10;
                    x = (float)random.NextDouble() * WorldWidth;
                    break;
                case 3:
                    y = (float)random.NextDouble() * WorldHeight;
                    x = -10f;
                    break;
            }
            var monsterPos = new PointF(x, y);
            var monsterId = random.Next(0, Player.Killed);
            if (monsterId > 90)
                Monsters.Add(new DeathEye(this, monsterPos));
            else if (monsterId > 60)
                Monsters.Add(new Slime(this, monsterPos));
            else if (monsterId > 48)
                Monsters.Add(new Ghost(this, monsterPos));
            else if (monsterId > 24)
                Monsters.Add(new Snake(this, monsterPos));
            else if (monsterId > 12)
                Monsters.Add(new Bee(this, monsterPos));
            else
                Monsters.Add(new Bat(this, monsterPos));
            spawnCooldownRemaining = spawnCooldown;
            if (Player.Killed % 8 == 0 && spawnCooldown != 1)
                spawnCooldown--;
        }

        public void MoveMonsters()
        {
            foreach (var monster in Monsters.ToList())
            {
                monster.Move();
                monster.MakeAnim();
            }
        }

        public bool IsLevelChanged()
        {
            if (LastPlayerLevel == Player.Level) return false;
            LastPlayerLevel = Player.Level;
            return true;
        }

        public void CheckDeathRing()
        {
            Player.DeathRingWeapon?.DoDamage();
        }

        public void CheckBullets()
        {
            Player.RacingSoulWeapon?.CreateBullet();
            foreach (var bullet in RacingSoulBullets.ToArray())
                bullet.MoveToTarget();
        }

        public void SpawnFeaturesAfterDying(Monster monster)
        {
            Monsters.Remove(monster);
            Player.Killed++;
            var rand = new Random();
            if (rand.Next(1, chestChance + 1) == 1)
            {
                var chest = new Chest(monster.CentralPosition);
                Entities.Add(chest);
            }
            else if (rand.Next(1, heartChance + 1) == 1)
            {
                var heart = new Heart(monster.CentralPosition);
                Entities.Add(heart);
            }
            else
            {
                var crystal = new Crystal(monster.CentralPosition, monster.Xp);
                Entities.Add(crystal);
            }
        }

        public void CheckEntities()
        {
            foreach (var entity in Entities.ToList())
            {
                var vector = new Vector(Player.CentralPosition.X - entity.CentralPosition.X,
                    Player.CentralPosition.Y - entity.CentralPosition.Y);
                if (!(vector.Length <= Player.PickupRange)) continue;
                vector.Normalize();
                var newEntityPos = entity.Move(vector);
                vector = new Vector(Player.CentralPosition.X - newEntityPos.X, Player.CentralPosition.Y - newEntityPos.Y);
                if (!(vector.Length <= 5)) continue;
                switch (entity)
                {
                    case Heart _:
                        Player.GetHp(entity.Value);
                        Entities.Remove(entity);
                        break;
                    case Crystal _:
                        Player.GetXp(entity.Value);
                        Entities.Remove(entity);
                        break;
                    case Chest _:
                        Player.GetXp((int)Player.XpToNextLevel - (int)Player.CurrentXp + 1);
                        Entities.Remove(entity);
                        break;
                }
            }
        }
    }
}
