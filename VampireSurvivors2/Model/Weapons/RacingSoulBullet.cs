using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VampireSurvivors2
{
    internal class RacingSoulBullet
    {
        public int Damage { get; set; }
        public Image Image { get; set; }
        public int Speed { get; set; }
        public PointF Position { get; set; }
        public System.Windows.Size Size { get; }
        public WorldModel World { get; set; }
        public IMonster Target { get; set; }
        public Vector VectorToTarget
        {
            get { return new Vector(Target.CentralPosition.X - Position.X, Target.CentralPosition.Y - Position.Y); }
        }
        public PointF CentralPosition
        {
            get
            {
                return new PointF((float)(Position.X + Size.Width / 2),
              (float)(Position.Y + Size.Height / 2));
            }
        }

        public RacingSoulBullet(PointF pos, int damage, WorldModel world, IMonster target)
        {
            Position = pos;
            Damage = damage;
            Image = View.Resources.bullet;
            Size = new System.Windows.Size(Image.Width, Image.Height);
            Speed = 9;
            World = world;
            Target = target;
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
            if (VectorToTarget.Length < 5)
            {
                Target.GetDamage(damage, World);
                World.RacingSoulBullets.Remove(this);
            }
        }
    }
}
