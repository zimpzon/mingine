using UnityEngine;

namespace Ming
{
    public static class MingGridUtil
    {
        public static long GetChunkId(Vector2Int gridPosition)
        {
            gridPosition += new Vector2Int(100000, 100000);
            return ((long)gridPosition.x) << 32 | ((long)gridPosition.y);
        }

        public static Vector2Int GetChunkGridPosition(long chunkId, int chunkSize)
        {
            int x = (int)(chunkId >> 32) - 100000;
            int y = (int)(chunkId & 0xffffffff) - 100000;
            return new Vector2Int(x * chunkSize, y * chunkSize);
        }

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

        public static RectInt GetTouchedChunks(RectInt cellRect, int chunkSize)
        {
            int x = Mathf.FloorToInt((float)cellRect.x / chunkSize);
            int y = Mathf.FloorToInt((float)cellRect.y / chunkSize);

            int xMax = Mathf.CeilToInt((float)(cellRect.x + cellRect.width) / chunkSize);
            int yMax = Mathf.CeilToInt((float)(cellRect.y + cellRect.height) / chunkSize);

            int w = xMax - x;
            int h = yMax - y;

            return new RectInt(x, y, w, h);
        }

        public static RectInt CellRectFromChunkSpaceRect(RectInt chunkSpaceRect, int chunkSize)
        {
            var cellX = chunkSpaceRect.x * chunkSize;
            var cellY = chunkSpaceRect.y * chunkSize;
            var cellWidth = chunkSpaceRect.width * chunkSize;
            var cellHeight = chunkSpaceRect.height * chunkSize;
            return new RectInt(cellX, cellY, cellWidth, cellHeight);
        }
    }
}
