using System.Drawing;

namespace VampireSurvivors2
{
    internal class Heart : Entity
    {
        public Heart(WorldModel world, PointF pos) : base(world, pos)
        {
            Position = pos;
            Value = 25;
            Image = View.Resources.heart;
            Size = new System.Windows.Size(Image.Width / 2, Image.Height / 2);
            Speed = 9;
        }
    }
}
