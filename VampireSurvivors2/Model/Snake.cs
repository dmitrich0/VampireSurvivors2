using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VampireSurvivors2
{
    internal class Snake : IAnimarable, IMonster
    {
        public Vector Direction { get; set; }
        public Animator Animator { get; set; }
        public Image[] Idle { get; set; }
        public Image[] Right { get; set; }
        public Image[] Left { get; set; }
        public Image[] Up { get; set; }
        public Image[] Down { get; set; }
        public float Speed { get; set; }
        public int CurrentCooldown { get; set; }
        public int CoolDown { get; set; }
        public int AttackRange { get; set; }
        public int XP { get; set; }
        public Image Image { get; set; }
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public int Damage { get; set; }
        public System.Drawing.Size Size { get; set; }
        public PointF Position { get; set; }
        public WorldModel World { get; set; }
        public PointF CentralPosition
        {
            get { return new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2); }
            set { ; }
        }

        public Snake(WorldModel world, PointF position)
        {
            Idle = new Image[] { View.Resources.snake__1_ };
            Right = new Image[] { View.Resources.snake__1_, View.Resources.snake__2_,
                View.Resources.snake__3_, View.Resources.snake__4_, View.Resources.snake__5_,
            View.Resources.snake__6_, View.Resources.snake__7_};
            Left = Right.Select(x => Extenstions.Flip(x)).ToArray();
            Up = Left.ToArray();
            Down = Left.ToArray();
            Animator = new Animator(this);
            Image = Animator.GetCurrentFrame();
            Position = position;
            Health = 20;
            MaxHealth = 20;
            Speed = 2.5f;
            Size = new System.Drawing.Size((int)(Image.Width * 1.3), (int)(Image.Height * 1.3));
            Damage = 3;
            CurrentCooldown = 0;
            CoolDown = 5;
            AttackRange = 30;
            World = world;
            XP = 15;
        }

        public void GetDamage(int damage, WorldModel world)
        {
            Health -= damage;
            var rand = new Random();
            if (Health <= 0)
            {
                world.Monsters.Remove(this);
                var crystal = new Crystal(CentralPosition, XP);
                world.Crystals.Add(crystal);
                world.Entities.Add(crystal);
                if (rand.Next(1, world.HeartChance + 1) == 1)
                {
                    var heart = new Heart(CentralPosition);
                    world.Hearts.Add(heart);
                    world.Entities.Add(heart);
                }
                World.Player.Killed++;
            }
        }
        public void MakeAnim()
        {
            Image = Animator.GetCurrentFrame();
        }

        public void Move()
        {
            var direction = new Vector(World.Player.CentralPosition.X - CentralPosition.X,
                World.Player.CentralPosition.Y - CentralPosition.Y);
            Direction = direction;
            TryDamagePlayer(direction);
            TryToGetBookDamage(direction);
            direction.Normalize();
            Position = new PointF(Position.X + (float)direction.X * Speed, Position.Y + (float)direction.Y * Speed);
        }

        public void TryDamagePlayer(Vector vector)
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

        public void TryToGetBookDamage(Vector vector)
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
    }
}
