using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

namespace Ming
{
    // sort order:
    //   floor tiles ("walls" are plain floor tiles, below a roof tile)
    //   props
    //   items
    //   npc's
    //   grounded projectiles
    //   player
    //   top part of tall props
    //   roof (only relevant where tile above is not a roof tile
    //   flying (incl. flying projectiles)

    // rendering:
    //    use sprite layers because particles (and possibly other things)

    /// <summary>
    /// 
    /// </summary>
    public class MingGridWorldRenderer : MingBehaviour
    {
        const int TileIdFloor = 0;
        const int TileIdWallSocket = 1;

        public float testX = 0;
        public float testY = 0;

        public float LightSize = 0.2f;
        public Color Color;
        public int ViewTileWidth = 30;
        public int ViewTileHeight = 20; 

        public string FloorSortingLayerName = "InfiniGridFloor";
        public int FloorSortingOrder = 0;
        public string RoofSortingLayerName = "InfiniGridRoof";
        public int RoofSortingOrder = 0;

        public MingGridTileRecipeCollection TileRecipeCollection;

        public Material FloorMaterial;
        public Material WallMaterial;

        private MingQuadRenderer _quadRendererFloorLayer;
        private MingQuadRenderer _quadRendererWallLayer;

        private MingGridWorld _world;
        private Transform _transform;

        private void OnDrawGizmos()
        {
            RectInt viewTileRect = MingGridUtil.GetViewTileRect(transform.position, ViewTileWidth, ViewTileHeight);
            MingGizmo.DrawRectangle(viewTileRect, MingConst.MingColor1, "gridView", MingConst.MingColorText1);
            if (_world == null)
            {
                MingGizmo.DrawRectangle(new RectInt(0, 0, 200, 200), MingConst.MingColor2, "editor-example-world-", MingConst.MingColorText1);
                return;
            }

            //_world.DrawGizmos();
            MingGizmo.DrawRectangle(_world.WorldRect, MingConst.MingColor2, "world", MingConst.MingColorText1);
        }

        private void Awake()
        {
            TileRecipeCollection.Init();

            _transform = transform;

            _world = new MingGridWorld(w: 100, h: 80, TileRecipeCollection);
            _quadRendererFloorLayer = CreateMingQuadMeshRenderer(FloorSortingLayerName, FloorSortingOrder);
            _quadRendererWallLayer = CreateMingQuadMeshRenderer(RoofSortingLayerName, RoofSortingOrder);
        }

        // we are a child go of the camera
        // from camera position find view tile rect we need to render
        // adjust our local position within the 1x1 to make it smooth
        private void Update()
        {
            PlayerScript.Color = Color;

            RectInt viewTileRect = MingGridUtil.GetViewTileRect(transform.position, ViewTileWidth, ViewTileHeight);
            Vector2Int gridBottomLeft = new Vector2Int(viewTileRect.x, viewTileRect.y);

            for (int y = ViewTileHeight - 2; y >= 1; y--)
            {
                for (int x = 1; x < ViewTileWidth - 1; x++)
                {
                    int worldX = gridBottomLeft.x + x;
                    int worldY = gridBottomLeft.y + y;
                    if (worldX >= _world.W || worldY >= _world.H || worldX < 0 || worldY < 0)
                    {
                        continue;
                    }

                    int idxWorld = worldY * _world.W + worldX;
                    uint tileId = _world.TileIdLayer[idxWorld]; ;
                    uint tileIdAbove = _world.TileIdLayer[idxWorld + _world.W];

                    MingGridTileRecipe tileRecipe = TileRecipeCollection.GetRecipe(tileId);

                    MingQuadRenderer quadRenderer = tileRecipe.RenderLayer == MingTileRenderLayer.Floor ?
                        _quadRendererFloorLayer :
                        _quadRendererWallLayer;

                    int idx = idxWorld;
                    // Sprites render with x, y at center, so currently tiles are centered at 0.5, 0.5!
                    bool L = _world.TileIdLayer[idx - 1] == TileIdWallSocket;
                    bool C = _world.TileIdLayer[idx] == TileIdWallSocket;
                    bool R = _world.TileIdLayer[idx + 1] == TileIdWallSocket;
                    bool BL = _world.TileIdLayer[idx - 1 - _world.W] == TileIdWallSocket;
                    bool B = _world.TileIdLayer[idx - _world.W] == TileIdWallSocket;
                    bool BR = _world.TileIdLayer[idx + 1 - _world.W] == TileIdWallSocket;
                    Vector2 pos = new Vector2(x + testX, y - testY);
                    if (L)
                        Debug.DrawRay(pos, Vector2.left * 0.25f, Color.yellow);
                    if (R)
                        Debug.DrawRay(pos, Vector2.right * 0.25f, Color.cyan);
                    if (B)
                        Debug.DrawRay(pos, Vector2.down * 0.25f, Color.green);
                    if (C)
                        Debug.DrawRay(pos, Vector2.up * 0.15f, Color.grey);
                    if (BL)
                        Debug.DrawRay(pos, (Vector2.down + Vector2.left).normalized * 0.25f, Color.magenta);
                    if (BR)
                        Debug.DrawRay(pos, (Vector2.down + Vector2.right).normalized * 0.25f, Color.red);

                    bool hasOverhang = !C && B;
                    if (hasOverhang)
                    {
                        uint overhangingTileId = _world.TileIdLayer[idxWorld - _world.W];
                        MingGridTileRecipe overhangingTile = TileRecipeCollection.GetRecipe(overhangingTileId);
                        if (!BL && BR)
                        {
                            AddToWallLayer(x, y, overhangingTile.RuleSprites[0], Color.white);
                        }
                        else if (BL && BR)
                        {
                            AddToWallLayer(x, y, overhangingTile.RuleSprites[1], Color.white);
                        }
                        else if (BL && !BR)
                        {
                            AddToWallLayer(x, y, overhangingTile.RuleSprites[2], Color.white);
                        }
                        else
                        {
                            AddToWallLayer(x, y, overhangingTile.RuleSprites[3], Color.white);
                        }
                    }
                    else
                    {

                    }

                    // set dist = distance to center of view rect
                    float distCenter = Vector2.Distance(new Vector2(worldX, worldY), viewTileRect.center);
                    float distPlayer = Vector2.Distance(new Vector2(worldX, worldY), PlayerScript.Pos);

                    //float v = 1.0f - (distCenter * 0.1f);
                    float v = 1.0f - (distPlayer * LightSize);
                    Color c = new Color(Color.r * v, Color.g * v, Color.b * v, 1.0f);

                    if (tileId == TileIdFloor)
                    {
                        AddToFloorLayer(x, y, tileRecipe.TileSprite, c);
                    }
                }
            }
        }

        private void AddToWallLayer(int x, int y, Sprite sprite, Color color)
        {
            _quadRendererWallLayer.AddQuad(
                center: new Vector3(x, y, 0),
                size: new Vector2(1, 1),
                rotationDegrees: 0,
                zSkew: 0,
                color,
                sprite,
                WallMaterial,
                gameObject.layer);
        }

        private void AddToFloorLayer(int x, int y, Sprite sprite, Color color)
        {
            _quadRendererFloorLayer.AddQuad(
                center: new Vector3(x, y, 0),
                size: new Vector2(1, 1),
                rotationDegrees: 0,
                zSkew: 0,
                color,
                sprite,
                FloorMaterial,
                gameObject.layer);
        }

        private MingQuadRenderer CreateMingQuadMeshRenderer(string sortingLayerName, int sortingOrder)
        {
            var go = new GameObject
            {
                layer = gameObject.layer,
                name = $"{nameof(MingGridWorldRenderer)} {sortingLayerName}/{sortingOrder}"
            };

            var quadRenderer = go.AddComponent<MingQuadRenderer>();
            quadRenderer.SortingLayerName = sortingLayerName;
            quadRenderer.SortingOrder = sortingOrder;
            go.transform.SetParent(this.transform, worldPositionStays: false);
            return quadRenderer;
        }
    }
}
