using UnityEngine;

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

        private MingGridWorld _mingGridWorld;
        private Transform _transform;

        private void OnDrawGizmos()
        {
            RectInt viewTileRect = MingGridUtil.GetViewTileRect(transform.position, ViewTileWidth, ViewTileHeight);
            MingGizmoHelper.DrawRectangle(viewTileRect, MingConst.MingColor1, "gridView", MingConst.MingColorText1);
            if (_mingGridWorld == null)
            {
                MingGizmoHelper.DrawRectangle(new RectInt(0, 0, 200, 200), MingConst.MingColor2, "editor-example-world-", MingConst.MingColorText1);
                return;
            }

            MingGizmoHelper.DrawRectangle(_mingGridWorld.WorldRect, MingConst.MingColor2, "world", MingConst.MingColorText1);
        }

        private void Awake()
        {
            _transform = transform;

            _mingGridWorld = new MingGridWorld(w: 100, h: 80);
            _quadRendererFloorLayer = CreateMingQuadMeshRenderer(FloorSortingLayerName, FloorSortingOrder);
            _quadRendererWallLayer = CreateMingQuadMeshRenderer(RoofSortingLayerName, RoofSortingOrder);

            TileRecipeCollection.Init();
        }

        // we are a child go of the camera
        // from camera position find view tile rect we need to render
        // adjust our local position within the 1x1 to make it smooth
        private void Update()
        {
            RectInt viewTileRect = MingGridUtil.GetViewTileRect(transform.position, ViewTileWidth, ViewTileHeight);
            Vector2Int gridBottomLeft = new Vector2Int(viewTileRect.x, viewTileRect.y);

            for (int y = 0; y < ViewTileHeight; y++)
            {
                for (int x = 0; x < ViewTileWidth; x++)
                {
                    int worldX = gridBottomLeft.x + x;
                    int worldY = gridBottomLeft.y + y;
                    if (worldX >= _mingGridWorld.W || worldY >= _mingGridWorld.H || worldX < 0 || worldY < 0)
                    {
                        continue;
                    }

                    int idxWorld = worldY * _mingGridWorld.W + worldX;
                    uint cellValue = _mingGridWorld.GridData[idxWorld];
                    uint tileId = cellValue;
                    MingGridTileRecipe tileRecipe = TileRecipeCollection.GetRecipe(tileId);

                    MingQuadRenderer quadRenderer = tileRecipe.RenderLayer == MingTileRenderLayer.Floor ?
                        _quadRendererFloorLayer :
                        _quadRendererWallLayer;

                    quadRenderer.AddQuad(
                        new Vector3(x, y, 0),
                        new Vector2(1, 1),
                        0,
                        0,
                        Color.white,
                        tileRecipe.TileSprite,
                        tileRecipe.RenderLayer == MingTileRenderLayer.Floor ? FloorMaterial : WallMaterial,
                        gameObject.layer);
                }
            }
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
