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
        public List<Crystal> Crystals;

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

        public void CheckCrystals()
        {
            foreach (var crystal in Crystals.ToList())
            {
                var vector = new Vector(crystal.CentralPosition.X - Player.CentralPosition.X,
                    crystal.CentralPosition.Y - Player.CentralPosition.Y);
                if (vector.Length <= Player.PickupRange)
                {
                    Player.GetXP(crystal.XP);
                    Crystals.Remove(crystal);
                }
            }
        }
    }
}
