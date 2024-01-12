using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Color Data Manager", menuName = "U.F.E. 2 F.T.E./Color/Color Data Manager")]
    public class ColorDataScriptableObjectManager : ScriptableObject
    {
        public ColorDataScriptableObject[] colorDataScriptableObjectArray;
    }
}