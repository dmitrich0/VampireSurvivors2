using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VampireSurvivors2
{
    internal class Crystal
    {
        public Image Image { get; set; }
        public PointF Position { get; set; }
        public int XP { get; set; }
        public System.Windows.Size Size { get; }
        public PointF CentralPosition
        {
            get
            {
                return new PointF((float)(Position.X + Size.Width / 2),
              (float)(Position.Y + Size.Height / 2));
            }
        }

        public Crystal(PointF pos, int xp)
        {
            Position = pos;
            XP = xp;
            Image = View.Resources.crystal2;
            Size = new System.Windows.Size(Image.Width, Image.Height);
        }
    }
}
