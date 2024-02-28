using UnityEngine;

namespace Ming
{
    public static class MingBuilderRandom
    {
        public static void Build(ushort[] grid, int w, int h, byte valueWalkable, byte valueSolid, float chanceWalkable = 0.75f)
        {
            for (int y = 1; y < h - 1; y++)
            {
                for (int x = 1; x < w - 1; x++)
                {
                    int idx = y * w + x;
                    MingAssert.Bounds(x, y, w, h);
                    grid[idx] = Random.value < chanceWalkable ? valueWalkable: valueSolid;
                }
            }
        }
    }
}
