using System.Drawing;
using System.Windows;
using VampireSurvivors2.Model.Interfaces;
using VampireSurvivors2.Model.Monsters;

namespace VampireSurvivors2.Model.Weapons
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
            CoolDown = 70;
            CurrentCooldown = 0;
            World = world;
            BaseDamage = 4;
            Damage = WeaponLevel * BaseDamage;
            LevelUpDescription = $"Shoots at the nearest enemy.";
        }

        public void CreateBullet()
        {
            CurrentCooldown++;
            Monster target = null;
            var minVector = new Vector(double.MaxValue, double.MaxValue);
            foreach (var monster in World.Monsters)
            {
                var vector = new Vector(World.Player.CentralPosition.X - monster.CentralPosition.X,
                    World.Player.CentralPosition.Y - monster.CentralPosition.Y);
                if (!(vector.Length < minVector.Length)) continue;
                minVector = vector;
                target = monster;
            }
            if (target == null || CurrentCooldown <= CoolDown) return;
            Damage = BaseDamage + BaseDamage / 2 * (WeaponLevel - 1);
            World.RacingSoulBullets.Add(new RacingSoulBullet(World.Player.Position, Damage, World, target));
            CurrentCooldown = 0;
        }
    }
}
