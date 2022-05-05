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
        public int Damage { get; set; }
        public int WeaponLevel { get; set; }
        public Image Icon { get; set; }
        public Image Image { get; set; }
        public int CoolDown { get; set; }
        public int CurrentCooldown { get; set; }

        public ProtectionBookWeapon()
        {
            Damage = 5;
            WeaponLevel = 1;
            Icon = View.Resources.protectionBook;
            Image = null;
            CoolDown = 20;
        }
    }
}
