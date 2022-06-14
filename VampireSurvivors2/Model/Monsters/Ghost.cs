using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VampireSurvivors2
{
    internal class Ghost : Monster
    {
        public Ghost(WorldModel world, PointF pos) : base(world, pos)
        {
            Idle = new Image[] { View.Resources.ghost_left__1_ };
            Right = new Image[]
            {
                View.Resources.ghost_right__1_, View.Resources.ghost_right__2_, View.Resources.ghost_right__3_,
                View.Resources.ghost_right__4_
            };
            Left = Right.Select(x => Extensions.Flip(x)).ToArray();
            Animator = new Animator(this);
            Image = Animator.GetCurrentFrame();
            Health = 70;
            MaxHealth = Health;
            Speed = 2.5f;
            Damage = 6;
            CoolDown = 5;
            AttackRange = 40;
            Xp = 50;
            Size = new Size((int)(Image.Width * 1.5), (int)(Image.Height * 1.5));
        }
    }
}
