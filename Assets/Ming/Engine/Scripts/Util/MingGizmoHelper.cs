using UnityEngine;

namespace Ming
{
    public static class MingGizmoHelper
    {
        public static void DrawRectangle(RectInt rect, Color color, string name = null, Color? nameColor = null, Vector2? nameOffset = null)
            => DrawRectangle(new Rect(rect.position, rect.size), color, name, nameColor, nameOffset);

        public static void DrawRectangle(Rect rect, Color color, string name = null, Color? nameColor = null, Vector2? nameOffset = null)
        {
#if UNITY_EDITOR
            Gizmos.color = color;
            Gizmos.DrawLine(new Vector3(rect.x, rect.y), new Vector3(rect.x + rect.width, rect.y));
            Gizmos.DrawLine(new Vector3(rect.x + rect.width, rect.y), new Vector3(rect.x + rect.width, rect.y + rect.height));
            Gizmos.DrawLine(new Vector3(rect.x + rect.width, rect.y + rect.height), new Vector3(rect.x, rect.y + rect.height));
            Gizmos.DrawLine(new Vector3(rect.x, rect.y + rect.height), new Vector3(rect.x, rect.y));

            drawString(name, new Vector3(rect.x, rect.yMax) + nameOffset ?? Vector2.zero, color);
#endif
        }

        static void drawString(string text, Vector3 worldPos, Color? colour = null)
        {
#if UNITY_EDITOR
            UnityEditor.Handles.color = Color.white;
            UnityEditor.Handles.Label(worldPos, text);
#endif
        }
    }
}
