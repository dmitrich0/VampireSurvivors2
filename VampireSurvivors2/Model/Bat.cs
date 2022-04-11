using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VampireSurvivors2
{
    internal class Bat : IAnimarable
    {
        public Image[] Idle { get; set; }
        public Image[] Right { get; set; }
        public Image[] Left { get; set; }
        public Image[] Up { get; set; }
        public Image[] Down { get; set; }

        private float speed;
        private int currentCooldown;
        private readonly int coolDown;
        private readonly int attackRange;
        public int XP { get; set; }
        public Image Image { get; private set; }
        public float Health { get; set; }
        public float MaxHealth { get; }
        public int Damage { get; }
        public Size Size { get; set; }
        public PointF Position { get; set; }
        public WorldModel World { get; private set; }
        public System.Windows.Vector Direction { get; set; }
        public Animator Animator { get; set; }
        public PointF CentralPosition
        {
            get { return new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2); }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Bat(WorldModel world, PointF position)
        {
            Idle = new Image[] { View.Resources.bat_face__3_ };
            Right = new Image[] { View.Resources.bat_right__1_, View.Resources.bat_right__2_,
                View.Resources.bat_right__3_, View.Resources.bat_right__4_ };
            Left = new Image[] { View.Resources.bat_left__1_, View.Resources.bat_left__2_,
                View.Resources.bat_left__3_, View.Resources.bat_left__4_ };
            Up = new Image[] { View.Resources.bat_back__1_, View.Resources.bat_back__2_,
                View.Resources.bat_back__3_, View.Resources.bat_back__4_ };
            Down = new Image[] { View.Resources.bat_face__1_, View.Resources.bat_face__2_,
                View.Resources.bat_face__3_, View.Resources.bat_face__4_ };
            Animator = new Animator(this);
            Image = Animator.GetCurrentFrame();
            Position = position;
            Health = 10;
            MaxHealth = 10;
            speed = 2;
            Size = new Size(Image.Width, Image.Height);
            Damage = 1;
            currentCooldown = 0;
            coolDown = 30;
            attackRange = 30;
            World = world;
            XP = 10;
        }

        public void Move()
        {
            var direction = new System.Windows.Vector(World.Player.CentralPosition.X - CentralPosition.X,
                World.Player.CentralPosition.Y - CentralPosition.Y);
            Direction = direction;
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
                    GetDamage(World.Player.Damage, World);
                    World.Player.CurrentCooldown++;
                }
                else if (World.Player.CurrentCooldown == World.Player.Cooldown)
                    World.Player.CurrentCooldown = 0;
                else
                    World.Player.CurrentCooldown++;
        }

        private void GetDamage(int damage, WorldModel world)
        {
            Health -= damage;
            if (Health <= 0)
            {
                world.Bats.Remove(this);
                World.Player.GetXP(XP);
            }
        }

        public void MakeAnim()
        {
            Image = Animator.GetCurrentFrame();
        }
    }
}
