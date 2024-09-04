using UnityEngine;
using UnityEditor;
using System.IO;

namespace EmirhanDur
{
    [CreateAssetMenu(fileName = "NewCharacterManager", menuName = "Character Manager", order = 51)]
    public class CharacterManager : ScriptableObject
    {
        public CharacterData[] characters;
        public CharacterData selectedCharacter;

        public void AddRandomCharacter()
        {

            CharacterData newCharacter = ScriptableObject.CreateInstance<CharacterData>();


            newCharacter.characterName = GetUniqueName(GetRandomName());
            newCharacter.characterType = (CharacterType)Random.Range(0, System.Enum.GetValues(typeof(CharacterType)).Length);
            newCharacter.health = Random.Range(1, 101);
            newCharacter.strength = Random.Range(1, 101);
            if (newCharacter.characterType == CharacterType.Wizard || newCharacter.characterType == CharacterType.Archer)
            {
                newCharacter.range = Random.Range(1, 101);
            }
            else
            {
                newCharacter.range = 0; 
            }


            string path = $"Assets/Characters/{newCharacter.characterName}.asset";
            AssetDatabase.CreateAsset(newCharacter, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();


            System.Array.Resize(ref characters, characters.Length + 1);
            characters[characters.Length - 1] = AssetDatabase.LoadAssetAtPath<CharacterData>(path);
        }


        public void DeleteLastCharacter()
        {
            if (characters.Length > 0)
            {
                string path = AssetDatabase.GetAssetPath(characters[characters.Length - 1]);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();


                System.Array.Resize(ref characters, characters.Length - 1);
            }
        }


        public void SelectRandomCharacter()
        {
            if (characters.Length > 0)
            {
                int randomIndex = Random.Range(0, characters.Length);
                selectedCharacter = characters[randomIndex];
            }
            else
            {
                Debug.LogWarning("No characters available to select.");
            }
        }


        private string GetUniqueName(string baseName)
        {
            string path = "Assets/Characters/";
            string uniqueName = baseName;
            int index = 1;

            while (File.Exists(Path.Combine(path, uniqueName + ".asset")))
            {
                uniqueName = baseName + index++;
            }

            return uniqueName;
        }

        private string GetRandomName()
        {
            string[] names = { "Artemis", "Luna", "Zephyr", "Thorn", "Eldric", "Niamh", "Cyrus", "Rowan" };
            return names[Random.Range(0, names.Length)];
        }
    }
}
