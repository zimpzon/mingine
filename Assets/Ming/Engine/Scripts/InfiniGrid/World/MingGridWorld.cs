using UnityEngine;

namespace Ming
{
    public class MingGridWorld
    {
        public int W;
        public int H;
        public RectInt WorldRect;

        public uint[] GridData;

        public MingGridWorld(int w, int h)
        {
            W = w;
            H = h;
            WorldRect = new RectInt(0, 0, w, h);
            GridData = new uint[w * h];

            for (int y = 1; y < h - 1; y++)
            {
                for (int x = 1; x < w - 1; x++)
                {
                    int idx = y * w + x;
                    GridData[idx] = Random.value < 0.15f ? 1U : 0U;
                }
            }
        }
    }
}
