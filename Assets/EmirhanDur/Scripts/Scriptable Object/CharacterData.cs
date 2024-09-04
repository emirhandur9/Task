using UnityEngine;

namespace EmirhanDur
{

    public enum CharacterType
    {
        Knight,
        Wizard,
        Archer
    }

    [CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character Data", order = 51)]
    public class CharacterData : ScriptableObject
    {
        public string characterName;
        public Sprite characterSprite;
        public CharacterType characterType;


        [CharInfo]
        public int health;

        [CharInfo]
        public int strength;

        [CharInfo]
        public int range;

        private void OnValidate()
        {
            if (characterType != CharacterType.Wizard && characterType != CharacterType.Archer)
            {
                range = 0; 
            }
        }

        public void GenerateRandomName()
        {
            string[] names = { "Artemis", "Leon", "Selene", "Lysander", "Helena", "Orion", "Thalia", "Darius", "Nerissa", "Cyrus", "Elysia", "Isolde", "Gideon", };

            characterName = names[Random.Range(0, names.Length)];
        }
    }

}
