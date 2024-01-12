using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New String Data", menuName = "U.F.E. 2 F.T.E./String/String Data")]
    public class StringDataScriptableObject : ScriptableObject
    {
        [TextArea(0, int.MaxValue)]
        public string stringData;
    }
}

