using System.Drawing;

namespace VampireSurvivors2
{
    internal class Bat : Monster, IAnimarable
    {
        public Bat(WorldModel world, PointF pos) : base(world, pos)
        {
            Idle = new Image[] { View.Resources.bat_face__3_ };
            Right = new Image[] { View.Resources.bat_face__1_, View.Resources.bat_face__2_,
                View.Resources.bat_face__3_, View.Resources.bat_face__4_ };
            Left = Right;
            Animator = new Animator(this);
            Image = Animator.GetCurrentFrame();
            Health = 10;
            MaxHealth = Health;
            Speed = 1.5f;
            Damage = 2;
            CoolDown = 5;
            AttackRange = 30;
            XP = 10;
            Size = new System.Drawing.Size((int)(Image.Width * 1.5), (int)(Image.Height * 1.5));
        }
    }
}
