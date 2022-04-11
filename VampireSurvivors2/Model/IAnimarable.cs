using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VampireSurvivors2
{
    internal interface IAnimarable
    {
        Vector Direction { get; set; }
        Animator Animator { get; set; }
        Image[] Idle { get; set; }
        Image[] Right { get; set; }
        Image[] Left { get; set; }
    }
}
