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
    }
}
