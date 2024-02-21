using UnityEngine;

namespace Ming
{
    public class MingGridWorldRenderer : MingBehaviour
    {
        public int ViewTileWidth = 30;
        public int ViewTileHeight = 20;

        public string FloorSortingLayerName = "InfiniGridFloor";
        public int FloorSortingOrder = 0;
        
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

            _mingGridWorld.EnsureLoaded(viewTileRect);

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

        MingQuadRenderer CreateMingQuadMeshRenderer(string sortingLayerName, int sortingOrder)
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
