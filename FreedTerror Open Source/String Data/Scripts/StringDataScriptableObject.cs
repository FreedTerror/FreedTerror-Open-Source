using UnityEngine;

namespace FreedTerror
{
    [CreateAssetMenu(menuName = "FreedTerror/String/String Data", fileName = "New String Data")]
    public class StringDataScriptableObject : ScriptableObject
    {
        [TextArea(0, int.MaxValue)]
        public string stringData;
    }
}

