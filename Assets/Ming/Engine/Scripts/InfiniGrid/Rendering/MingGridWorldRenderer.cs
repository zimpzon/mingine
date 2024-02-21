using UnityEngine;

namespace Ming
{
    public class MingGridWorldRenderer : MonoBehaviour
    {
        public Vector2 WorldPosition;
        public int ViewTileWidth = 30;
        public int ViewTileHeight = 20;

        public string FloorSortingLayerName = "InfiniGridFloor";
        public int FloorSortingOrder = 0;
        
        public string RoofSortingLayerName = "InfiniGridRoof";
        public int RoofSortingOrder = 0;

        private MingQuadRenderer _quadRendererFloorLayer;
        private MingQuadRenderer _quadRendererRoofLayer;

        private MingGridWorld _mingGridWorld;

        private void OnDrawGizmos()
        {
            if (_mingGridWorld == null)
                return;
            Debug.Log("OnDrawGizmos22");

            RectInt viewTileRect = MingGridUtil.GetViewTileRect(WorldPosition, ViewTileWidth, ViewTileHeight);
            _mingGridWorld.EnsureLoaded(viewTileRect);

            foreach (MingGridChunk chunk in _mingGridWorld.LoadedChunks.Values)
            {
                MingGizmoHelper.DrawRectangle(chunk.Bounds, Color.red, "chunkBounds", Color.white);
            }
            MingGizmoHelper.DrawRectangle(viewTileRect, Color.green, "gridView", Color.white);
        }

        void Awake()
        {
            Debug.Log("AWAKE");
            _mingGridWorld = new MingGridWorld(new MingGridChunkStore(), new MingGridWorldBuilderDefault());
            _quadRendererFloorLayer = CreateMingQuadMeshRenderer(FloorSortingLayerName, FloorSortingOrder);
            _quadRendererRoofLayer = CreateMingQuadMeshRenderer(RoofSortingLayerName, RoofSortingOrder);
        }

        MingQuadRenderer CreateMingQuadMeshRenderer(string sortingLayerName, int sortingOrder)
        {
            var go = new GameObject();
            go.layer = gameObject.layer;
            go.name = $"{nameof(MingGridWorldRenderer)} {sortingLayerName}/{sortingOrder}";

            var quadRenderer = go.AddComponent<MingQuadRenderer>();
            quadRenderer.SortingLayerName = sortingLayerName;
            quadRenderer.SortingOrder = sortingOrder;
            go.transform.SetParent(this.transform);
            return quadRenderer;
        }
    }
}
