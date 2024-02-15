using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror
{
    public class URLUIController : MonoBehaviour
    {
        public URLDataScriptableObject urlDataScriptableObject;
        [SerializeField]
        private Text urlNameText;

        private void Update()
        {
            if (urlDataScriptableObject != null
                && urlNameText != null)
            {
                urlNameText.text = urlDataScriptableObject.urlName;
            }
        }

        public void OpenURL()
        {
            if (urlDataScriptableObject == null)
            {
                return;
            }

            urlDataScriptableObject.OpenURL();
        }
    }
}
