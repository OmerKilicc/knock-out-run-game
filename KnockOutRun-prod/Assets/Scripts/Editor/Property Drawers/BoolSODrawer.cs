using UnityEditor;
using UnityEngine;

namespace Euphrates.Editor
{
    [CustomPropertyDrawer(typeof(BoolSO))]
    public class BoolSODrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            position.height = 16;

            EditorGUIUtility.labelWidth = 1f;

            if (property.objectReferenceValue == null)
            {
                Rect rect = new Rect(position.x, position.y, EditorGUIUtility.currentViewWidth - position.x - 20, position.height);
                EditorGUI.ObjectField(rect, property);
                return;
            }

            SerializedObject SO = new SerializedObject(property.objectReferenceValue);

            Rect objrect = new Rect(position.x, position.y, EditorGUIUtility.currentViewWidth - position.x - 75, position.height);
            Rect valRect = new Rect(EditorGUIUtility.currentViewWidth - 70, position.y, 50, position.height);

            EditorGUI.ObjectField(objrect, property);

            SerializedProperty myProp = SO.FindProperty("_value");
            myProp.boolValue = EditorGUI.Toggle(valRect, myProp.boolValue);

            SO.ApplyModifiedProperties();
        }
    }
}