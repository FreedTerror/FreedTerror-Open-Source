using UnityEngine;

namespace FreedTerror.UFE2
{
    [CreateAssetMenu(menuName = "FreedTerror/UFE 2/Character Info/Character Info References", fileName = "New Character Info References")]
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
                var item = characterInfoReferencesOptionsArray[i];
                if (item.characterInfo == null
                    || item.characterInfo.characterName != characterInfo.characterName)
                {
                    continue;
                }

                return Resources.Load<RuntimeAnimatorController>(item.characterSelectAnimatorControllerPath);
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
                var item = characterInfoReferencesOptionsArray[i];
                if (item.characterInfo == null
                    || item.characterInfo.characterName != characterInfo.characterName)
                {
                    continue;
                }

                return item.characterUnselectedAnimationName;
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
                var item = characterInfoReferencesOptionsArray[i];
                if (item.characterInfo == null
                    || item.characterInfo.characterName != characterInfo.characterName)
                {
                    continue;
                }

                return item.characterSelectedAnimationName;
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
                var item = characterInfoReferencesOptionsArray[i];
                if (item.characterInfo == null
                    || item.characterInfo.characterName != characterInfo.characterName)
                {
                    continue;
                }

                return item.characterSelectedIdleAnimationName;
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
                var item = characterInfoReferencesOptionsArray[i];
                if (item.characterInfo == null
                    || item.characterInfo.characterName != characterInfo.characterName)
                {
                    continue;
                }

                return Resources.Load<ChallengeModeScriptableObject>(item.challengeModeScriptableObjectPath);
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
                var item = characterInfoReferencesOptionsArray[i];
                if (item.characterInfo == null
                    || item.characterInfo.characterName != characterInfo.characterName)
                {
                    continue;
                }

                return Resources.Load<CharacterSpecificMoveInfoScriptableObject>(item.challengeModeScriptableObjectPath);
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
                var item = characterInfoReferencesOptionsArray[i];
                if (item.characterInfo == null
                    || item.characterInfo.characterName != characterInfo.characterName)
                {
                    continue;
                }

                return Resources.Load<MoveListScriptableObject>(item.moveListScriptableObjectPath);
            }

            return null;
        }
    }
}