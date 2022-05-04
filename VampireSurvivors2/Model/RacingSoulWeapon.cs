using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VampireSurvivors2
{
    internal class RacingSoulWeapon : IWeapon
    {
        public int Damage { get; set; }
        public int WeaponLevel { get; set; }
        public Image Icon { get; set; }
        public Image Image { get; set; }
        public int CoolDown { get; set; }
        public List<RacingSoulBullet> Bullets { get; set; }

        public RacingSoulWeapon()
        {
            Bullets = new List<RacingSoulBullet>();
            WeaponLevel = 1;
            Icon = View.Resources.bulletIcon;
            Image = null;
            CoolDown = 30;
        }
    }
}
