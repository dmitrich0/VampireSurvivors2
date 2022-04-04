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
            var vector = new System.Windows.Vector(World.Player.Position.X + World.PlayerImage.Size.Width / 2 - Position.X,
                World.Player.Position.Y + World.PlayerImage.Size.Height / 2 - Position.Y);
            vector.Normalize();
            Position = new PointF(Position.X + (float)vector.X*Speed, Position.Y + (float)vector.Y * Speed);
        }
    }
}
