using UnityEditor;
using UnityEngine;

namespace EmirhanDur
{

    [CustomPropertyDrawer(typeof(CharInfoAttribute))]
    public class CharInfoPropertyDrawer : PropertyDrawer
    {
        private GUIStyle labelStyle;
        private GUIStyle fieldStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (labelStyle == null || fieldStyle == null)
            {
                Font titleFont = Resources.Load<Font>("Fonts/Lato-Bold");
                Font fieldFont = Resources.Load<Font>("Fonts/Lato-Thin");


                labelStyle = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 12,
                    font = titleFont,
                    normal = { textColor = Color.white }
                };

                fieldStyle = new GUIStyle(GUI.skin.textField)
                {
                    fontSize = 12,
                    font = fieldFont,
                    normal = { textColor = Color.white }
                };
            }

            EditorGUI.LabelField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), label.text, labelStyle);
            position.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, property, GUIContent.none, true);
        }
    }

}
