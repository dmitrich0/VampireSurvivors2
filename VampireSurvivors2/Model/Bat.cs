using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VampireSurvivors2
{
    internal class Bat : IAnimarable, IMonster
    {
        public Image[] Idle { get; set; }
        public Image[] Right { get; set; }
        public Image[] Left { get; set; }
        public Image[] Up { get; set; }
        public Image[] Down { get; set; }
        public int CurrentCooldown { get; set; }
        public int CoolDown { get; set; }
        public int AttackRange { get; set; }
        public int XP { get; set; }
        public Image Image { get; set; }
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public int Damage { get; set; }
        public Size Size { get; set; }
        public PointF Position { get; set; }
        public WorldModel World { get; set; }
        public System.Windows.Vector Direction { get; set; }
        public Animator Animator { get; set; }
        public PointF CentralPosition
        {
            get { return new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2); }
            set { ; }
        }

        public float Speed { get; set; }

        public Bat(WorldModel world, PointF position)
        {
            Idle = new Image[] { View.Resources.bat_face__3_ };
            Right = new Image[] { View.Resources.bat_face__1_, View.Resources.bat_face__2_,
                View.Resources.bat_face__3_, View.Resources.bat_face__4_ };
            Left = Right;
            Animator = new Animator(this);
            Image = Animator.GetCurrentFrame();
            Position = position;
            Health = 10;
            MaxHealth = 10;
            Speed = 2;
            Size = new Size((int)(Image.Width * 1.5), (int)(Image.Height * 1.5));
            Damage = 1;
            CurrentCooldown = 0;
            CoolDown = 5;
            AttackRange = 30;
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

        public void TryDamagePlayer(System.Windows.Vector vector)
        {
            if (vector.Length <= AttackRange)
            {
                if (CurrentCooldown == 0)
                {
                    World.Player.GetDamage(Damage);
                    CurrentCooldown++;
                }
                else if (CurrentCooldown == CoolDown)
                    CurrentCooldown = 0;
                else
                    CurrentCooldown++;
            }
        }

        public void TryToGetDamage(System.Windows.Vector vector)
        {
            var rnd = new Random();
            var damage = World.Player.Damage + rnd.Next((int)(-World.Player.Damage * 0.2), (int)(World.Player.Damage * 0.2));
            if (vector.Length <= World.Player.AttackRange)
                if (World.Player.CurrentCooldown == 0)
                {
                    GetDamage(damage, World);
                    World.Player.CurrentCooldown++;
                }
                else if (World.Player.CurrentCooldown == World.Player.Cooldown)
                    World.Player.CurrentCooldown = 0;
                else
                    World.Player.CurrentCooldown++;
        }

        public void GetDamage(int damage, WorldModel world)
        {
            Health -= damage;
            if (Health <= 0)
            {
                world.Monsters.Remove(this);
                world.Crystals.Add(new Crystal(CentralPosition, XP));
                World.Player.Killed++;
            }
        }

        public void MakeAnim()
        {
            Image = Animator.GetCurrentFrame();
        }
    }
}
