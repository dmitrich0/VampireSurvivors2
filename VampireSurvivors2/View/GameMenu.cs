using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VampireSurvivors2.View
{
    public partial class GameMenu : Form
    {
        private PrivateFontCollection myFontCollection;
        public FontFamily myFont;
        public Timer MainTimer;
        private float titleDy;
        private bool isTitleGoingUp;
        private SoundPlayer MusicPlayer;
        public GameMenu()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FormBorderStyle = FormBorderStyle.None;
            DoubleBuffered = true;
            WindowState = FormWindowState.Maximized;
            base.OnLoad(e);
            myFontCollection = new PrivateFontCollection();
            myFontCollection.AddFontFile(@"C:\Users\ivano\source\repos\VampireSurvivors2\VampireSurvivors2\View\font2.otf");
            MusicPlayer = new SoundPlayer(@"C:\Users\ivano\source\repos\VampireSurvivors2\VampireSurvivors2\Resources\menuMusic.wav");
            myFont = myFontCollection.Families[0];
            this.Text = "Main Menu";
            SizeChanged += Update;
            MusicPlayer.PlayLooping();
            BackgroundImage = Resources.menuBg1;
            MainTimer = new Timer();
            MainTimer.Interval = 570;
            MainTimer.Tick += new EventHandler(Update);
            MainTimer.Start();
            titleDy = 3;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            DrawTitle(g);
        }

        private void DrawTitle(Graphics g)
        {
            titleDy = isTitleGoingUp ? titleDy : -titleDy;
            isTitleGoingUp = !isTitleGoingUp;
            var titlePostiton = new PointF(Size.Width / 4, Size.Height / 6 + titleDy);
            g.DrawString("Vampire Survivors 2", new Font(myFont, 54), Brushes.DarkRed, titlePostiton);
        }

        private void StartGame(object sender, EventArgs e)
        {
            Close();
            new GameWindow().Show();
        }

        public void Update(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
