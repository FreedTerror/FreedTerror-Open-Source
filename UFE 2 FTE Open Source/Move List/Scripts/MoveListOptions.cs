using UnityEngine;

namespace UFE2FTE
{
    [System.Serializable]
    public class MoveListOptions
    {
        [System.Serializable]
        public class MoveListScriptableObjectOptions
        {
            public string moveListScriptableObjectPath;
            public UFE3D.CharacterInfo characterInfo;
        }
        public MoveListScriptableObjectOptions[] moveListScriptableObjectOptionsArray;

        public MoveListScriptableObject GetMoveListScriptableObject(string path)
        {
            int length = moveListScriptableObjectOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (UFE2FTE.IsStringMatch(path, moveListScriptableObjectOptionsArray[i].moveListScriptableObjectPath) == false)
                {
                    continue;
                }

                return Resources.Load<MoveListScriptableObject>(moveListScriptableObjectOptionsArray[i].moveListScriptableObjectPath);
            }

            return null;
        }

        public MoveListScriptableObject GetMoveListScriptableObject(UFE3D.CharacterInfo characterInfo)
        {
            if (characterInfo == null)
            {
                return null;
            }

            int length = moveListScriptableObjectOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterInfo.characterName != moveListScriptableObjectOptionsArray[i].characterInfo.characterName)
                {
                    continue;
                }

                return Resources.Load<MoveListScriptableObject>(moveListScriptableObjectOptionsArray[i].moveListScriptableObjectPath);
            }

            return null;
        }
    }
}