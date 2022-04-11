using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VampireSurvivors2.View
{
    public partial class GameMenu : Form
    {
        private PrivateFontCollection myFontCollection;
        public FontFamily myFont;
        public GameMenu()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            base.OnLoad(e);
            myFontCollection = new PrivateFontCollection();
            myFontCollection.AddFontFile(@"C:\Users\ivano\source\repos\VampireSurvivors2\VampireSurvivors2\View\font.ttf");
            myFont = myFontCollection.Families[0];
            this.Text = "Main Menu";
            SizeChanged += Update;
            BackColor = Color.PeachPuff;
        }

        private void StartGame(object sender, EventArgs e)
        {
            new GameWindow().Show();
        }

        public void Update(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
