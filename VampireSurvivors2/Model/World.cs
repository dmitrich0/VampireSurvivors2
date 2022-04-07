using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace VampireSurvivors2.Model
{
    internal static class World
    {
        public static Image PlayerImage = View.Resources.idle_down;
        public static Image BatImage = View.Resources.bat;
        public static List<Bat> Bats = new List<Bat>();
        public static Player Player = new Player();
        public static Random Random = new Random();
        public static Stopwatch stopwatch = new Stopwatch();

        public static void SpawnMonster(object sender, EventArgs e)
        {
            if (GameWindow.ActiveForm == null)
                return;
            var sideId = Random.Next(0, 4);
            var x = 0f;
            var y = 0f;
            switch (sideId)
            {
                case 0:
                    y = -10f;
                    x = (float)Random.NextDouble() * GameWindow.ActiveForm.ClientSize.Width;
                    break;
                case 1:
                    y = (float)Random.NextDouble() * GameWindow.ActiveForm.ClientSize.Height;
                    x = GameWindow.ActiveForm.ClientSize.Width + 10;
                    break;
                case 2:
                    y = GameWindow.ActiveForm.ClientSize.Height + 10;
                    x = (float)Random.NextDouble() * GameWindow.ActiveForm.ClientSize.Width;
                    break;
                case 3:
                    y = (float)Random.NextDouble() * GameWindow.ActiveForm.ClientSize.Height;
                    x = -10f;
                    break;
            }
            var batPosition = new PointF(x, y);
            var bat = new Bat(batPosition);
            Bats.Add(bat);
        }

        public static void MoveMonsters(object sender, EventArgs e)
        {
            foreach (var monster in Bats)
                monster.Move();
        }
    }
}
