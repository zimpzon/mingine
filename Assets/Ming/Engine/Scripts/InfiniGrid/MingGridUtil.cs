using UnityEngine;

namespace Ming
{
    public static class MingGridUtil
    {
        public static RectInt GetViewTileRect(Vector2 worldPosition, int viewWidth, int viewHeight)
        {
            var x = Mathf.FloorToInt(worldPosition.x - viewWidth / 2);
            var y = Mathf.FloorToInt(worldPosition.y - viewHeight / 2);
            return new RectInt(x, y, viewWidth, viewHeight);
        }

        public static RectInt ChunkRectFromCellRect(RectInt cellRect)
        {
            var chunkX = cellRect.x / MingGridWorld.ChunkSize;
            var chunkY = cellRect.y / MingGridWorld.ChunkSize;
            var chunkWidth = (cellRect.width + MingGridWorld.ChunkSize - 1) / MingGridWorld.ChunkSize;
            var chunkHeight = (cellRect.height + MingGridWorld.ChunkSize - 1) / MingGridWorld.ChunkSize;
            return new RectInt(chunkX, chunkY, chunkWidth, chunkHeight);
        }

        public static RectInt CellRectFromChunkRect(RectInt chunkRect)
        {
            var cellX = chunkRect.x * MingGridWorld.ChunkSize;
            var cellY = chunkRect.y * MingGridWorld.ChunkSize;
            var cellWidth = chunkRect.width * MingGridWorld.ChunkSize;
            var cellHeight = chunkRect.height * MingGridWorld.ChunkSize;
            return new RectInt(cellX, cellY, cellWidth, cellHeight);
        }
    }
}
