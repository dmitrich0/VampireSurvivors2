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

        public int MaxHealth { get; }

        public Size Size
        {
            get { return _size; }
        }


        public float Speed
        {
            get { return _speed; }
        }


        public int Health
        {
            get { return _health; }

        }

        public PointF Position
        {
            get { return _position; }
        }

        public Image Image
        {
            get { return _img; }
        }

        public Player()
        {
            _img = World.PlayerImage;
            _health = 100;
            _speed = 5;
            _position = new PointF(800f, 400f);
            MaxHealth = 100;
            _size = new Size(World.PlayerImage.Width * 2, World.PlayerImage.Height * 2);
        }

        public void GetDamage(int damage)
        {
            _health -= damage;
        }

        public bool CanMove(KeyEventArgs key)
        {
            if (GameWindow.ActiveForm == null)
                return false;
            if (Math.Abs(Position.X - 0) < 3 && key.KeyCode == Keys.A
                || Math.Abs(Position.X + Size.Width - GameWindow.ActiveForm.ClientSize.Width) < 3 && key.KeyCode == Keys.D
                || Math.Abs(Position.Y - 0) < 3 && key.KeyCode == Keys.W
                || Math.Abs(Position.Y + Size.Height - GameWindow.ActiveForm.ClientSize.Height) < 3 && key.KeyCode == Keys.S)
                return false;
            return true;
        }

        public void Move(object sender, KeyEventArgs e)
        {
            if (!CanMove(e)) return;
            switch (e.KeyCode)
            {
                case Keys.W:
                    _position = new PointF(Position.X, Position.Y - Speed);
                    break;
                case Keys.A:
                    _position = new PointF(Position.X - Speed, Position.Y);
                    break;
                case Keys.S:
                    _position = new PointF(Position.X, Position.Y + Speed);
                    break;
                case Keys.D:
                    _position = new PointF(Position.X + Speed, Position.Y);
                    break;
            }
        }
    }
}
