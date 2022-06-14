using System.Drawing;
using System.Windows;

namespace VampireSurvivors2.Model.Interfaces
{
    internal interface IAnimarable
    {
        Vector Direction { get; set; }
        Image[] Idle { get; set; }
        Image[] Right { get; set; }
        Image[] Left { get; set; }
    }
}
