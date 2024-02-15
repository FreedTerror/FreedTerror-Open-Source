using UnityEngine;

namespace FreedTerror
{
    [CreateAssetMenu(menuName = "FreedTerror/URL/URL Data Manager", fileName = "New URL Data Manager" )]
    public class URLDataScriptableObjectManager : ScriptableObject
    {
        public URLDataScriptableObject[] urlDataScriptableObjectArray;
    }
}
