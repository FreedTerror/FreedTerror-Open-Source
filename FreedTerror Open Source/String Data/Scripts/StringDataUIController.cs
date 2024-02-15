using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror
{
    [ExecuteInEditMode]
    public class StringDataUIController : MonoBehaviour
    {
        [SerializeField]
        private StringDataScriptableObject stringDataScriptableObject;
        [SerializeField]
        private Text stringDataText;

        private void Update()
        {
            if (stringDataScriptableObject != null
                || stringDataText != null)
            {
                stringDataText.text = stringDataScriptableObject.stringData;
            }
        }
    }
}
