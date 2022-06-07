﻿using System.Drawing;

namespace VampireSurvivors2
{
    internal class Chest : Entity
    {
        public Chest(PointF pos) : base(pos)
        {
            Speed = 0;
            Image = View.Resources.chest;
        }
    }
}
