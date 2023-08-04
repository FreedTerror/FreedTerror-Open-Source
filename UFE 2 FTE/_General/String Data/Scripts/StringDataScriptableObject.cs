using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(menuName = "U.F.E. 2 F.T.E./String/String Data")]
    public class StringDataScriptableObject : ScriptableObject
    {    
        [field: TextArea(0, 100)]
        [field: SerializeField]
        public string stringData { get; private set; }
    }
}

