using UnityEngine;

public class MingGridWorld
{
    public int W, H;
    public RectInt WorldRect;
    public uint[] GridData;

    public MingGridWorld(int w, int h)
    {
        W = w;
        H = h;
        WorldRect = new RectInt(0, 0, w, h);
        GridData = new uint[w * h];

        float scale = 0.1f; // Adjust for more or less detail in your noise
        float threshold = 0.5f; // Adjust to make the caves larger or smaller

        for (int y = 1; y < h - 1; y++)
        {
            for (int x = 1; x < w - 1; x++)
            {
                int idx = y * w + x;
                float xCoord = (float)x * scale;
                float yCoord = (float)y * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                GridData[idx] = sample > threshold ? 1U : 0U; // Change the threshold to control fill percentage
            }
        }
    }
}
