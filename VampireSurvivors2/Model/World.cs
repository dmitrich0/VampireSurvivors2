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
        public static Image PlayerImage = View.Resources.player;
        public static Image BatImage = View.Resources.bat;
        public static List<Bat> Bats = new List<Bat>();
        public static Player Player = new Player();
        public static Random Random = new Random();
        public static Stopwatch stopwatch = new Stopwatch();

        public static void SpawnMonster(object sender, EventArgs e)
        {
            if (GameWindow.ActiveForm == null)
                return;
            var x = Random.Next(-10, GameWindow.ActiveForm.ClientSize.Width + 10);
            var y = Random.Next(-10, GameWindow.ActiveForm.ClientSize.Height + 10);
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
