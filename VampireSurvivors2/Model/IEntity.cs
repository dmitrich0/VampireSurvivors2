using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VampireSurvivors2
{
    internal interface IEntity
    {
        Image Image { get; set; }
        PointF Position { get; set; }
        int Value { get; set; }
        System.Windows.Size Size { get; }
        float Speed { get; set; }
        PointF CentralPosition { get; }
        PointF Move(Vector direction);
    }
}
