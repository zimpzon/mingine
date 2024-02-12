using Ming.Util;
using UnityEditor;
using UnityEngine;

// Code from https://answers.unity.com/questions/609385/type-for-layer-selection.html

[CustomPropertyDrawer(typeof(MingLayerAttribute))]
class LayerAttributeEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.intValue = EditorGUI.LayerField(position, label, property.intValue);
    }
}
