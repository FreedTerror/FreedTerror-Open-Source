using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    [ExecuteInEditMode]
    public class StringDataUIController : MonoBehaviour
    {
        [SerializeField]
        private StringDataScriptableObject stringDataScriptableObject;
        [SerializeField]
        private Text stringDataText;

        private void Start()
        {
            SetStringDataTextMessage();
        }

#if UNITY_EDITOR
        private void Update()
        {
            SetStringDataTextMessage();
        }
#endif

        private void SetStringDataTextMessage()
        {
            if (stringDataScriptableObject == null
                || stringDataText == null)
            {
                return;
            }

            stringDataText.text = stringDataScriptableObject.stringData;
        }
    }
}
