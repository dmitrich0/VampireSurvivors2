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
        private readonly Stopwatch _gameTimer = new Stopwatch();
        public Timer timer = new Timer();
        public Timer MonsterTimer = new Timer(); // TO CHANGE
        private PrivateFontCollection myFontCollection = new PrivateFontCollection();
        private FontFamily _myFont;
        private Color _bgcolor = Color.FromArgb(2, 85, 23);
        public GameWindow()
        {
            myFontCollection.AddFontFile(@"C:\Users\ivano\source\repos\VampireSurvivors2\VampireSurvivors2\View\font.ttf");
            _myFont = myFontCollection.Families[0];
            DoubleBuffered = true;
            InitializeComponent();
            timer.Interval = 30;
            MonsterTimer.Interval = 2000; // TO CHANGE
            MonsterTimer.Tick += Model.World.SpawnMonster; // TO CHANGE
            timer.Tick += new EventHandler(Update);
            timer.Tick += Model.World.MoveMonsters; // TO CHANGE
            KeyDown += Model.World.Player.Move;
            timer.Start();
            MonsterTimer.Start();
            _gameTimer.Start();
            WindowState = FormWindowState.Maximized;
            BackColor = _bgcolor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            string totalTime;
            g.DrawImage(Model.World.PlayerImage, Model.World.Player.Position.X, Model.World.Player.Position.Y,
                Model.World.Player.Size.Width, Model.World.Player.Size.Height);
            if ((int)_gameTimer.Elapsed.TotalSeconds % 60 >= 10)
                totalTime = ((int)_gameTimer.Elapsed.TotalMinutes).ToString() + ":" + ((int)_gameTimer.Elapsed.TotalSeconds % 60).ToString();
            else
                totalTime = ((int)_gameTimer.Elapsed.TotalMinutes).ToString() + ":0" + ((int)_gameTimer.Elapsed.TotalSeconds % 60).ToString();
           
            g.DrawString(totalTime, new Font(_myFont, 32), Brushes.BlanchedAlmond, new PointF((float)(ClientSize.Width / 2.15), 40));
            g.DrawRectangle(Pens.Black, 40, 50, 100 * 2, 25);
            g.FillRectangle(Brushes.Red, 40, 50, Model.World.Player.Health * 2, 25);
            foreach (var bat in Model.World.Bats)
                g.DrawImage(Model.World.BatImage, bat.Position.X, bat.Position.Y, bat.Size.Width, bat.Size.Height);
        }


        public void Update(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
