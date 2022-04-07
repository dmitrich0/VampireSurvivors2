using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VampireSurvivors2.Model
{
    public class Bat
    {
        private int _health;
        private PointF _position;
        private float _speed;
        private Size _size;
        private int _currentCooldown;
        private readonly int _cooldown;
        private readonly int _attackRange;

        public int Damage { get; }

        public Size Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }


        public Bat(PointF position)
        {
            _position = position;
            _health = 10;
            _speed = 2;
            _size = new Size(World.BatImage.Width, World.BatImage.Height);
            Damage = 1;
            _currentCooldown = 0;
            _cooldown = 30;
            _attackRange = 30;
        }

        public PointF Position
        {
            get { return _position; }
            set { _position = value; }
        }


        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public void Move()
        {
            var vector = new System.Windows.Vector((World.Player.Position.X + World.PlayerImage.Size.Width / 2)
                - (Position.X + World.BatImage.Width / 2),
                World.Player.Position.Y + World.PlayerImage.Size.Height / 2
                - ((Position.Y + World.BatImage.Height / 2)));
            vector.Normalize();
            Position = new PointF(Position.X + (float)vector.X * Speed, Position.Y + (float)vector.Y * Speed);
            if (Position.X.EqualTo(World.Player.Position.X + World.PlayerImage.Width / 2, _attackRange)
                && Position.Y.EqualTo(World.Player.Position.Y + World.PlayerImage.Height / 2, _attackRange))
            {
                if (_currentCooldown == 0)
                {
                    World.Player.GetDamage(Damage);
                    _currentCooldown++;
                }
                else if (_currentCooldown == _cooldown)
                    _currentCooldown = 0;
                else
                    _currentCooldown++;
            }
        }
    }
}
