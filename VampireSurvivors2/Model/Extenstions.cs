﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VampireSurvivors2.Model
{
    public static class Extenstions
    {
        public static bool EqualTo(this float value1, float value2, float epsilon)
        {
            return Math.Abs(value1 - value2) < epsilon;
        }
    }
}