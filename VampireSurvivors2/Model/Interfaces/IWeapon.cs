using System.Drawing;

namespace VampireSurvivors2.Model.Interfaces
{
    internal interface IWeapon
    {
        Image Icon { get; set; }
        string LevelUpDescription { get; set; }
    }
}
