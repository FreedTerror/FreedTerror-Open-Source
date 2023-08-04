using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEStagePreviewController : MonoBehaviour
    {
        [SerializeField]
        private Text stageNameText;
        public UFE2FTEStagePreviewScriptableObject stagePreviewScriptableObject;

        private void Update()
        {
            if (stageNameText != null
                && stagePreviewScriptableObject != null)
            {
                UFE2FTEStagePreviewScriptableObject.StagePreviewOptions.SetStagePreviewByStageName(stageNameText.text, stagePreviewScriptableObject.stagePreviewOptionsArray);
            }
        }
    }
}