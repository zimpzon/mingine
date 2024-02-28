using UnityEngine;

namespace Ming
{
    public class MingGridFlow
    {
        public int W, H;
        public float[] Flow;

        public MingGridFlow(int w, int h)
        {
            SetSize(w, h);
            Reset();
        }

        public void SetSize(int w, int h)
        {
            W = w;
            H = h;
            Flow = new float[w * h];
        }

        public void Reset()
        {
            for (int i = 0; i < Flow.Length; ++i)
                Flow[i] = ushort.MaxValue;
        }

        public void SetGoalValue(int x, int y, ushort value)
        {
            MingAssert.Bounds(x, y, W, H);
            Flow[y * W + x] = value;
        }

        public void DrawGizmos(Vector2 offset)
        {
            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    MingAssert.Bounds(x, y, W, H);
                    int idx = y * W + x;
                    float value = Flow[idx];
                    string txt = value > 999 ? "*" : MingIntToStrLut.GetString((int)value);
                    MingGizmo.Text(txt, new Vector2(x, y) + offset, Color.gray);
                }
            }
        }
    }
}
