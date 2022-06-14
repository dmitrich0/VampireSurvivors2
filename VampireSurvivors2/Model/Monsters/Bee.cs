using System.Drawing;

namespace VampireSurvivors2.Model.Monsters
{
    internal class Bee : Monster
    {
        public Bee(WorldModel world, PointF pos) : base(world, pos)
        {
            Idle = new Image[] { View.Resources.bee };
            Right = new Image[] { View.Resources.bee };
            Left = new Image[] { View.Resources.bee2 }; ;
            Animator = new Animator(this);
            Image = Animator.GetCurrentFrame();
            Health = 20;
            MaxHealth = Health;
            Speed = 1.8f;
            Damage = 3;
            CoolDown = 5;
            AttackRange = 30;
            Xp = 20;
            Size = new Size((int)(Image.Width * 1.5), (int)(Image.Height * 1.5));
        }
    }
}
