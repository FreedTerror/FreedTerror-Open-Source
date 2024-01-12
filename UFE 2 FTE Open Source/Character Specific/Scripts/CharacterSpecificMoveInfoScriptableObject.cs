using UFE3D;
using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Character Specific Move Info", menuName = "U.F.E. 2 F.T.E./Character Info/Character Specific Move Info")]
    public class CharacterSpecificMoveInfoScriptableObject : ScriptableObject
    {
        public UFE3D.CharacterInfo characterInfo;

        [System.Serializable]
        public class DefaultMoveInfoOptions
        {
            public CombatStances combatStance;
            public MoveInfo introMoveInfo;
            public MoveInfo roundWonMoveInfo;
            public MoveInfo timeOutMoveInfo;
            public MoveInfo gameWonMoveInfo;
        }
        public DefaultMoveInfoOptions[] defaultMoveInfoOptionsArray;

        [System.Serializable]
        public class OpponentMoveInfoOptions
        {
            public UFE3D.CharacterInfo opponentCharacterInfo;
            public CombatStances combatStance;
            public MoveInfo introMoveInfo;
            public MoveInfo roundWonMoveInfo;
            public MoveInfo timeOutMoveInfo;
            public MoveInfo gameWonMoveInfo;
        }
        public OpponentMoveInfoOptions[] opponentMoveInfoOptionsArray;
    }
}
