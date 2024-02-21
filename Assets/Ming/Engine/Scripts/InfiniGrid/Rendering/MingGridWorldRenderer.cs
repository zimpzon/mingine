using UnityEngine;

namespace Ming
{
    public class MingGridWorldRenderer : MingBehaviour
    {
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
            MingGizmoHelper.DrawRectangle(viewTileRect, Color.green, "gridView", Color.white);

            if (_mingGridWorld == null)
                return;

            foreach (MingGridChunk chunk in _mingGridWorld.LoadedChunks.Values)
            {
                MingGizmoHelper.DrawRectangle(chunk.GridBounds, Color.cyan, $"{chunk.ChunkId}", Color.white);
            }
        }

        private void Awake()
        {
            _transform = transform;

            _mingGridWorld = new MingGridWorld(new MingGridChunkStore(), new MingGridWorldBuilderDefault());
            _quadRendererFloorLayer = CreateMingQuadMeshRenderer(FloorSortingLayerName, FloorSortingOrder);
            _quadRendererRoofLayer = CreateMingQuadMeshRenderer(RoofSortingLayerName, RoofSortingOrder);
        }

        private void Update()
        {
            RectInt viewTileRect = MingGridUtil.GetViewTileRect(transform.position, ViewTileWidth, ViewTileHeight);
            _mingGridWorld.EnsureLoaded(MingGridUtil.AddPadding(viewTileRect, PreloadPadding));

            var chunks = _mingGridWorld.LoadedChunks.Values;

            for (int y = viewTileRect.yMin; y < viewTileRect.yMax; y++)
            {
                for (int x = viewTileRect.xMin; x < viewTileRect.xMax; x++)
                {
                    Vector2Int chunkId = new Vector2Int(x / MingGridWorld.ChunkSize, y / MingGridWorld.ChunkSize);
                    MingGridChunk chunk = _mingGridWorld.LoadedChunks[chunkId];
                    int localX = x - chunk.GridPosition.x;
                    int localY = y - chunk.GridPosition.y;
                    Debug.DrawRay(new Vector3(x + 0.5f, y + 0.5f, 0), Vector3.up * 0.25f, Color.magenta, 0.1f);
                    //int tileId = chunk.FloorTiles[localX + localY * MingGridWorld.ChunkSize];

                    //_quadRendererFloorLayer.AddQuad(
                    //    new Vector3(x, y, 0),
                    //    new Vector2(1, 1),
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
