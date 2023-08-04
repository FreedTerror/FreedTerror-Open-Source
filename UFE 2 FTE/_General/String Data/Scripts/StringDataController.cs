using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class StringDataController : MonoBehaviour
    {
        public StringDataScriptableObject stringDataScriptableObject;
        public Text stringDataText;

        private void Start()
        {
            SetStringDataTextMessage();
        }

        [NaughtyAttributes.Button]
        private void SetStringDataTextMessage()
        {
            if (stringDataScriptableObject == null
                || stringDataText == null)
            {
                return;
            }

            stringDataText.text = stringDataScriptableObject.stringData;

#if UNITY_EDITOR
            if (Application.isEditor == true)
            {
                UnityEditor.EditorUtility.SetDirty(stringDataText);
            }
#endif
        }

        [NaughtyAttributes.Button]
        private void ClearStringDataTextMessage()
        {
            if (stringDataText == null)
            {
                return;
            }

            stringDataText.text = "";

#if UNITY_EDITOR
            if (Application.isEditor == true)
            {
                UnityEditor.EditorUtility.SetDirty(stringDataText);
            }
#endif
        }
    }
}
