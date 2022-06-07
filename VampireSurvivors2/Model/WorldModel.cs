using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;

namespace VampireSurvivors2
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
            AllWeapons = new List<IWeapon>() { new RacingSoulWeapon(this), new DeathRingWeapon(this), new ProtectionBookWeapon() };
        }

        public void SpawnMonster()
        {
            if (spawnCooldownRemaining != 0)
                spawnCooldownRemaining--;
            else
            {
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
                var monsterId = random.Next(0, 101);
                if (monsterId > 90)
                    Monsters.Add(new Snake(this, monsterPos));
                else if (monsterId > 80)
                    Monsters.Add(new Bee(this, monsterPos));
                else
                    Monsters.Add(new Bat(this, monsterPos));
                spawnCooldownRemaining = spawnCooldown;
                MonstersSpawned++;
                if (MonstersSpawned % 30 == 0 && spawnCooldown != 1)
                    spawnCooldown--;
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

        public bool IsLevelChanged()
        {
            if (LastPlayerLevel != Player.Level)
            {
                LastPlayerLevel = Player.Level;
                return true;
            }
            return false;
        }

        public void CheckDeathRing()
        {
            if (Player.DeathRingWeapon != null)
                Player.DeathRingWeapon.DoDamage();
        }

        public void CheckBullets()
        {
            if (Player.RacingSoulWeapon != null)
                Player.RacingSoulWeapon.CreateBullet();
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
                if (vector.Length <= Player.PickupRange)
                {
                    vector.Normalize();
                    var newEntityPos = entity.Move(vector);
                    vector = new Vector(Player.CentralPosition.X - newEntityPos.X, Player.CentralPosition.Y - newEntityPos.Y);
                    if (vector.Length <= 5)
                    {
                        if (entity is Heart)
                        {
                            Player.GetHp(entity.Value);
                            Entities.Remove(entity);
                        }
                        else if (entity is Crystal)
                        {
                            Player.GetXp(entity.Value);
                            Entities.Remove(entity);
                        }
                        else if (entity is Chest)
                        {
                            Player.GetXp((int)Player.XpToNextLevel - (int)Player.CurrentXp + 1);
                            Entities.Remove(entity);
                        }
                    }
                }
            }
        }
    }
}
