using UnityEditor;
using UnityEngine;

namespace Ming
{
    // Code from https://answers.unity.com/questions/609385/type-for-layer-selection.html
    [CustomPropertyDrawer(typeof(MingLayerAttribute))]
    class MingLayerAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.intValue = EditorGUI.LayerField(position, label, property.intValue);
        }
    }
}
