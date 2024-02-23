//using UnityEngine;
//using UnityEditor;
//using Ming;

//[CustomPropertyDrawer(typeof(MingGridTileRecipe))]
//public class MingGridTileRecipeDrawer : PropertyDrawer
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        EditorGUI.BeginProperty(position, label, property);

//        // Start the indentation.
//        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

//        var indent = EditorGUI.indentLevel;
//        EditorGUI.indentLevel = 0;

//        // Calculate rects
//        var tiledIdRect = new Rect(position.x, position.y, 30, position.height);
//        var defaultSpriteRect = new Rect(position.x + 35, position.y, position.width - 70, position.height);
//        var southOfWallSpriteRect = new Rect(position.x + position.width - 30, position.y, 30, position.height);

//        // Draw fields - pass GUIContent.none to each so they don't draw labels
//        EditorGUI.PropertyField(tiledIdRect, property.FindPropertyRelative("TiledId"), GUIContent.none);
//        EditorGUI.PropertyField(defaultSpriteRect, property.FindPropertyRelative("DefaultSprite"), GUIContent.none);
//        EditorGUI.PropertyField(southOfWallSpriteRect, property.FindPropertyRelative("SouthOfWallSprite"), GUIContent.none);

//        // Set indent back to what it was
//        EditorGUI.indentLevel = indent;

//        EditorGUI.EndProperty();
//    }
//}
