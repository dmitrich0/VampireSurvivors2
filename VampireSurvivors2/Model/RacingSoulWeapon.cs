using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VampireSurvivors2
{
    internal class RacingSoulWeapon : IWeapon
    {
        public int BaseDamage { get; set; }
        public int Damage { get; set; }
        public int WeaponLevel { get; set; }
        public Image Icon { get; set; }
        public Image Image { get; set; }
        public int CoolDown { get; set; }
        public int CurrentCooldown { get; set; }
        public WorldModel World { get; set; }
        public string LevelUpDescription { get; set; }

        public RacingSoulWeapon(WorldModel world)
        {
            WeaponLevel = 1;
            Icon = View.Resources.bulletIcon;
            Image = null;
            CoolDown = 60;
            CurrentCooldown = 0;
            World = world;
            BaseDamage = 5;
            Damage = WeaponLevel * BaseDamage;
            LevelUpDescription = $"Shoots at the nearest enemy.";
        }

        public void CreateBullet()
        {
            CurrentCooldown++;
            IMonster target = null;
            var minVector = new Vector(double.MaxValue, double.MaxValue);
            foreach (var monster in World.Monsters)
            {
                var vector = new Vector(World.Player.CentralPosition.X - monster.CentralPosition.X,
                    World.Player.CentralPosition.Y - monster.CentralPosition.Y);
                if (vector.Length < minVector.Length)
                {
                    minVector = vector;
                    target = monster;
                }
            }
            if (target != null && CurrentCooldown > CoolDown)
            {
                Damage = WeaponLevel * BaseDamage;
                World.RacingSoulBullets.Add(new RacingSoulBullet(World.Player.Position, Damage, World, target));
                CurrentCooldown = 0;
            }
        }
    }
}
