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
        public PointF CentralPosition
        {
            get { return new PointF(Position.X + Size.Width / 2,
                Position.Y + Size.Height / 2); }
        }

        public int Damage { get; }
        public int CurrentCooldown { get; set; }
        public int Cooldown { get; set; }
        public float AttackRange { get; }
        public int MaxHealth { get; }
        public Size Size { get; }
        public float Speed { get; }
        public float Health { get; private set; }
        public PointF Position { get; private set; }
        public Image Image { get; }

        public Player()
        {
            Image = World.PlayerImage;
            Health = 100;
            Speed = 5;
            Position = new PointF(800f, 400f);
            MaxHealth = 100;
            Size = new Size(World.PlayerImage.Width * 2, World.PlayerImage.Height * 2);
            CurrentCooldown = 0;
            Cooldown = 20;
            AttackRange = 120;
            Damage = 5;
        }

        public void GetDamage(int damage)
        {
            Health -= damage;
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
