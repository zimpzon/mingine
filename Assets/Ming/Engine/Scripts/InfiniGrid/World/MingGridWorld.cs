using Ming;
using UnityEngine;

public class MingGridWorld
{
    public int W, H;
    public RectInt WorldRect;
    public ushort[] TileIdLayer;
    public ushort[] PropsIdLayer;
    public MingGridCollision Collision;
    public MingGridFlow FlowFieldHome;
    public MingGridFlow FlowFieldResources;

    private readonly MingGridTileRecipeCollection _tileRecipes;

    public MingGridWorld(int w, int h, MingGridTileRecipeCollection tileRecipes)
    {
        _tileRecipes = tileRecipes;

        W = w;
        H = h;
        SetSize(w, h);

        MingBuilderPerlinNoise.BuildCaves(TileIdLayer, W, H, valueWalkable: 0, valueSolid: 1);
        Collision.UpdateFromTiles(TileIdLayer, _tileRecipes);
    }

    public void SetSize(int w, int h)
    {
        W = w;
        H = h;
        WorldRect = new RectInt(0, 0, w, h);

        TileIdLayer = new ushort[w * h];
        PropsIdLayer = new ushort[w * h];

        Collision = new MingGridCollision(w, h);

        FlowFieldHome = new MingGridFlow(w, h);
        FlowFieldResources = new MingGridFlow(w, h);
    }

    public void DrawGizmos()
    {
        Collision.DrawGizmos(Vector2.zero);
    }
}
