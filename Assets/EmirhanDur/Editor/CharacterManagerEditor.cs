using UnityEditor;
using UnityEngine;

namespace EmirhanDur
{
    [CustomEditor(typeof(CharacterManager))]
    public class CharacterManagerEditor : Editor
    {
        private SerializedProperty charactersProp;
        private SerializedProperty selectedCharacterProp;
        private GUIStyle titleStyle;
        private GUIStyle descriptionStyle;
        private GUIStyle buttonStyle;

        private void OnEnable()
        {
            charactersProp = serializedObject.FindProperty("characters");
            selectedCharacterProp = serializedObject.FindProperty("selectedCharacter");


            titleStyle = new GUIStyle
            {
                fontSize = 16,
                font = Resources.Load<Font>("Fonts/Lato-Bold"),
                normal = { textColor = Color.white }
            };


            descriptionStyle = new GUIStyle
            {
                fontSize = 14,
                font = Resources.Load<Font>("Fonts/Lato-Italic"),
                normal = { textColor = Color.white }
            };

            //buttonStyle = new GUIStyle(GUI.skin.button)
            //{
            //    fontSize = 14,
            //    font = Resources.Load<Font>("Fonts/Lato-Thin"),
            //    normal = { textColor = Color.white },
            //    active = { textColor = Color.gray }
            //};
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical("box");

            GUILayout.Label("Character Manager", titleStyle);

            EditorGUILayout.Space();


            EditorGUILayout.LabelField("Character List", titleStyle);
            EditorGUILayout.PropertyField(charactersProp, true);


            if (GUILayout.Button("Add Random Character"))
            {
                CharacterManager manager = (CharacterManager)target;
                manager.AddRandomCharacter();
            }


            if (GUILayout.Button("Delete Last Character"))
            {
                CharacterManager manager = (CharacterManager)target;
                manager.DeleteLastCharacter();
            }


            if (GUILayout.Button("Select Random Character from List"))
            {
                CharacterManager manager = (CharacterManager)target;
                manager.SelectRandomCharacter();
            }

            EditorGUILayout.Space();


            EditorGUILayout.LabelField("Selected Character Info", titleStyle);
            EditorGUILayout.PropertyField(selectedCharacterProp);

            if (selectedCharacterProp.objectReferenceValue != null)
            {
                CharacterData selectedCharacter = (CharacterData)selectedCharacterProp.objectReferenceValue;
                GUILayout.Label($"Name: {selectedCharacter.characterName}", descriptionStyle);
                GUILayout.Label($"Type: {selectedCharacter.characterType}", descriptionStyle);
                GUILayout.Label($"Health: {selectedCharacter.health}", descriptionStyle);
                GUILayout.Label($"Strength: {selectedCharacter.strength}", descriptionStyle);
                if (selectedCharacter.characterType == CharacterType.Wizard || selectedCharacter.characterType == CharacterType.Archer)
                {
                    GUILayout.Label($"Range: {selectedCharacter.range}", descriptionStyle);
                }
            }

            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
