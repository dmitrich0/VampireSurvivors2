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
        public PointF CentralPosition
        {
            get
            {
                return new PointF((float)(Position.X + Size.Width / 2),
              (float)(Position.Y + Size.Height / 2));
            }
        }

        public RacingSoulBullet(PointF pos, int damage, WorldModel world)
        {
            Position = pos;
            Damage = damage;
            Image = View.Resources.bullet;
            Size = new System.Windows.Size(Image.Width, Image.Height);
            Speed = 9;
            World = world;
        }

        public void Move()
        {
            var direction = new Vector(double.PositiveInfinity, double.PositiveInfinity);
            foreach (var monster in World.Monsters)
            {
                var vector = new Vector(monster.Position.X - World.Player.Position.X,
                    monster.Position.Y - World.Player.Position.Y);
                if (vector.Length < direction.Length)
                    direction = vector;
            }
            direction.Normalize();
            Position = new PointF((float)(Position.X + Speed * direction.X), (float)(Position.Y + Speed * direction.Y));
        }
    }
}
