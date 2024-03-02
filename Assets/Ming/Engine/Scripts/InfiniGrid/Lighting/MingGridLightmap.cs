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

        public void AddLight(int x, int y, Color c, float expireTime)
        {
            Colors[y * W + x] = new Vector4(c.r, c.g, c.b, expireTime);
        }

        public void GrowLight(MingGridCollisionMap collision)
        {
            for (int y = 1; y < H - 1; ++y)
            {
                int lineStart = y * W;
                int lineEnd = (y * W) + W - 1;
                GrowLine(lineEnd, lineStart, -1, collision);
                GrowLine(lineStart, lineEnd, 1, collision);
            }

            for (int x = 1; x < W - 1; ++x)
            {
                int lineStart = x;
                int lineEnd = (H - 2) * W + x;
                GrowLine(lineEnd, lineStart, -W, collision);
                GrowLine(lineStart, lineEnd, W, collision);
            }
        }

        public Color GetColor(int x, int y)
        {
            Vector4 c = Colors[y * W + x];
            c.w = 1;
            return c;
        }

        private void GrowLine(int idxFirst, int idxLast, int stride, MingGridCollisionMap collision)
        {
            float airDecay = 0.8f;
            float wallDecay = 0.35f;

            const float Cutoff = 0.02f;

            Vector4 scan = Vector4.zero;
            bool hasRed = false;
            bool hasGreen = false;
            bool hasBlue = false;

            for (int idx = idxFirst; idx != idxLast + stride; idx += stride)
            {
                Vector4 col = Colors[idx];
                float decay = collision.Cells[idx] == 0 ? airDecay : wallDecay;

                // red
                if (col.x > scan.x)
                {
                    scan.x = col.x;
                    hasRed = true;
                }
                else if (hasRed)
                {
                    if (scan.x < Cutoff)
                    {
                        hasRed = false;
                    }
                    else
                    {
                        Colors[idx].x = scan.x;
                    }
                }

                if (hasRed)
                {
                    scan.x *= decay;
                }

                // green
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

                // blue
                if (col.z > scan.z)
                {
                    scan.z = col.z;
                    hasBlue = true;
                }
                else if (hasBlue)
                {
                    if (scan.z < Cutoff)
                    {
                        hasBlue = false;
                    }
                    else
                    {
                        Colors[idx].z = scan.z;
                    }
                }

                if (hasBlue)
                {
                    scan.z *= decay;
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < Colors.Length; ++i)
            {
                var col = Colors[i];
                float v = col.w;
                float magnitude = Mathf.Clamp01((v - MingTime.Time) * 5f);
                col *= magnitude;
                col.w = v;
                Colors[i] = col;
            }
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
