using System.Drawing;
using VampireSurvivors2.Model.Interfaces;

namespace VampireSurvivors2.Model.Weapons
{
    internal class DeathRingWeapon : IWeapon
    {
        public int BaseDamage { get; set; }
        public int CurrentCooldown { get; set; }
        public Image Image { get; set; }
        public int WeaponLevel { get; set; }
        public int CoolDown { get; set; }
        public int Damage { get; set; }
        public Image Icon { get; set; }
        public WorldModel World { get; set; }
        public string LevelUpDescription { get; set; }

        public DeathRingWeapon(WorldModel world)
        {
            BaseDamage = int.MaxValue;
            CoolDown = 4200 - 165 * WeaponLevel; // 1 сек ~ 33 единицы CoolDown
            CurrentCooldown = 0;
            WeaponLevel = 1;
            Damage = BaseDamage;
            Icon = View.Resources.wrath;
            Image = null;
            World = world;
            LevelUpDescription = "Kills all enemies every 2 minutes.";
        }

        public void DoDamage()
        {
            CoolDown = 4200 - 165 * WeaponLevel;
            if (CurrentCooldown == 0)
            {
                CurrentCooldown++;
                foreach (var monster in World.Monsters.ToArray())
                    monster.GetDamage(Damage);
            }
            else if (CurrentCooldown == CoolDown)
                CurrentCooldown = 0;
            else
                CurrentCooldown++;
        }

    }
}
