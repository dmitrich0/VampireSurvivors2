using System.Drawing;
using VampireSurvivors2.Model.Interfaces;

namespace VampireSurvivors2.Model
{
    internal class Animator
    {
        private IAnimarable Entity { get; }
        private Image[] CurrentAnimation { get; set; }
        private int CurrentFrame { get; set; }
        private int CoolDown { get; }
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
            CurrentFrame = 0;
            return CurrentAnimation[CurrentFrame % CurrentAnimation.Length];
        }
    }
}
