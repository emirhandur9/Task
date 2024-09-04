using UnityEngine;
using UnityEditor;

namespace EmirhanDur
{
    [CustomEditor(typeof(CharacterData))]
    public class CharacterDataEditor : Editor
    {
        private SerializedProperty characterNameProp;
        private SerializedProperty characterSpriteProp;
        private SerializedProperty healthProp;
        private SerializedProperty strengthProp;
        private SerializedProperty rangeProp;
        private SerializedProperty characterTypeProp;

        private GUIStyle titleStyle;
        private GUIStyle descriptionStyle;
        private GUIStyle propertyStyle;

        private void OnEnable()
        {
            characterNameProp = serializedObject.FindProperty("characterName");
            characterSpriteProp = serializedObject.FindProperty("characterSprite");
            healthProp = serializedObject.FindProperty("health");
            strengthProp = serializedObject.FindProperty("strength");
            rangeProp = serializedObject.FindProperty("range");
            characterTypeProp = serializedObject.FindProperty("characterType");

            titleStyle = new GUIStyle();
            titleStyle.fontSize = 16;
            titleStyle.normal.textColor = Color.white;

            Font titleFont = Resources.Load<Font>("Fonts/Lato-Bold");
            if (titleFont != null)
            {
                titleStyle.font = titleFont;
            }

            descriptionStyle = new GUIStyle();
            descriptionStyle.fontSize = 14;
            descriptionStyle.normal.textColor = Color.white;

            Font descriptionFont = Resources.Load<Font>("Fonts/Lato-Italic");
            if (descriptionFont != null)
            {
                descriptionStyle.font = descriptionFont;
            }

            propertyStyle = new GUIStyle();
            propertyStyle.fontSize = 12;
            propertyStyle.normal.textColor = Color.white;

            Font propertyFont = Resources.Load<Font>("Fonts/Lato-Thin");
            if (propertyFont != null)
            {
                propertyStyle.font = propertyFont;
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical("box");

            GUILayout.Label("Character Data", titleStyle);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(characterTypeProp);

            GUILayout.Label(GetCharacterTypeDescription((CharacterType)characterTypeProp.enumValueIndex), descriptionStyle);

            EditorGUILayout.Space();

            Sprite selectedSprite = (Sprite)characterSpriteProp.objectReferenceValue;
            if (selectedSprite != null)
            {
                GUILayout.Label(selectedSprite.texture, GUILayout.Height(100));
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(characterNameProp, GUIContent.none, GUILayout.Width(200));
            EditorGUILayout.Space();
            if (GUILayout.Button("Random Name", GUILayout.Width(100)))
            {
                CharacterData characterData = (CharacterData)target;
                characterData.GenerateRandomName();
                serializedObject.Update();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(healthProp);
            EditorGUILayout.Space(15);
            EditorGUILayout.PropertyField(strengthProp);
            EditorGUILayout.Space(15);
            if ((CharacterType)characterTypeProp.enumValueIndex == CharacterType.Wizard ||
                (CharacterType)characterTypeProp.enumValueIndex == CharacterType.Archer)
            {
                EditorGUILayout.PropertyField(rangeProp);
            }

            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }

        private string GetCharacterTypeDescription(CharacterType type)
        {
            switch (type)
            {
                case CharacterType.Knight:
                    return "Knight: A strong melee fighter with high defense.";
                case CharacterType.Wizard:
                    return "Wizard: A master of elemental magic, capable of casting powerful spells.";
                case CharacterType.Archer:
                    return "Archer: A ranged attacker, skilled in precision shooting.";
                default:
                    return "Unknown character type.";
            }
        }
    }
}
