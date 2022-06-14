using System;
using System.Drawing;
using System.Windows;
using VampireSurvivors2.Model.Monsters;

namespace VampireSurvivors2.Model.Weapons
{
    internal class RacingSoulBullet
    {
        private int Damage { get; }
        public Image Image { get; }
        private int Speed { get; }
        public PointF Position { get; set; }
        public System.Windows.Size Size { get; }
        public WorldModel World { get; }
        private Monster Target { get; }
        public Vector VectorToTarget => new Vector(Target.CentralPosition.X - Position.X, Target.CentralPosition.Y - Position.Y);

        public RacingSoulBullet(PointF pos, int damage, WorldModel world, Monster target)
        {
            Position = pos;
            Damage = damage;
            Image = View.Resources.bullet;
            Size = new System.Windows.Size(Image.Width, Image.Height);
            Speed = 9;
            World = world;
            this.Target = target;
        }

        public void MoveToTarget()
        {
            if (!World.Monsters.Contains(Target))
            {
                World.RacingSoulBullets.Remove(this);
                return;
            }
            var vector = new Vector(Target.CentralPosition.X - Position.X, Target.CentralPosition.Y - Position.Y);
            vector.Normalize();
            Position = new PointF((float)(Position.X + Speed * vector.X), (float)(Position.Y + Speed * vector.Y));
            TryToDamage();
        }

        public void TryToDamage()
        {
            var rnd = new Random();
            var damage = Damage + rnd.Next((int)(Damage * 0.2), (int)(Damage * 0.2));
            if (!(VectorToTarget.Length < 5)) return;
            Target.GetDamage(damage);
            World.RacingSoulBullets.Remove(this);
        }
    }
}
