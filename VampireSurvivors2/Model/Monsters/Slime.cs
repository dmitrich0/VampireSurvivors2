using System.Drawing;
using System.Linq;

namespace VampireSurvivors2.Model.Monsters
{
    internal class Slime : Monster
    {
        public Slime(WorldModel world, PointF pos) : base(world, pos)
        {
            Idle = new Image[] { View.Resources.slime__1_ };
            Right = new Image[]
            {
                View.Resources.slime__1_, View.Resources.slime__2_, View.Resources.slime__3_, View.Resources.slime__4_,
                View.Resources.slime__5_, View.Resources.slime__6_
            };
            Left = Right.Select(Extensions.Flip).ToArray();
            Animator = new Animator(this);
            Image = Animator.GetCurrentFrame();
            Health = 90;
            MaxHealth = Health;
            Speed = 2.5f;
            Damage = 9;
            CoolDown = 5;
            AttackRange = 40;
            Xp = 75;
            Size = new Size((int)(Image.Width * 1.5), (int)(Image.Height * 1.5));
        }
    }
}
