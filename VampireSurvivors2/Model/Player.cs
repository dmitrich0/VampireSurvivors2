using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace VampireSurvivors2
{
    internal class Player
    {
        public PointF CentralPosition
        {
            get { return new PointF((float)(Position.X + Size.Width / 2),
                (float)(Position.Y + Size.Height / 2)); }
        }

        public int Damage { get; }
        public int CurrentCooldown { get; set; }
        public int Cooldown { get; set; }
        public float AttackRange { get; }
        public int MaxHealth { get; }
        public System.Windows.Size Size { get; }
        public float Speed { get; }
        public float Health { get; private set; }
        public PointF Position { get; private set; }
        public Image Image { get; }
        public WorldModel World { get; private set; }

        public Player(WorldModel world)
        {
            Image = View.Resources.idle_down;
            Health = 100;
            Speed = 5;
            Position = new PointF(800f, 400f);
            MaxHealth = 100;
            Size = new System.Windows.Size(Image.Width * 2, Image.Height * 2);
            CurrentCooldown = 0;
            Cooldown = 20;
            AttackRange = 120;
            Damage = 5;
        }

        public void GetDamage(int damage)
        {
            Health -= damage;
        }

        public bool CanMove(Vector direction)
        {
            var newPosition = new PointF((float)(Position.X + direction.X), (float)(Position.Y + direction.Y));
            if (newPosition.X.EqualTo(0, 3) && direction.X < 0 
                || newPosition.X.EqualTo(World.WorldWidth, 3) && direction.X > 0
                || newPosition.Y.EqualTo(0, 3) && direction.Y < 0
                || newPosition.Y.EqualTo(World.WorldHeight, 3) && direction.Y > 0)
                return false;
            return true;
        }

        public void Move(Vector direction)
        {
            //if (!CanMove(direction)) return;
            Position = new PointF((float)(Position.X + Speed * direction.X), (float)(Position.Y + Speed * direction.Y));
        }
    }
}
