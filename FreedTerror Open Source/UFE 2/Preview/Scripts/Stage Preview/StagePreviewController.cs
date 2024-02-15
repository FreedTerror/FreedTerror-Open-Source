using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class StagePreviewController : MonoBehaviour
    {
        [SerializeField]
        private StagePreviewScriptableObject stagePreviewScriptableObject;
        [SerializeField]
        private Text stageNameText;
 
        private void Update()
        {
            if (stageNameText != null)
            {
                if (UFE.GetStage() != null)
                {
                    stageNameText.text = UFE.GetStage().stageName;
                }

                if (stagePreviewScriptableObject != null)
                {
                    stagePreviewScriptableObject.PreviewStage(stageNameText.text);
                }
            }
        }

        private void OnDisable()
        {
            if (stagePreviewScriptableObject != null)
            {
                stagePreviewScriptableObject.UnloadAllStagePreview();
            }
        }
    }
}