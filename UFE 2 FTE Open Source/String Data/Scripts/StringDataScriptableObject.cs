using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(menuName = "U.F.E. 2 F.T.E./String Data/String Data", fileName = "New String Data")]
    public class StringDataScriptableObject : ScriptableObject
    {
        [TextArea(0, int.MaxValue)]
        public string stringData;
    }
}

