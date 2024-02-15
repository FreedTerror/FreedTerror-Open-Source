using UnityEngine;
using UFE3D;

namespace FreedTerror.UFE2
{
    [CreateAssetMenu(menuName = "FreedTerror/UFE 2/Move List/Move List", fileName = "New Move List")]
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