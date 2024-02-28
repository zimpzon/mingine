using System;

namespace Ming
{
    public static class MingAssert
    {
        public static void Bounds(int x, int y, int w, int h)
        {
            if (x < 0 || y < 0 || x >= w || y >= h)
                throw new ArgumentException($"out of bounds: x = {x}, y = {y}, w = {w}, h = {h}");
        }
    }
}
