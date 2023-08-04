using System.Collections.Generic;
using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTECharacterSpecificMoveInfoController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTECharacterSpecificMoveInfoScriptableObject[] characterSpecificMoveInfoScriptableObjectArray;
        private List<StanceInfo> stanceInfoList = new List<StanceInfo>();

        private void Update()
        {
            SetDefaultMoveInfoOptions(UFE.GetPlayer1());

            SetDefaultMoveInfoOptions(UFE.GetPlayer2());

            SetOpponentMoveInfoOptions(UFE.GetPlayer1(), UFE.GetPlayer2());

            SetOpponentMoveInfoOptions(UFE.GetPlayer2(), UFE.GetPlayer1());
        }

        private void SetDefaultMoveInfoOptions(UFE3D.CharacterInfo characterInfo)
        {
            if (characterInfo == null)
            {
                return;
            }

            foreach (string path in characterInfo.stanceResourcePath)
            {
                stanceInfoList.Add(Resources.Load<StanceInfo>(path));
            }

            int length = characterSpecificMoveInfoScriptableObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterInfo.characterName != characterSpecificMoveInfoScriptableObjectArray[i].characterName)
                {
                    continue;
                }

                int lengthA = characterSpecificMoveInfoScriptableObjectArray[i].defaultMoveInfoOptionsArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    int lengthB = characterInfo.moves.Length;
                    for (int b = 0; b < lengthB; b++)
                    {
                        if (characterInfo.moves[b].combatStance != characterSpecificMoveInfoScriptableObjectArray[i].defaultMoveInfoOptionsArray[a].combatStance)
                        {
                            continue;
                        }

                        characterInfo.moves[b].cinematicIntro = GetMoveInfoByMoveNameFromMoveInfoCollection(characterSpecificMoveInfoScriptableObjectArray[i].defaultMoveInfoOptionsArray[a].introMoveName, characterInfo.moves[b].attackMoves);

                        characterInfo.moves[b].roundOutro = GetMoveInfoByMoveNameFromMoveInfoCollection(characterSpecificMoveInfoScriptableObjectArray[i].defaultMoveInfoOptionsArray[a].roundWinOutroMoveName, characterInfo.moves[b].attackMoves);

                        characterInfo.moves[b].timeOutOutro = GetMoveInfoByMoveNameFromMoveInfoCollection(characterSpecificMoveInfoScriptableObjectArray[i].defaultMoveInfoOptionsArray[a].timeOutOutroMoveName, characterInfo.moves[b].attackMoves);

                        characterInfo.moves[b].cinematicOutro = GetMoveInfoByMoveNameFromMoveInfoCollection(characterSpecificMoveInfoScriptableObjectArray[i].defaultMoveInfoOptionsArray[a].gameWinOutroMoveName, characterInfo.moves[b].attackMoves);

                        break;
                    }

                    int countB = stanceInfoList.Count;
                    for (int b = 0; b < countB; b++)
                    {
                        if (stanceInfoList[b] == null
                            || stanceInfoList[b].combatStance != characterSpecificMoveInfoScriptableObjectArray[i].defaultMoveInfoOptionsArray[a].combatStance)
                        {
                            continue;
                        }

                        stanceInfoList[b].cinematicIntro = GetMoveInfoByMoveNameFromMoveInfoCollection(characterSpecificMoveInfoScriptableObjectArray[i].defaultMoveInfoOptionsArray[a].introMoveName, stanceInfoList[b].attackMoves);

                        stanceInfoList[b].roundOutro = GetMoveInfoByMoveNameFromMoveInfoCollection(characterSpecificMoveInfoScriptableObjectArray[i].defaultMoveInfoOptionsArray[a].roundWinOutroMoveName, stanceInfoList[b].attackMoves);

                        stanceInfoList[b].timeOutOutro = GetMoveInfoByMoveNameFromMoveInfoCollection(characterSpecificMoveInfoScriptableObjectArray[i].defaultMoveInfoOptionsArray[a].timeOutOutroMoveName, stanceInfoList[b].attackMoves);

                        stanceInfoList[b].cinematicOutro = GetMoveInfoByMoveNameFromMoveInfoCollection(characterSpecificMoveInfoScriptableObjectArray[i].defaultMoveInfoOptionsArray[a].gameWinOutroMoveName, stanceInfoList[b].attackMoves);

                        break;
                    }
                }

                break;
            }

            stanceInfoList.Clear();
        }

        private void SetOpponentMoveInfoOptions(UFE3D.CharacterInfo characterInfo, UFE3D.CharacterInfo opponentCharacterInfo)
        {
            if (characterInfo == null
                || opponentCharacterInfo == null)
            {
                return;
            }

            foreach (string path in characterInfo.stanceResourcePath)
            {
                stanceInfoList.Add(Resources.Load<StanceInfo>(path));
            }

            int length = characterSpecificMoveInfoScriptableObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterInfo.characterName != characterSpecificMoveInfoScriptableObjectArray[i].characterName)
                {
                    continue;
                }

                int lengthA = characterSpecificMoveInfoScriptableObjectArray[i].opponentMoveInfoOptionsArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    int lengthB = characterSpecificMoveInfoScriptableObjectArray[i].opponentMoveInfoOptionsArray[a].opponentCharacterNameArray.Length;
                    for (int b = 0; b < lengthB; b++)
                    {
                        if (opponentCharacterInfo.characterName != characterSpecificMoveInfoScriptableObjectArray[i].opponentMoveInfoOptionsArray[a].opponentCharacterNameArray[b])
                        {
                            continue;
                        }

                        int lengthC = characterInfo.moves.Length;
                        for (int c = 0; c < lengthC; c++)
                        {
                            if (characterInfo.moves[c].combatStance != characterSpecificMoveInfoScriptableObjectArray[i].opponentMoveInfoOptionsArray[a].combatStance)
                            {
                                continue;
                            }

                            characterInfo.moves[c].cinematicIntro = GetMoveInfoByMoveNameFromMoveInfoCollection(characterSpecificMoveInfoScriptableObjectArray[i].opponentMoveInfoOptionsArray[a].introMoveName, characterInfo.moves[c].attackMoves);

                            characterInfo.moves[c].roundOutro = GetMoveInfoByMoveNameFromMoveInfoCollection(characterSpecificMoveInfoScriptableObjectArray[i].opponentMoveInfoOptionsArray[a].roundWinOutroMoveName, characterInfo.moves[c].attackMoves);

                            characterInfo.moves[c].timeOutOutro = GetMoveInfoByMoveNameFromMoveInfoCollection(characterSpecificMoveInfoScriptableObjectArray[i].opponentMoveInfoOptionsArray[a].timeOutOutroMoveName, characterInfo.moves[c].attackMoves);

                            characterInfo.moves[c].cinematicOutro = GetMoveInfoByMoveNameFromMoveInfoCollection(characterSpecificMoveInfoScriptableObjectArray[i].opponentMoveInfoOptionsArray[a].gameWinOutroMoveName, characterInfo.moves[c].attackMoves);

                            break;
                        }

                        int countC = stanceInfoList.Count;
                        for (int c = 0; c < countC; c++)
                        {
                            if (stanceInfoList[c] == null
                                || stanceInfoList[c].combatStance != characterSpecificMoveInfoScriptableObjectArray[i].opponentMoveInfoOptionsArray[a].combatStance)
                            {
                                continue;
                            }

                            stanceInfoList[c].cinematicIntro = GetMoveInfoByMoveNameFromMoveInfoCollection(characterSpecificMoveInfoScriptableObjectArray[i].opponentMoveInfoOptionsArray[a].introMoveName, stanceInfoList[c].attackMoves);

                            stanceInfoList[c].roundOutro = GetMoveInfoByMoveNameFromMoveInfoCollection(characterSpecificMoveInfoScriptableObjectArray[i].opponentMoveInfoOptionsArray[a].roundWinOutroMoveName, stanceInfoList[c].attackMoves);

                            stanceInfoList[c].timeOutOutro = GetMoveInfoByMoveNameFromMoveInfoCollection(characterSpecificMoveInfoScriptableObjectArray[i].opponentMoveInfoOptionsArray[a].timeOutOutroMoveName, stanceInfoList[c].attackMoves);

                            stanceInfoList[c].cinematicOutro = GetMoveInfoByMoveNameFromMoveInfoCollection(characterSpecificMoveInfoScriptableObjectArray[i].opponentMoveInfoOptionsArray[a].gameWinOutroMoveName, stanceInfoList[c].attackMoves);

                            break;
                        }

                        break;
                    }
                }

                break;
            }

            stanceInfoList.Clear();
        }

        private static MoveInfo GetMoveInfoByMoveNameFromMoveInfoCollection(string moveName, MoveInfo[] moveInfoArray)
        {
            if (moveInfoArray == null)
            {
                return null;
            }

            int length = moveInfoArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (moveName != moveInfoArray[i].moveName)
                {
                    continue;
                }

                return moveInfoArray[i];
            }

            return null;
        }
    }
}