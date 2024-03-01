using UnityEngine;

namespace Ming
{
    public class MingGridLightmap
    {
        public int W, H;
        private Vector4[] Colors;

        public MingGridLightmap(int w, int h)
        {
            SetSize(w, h);
        }

        public void SetSize(int w, int h)
        {
            W = w;
            H = h;
            Colors = new Vector4[w * h];
        }

        public void AddLight(int x, int y, Color c)
        {
            Colors[y * W + x] = c;
        }

        public void GrowLight(MingGridCollisionMap collision)
        {
            for (int y = 1; y < H - 1; ++y)
            {
                int lineStart = y * W;
                int lineEnd = (y * W) + W - 1;
                GrowLine(lineStart, lineEnd, 1, collision);
                //GrowLine(lineEnd, lineStart, -1, collision);
            }

            //for (int x = 1; x < W - 1; ++x)
            //    GrowLine(1, W - 1, 1, collision);
        }

        private void GrowLine(int idxFirst, int idxLast, int stride, MingGridCollisionMap collision)
        {
            float airDecay = 0.5f;
            float wallDecay = 0.15f;

            const float Cutoff = 0.02f;

            Vector4 scan = Vector4.zero;
            bool hasRed = false;
            bool hasGreen = false;
            bool hasBlue = false;

            for (int idx = idxFirst; idx < idxLast + stride; idx += stride)
            {
                Vector4 col = Colors[idx];
                float decay = collision.Cells[idx] == 0 ? airDecay : wallDecay;

                if (col.y > scan.y)
                {
                    scan.y = col.y;
                    hasGreen = true;
                }
                else if (hasGreen)
                {
                    if (scan.y < Cutoff)
                    {
                        hasGreen = false;
                    }
                    else
                    {
                        Colors[idx].y = scan.y;
                    }
                }

                if (hasGreen)
                {
                    scan.y *= decay;
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < Colors.Length; ++i)
                Colors[i] = new Vector4(0, 0, 0, 1);
        }

        public void DrawGizmos(Vector2 offset)
        {
            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    MingAssert.Bounds(x, y, W, H);
                    int idx = y * W + x;
                    Vector4 value = Colors[idx];
                    value.w = 1;
                    Debug.DrawRay(new Vector2(x, y) + offset, Vector2.up * 0.25f, value);
                }
            }
        }
    }
}
