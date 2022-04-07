using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VampireSurvivors2.Model
{
    public class Bat
    {
        private float speed;
        private int currentCooldown;
        private readonly int coolDown;
        private readonly int attackRange;
        public float Health { get; set; }
        public float MaxHealth { get; }
        public int Damage { get; }
        public Size Size { get; set; }
        public PointF Position { get; set; }
        public PointF CentralPosition
        {
            get { return new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2); }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Bat(PointF position)
        {
            Position = position;
            Health = 10;
            MaxHealth = 10;
            speed = 2;
            Size = new Size(World.BatImage.Width, World.BatImage.Height);
            Damage = 1;
            currentCooldown = 0;
            coolDown = 30;
            attackRange = 30;
        }

        public void Move()
        {
            var direction = new System.Windows.Vector(World.Player.CentralPosition.X - CentralPosition.X,
                World.Player.CentralPosition.Y - CentralPosition.Y);
            TryDamagePlayer(direction);
            TryToGetDamage(direction);
            direction.Normalize();
            Position = new PointF(Position.X + (float)direction.X * Speed, Position.Y + (float)direction.Y * Speed);
        }

        private void TryDamagePlayer(System.Windows.Vector vector)
        {
            if (vector.Length <= attackRange)
            {
                if (currentCooldown == 0)
                {
                    World.Player.GetDamage(Damage);
                    currentCooldown++;
                }
                else if (currentCooldown == coolDown)
                    currentCooldown = 0;
                else
                    currentCooldown++;
            }
        }

        private void TryToGetDamage(System.Windows.Vector vector)
        {
            if (vector.Length <= World.Player.AttackRange)
                if (World.Player.CurrentCooldown == 0)
                {
                    GetDamage(World.Player.Damage);
                    World.Player.CurrentCooldown++;
                }
                else if (World.Player.CurrentCooldown == World.Player.Cooldown)
                    World.Player.CurrentCooldown = 0;
                else
                    World.Player.CurrentCooldown++;
        }

        private void GetDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
                World.Bats.Remove(this);
        }
    }
}
