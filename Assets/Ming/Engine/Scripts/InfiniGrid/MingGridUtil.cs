using UnityEngine;

namespace Ming
{
    public static class MingGridUtil
    {
        public static int ToChunkSpace(int x, int chunkSize)
            => Mathf.FloorToInt(x / (float)chunkSize);
    
        public static ulong GetChunkId(Vector2Int gridPosition, int chunkSize)
            => GetChunkId(gridPosition.x, gridPosition.y, chunkSize);

        public static ulong GetChunkId(int x, int y, int chunkSize)
        {
            int chunkX = ToChunkSpace(x, chunkSize);
            int chunkY = ToChunkSpace(y, chunkSize);
            return ((ulong)chunkX << 32) + (uint)chunkY;
        }

        public static Vector2Int GetChunkGridBottomLeftPosition(ulong chunkId, int chunkSize)
        {
            int x = (int)(chunkId >> 32);
            int y = (int)chunkId;
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

        public static RectInt GetOverlappedChunks(RectInt cellRect, int chunkSize)
        {
            int x = ToChunkSpace(cellRect.xMin, chunkSize);
            int y = ToChunkSpace(cellRect.yMin, chunkSize);
            int w = ToChunkSpace(cellRect.xMax - 1, chunkSize) - x + 1;
            int h = ToChunkSpace(cellRect.yMax - 1, chunkSize) - y + 1;
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
