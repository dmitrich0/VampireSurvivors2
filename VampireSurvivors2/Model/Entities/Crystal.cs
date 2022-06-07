using System.Drawing;

namespace VampireSurvivors2
{
    internal class Crystal : Entity
    {
        public Crystal(PointF pos, int xp) : base(pos)
        {
            Position = pos;
            Value = xp;
            Image = View.Resources.crystal2;
            Size = new System.Windows.Size(Image.Width, Image.Height);
            Speed = 9;
        }
    }
}
