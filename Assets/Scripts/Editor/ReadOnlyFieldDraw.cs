using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyField))]
    internal class ReadOnlyFieldDraw: PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, false);
            GUI.enabled = true;
        }
    }
}
