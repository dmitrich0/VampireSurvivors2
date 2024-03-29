﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using VampireSurvivors2.Model.Interfaces;
using VampireSurvivors2.Model.Weapons;

namespace VampireSurvivors2.Model
{
    internal class Player : IAnimarable
    {
        public Image[] Idle { get; set; }
        public Image[] Right { get; set; }
        public Image[] Left { get; set; }
        public Image[] Up { get; set; }
        public Image[] Down { get; set; }
        public int PickupRange { get; set; }
        public int MaxHealth { get; }
        public System.Windows.Size Size { get; }
        public float Speed { get; }
        public float Health { get; private set; }
        public PointF Position { get; private set; }
        public Image Image { get; private set; }
        public WorldModel World { get; }
        public Vector Direction { get; set; }
        public Animator Animator { get; set; }
        public double CurrentXp { get; set; }
        public double XpToNextLevel { get; set; }
        public int Level { get; set; }
        public int Killed { get; set; }
        public List<IWeapon> Weapons { get; set; }
        public ProtectionBookWeapon ProtectionBookWeapon { get; set; }
        public RacingSoulWeapon RacingSoulWeapon { get; set; }
        public DeathRingWeapon DeathRingWeapon { get; set; }
        public PointF CentralPosition => new PointF((float)(Position.X + Size.Width / 2), (float)(Position.Y + Size.Height / 2));

        public Player(WorldModel world)
        {
            CurrentXp = 0;
            XpToNextLevel = 70;
            Level = 1;
            Health = 100;
            Speed = 4;
            Position = new PointF(800f, 400f);
            MaxHealth = 100;
            World = world;
            Idle = new Image[] { View.Resources.idle };
            Right = new Image[] { View.Resources.walk1, View.Resources.walk2,
                View.Resources.walk3, View.Resources.walk4, View.Resources.walk5, View.Resources.walk6 };
            Left = Right.Select(Extensions.Flip).ToArray();
            Up = new Image[] { View.Resources.up1, View.Resources.up2, 
                View.Resources.up3, View.Resources.up4, View.Resources.up5, View.Resources.up6 };
            Down = new Image[] { View.Resources.down1, View.Resources.down2, 
                View.Resources.down3, View.Resources.down4, View.Resources.down5, View.Resources.down6 };
            Animator = new Animator(this);
            Image = Animator.GetCurrentFrame();
            Size = new System.Windows.Size(Image.Width * 2, Image.Height * 2);
            PickupRange = 50;
            ProtectionBookWeapon = new ProtectionBookWeapon();
            RacingSoulWeapon = null;
            DeathRingWeapon = null;
            Weapons = new List<IWeapon>() { ProtectionBookWeapon };
        }

        public void GetDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
                Application.Exit();
        }

        public bool CanMove(Vector direction)
        {
            var newPosition = new PointF((float)(Position.X + direction.X), (float)(Position.Y + direction.Y));
            return (!newPosition.X.EqualTo(0, 3) || !(direction.X < 0)) 
                   && (!newPosition.X.EqualTo(World.WorldWidth, 3) 
                       || !(direction.X > 0)) && (!newPosition.Y.EqualTo(0, 3) 
                                                  || !(direction.Y < 0)) 
                   && (!newPosition.Y.EqualTo(World.WorldHeight, 3) || !(direction.Y > 0));
        }

        public void Move(Vector direction)
        {
            if (!CanMove(direction)) return;
            Position = new PointF((float)(Position.X + Speed * direction.X), (float)(Position.Y + Speed * direction.Y));
            Direction = direction;
        }

        public void GetXp(int xp)
        {
            CurrentXp += xp;
            if (!(CurrentXp >= XpToNextLevel)) return;
            CurrentXp -= XpToNextLevel;
            Level++;
            XpToNextLevel *= 1.2;
        }

        public void GetHp(int hp)
        {
            Health += hp;
            Health = Health > 100 ? 100 : Health;
        }

        public void MakeAnim()
        {
            Image = Animator.GetCurrentFrame();
        }
    }
}
