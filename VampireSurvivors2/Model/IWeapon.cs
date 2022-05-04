using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VampireSurvivors2
{
    internal interface IWeapon
    {
        int Damage { get; set; }
        Image Icon { get; set; }
        int WeaponLevel { get; set; }
        Image Image { get; set; }
        int CoolDown { get; set; }
    }
}
