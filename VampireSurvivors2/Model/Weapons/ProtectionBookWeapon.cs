using System;
using System.Drawing;
using System.Windows;

namespace VampireSurvivors2
{
    internal class ProtectionBookWeapon : IWeapon
    {
        public int BaseDamage { get; set; }
        public int WeaponLevel { get; set; }
        public Image Icon { get; set; }
        public Image Image { get; set; }
        public int CoolDown { get; set; }
        public int CurrentCooldown { get; set; }
        public int Damage { get; set; }
        public string LevelUpDescription { get; set; }
        public int AttackRange { get; set; }

        public ProtectionBookWeapon()
        {
            WeaponLevel = 1;
            BaseDamage = 3;
            Damage = BaseDamage * WeaponLevel;
            Icon = View.Resources.protectionBook;
            Image = null;
            CoolDown = 40;
            AttackRange = 120 + 5 * WeaponLevel;
            LevelUpDescription = "Creates an aura around the player that deals damage.";
        }

        public int DoDamage(Vector vectorToTarget)
        {
            var rnd = new Random();
            Damage = BaseDamage * WeaponLevel;
            var damage = Damage + rnd.Next((int)(Damage * 0.2), (int)(Damage * 0.2));
            if (!(vectorToTarget.Length <= AttackRange)) return 0;
            if (CurrentCooldown == 0)
            {
                CurrentCooldown++;
                return damage;
            }
            if (CurrentCooldown == CoolDown)
                CurrentCooldown = 0;
            else
                CurrentCooldown++;
            return 0;
        }
    }
}
