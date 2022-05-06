using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ProtectionBookWeapon()
        {
            WeaponLevel = 1;
            BaseDamage = 5;
            Damage = BaseDamage * WeaponLevel;
            Icon = View.Resources.protectionBook;
            Image = null;
            CoolDown = 20;
        }
    }
}
