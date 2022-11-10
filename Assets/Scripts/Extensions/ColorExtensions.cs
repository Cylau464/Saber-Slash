using UnityEngine;

namespace Extensions
{
    public static class ColorExtensions
    {
        public static Color ChangeAlpha(this Color color, float targetAlpha)
        {
            color.a = targetAlpha;
            return color;
        }
    }
}
