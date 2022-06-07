using System.Drawing;
using System.Linq;

namespace VampireSurvivors2
{
    internal class Snake : Monster
    {
        public Snake(WorldModel world, PointF position) : base(world, position)
        {
            Idle = new Image[] { View.Resources.snake__1_ };
            Right = new Image[] { View.Resources.snake__1_, View.Resources.snake__2_,
                View.Resources.snake__3_, View.Resources.snake__4_, View.Resources.snake__5_,
            View.Resources.snake__6_, View.Resources.snake__7_};
            Left = Right.Select(x => Extensions.Flip(x)).ToArray();
            Animator = new Animator(this);
            Image = Animator.GetCurrentFrame();
            Health = 40;
            MaxHealth = Health;
            Speed = 2.4f;
            Damage = 4;
            CoolDown = 5;
            AttackRange = 30;
            Xp = 15;
            Size = new Size((int)(Image.Width * 1.5), (int)(Image.Height * 1.5));
        }
    }
}
