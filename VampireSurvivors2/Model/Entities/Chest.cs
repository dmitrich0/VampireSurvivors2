using System.Drawing;

namespace VampireSurvivors2
{
    internal class Chest : Entity
    {
        public Chest(WorldModel world, PointF pos) : base(world, pos)
        {
            Speed = 0;
            Image = View.Resources.chest;
        }
    }
}
