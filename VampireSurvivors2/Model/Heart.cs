using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VampireSurvivors2
{
    internal class Heart : IEntity
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

        public Heart(PointF pos)
        {
            Position = pos;
            Value = 25;
            Image = View.Resources.heart;
            Size = new System.Windows.Size(Image.Width / 2, Image.Height / 2);
            Speed = 9;
        }

        public void Move(Vector direction)
        {
            Position = new PointF((float)(Position.X + Speed * direction.X), (float)(Position.Y + Speed * direction.Y));
        }
    }
}
