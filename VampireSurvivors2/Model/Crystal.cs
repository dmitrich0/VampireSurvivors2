using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VampireSurvivors2
{
    internal class Crystal : IEntity
    {
        public Image Image { get; set; }
        public PointF Position { get; set; }
        public int Value { get; set; }
        public System.Windows.Size Size { get; }
        public float Speed { get; set; }
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
            Value = xp;
            Image = View.Resources.crystal2;
            Size = new System.Windows.Size(Image.Width, Image.Height);
            Speed = 9;
        }

        public PointF Move(Vector direction)
        {
            Position = new PointF((float)(Position.X + Speed * direction.X), (float)(Position.Y + Speed * direction.Y));
            return Position;
        }
    }
}
