using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VampireSurvivors2.View;

namespace VampireSurvivors2
{
    public partial class NewLevelMenu : Form
    {
        private PrivateFontCollection myFontCollection;
        public FontFamily myFont;
        private SoundPlayer MusicPlayer;

        public NewLevelMenu()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            BackColor = Color.Gray;
            myFontCollection = new PrivateFontCollection();
            myFontCollection.AddFontFile(@"C:\Users\ivano\source\repos\VampireSurvivors2\VampireSurvivors2\View\font2.otf");
            MusicPlayer = new SoundPlayer(@"C:\Users\ivano\source\repos\VampireSurvivors2\VampireSurvivors2\Resources\menuMusic.wav");
            myFont = myFontCollection.Families[0];
            Text = "Chest Menu";
            MusicPlayer.PlayLooping();
            BackgroundImage = Resources.menuBg1;
        }
    }
}
