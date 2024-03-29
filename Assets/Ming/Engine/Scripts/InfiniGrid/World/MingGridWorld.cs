﻿using Ming;
using UnityEngine;

public class MingGridWorld
{
    public int W, H;
    public RectInt WorldRect;
    public ushort[] TileIdLayer;
    public ushort[] PropsIdLayer;
    public MingGridCollisionMap CollisionMap;
    public MingGridFlow FlowFieldHome;
    public MingGridFlow FlowFieldResources;
    public MingGridLightmap LightmapActive;
    public MingGridLightmap LightmapWorking;

    private readonly MingGridTileRecipeCollection _tileRecipes;

    public MingGridWorld(int w, int h, MingGridTileRecipeCollection tileRecipes)
    {
        _tileRecipes = tileRecipes;

        W = w;
        H = h;
        SetSize(w, h);

        MingBuilderPerlinNoise.BuildCaves(TileIdLayer, W, H, valueWalkable: 0, valueSolid: 1);
        //MingBuilderRandom.Build(TileIdLayer, W, H, valueWalkable: 0, valueSolid: 1);
        //MingBuilderRandomHoles.Build(TileIdLayer, W, H, valueWalkable: 0, chanceWalkable: 0.1f);
        CollisionMap.UpdateFromTiles(TileIdLayer, _tileRecipes);

        CollisionMap.UpdateFromTiles(TileIdLayer, _tileRecipes);
        LightmapActive.Clear();
    }

    public void Update()
    {
        LightmapActive.Clear();
        LightmapActive.AddLight((int)PlayerScript.Pos.x, (int)PlayerScript.Pos.y, MingGridWorldRenderer.S_PlayerCol, MingTime.Time + 5 + Random.value * 2.0f);
        if (MingGridWorldRenderer.S_GrowLight)
        {
            LightmapActive.GrowLight(CollisionMap);
            LightmapActive.GrowLight(CollisionMap);

            //var tmp = LightmapActive;
            //LightmapActive = LightmapWorking;
            //LightmapWorking = tmp;
        }
    }

    public void SetSize(int w, int h)
    {
        W = w;
        H = h;
        WorldRect = new RectInt(0, 0, w, h);

        TileIdLayer = new ushort[w * h];
        PropsIdLayer = new ushort[w * h];

        CollisionMap = new MingGridCollisionMap(w, h);
        LightmapActive = new MingGridLightmap(w, h);
        LightmapWorking = new MingGridLightmap(w, h);

        FlowFieldHome = new MingGridFlow(w, h);
        FlowFieldResources = new MingGridFlow(w, h);
    }
}
