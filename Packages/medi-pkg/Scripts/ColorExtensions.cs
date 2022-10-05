using UnityEngine;

namespace Medi
{
    public static class ColorExtensions
    {
        public static Vector3 ToVector3(this Color color)
        {
            return new Vector3(color.r, color.g, color.b);
        }
    }
}
