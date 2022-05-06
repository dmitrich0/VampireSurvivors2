using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VampireSurvivors2
{
    internal class Chest : IEntity
    {
        public Image Image { get; set; }
        public PointF Position { get; set; }
        public int Value { get; set; }
        public System.Windows.Size Size { get; }
        public float Speed { get; set; }
        WorldModel World { get; set; }
        public PointF CentralPosition
        {
            get
            {
                return new PointF((float)(Position.X + Size.Width / 2),
              (float)(Position.Y + Size.Height / 2));
            }
        }

        public Chest(WorldModel world, PointF pos)
        {
            World = world;
            Speed = 0;
            Position = pos;
            Image = View.Resources.chest;
        }

        public PointF Move(Vector vector)
        {
            return CentralPosition;
        }
    }
}
