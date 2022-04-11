using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VampireSurvivors2
{
    internal class Animator
    {
        public IAnimarable Entity { get; set; }
        public Image[] CurrentAnimation { get; set; }
        public int CurrentFrame { get; set; }
        private int CoolDown { get; set; }
        private int CurrentCoolDown { get; set; }
        public Animator(IAnimarable entity)
        {
            Entity = entity;
            CurrentAnimation = entity.Idle;
            CoolDown = 5;
            CurrentCoolDown = 0;
        }

        public Image GetCurrentFrame()
        {
            if (CurrentCoolDown != CoolDown)
            {
                CurrentCoolDown++;
                return CurrentAnimation[CurrentFrame % CurrentAnimation.Length];
            }
            else
            {
                CurrentCoolDown = 0;
                var oldCurrentAnim = CurrentAnimation;
                if (Entity.Direction.Length == 0)
                    CurrentAnimation = Entity.Idle;
                else if (Entity.Direction.X > 0)
                    CurrentAnimation = Entity.Right;
                else if (Entity.Direction.X < 0)
                    CurrentAnimation = Entity.Left;
                else
                    CurrentAnimation = Entity.Right;
                if (oldCurrentAnim == CurrentAnimation)
                {
                    CurrentFrame++;
                    return CurrentAnimation[CurrentFrame % CurrentAnimation.Length];
                }
                else
                {
                    CurrentFrame = 0;
                    return CurrentAnimation[CurrentFrame % CurrentAnimation.Length];
                }
            }
        }
    }
}
