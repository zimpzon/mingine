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

    // ChunkSize^2 must be uploaded to the GPU when a single tile changes
    // Up to 4 chunks may be affected by explosions, etc.
    public class MingGridWorldRenderer : MingBehaviour
    {
        const int ChunkSize = 32;

        public int ViewTileWidth = 30;
        public int ViewTileHeight = 20; 
        public int PreloadPadding = 10;

        public string FloorSortingLayerName = "InfiniGridFloor";
        public int FloorSortingOrder = 0;
        public MingGridTileRecipeCollection TileRecipeCollection;
        public Material FloorMaterial;
        
        public string RoofSortingLayerName = "InfiniGridRoof";
        public int RoofSortingOrder = 0;

        private MingQuadRenderer _quadRendererFloorLayer;
        private MingQuadRenderer _quadRendererRoofLayer;

        private MingGridWorld _mingGridWorld;
        private Transform _transform;

        private void OnDrawGizmos()
        {
            RectInt viewTileRect = MingGridUtil.GetViewTileRect(transform.position, ViewTileWidth, ViewTileHeight);
            RectInt paddedViewTileRect = MingGridUtil.AddPadding(viewTileRect, PreloadPadding);
            MingGizmoHelper.DrawRectangle(paddedViewTileRect, MingConst.MingColor2);
            MingGizmoHelper.DrawRectangle(viewTileRect, MingConst.MingColor1, "gridView", MingConst.MingColorText1);

            RectInt chunkSpaceRect = MingGridUtil.GetOverlappedChunks(paddedViewTileRect, ChunkSize);
            MingGizmoHelper.DrawRectangle(MingGridUtil.CellRectFromChunkSpaceRect(chunkSpaceRect, ChunkSize), Color.cyan);

            if (_mingGridWorld == null)
                return;

            foreach (MingGridChunk chunk in _mingGridWorld.ActiveChunks.Values)
            {
                MingGizmoHelper.DrawRectangle(chunk.GridBounds, Color.grey);
            }
        }

        private void Awake()
        {
            _transform = transform;

            _mingGridWorld = new MingGridWorld(new MingGridChunkStore(), new MingGridWorldBuilderDefault(), ChunkSize);
            _quadRendererFloorLayer = CreateMingQuadMeshRenderer(FloorSortingLayerName, FloorSortingOrder);
            _quadRendererRoofLayer = CreateMingQuadMeshRenderer(RoofSortingLayerName, RoofSortingOrder);

            TileRecipeCollection.Init();
        }

        private void Update()
        {
            RectInt viewTileRect = MingGridUtil.GetViewTileRect(transform.position, ViewTileWidth, ViewTileHeight);
            _mingGridWorld.EnsureLoaded(MingGridUtil.AddPadding(viewTileRect, PreloadPadding));

            for (int y = viewTileRect.yMax - 1; y >= viewTileRect.yMin; y--)
            {
                for (int x = viewTileRect.xMin; x < viewTileRect.xMax; x++)
                {
                    ulong chunkId = MingGridUtil.GetChunkId(x, y, ChunkSize);

                    Vector2Int chunkBottomLeft = MingGridUtil.GetChunkGridBottomLeftPosition(chunkId, ChunkSize);
                    if (!_mingGridWorld.ActiveChunks.TryGetValue(chunkId, out MingGridChunk chunk))
                    {   
                        Debug.LogError($"Chunk {chunkId} not loaded ({chunkBottomLeft})");
                    }

                    int localX = x - chunkBottomLeft.x;
                    int localY = y - chunkBottomLeft.y;

                    int tileId = chunk.FloorTiles[localY * ChunkSize + localX];
                    if (tileId > 0)
                    {
                        MingGridTileRecipe tileRecipeCollection = TileRecipeCollection.GetRecipe(tileId);

                        _quadRendererFloorLayer.AddQuad(
                            new Vector3(x, y, 0),
                            new Vector2(1, 1),
                            0,
                            0,
                            Color.white,
                            tileRecipeCollection.Floor,
                            FloorMaterial,
                            gameObject.layer);
                    }
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
            go.transform.SetParent(this.transform);
            return quadRenderer;
        }
    }
}
