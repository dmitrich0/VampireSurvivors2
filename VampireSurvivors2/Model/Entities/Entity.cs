using System.Drawing;
using System.Windows;

namespace VampireSurvivors2
{
    internal class Entity
    {
        public Image Image { get; set; }
        public PointF Position { get; set; }
        public int Value { get; set; }
        public System.Windows.Size Size { get; protected set; }
        protected float Speed { get; set; }

        public PointF CentralPosition =>
            new PointF((float)(Position.X + Size.Width / 2),
                (float)(Position.Y + Size.Height / 2));

        public Entity(PointF pos)
        {
            Position = pos;
        }

        public PointF Move(Vector direction)
        {
            Position = new PointF((float)(Position.X + Speed * direction.X), (float)(Position.Y + Speed * direction.Y));
            return CentralPosition;
        }
    }
}
