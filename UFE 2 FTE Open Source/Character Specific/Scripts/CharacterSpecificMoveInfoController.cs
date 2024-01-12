using System.Collections.Generic;
using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class CharacterSpecificMoveInfoController : MonoBehaviour
    {        
        [SerializeField]
        private CharacterInfoReferencesScriptableObject characterInfoReferencesScriptableObject;

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
            if (characterInfoReferencesScriptableObject == null
                || characterInfo == null)
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

                    characterInfo.moves[a].basicMoves.intro.moveInfo = GetMoveInfo(characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].introMoveInfo, characterInfo.moves[a].attackMoves);
                    characterInfo.moves[a].basicMoves.roundWon.moveInfo = GetMoveInfo(characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].roundWonMoveInfo, characterInfo.moves[a].attackMoves);
                    characterInfo.moves[a].basicMoves.timeOut.moveInfo = GetMoveInfo(characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].timeOutMoveInfo, characterInfo.moves[a].attackMoves);
                    characterInfo.moves[a].basicMoves.gameWon.moveInfo = GetMoveInfo(characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].gameWonMoveInfo, characterInfo.moves[a].attackMoves);

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

                    stanceInfoList[a].basicMoves.intro.moveInfo = GetMoveInfo(characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].introMoveInfo, characterInfo.moves[a].attackMoves);
                    stanceInfoList[a].basicMoves.roundWon.moveInfo = GetMoveInfo(characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].roundWonMoveInfo, characterInfo.moves[a].attackMoves);
                    stanceInfoList[a].basicMoves.timeOut.moveInfo = GetMoveInfo(characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].timeOutMoveInfo, characterInfo.moves[a].attackMoves);
                    stanceInfoList[a].basicMoves.gameWon.moveInfo = GetMoveInfo(characterSpecificMoveInfoScriptableObject.defaultMoveInfoOptionsArray[i].gameWonMoveInfo, characterInfo.moves[a].attackMoves);

                    break;
                }
            }

            stanceInfoList.Clear();
        }

        private void SetOpponentMoveInfoOptions(UFE3D.CharacterInfo characterInfo, UFE3D.CharacterInfo opponentCharacterInfo)
        {
            if (characterInfoReferencesScriptableObject == null
                || characterInfo == null
                || opponentCharacterInfo == null)
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

                    characterInfo.moves[a].basicMoves.intro.moveInfo = GetMoveInfo(characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].introMoveInfo, characterInfo.moves[a].attackMoves);
                    characterInfo.moves[a].basicMoves.roundWon.moveInfo = GetMoveInfo(characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].roundWonMoveInfo, characterInfo.moves[a].attackMoves);
                    characterInfo.moves[a].basicMoves.timeOut.moveInfo = GetMoveInfo(characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].timeOutMoveInfo, characterInfo.moves[a].attackMoves);
                    characterInfo.moves[a].basicMoves.gameWon.moveInfo = GetMoveInfo(characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].gameWonMoveInfo, characterInfo.moves[a].attackMoves);

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

                    stanceInfoList[a].basicMoves.intro.moveInfo = GetMoveInfo(characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].introMoveInfo, characterInfo.moves[a].attackMoves);
                    stanceInfoList[a].basicMoves.roundWon.moveInfo = GetMoveInfo(characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].roundWonMoveInfo, characterInfo.moves[a].attackMoves);
                    stanceInfoList[a].basicMoves.timeOut.moveInfo = GetMoveInfo(characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].timeOutMoveInfo, characterInfo.moves[a].attackMoves);
                    stanceInfoList[a].basicMoves.gameWon.moveInfo = GetMoveInfo(characterSpecificMoveInfoScriptableObject.opponentMoveInfoOptionsArray[i].gameWonMoveInfo, characterInfo.moves[a].attackMoves);

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