using System.Drawing;

namespace VampireSurvivors2
{
    internal interface IWeapon
    {
        Image Icon { get; set; }
        string LevelUpDescription { get; set; }
    }
}
