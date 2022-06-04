using System.Drawing;

namespace VampireSurvivors2
{
    internal interface IWeapon
    {
        int BaseDamage { get; set; }
        int Damage { get; set; }
        Image Icon { get; set; }
        int WeaponLevel { get; set; }
        Image Image { get; set; }
        int CoolDown { get; set; }
        int CurrentCooldown { get; set; }
        string LevelUpDescription { get; set; }
    }
}
