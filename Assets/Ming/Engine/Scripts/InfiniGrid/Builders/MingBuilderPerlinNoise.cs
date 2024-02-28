using UnityEngine;

namespace Ming
{
    public static class MingBuilderPerlinNoise
    {
        // scale: Adjust for more or less detail in your noise
        // threshold: Adjust to make the caves larger or smaller
        public static void BuildCaves(ushort[] grid, int w, int h, byte valueWalkable, byte valueSolid, float scale = 0.1f, float threshold = 0.5f)
        {

            for (int y = 1; y < h - 1; y++)
            {
                for (int x = 1; x < w - 1; x++)
                {
                    int idx = y * w + x;
                    MingAssert.Bounds(x, y, w, h);
                    float xCoord = x * scale;
                    float yCoord = y * scale;
                    float sample = Mathf.PerlinNoise(xCoord, yCoord);
                    grid[idx] = sample > threshold ? valueSolid : valueWalkable;
                }
            }
        }
    }
}
