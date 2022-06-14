using System.Drawing;
using System.Linq;

namespace VampireSurvivors2.Model.Monsters
{
    internal class DeathEye : Monster
    {
        public DeathEye(WorldModel world, PointF pos) : base(world, pos)
        {
            Idle = new Image[] { View.Resources.deathEye__1_ };
            Left = new Image[]
            {
                View.Resources.deathEye__1_, View.Resources.deathEye__2_ , View.Resources.deathEye__3_, View.Resources.deathEye__4_,
            };
            Right = Left.Select(Extensions.Flip).ToArray();
            Animator = new Animator(this);
            Image = Animator.GetCurrentFrame();
            Health = 120;
            MaxHealth = Health;
            Speed = 2.5f;
            Damage = 15;
            CoolDown = 5;
            AttackRange = 45;
            Xp = 110;
            Size = new Size((int)(Image.Width * 2.4), (int)(Image.Height * 2.4));
        }
    }
}
