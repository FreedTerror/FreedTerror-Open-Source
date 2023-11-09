using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    [CreateAssetMenu(menuName = "U.F.E. 2 F.T.E./Move List/Move List", fileName = "New Move List")]
    public class MoveListScriptableObject : ScriptableObject
    {
        [System.Serializable]
        public class MoveListOptions
        {
            public CombatStances[] combatStanceArray;
            public MoveInfo[] moveInfoArray;
        }
        public MoveListOptions[] moveListOptionsArray;
    }
}