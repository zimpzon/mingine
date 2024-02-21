using UnityEngine;
using UnityEditor;

// This attribute links your custom drawer to the MingGridTileSprites class
[CustomPropertyDrawer(typeof(Ming.MingGridTileSprites))]
public class MingGridTileSpritesDrawer : PropertyDrawer
{
    // This method overrides the default rendering for the TileSprites property
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Debug.Log("CUSTOM");
        // Start of the property
        EditorGUI.BeginProperty(position, label, property);

        // Draw the label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Get the TileSprites array property
        SerializedProperty tileSprites = property.FindPropertyRelative("TileSprites");

        // Set the indentation level for better layout
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Check if the array is not null
        if (tileSprites != null && tileSprites.isArray)
        {
            // Calculate the height for each element
            float singleLineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            int arraySize = tileSprites.arraySize;

            // Iterate through the array elements
            for (int i = 0; i < arraySize; i++)
            {
                // Calculate the position for each sprite
                Rect spriteRect = new Rect(position.x, position.y + i * singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);

                // Get the current array element
                SerializedProperty spriteProperty = tileSprites.GetArrayElementAtIndex(i);

                // Draw the sprite field with the index label
                EditorGUI.ObjectField(spriteRect, spriteProperty, new GUIContent($"Sprite {i}"));
            }

            // Adjust the property height according to the number of sprites
            property.serializedObject.ApplyModifiedProperties(); // Apply changes
        }

        // Reset the indentation level
        EditorGUI.indentLevel = indent;

        // End of the property
        EditorGUI.EndProperty();
    }

    // This method returns the height needed for the property
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Get the TileSprites array property
        SerializedProperty tileSprites = property.FindPropertyRelative("TileSprites");
        if (tileSprites != null && tileSprites.isArray)
        {
            // Calculate total height (number of elements * single line height)
            return (tileSprites.arraySize * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing));
        }
        return EditorGUIUtility.singleLineHeight; // Default height
    }
}
