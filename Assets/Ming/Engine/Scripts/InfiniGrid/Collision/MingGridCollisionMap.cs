using UnityEngine;

namespace Ming
{
    public class MingGridCollisionMap
    {
        public int W, H;
        public byte[] Cells;

        public MingGridCollisionMap(int w, int h)
        {
            SetSize(w, h);
        }

        public void SetSize(int w, int h)
        {
            W = w;
            H = h;
            Cells = new byte[w * h];
        }

        public void UpdateFromTiles(ushort[] tileIds, MingGridTileRecipeCollection tileRecipes)
        {
            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    MingAssert.Bounds(x, y, W, H);
                    int idx = y * W + x;
                    ushort tileId = tileIds[idx];
                    MingGridTileRecipe recipe = tileRecipes.GetRecipe(tileId);
                    Cells[idx] = recipe.Walkable ? (byte)0 : (byte)1;
                }
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
                    byte value = Cells[idx];
                    Debug.DrawRay(new Vector2(x, y) + offset, Vector2.up * 0.25f, value == 0 ? Color.green : Color.red);
                }
            }
        }
    }
}
