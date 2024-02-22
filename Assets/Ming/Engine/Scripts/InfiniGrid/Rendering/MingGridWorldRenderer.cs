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
        public Sprite FloorSprite;
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
            MingGizmoHelper.DrawRectangle(viewTileRect, MingConst.MingColor1, "gridView", MingConst.MingColorText1);

            if (_mingGridWorld == null)
                return;

            foreach (MingGridChunk chunk in _mingGridWorld.ActiveChunks.Values)
            {
                MingGizmoHelper.DrawRectangle(
                    chunk.GridBounds,
                    MingConst.MingColor2,
                    $"{chunk.GridPosition}",
                    MingConst.MingColorText1,
                    nameOffset: Vector2.down * ChunkSize * 0.9f + Vector2.right);
            }
        }

        private void Awake()
        {
            _transform = transform;

            _mingGridWorld = new MingGridWorld(new MingGridChunkStore(), new MingGridWorldBuilderDefault(), ChunkSize);
            _quadRendererFloorLayer = CreateMingQuadMeshRenderer(FloorSortingLayerName, FloorSortingOrder);
            _quadRendererRoofLayer = CreateMingQuadMeshRenderer(RoofSortingLayerName, RoofSortingOrder);
        }

        private void Update()
        {
            RectInt viewTileRect = MingGridUtil.GetViewTileRect(transform.position, ViewTileWidth, ViewTileHeight);
            _mingGridWorld.EnsureLoaded(MingGridUtil.AddPadding(viewTileRect, PreloadPadding));

            for (int y = viewTileRect.yMax - 1; y >= viewTileRect.yMin; y--)
            {
                for (int x = viewTileRect.xMin; x < viewTileRect.xMax; x++)
                {
                    long chunkId = MingGridUtil.GetChunkId(new Vector2Int(x / ChunkSize, y / ChunkSize));
                    if (!_mingGridWorld.ActiveChunks.TryGetValue(chunkId, out MingGridChunk chunk))
                    {
                        Debug.LogError($"Chunk {chunkId} not loaded ({MingGridUtil.GetChunkGridPosition(chunkId, ChunkSize)})");
                    }

                    int localX = x - chunk.GridPosition.x;
                    int localY = y - chunk.GridPosition.y;
                    int idx = localX + localY * ChunkSize;
                    bool outOfBounds = idx >= _mingGridWorld.ChunkCells;
                    Color c = outOfBounds ? Color.red : Color.green;
                    if (outOfBounds)
                    {
                        Debug.Log("idx: " + idx);
                        Debug.Log("lx: " + localX);
                        Debug.Log("ly: " + localY);
                    }
                    Debug.DrawRay(new Vector3(x + 0.5f, y + 0.5f, 0), Vector3.up * 0.25f, c, 0.1f);
                    //int tileId = chunk.FloorTiles[idx];

                    //_quadRendererFloorLayer.AddQuad(
                    //    new Vector3(x, y, 0),
                    //    new Vector2(1, 1) * 0.25f,
                    //    0,
                    //    0,
                    //    Color.white,
                    //    FloorSprite,
                    //    FloorMaterial,
                    //    gameObject.layer);
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
