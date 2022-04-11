using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Media;
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
        private SoundPlayer MusicPlayer;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            visibleTimer = new Stopwatch();
            myFontCollection = new PrivateFontCollection();
            bgColor = Color.FromArgb(112, 85, 23);
            MainTimer = new Timer();
            MainTimer.Interval = 30;
            world = new WorldModel(ClientSize.Width, ClientSize.Height, MainTimer.Interval);
            player = world.Player;
            MusicPlayer = new SoundPlayer(@"C:\Users\ivano\source\repos\VampireSurvivors2\VampireSurvivors2\Resources\music.wav");
            myFontCollection.AddFontFile(@"C:\Users\ivano\source\repos\VampireSurvivors2\VampireSurvivors2\View\font2.otf");
            myFont = myFontCollection.Families[0];
            BackColor = bgColor;
            MainTimer.Tick += new EventHandler(Update);
            MainTimer.Start();
            visibleTimer.Start();
            ActiveKeys = new List<Keys>();
            KeyDown += AddKeys;
            KeyUp += RemoveKeys;
            MusicPlayer.PlayLooping();
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
            var padding = 8;
            var XPWidth = (int)(((float)player.CurrentXP / (float)player.XPToNextLevel) * world.WorldWidth - 2 * padding);
            var levelPosition = new PointF(world.WorldWidth - 80, padding - 2);
            var XPRectangle = new Rectangle(padding, padding, XPWidth, 25);
            var XPBg = new Rectangle(padding - 2, padding - 2, (int)(world.WorldWidth - 2 * padding) + 4, 25 + 4);
            var XPBorder = new Rectangle(padding - 4, padding - 4, (int)(world.WorldWidth - 2 * padding) + 8, 25 + 8);

            g.DrawImage(player.Image, player.Position.X, player.Position.Y,
                (float)player.Size.Width, (float)player.Size.Height);

            g.DrawRectangle(Pens.Black, padding, 60, player.MaxHealth * 3, 25);
            g.FillRectangle(Brushes.Red, padding, 60, player.Health * 3, 25);

            g.FillRectangle(Brushes.Gold, XPBorder);
            g.FillRectangle(Brushes.Black, XPBg);
            g.FillRectangle(Brushes.Blue, XPRectangle);

            g.DrawEllipse(Pens.Gold, Extenstions.GetCircleRect(player.CentralPosition.X,
                player.CentralPosition.Y, player.AttackRange));
            g.DrawString("LV " + player.Level.ToString(), new Font(myFont, 14),
                Brushes.AntiqueWhite, levelPosition);
        }

        private void DrawTime(Graphics g)
        {
            var seconds = (int)visibleTimer.Elapsed.TotalSeconds % 60;
            var minutes = (int)visibleTimer.Elapsed.TotalMinutes;
            var timePosition = new PointF((float)(ClientSize.Width / 2.15), 30);
            var tagetTimePosition = new PointF((float)(ClientSize.Width / 2.082), 90);
            var totalTime = seconds % 60 >= 10 ? minutes.ToString() + ":" + seconds.ToString()
                : minutes.ToString() + ":0" + seconds.ToString();
            g.DrawString(totalTime, new Font(myFont, 36), Brushes.BlanchedAlmond, timePosition);
            g.DrawString("15:00", new Font(myFont, 18), Brushes.Gray, tagetTimePosition);
        }
        
        private void DrawMonsters(Graphics g)
        {
            foreach (var monster in world.Monsters.ToList())
            {
                var healthWidth = (monster.Health / monster.MaxHealth) * monster.Size.Width;
                g.DrawImage(monster.Image, monster.Position.X, monster.Position.Y, monster.Size.Width, monster.Size.Height);
                if (monster.Health != monster.MaxHealth)
                {
                    g.FillRectangle(Brushes.Red, monster.Position.X, monster.Position.Y + monster.Size.Height + 10,
                        healthWidth, 5);
                    g.DrawRectangle(Pens.Black, monster.Position.X, monster.Position.Y + monster.Size.Height + 10,
                       monster.Size.Width, 5);
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
                direction.Y += -1;
            if (ActiveKeys.Contains(Keys.A))
                direction.X += -1;
            if (ActiveKeys.Contains(Keys.S))
                direction.Y += 1;
            if (ActiveKeys.Contains(Keys.D))
                direction.X += 1;
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
