using System;
using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Character Specific Move Info", menuName = "U.F.E. 2 F.T.E./Character Specific Move Info/Character Specific Move Info")]
    public class UFE2FTECharacterSpecificMoveInfoScriptableObject : ScriptableObject
    {
        public string characterName;

        [Serializable]
        public class DefaultMoveInfoOptions
        {
            public CombatStances combatStance;
            public string introMoveName;
            public string roundWinOutroMoveName;
            public string timeOutOutroMoveName;
            public string gameWinOutroMoveName;
        }
        public DefaultMoveInfoOptions[] defaultMoveInfoOptionsArray;

        [Serializable]
        public class OpponentMoveInfoOptions
        {
            public string[] opponentCharacterNameArray;
            public CombatStances combatStance;
            public string introMoveName;
            public string roundWinOutroMoveName;
            public string timeOutOutroMoveName;
            public string gameWinOutroMoveName;
        }
        public OpponentMoveInfoOptions[] opponentMoveInfoOptionsArray;
    }
}
