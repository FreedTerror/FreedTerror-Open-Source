using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Character Info References", menuName = "U.F.E. 2 F.T.E./Character Info/Character Info References")]
    public class CharacterInfoReferencesScriptableObject : ScriptableObject
    {
        [System.Serializable]
        public class CharacterInfoReferencesOptions
        {
            public UFE3D.CharacterInfo characterInfo;
            public string characterSelectAnimatorControllerPath;
            public string characterUnselectedAnimationName;
            public string characterSelectedAnimationName;
            public string characterSelectedIdleAnimationName;
            public string challengeModeScriptableObjectPath;
            public string characterSpecificMoveInfoScriptableObjectPath;   
            public string moveListScriptableObjectPath;
        }
        public CharacterInfoReferencesOptions[] characterInfoReferencesOptionsArray;

        public RuntimeAnimatorController GetCharacterSelectAnimatorController(UFE3D.CharacterInfo characterInfo)
        {
            if (characterInfo == null)
            {
                return null;
            }

            int length = characterInfoReferencesOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterInfo.characterName != characterInfoReferencesOptionsArray[i].characterInfo.characterName)
                {
                    continue;
                }

                return Resources.Load<RuntimeAnimatorController>(characterInfoReferencesOptionsArray[i].characterSelectAnimatorControllerPath);
            }

            return null;
        }

        public string GetCharacterUnselectedAnimationName(UFE3D.CharacterInfo characterInfo)
        {
            if (characterInfo == null)
            {
                return "";
            }

            int length = characterInfoReferencesOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterInfo.characterName != characterInfoReferencesOptionsArray[i].characterInfo.characterName)
                {
                    continue;
                }

                return characterInfoReferencesOptionsArray[i].characterUnselectedAnimationName;
            }

            return "";
        }

        public string GetCharacterSelectedAnimationName(UFE3D.CharacterInfo characterInfo)
        {
            if (characterInfo == null)
            {
                return "";
            }

            int length = characterInfoReferencesOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterInfo.characterName != characterInfoReferencesOptionsArray[i].characterInfo.characterName)
                {
                    continue;
                }

                return characterInfoReferencesOptionsArray[i].characterSelectedAnimationName;
            }

            return "";
        }

        public string GetCharacterSelectedIdleAnimationName(UFE3D.CharacterInfo characterInfo)
        {
            if (characterInfo == null)
            {
                return "";
            }

            int length = characterInfoReferencesOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterInfo.characterName != characterInfoReferencesOptionsArray[i].characterInfo.characterName)
                {
                    continue;
                }

                return characterInfoReferencesOptionsArray[i].characterSelectedIdleAnimationName;
            }

            return "";
        }

        public ChallengeModeScriptableObject GetChallengeModeScriptableObject(UFE3D.CharacterInfo characterInfo)
        {
            if (characterInfo == null)
            {
                return null;
            }

            int length = characterInfoReferencesOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterInfo.characterName != characterInfoReferencesOptionsArray[i].characterInfo.characterName)
                {
                    continue;
                }

                return Resources.Load<ChallengeModeScriptableObject>(characterInfoReferencesOptionsArray[i].challengeModeScriptableObjectPath);
            }

            return null;
        }

        public CharacterSpecificMoveInfoScriptableObject GetCharacterSpecificMoveInfoScriptableObject(UFE3D.CharacterInfo characterInfo)
        {
            if (characterInfo == null)
            {
                return null;
            }

            int length = characterInfoReferencesOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterInfo.characterName != characterInfoReferencesOptionsArray[i].characterInfo.characterName)
                {
                    continue;
                }

                return Resources.Load<CharacterSpecificMoveInfoScriptableObject>(characterInfoReferencesOptionsArray[i].challengeModeScriptableObjectPath);
            }

            return null;
        }

        public MoveListScriptableObject GetMoveListScriptableObject(UFE3D.CharacterInfo characterInfo)
        {
            if (characterInfo == null)
            {
                return null;
            }

            int length = characterInfoReferencesOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterInfo.characterName != characterInfoReferencesOptionsArray[i].characterInfo.characterName)
                {
                    continue;
                }

                return Resources.Load<MoveListScriptableObject>(characterInfoReferencesOptionsArray[i].moveListScriptableObjectPath);
            }

            return null;
        }
    }
}