using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VampireSurvivors2
{
    internal class Crystal : Entity
    {
        public Crystal(WorldModel world, PointF pos, int xp) : base(world, pos)
        {
            Position = pos;
            Value = xp;
            Image = View.Resources.crystal2;
            Size = new System.Windows.Size(Image.Width, Image.Height);
            Speed = 9;
        }
    }
}
