using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VampireSurvivors2
{
    internal partial class GameWindow : Form
    {
        private readonly Stopwatch visibleTimer = new Stopwatch();
        private PrivateFontCollection myFontCollection = new PrivateFontCollection();
        private FontFamily myFont;
        private Color bgColor = Color.FromArgb(2, 85, 23);
        private Model.Player player = Model.World.Player;
        public Timer MainTimer = new Timer();
        public Timer MonsterTimer = new Timer(); // TO CHANGE
        public GameWindow()
        {
            myFontCollection.AddFontFile(@"C:\Users\ivano\source\repos\VampireSurvivors2\VampireSurvivors2\View\font.ttf");
            myFont = myFontCollection.Families[0];
            DoubleBuffered = true;
            InitializeComponent();
            MainTimer.Interval = 30;
            MonsterTimer.Interval = 2000; // TO CHANGE
            MonsterTimer.Tick += Model.World.SpawnMonster; // TO CHANGE
            MainTimer.Tick += new EventHandler(Update);
            MainTimer.Tick += Model.World.MoveMonsters; // TO CHANGE
            KeyDown += Model.World.Player.Move;
            MainTimer.Start();
            MonsterTimer.Start();
            visibleTimer.Start();
            WindowState = FormWindowState.Maximized;
            BackColor = bgColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawTime(g);
            DrawMonsters(g);
            DrawPlayerWithHP(g);
        }

        private void DrawPlayerWithHP(Graphics g)
        {
            g.DrawImage(Model.World.PlayerImage, player.Position.X, player.Position.Y,
                player.Size.Width, player.Size.Height);
            g.DrawRectangle(Pens.Black, 40, 50, player.MaxHealth * 2, 25);
            g.FillRectangle(Brushes.Red, 40, 50, player.Health * 2, 25);
            g.DrawEllipse(Pens.Gold, Model.Extenstions.GetCircleRect(player.CentralPosition.X,
                player.CentralPosition.Y, player.AttackRange));

        }

        private void DrawTime(Graphics g)
        {
            var seconds = (int)visibleTimer.Elapsed.TotalSeconds % 60;
            var minutes = (int)visibleTimer.Elapsed.TotalMinutes;
            var timePosition = new PointF((float)(ClientSize.Width / 2.15), 40);
            var totalTime = seconds % 60 >= 10 ? minutes.ToString() + ":" + seconds.ToString()
                : minutes.ToString() + ":0" + seconds.ToString();
            g.DrawString(totalTime, new Font(myFont, 32), Brushes.BlanchedAlmond, timePosition);
        }
        
        private void DrawMonsters(Graphics g)
        {
            foreach (var bat in Model.World.Bats.ToList())
            {
                var healthWidth = (bat.Health / bat.MaxHealth) * bat.Size.Width;
                g.DrawImage(Model.World.BatImage, bat.Position.X, bat.Position.Y, bat.Size.Width, bat.Size.Height);
                g.FillRectangle(Brushes.Red, bat.Position.X, bat.Position.Y + bat.Size.Height + 10,
                    healthWidth, 5);
                g.DrawRectangle(Pens.Black, bat.Position.X, bat.Position.Y + bat.Size.Height + 10,
                   bat.Size.Width, 5);
            }
        }

        public void Update(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
