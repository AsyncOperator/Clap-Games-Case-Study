using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer( typeof( ReadOnlyAttribute ) )]
public sealed class ReadOnlyDrawer : PropertyDrawer {
    public override void OnGUI( Rect position, SerializedProperty property, GUIContent label ) {
        var previousGUIState = GUI.enabled;
        GUI.enabled = false;
        EditorGUI.PropertyField( position, property, label );
        GUI.enabled = previousGUIState;
    }
}