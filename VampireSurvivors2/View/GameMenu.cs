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
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            myFontCollection = new PrivateFontCollection();
            myFontCollection.AddFontFile(@"..\..\View\font.otf");
            MusicPlayer = new SoundPlayer(@"..\..\Resources\menuMusic.wav");
            myFont = myFontCollection.Families[0];
            Text = "Main Menu";
            SizeChanged += Update;
            MusicPlayer.PlayLooping();
            BackgroundImage = Resources.menuBg1;
            MainTimer = new Timer { Interval = 570 };
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
            DrawPlayButton();
            DrawExitButton();
        }

        private void DrawTitle(Graphics g)
        {
            titleDy = isTitleGoingUp ? titleDy : -titleDy;
            isTitleGoingUp = !isTitleGoingUp;
            var titlePostiton = new PointF(Size.Width / 4, Size.Height / 6 + titleDy);
            g.DrawString("Vampire Survivors 2", new Font(myFont, 54), Brushes.DarkRed, titlePostiton);
        }

        private void DrawPlayButton()
        {
            var btnLocation = new Point(Size.Width / 2, Size.Height / 2);
            var playButton = new Button() { Text = "Go to battle", BackColor = Color.FromArgb(11, 52, 135),
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
                Text = "Exit",
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

        private void StartGame(object sender, EventArgs e)
        {
            MainTimer.Stop();
            Hide();
            new GameWindow().Show();
        }

        public void Update(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
