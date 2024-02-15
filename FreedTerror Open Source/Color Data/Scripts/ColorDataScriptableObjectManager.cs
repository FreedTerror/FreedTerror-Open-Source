using UnityEngine;

namespace FreedTerror
{
    [CreateAssetMenu(menuName = "FreedTerror/Color/Color Data Manager", fileName = "New Color Data Manager")]
    public class ColorDataScriptableObjectManager : ScriptableObject
    {
        public ColorDataScriptableObject[] colorDataScriptableObjectArray;
    }
}