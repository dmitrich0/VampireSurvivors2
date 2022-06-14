using System;
using System.Drawing;

namespace VampireSurvivors2.Model
{
    public static class Extensions
    {
        public static bool EqualTo(this float value1, float value2, float epsilon)
        {
            return Math.Abs(value1 - value2) < epsilon;
        }

        public static RectangleF GetCircleRect(float x, float y, float r)
        {
            return new RectangleF(x - r / 2, y - r / 2, r, r);
        }

        public static Image Flip(Image img)
        {
            var result = (Image)img.Clone();
            result.RotateFlip(RotateFlipType.RotateNoneFlipX);
            return result;
        }
    }
}
