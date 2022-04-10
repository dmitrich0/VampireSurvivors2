using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace VampireSurvivors2
{
    internal class WorldModel
    {
        public List<Bat> Bats;
        public Player Player;
        public Random Random;
        public Stopwatch Timer;
        public float WorldHeight;
        public float WorldWidth;
        public int SpawnCooldown;
        public int SpawnCooldownRemaining;
        public int MonstersSpawned;

        public WorldModel(float width, float height, int spawnCooldown)
        {
            WorldHeight = height;
            WorldWidth = width;
            Bats = new List<Bat>();
            Player = new Player(this);
            Random = new Random();
            Timer = new Stopwatch();
            SpawnCooldown = spawnCooldown;
            Timer.Start();
            SpawnCooldownRemaining = SpawnCooldown;
            MonstersSpawned = 0;
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
                var batPosition = new PointF(x, y);
                var bat = new Bat(this, batPosition);
                Bats.Add(bat);
                SpawnCooldownRemaining = SpawnCooldown;
                MonstersSpawned++;
                if (MonstersSpawned % 10 == 0 && SpawnCooldown != 1)
                    SpawnCooldown--;
            }
        }

        public void MoveMonsters()
        {
            foreach (var monster in Bats.ToList())
                monster.Move();
        }
    }
}
