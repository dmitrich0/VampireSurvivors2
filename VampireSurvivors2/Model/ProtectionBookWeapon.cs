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

        public int AttackRange { get; set; }

        public ProtectionBookWeapon()
        {
            WeaponLevel = 1;
            BaseDamage = 5;
            Damage = BaseDamage * WeaponLevel;
            Icon = View.Resources.protectionBook;
            Image = null;
            CoolDown = 20;
            AttackRange = 120 + 10 * WeaponLevel;
        }

        public int DoDamage(Vector vectorToTarget)
        {
            var rnd = new Random();
            var damage = Damage + rnd.Next((int)(Damage * 0.2), (int)(Damage * 0.2));
            if (vectorToTarget.Length <= AttackRange)
                if (CurrentCooldown == 0)
                {
                    CurrentCooldown++;
                    return damage;
                }
                else if (CurrentCooldown == CoolDown)
                    CurrentCooldown = 0;
                else
                    CurrentCooldown++;
            return 0;
        }
    }
}
