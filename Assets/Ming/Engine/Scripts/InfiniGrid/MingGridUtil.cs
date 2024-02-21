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

        public static RectInt AddPadding(RectInt rect, int padding)
        {
            rect.x -= padding;
            rect.y -= padding;
            rect.width += padding * 2;
            rect.height += padding * 2;
            return rect;
        }

        public static RectInt GetOverlappingChunks(RectInt cellRect)
        {
            // Calculate the starting chunk indices
            int x = Mathf.FloorToInt((float)cellRect.x / MingGridWorld.ChunkSize);
            int y = Mathf.FloorToInt((float)cellRect.y / MingGridWorld.ChunkSize);

            // Calculate the ending chunk indices based on the cellRect's max values
            int xMax = Mathf.CeilToInt((float)(cellRect.x + cellRect.width) / MingGridWorld.ChunkSize);
            int yMax = Mathf.CeilToInt((float)(cellRect.y + cellRect.height) / MingGridWorld.ChunkSize);

            // Compute width and height of the chunk area
            int w = xMax - x;
            int h = yMax - y;

            return new RectInt(x, y, w, h);
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
