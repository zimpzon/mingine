using UnityEngine;

namespace Ming
{
    // To get a Dijkstra map, you start with an integer array representing your map, with some set of goal cells set to zero and all the rest set
    // to a very high number.Iterate through the map's "floor" cells -- skip the impassable wall cells. If any floor tile has a value greater than
    // 1 regarding to its lowest-value floor neighbour (in a cardinal direction - i.e. up, down, left or right; a cell next to the one we are checking),
    // set it to be exactly 1 greater than its lowest value neighbor. Repeat until no changes are made. The resulting grid of numbers represents the
    // number of steps that it will take to get from any given tile to the nearest goal.

    // ideas:
    //   timestamp for last cell update to estimate validity? (for slow or partially updated fields)
    //   update a sub-area multiple times, ex when adding or moving tiles
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
