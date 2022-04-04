using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VampireSurvivors2.Model
{
    internal class Player
    {
        private Image _img;
        private PointF _position;
        private int _health;
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


        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public PointF Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Image Image
        {
            get { return _img; }
            set { _img = value; }
        }

        public Player()
        {
            _img = World.PlayerImage;
            _health = 100;
            _speed = 5;
            _position = new PointF(100f, 100f);
            _size = new Size(World.PlayerImage.Width * 2, World.PlayerImage.Height * 2);
        }

        public void GetDamage(int damage)
        {
            Health -= damage;
        }

        public void Move(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    Position = new PointF(Position.X, Position.Y - Speed);
                    break;
                case Keys.A:
                    Position = new PointF(Position.X - Speed, Position.Y);
                    break;
                case Keys.S:
                    Position = new PointF(Position.X, Position.Y + Speed);
                    break;
                case Keys.D:
                    Position = new PointF(Position.X + Speed, Position.Y);
                    break;
            }
        }
    }
}
