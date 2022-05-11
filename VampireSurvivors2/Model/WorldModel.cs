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
        public List<Chest> Chests;
        public int LastPlayerLevel;
        public int HeartChance { get; set; }
        public int ChestChance { get; set; }

        public WorldModel(float width, float height, int spawnCooldown)
        {
            WorldHeight = height;
            WorldWidth = width;
            Monsters = new List<IMonster>();
            Player = new Player(this);
            Random = new Random();
            Timer = new Stopwatch();
            Timer.Start();
            SpawnCooldown = spawnCooldown;
            SpawnCooldownRemaining = SpawnCooldown;
            Crystals = new List<Crystal>();
            Hearts = new List<Heart>();
            Entities = new List<IEntity>();
            RacingSoulBullets = new List<RacingSoulBullet>();
            Chests = new List<Chest>();
            MonstersSpawned = 0;
            HeartChance = 30;
            ChestChance = 100;
            LastPlayerLevel = 1;
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

        public void SpawnFeaturesAfterDying(IMonster monster)
        {
            Monsters.Remove(monster);
            Player.Killed++;
            var rand = new Random();
            if (rand.Next(1, ChestChance + 1) == 1)
            {
                var chest = new Chest(this, monster.CentralPosition);
                Chests.Add(chest);
                Entities.Add(chest);
            }
            else if (rand.Next(1, HeartChance + 1) == 1)
            {
                var heart = new Heart(monster.CentralPosition);
                Hearts.Add(heart);
                Entities.Add(heart);
            }
            else
            {
                var crystal = new Crystal(monster.CentralPosition, monster.XP);
                Crystals.Add(crystal);
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
                            Player.GetHP(entity.Value);
                            Hearts.Remove((Heart)entity);
                            Entities.Remove(entity);
                        }
                        else if (entity is Crystal)
                        {
                            Player.GetXP(entity.Value);
                            Crystals.Remove((Crystal)entity);
                            Entities.Remove(entity);
                        }
                    }
                }
            }
        }
    }
}
