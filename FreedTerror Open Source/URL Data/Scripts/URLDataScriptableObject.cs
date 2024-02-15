using UnityEngine;

namespace FreedTerror
{
    [CreateAssetMenu(menuName = "FreedTerror/URL/URL Data", fileName = "New URL Data")]
    public class URLDataScriptableObject : ScriptableObject
    {
        [TextArea(0, int.MaxValue)]
        public string urlName;
        [TextArea(0, int.MaxValue)]
        public string url;

        [NaughtyAttributes.Button]
        public void OpenURL()
        {
            if (url == "")
            {
                return;
            }

            Application.OpenURL(url);
        }
    }
}