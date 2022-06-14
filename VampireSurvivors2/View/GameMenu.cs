using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Media;
using System.Windows.Forms;

namespace VampireSurvivors2.View
{
    public partial class GameMenu : Form
    {
        private PrivateFontCollection myFontCollection;
        private FontFamily myFont;
        private Timer mainTimer;
        private float titleDy;
        private bool isTitleGoingUp;
        private SoundPlayer musicPlayer;
        public GameMenu()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            myFontCollection = new PrivateFontCollection();
            myFontCollection.AddFontFile(@"font.otf");
            musicPlayer = new SoundPlayer(Resources.menuMusic);
            myFont = myFontCollection.Families[0];
            Text = @"Main Menu";
            SizeChanged += Update;
            musicPlayer.PlayLooping();
            BackgroundImage = Resources.menuBg1;
            mainTimer = new Timer { Interval = 570 };
            mainTimer.Tick += Update;
            mainTimer.Start();
            titleDy = 3;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            DrawTitle(g);
            DrawPlayButton();
            DrawExitButton();
        }

        private void DrawTitle(Graphics g)
        {
            titleDy = isTitleGoingUp ? titleDy : -titleDy;
            isTitleGoingUp = !isTitleGoingUp;
            var titlePosition = new PointF(Size.Width / 4, Size.Height / 6 + titleDy);
            g.DrawString("Vampire Survivors 2", new Font(myFont, 54), Brushes.DarkRed, titlePosition);
        }

        private void DrawPlayButton()
        {
            var playButton = new Button() { Text = @"Go to battle", BackColor = Color.FromArgb(11, 52, 135),
                Font = new Font(myFont, 22), Height = 60, Width = 300, TextAlign = ContentAlignment.MiddleCenter};
            playButton.Location = new Point(Size.Width / 2 - playButton.Width / 2, Size.Height / 2);
            playButton.FlatStyle = FlatStyle.Flat;
            Controls.Add(playButton);
            playButton.Click += (s, e) => { new GameWindow().Show(); };
        }

        private void DrawExitButton()
        {
            var exitButton = new Button()
            {
                Text = @"Exit",
                BackColor = Color.FromArgb(11, 52, 135),
                Font = new Font(myFont, 22),
                Height = 60,
                Width = 300,
                TextAlign = ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Flat,
            };
            exitButton.Location = new Point(Size.Width / 2 - exitButton.Width / 2, Size.Height / 2 + 120);
            Controls.Add(exitButton);
            exitButton.Click += (s, e) => { Application.Exit(); };
        }

        public void Update(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
