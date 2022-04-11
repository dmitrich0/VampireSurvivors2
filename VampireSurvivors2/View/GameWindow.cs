using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace VampireSurvivors2
{
    internal partial class GameWindow : Form
    {
        private WorldModel world;
        private Stopwatch visibleTimer;
        private PrivateFontCollection myFontCollection;
        private FontFamily myFont;
        private Color bgColor;
        private Player player;
        public Timer MainTimer;
        private List<Keys> ActiveKeys;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            visibleTimer = new Stopwatch();
            myFontCollection = new PrivateFontCollection();
            bgColor = Color.FromArgb(2, 85, 23);
            MainTimer = new Timer();
            MainTimer.Interval = 30;
            world = new WorldModel(ClientSize.Width, ClientSize.Height, MainTimer.Interval);
            player = world.Player;
            myFontCollection.AddFontFile(@"C:\Users\ivano\source\repos\VampireSurvivors2\VampireSurvivors2\View\font.ttf");
            myFont = myFontCollection.Families[0];
            BackColor = bgColor;
            MainTimer.Tick += new EventHandler(Update);
            MainTimer.Start();
            visibleTimer.Start();
            ActiveKeys = new List<Keys>();
            KeyDown += AddKeys;
            KeyUp += RemoveKeys;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            DrawTime(g);
            DrawMonsters(g);
            DrawPlayerAndHUD(g);
        }

        private void DrawPlayerAndHUD(Graphics g)
        {
            var paddingXP = 50f;
            var XPWidth = ((float)player.CurrentXP / (float)player.XPToNextLevel) * world.WorldWidth - 10;
            var levelPosition = new PointF(world.WorldWidth - 200, 50);
            g.DrawImage(player.Image, player.Position.X, player.Position.Y,
                (float)player.Size.Width, (float)player.Size.Height);
            g.DrawRectangle(Pens.Black, 40, 60, player.MaxHealth * 3, 25);
            g.FillRectangle(Brushes.Red, 40, 60, player.Health * 3, 25);
            g.FillRectangle(Brushes.Blue, paddingXP, 20, XPWidth, 10);
            g.DrawRectangle(Pens.Black, paddingXP, 20, world.WorldWidth - paddingXP*2, 10);
            g.DrawEllipse(Pens.Gold, Extenstions.GetCircleRect(player.CentralPosition.X,
                player.CentralPosition.Y, player.AttackRange));
            g.DrawString("Level: " + player.Level.ToString(), new Font(myFont, 24),
                Brushes.Aquamarine, levelPosition);
        }

        private void DrawTime(Graphics g)
        {
            var seconds = (int)visibleTimer.Elapsed.TotalSeconds % 60;
            var minutes = (int)visibleTimer.Elapsed.TotalMinutes;
            var timePosition = new PointF((float)(ClientSize.Width / 2.15), 60);
            var tagetTimePosition = new PointF((float)(ClientSize.Width / 2.1), 110);
            var totalTime = seconds % 60 >= 10 ? minutes.ToString() + ":" + seconds.ToString()
                : minutes.ToString() + ":0" + seconds.ToString();
            g.DrawString(totalTime, new Font(myFont, 36), Brushes.BlanchedAlmond, timePosition);
            g.DrawString("15:00", new Font(myFont, 20), Brushes.Gray, tagetTimePosition);
        }
        
        private void DrawMonsters(Graphics g)
        {
            foreach (var bat in world.Bats.ToList())
            {
                var healthWidth = (bat.Health / bat.MaxHealth) * bat.Size.Width;
                g.DrawImage(bat.Image, bat.Position.X, bat.Position.Y, bat.Size.Width, bat.Size.Height);
                if (bat.Health != bat.MaxHealth)
                {
                    g.FillRectangle(Brushes.Red, bat.Position.X, bat.Position.Y + bat.Size.Height + 10,
                        healthWidth, 5);
                    g.DrawRectangle(Pens.Black, bat.Position.X, bat.Position.Y + bat.Size.Height + 10,
                       bat.Size.Width, 5);
                }
            }
        }

        private void AddKeys(object sender, KeyEventArgs e)
        {
            if (!ActiveKeys.Contains(e.KeyCode))
                ActiveKeys.Add(e.KeyCode);
        }

        private void RemoveKeys(object sender, KeyEventArgs e)
        {
            if (ActiveKeys.Contains(e.KeyCode))
                ActiveKeys.Remove(e.KeyCode);
        }

        private Vector GetPlayerDirection()
        {
            var direction = new Vector();
            if (ActiveKeys.Contains(Keys.W))
                direction.Y = -1;
            if (ActiveKeys.Contains(Keys.A))
                direction.X = -1;
            if (ActiveKeys.Contains(Keys.S))
                direction.Y = 1;
            if (ActiveKeys.Contains(Keys.D))
                direction.X = 1;
            if (direction.X == 0 && direction.Y == 0)
                return new Vector(0, 0);
            direction.Normalize();
            return direction;
        }

        public void Update(object sender, EventArgs e)
        {
            world.SpawnMonster();
            world.MoveMonsters();
            player.Move(GetPlayerDirection());
            player.MakeAnim();
            Invalidate();
        }
    }
}
