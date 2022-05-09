using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VampireSurvivors2
{
    internal class Gods_Wrath : IWeapon
    {
        public int BaseDamage { get; set; }
        public int CurrentCooldown { get; set; }
        public Image Image { get; set; }
        public int WeaponLevel { get; set; }
        public int CoolDown { get; set; }
        public int Damage { get; set; }
        public Image Icon { get; set; }

        public Gods_Wrath()
        {
            BaseDamage = int.MaxValue;
            CoolDown = 120 - 5 * WeaponLevel;
            CurrentCooldown = 0;
            WeaponLevel = 1;
            Damage = int.MaxValue;
            Icon = View.Resources.wrath;
            Image = null;
        }

    }
}
