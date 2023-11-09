using System.Collections.Generic;
using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class CharacterSpecificMoveInfoController : MonoBehaviour
    {
        private List<StanceInfo> stanceInfoList = new List<StanceInfo>();
        [SerializeField]
        private CharacterInfoReferencesScriptableObject characterInfoReferencesScriptableObject;

        private void Update()
        {
            SetDefaultMoveInfoOptions(UFE.GetPlayer1());
            SetDefaultMoveInfoOptions(UFE.GetPlayer2());

            SetOpponentMoveInfoOptions(UFE.GetPlayer1(), UFE.GetPlayer2());
            SetOpponentMoveInfoOptions(UFE.GetPlayer2(), UFE.GetPlayer1());
        }

        private void SetDefaultMoveInfoOptions(UFE3D.CharacterInfo characterInfo)
        {
            if (characterInfo == null
                || characterInfoReferencesScriptableObject == null)
            {
                return;
            }

            CharacterSpecificMoveInfoScriptableObject characterSpecificMoveInfoScriptableObject = characterInfoReferencesScriptableObject.GetCharacterSpecificMoveInfoScriptableObject(characterInfo);
            if (characterSpecificMoveInfoScriptableObject == null)
            {
                return;
            }

            foreach (string path in characterInfo.stanceResourcePath)
            {
                stanceInfoList.Add(Resources.Load<StanceInfo>(path));
            }

            int length = characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                int lengthA = characterInfo.moves.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (characterInfo.moves[a].combatStance != characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].combatStance)
                    {
                        continue;
                    }

                    characterInfo.moves[a].cinematicIntro = GetMoveInfo(characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].introMoveInfo, characterInfo.moves[a].attackMoves);
                    characterInfo.moves[a].roundOutro = GetMoveInfo(characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].roundWinOutroMoveInfo, characterInfo.moves[a].attackMoves);
                    characterInfo.moves[a].timeOutOutro = GetMoveInfo(characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].timeOutOutroMoveInfo, characterInfo.moves[a].attackMoves);
                    characterInfo.moves[a].cinematicOutro = GetMoveInfo(characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].gameWinOutroMoveInfo, characterInfo.moves[a].attackMoves);

                    break;
                }

                lengthA = stanceInfoList.Count;
                for (int a = 0; a < lengthA; a++)
                {
                    if (stanceInfoList[a] == null
                        || stanceInfoList[a].combatStance != characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].combatStance)
                    {
                        continue;
                    }

                    stanceInfoList[a].cinematicIntro = GetMoveInfo(characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].introMoveInfo, stanceInfoList[a].attackMoves);
                    stanceInfoList[a].roundOutro = GetMoveInfo(characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].roundWinOutroMoveInfo, stanceInfoList[a].attackMoves);
                    stanceInfoList[a].timeOutOutro = GetMoveInfo(characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].roundWinOutroMoveInfo, stanceInfoList[a].attackMoves);
                    stanceInfoList[a].cinematicOutro = GetMoveInfo(characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].roundWinOutroMoveInfo, stanceInfoList[a].attackMoves);

                    break;
                }
            }

            stanceInfoList.Clear();
        }

        private void SetOpponentMoveInfoOptions(UFE3D.CharacterInfo characterInfo, UFE3D.CharacterInfo opponentCharacterInfo)
        {
            if (characterInfo == null
                || opponentCharacterInfo == null
                || characterInfoReferencesScriptableObject == null)
            {
                return;
            }

            CharacterSpecificMoveInfoScriptableObject characterSpecificMoveInfoScriptableObject = characterInfoReferencesScriptableObject.GetCharacterSpecificMoveInfoScriptableObject(characterInfo);
            if (characterSpecificMoveInfoScriptableObject == null)
            {
                return;
            }

            foreach (string path in characterInfo.stanceResourcePath)
            {
                stanceInfoList.Add(Resources.Load<StanceInfo>(path));
            }

            int length = characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (opponentCharacterInfo.characterName != characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].opponentCharacterInfo.characterName)
                {
                    continue;
                }

                int lengthA = characterInfo.moves.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (characterInfo.moves[a].combatStance != characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].combatStance)
                    {
                        continue;
                    }

                    characterInfo.moves[a].cinematicIntro = GetMoveInfo(characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].introMoveInfo, characterInfo.moves[a].attackMoves);
                    characterInfo.moves[a].roundOutro = GetMoveInfo(characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].roundWinOutroMoveInfo, characterInfo.moves[a].attackMoves);
                    characterInfo.moves[a].timeOutOutro = GetMoveInfo(characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].timeOutOutroMoveInfo, characterInfo.moves[a].attackMoves);
                    characterInfo.moves[a].cinematicOutro = GetMoveInfo(characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].gameWinOutroMoveInfo, characterInfo.moves[a].attackMoves);

                    break;
                }

                lengthA = stanceInfoList.Count;
                for (int a = 0; a < lengthA; a++)
                {
                    if (stanceInfoList[a] == null
                        || stanceInfoList[a].combatStance != characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].combatStance)
                    {
                        continue;
                    }

                    stanceInfoList[a].cinematicIntro = GetMoveInfo(characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].introMoveInfo, stanceInfoList[a].attackMoves);
                    stanceInfoList[a].roundOutro = GetMoveInfo(characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].roundWinOutroMoveInfo, stanceInfoList[a].attackMoves);
                    stanceInfoList[a].timeOutOutro = GetMoveInfo(characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].roundWinOutroMoveInfo, stanceInfoList[a].attackMoves);
                    stanceInfoList[a].cinematicOutro = GetMoveInfo(characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].roundWinOutroMoveInfo, stanceInfoList[a].attackMoves);

                    break;
                }
            }

            stanceInfoList.Clear();
        }

        private static MoveInfo GetMoveInfo(MoveInfo comparing, MoveInfo[] matching)
        {
            if (comparing == null
                || matching == null)
            {
                return null;
            }

            int length = matching.Length;
            for (int i = 0; i < length; i++)
            {
                if (comparing.moveName != matching[i].moveName)
                {
                    continue;
                }

                return matching[i];
            }

            return null;
        }
    }
}