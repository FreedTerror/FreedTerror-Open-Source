using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class AIUIController : MonoBehaviour
    {
        [SerializeField]
        private Text aiModeText;
        private UFE2Manager.AIMode previousAiMode;
        [SerializeField]
        private Text aiThrowTechModeText;
        private UFE2Manager.Toggle previousAiThrowTechMode;

        private void Start()
        {
            previousAiThrowTechMode = UFE2Manager.instance.aiThrowTechMode;

            if (aiThrowTechModeText != null)
            {
                aiThrowTechModeText.text = System.Enum.GetName(typeof(UFE2Manager.Toggle), UFE2Manager.instance.aiThrowTechMode);
            }

            previousAiMode = UFE2Manager.instance.aiMode;

            if (aiModeText != null)
            {
                aiModeText.text = Utility.AddSpacesBeforeCapitalLetters(System.Enum.GetName(typeof(UFE2Manager.AIMode), UFE2Manager.instance.aiMode));
            }
        }

        private void Update()
        {
            if (previousAiMode != UFE2Manager.instance.aiMode)
            {
                previousAiMode = UFE2Manager.instance.aiMode;

                if (aiModeText != null)
                {
                    aiModeText.text = Utility.AddSpacesBeforeCapitalLetters(System.Enum.GetName(typeof(UFE2Manager.AIMode), UFE2Manager.instance.aiMode));
                }
            }
        
            if (previousAiThrowTechMode != UFE2Manager.instance.aiThrowTechMode)
            {
                previousAiThrowTechMode = UFE2Manager.instance.aiThrowTechMode;

                if (aiThrowTechModeText != null)
                {
                    aiThrowTechModeText.text = System.Enum.GetName(typeof(UFE2Manager.Toggle), UFE2Manager.instance.aiThrowTechMode);
                }
            }
        }

        public void NextAIMode()
        {
            UFE2Manager.instance.aiMode = Utility.GetNextEnum(UFE2Manager.instance.aiMode);
        }

        public void PreviousAIMode()
        {
            UFE2Manager.instance.aiMode = Utility.GetPreviousEnum(UFE2Manager.instance.aiMode);
        }

        public void NextAIThrowTechMode()
        {
            UFE2Manager.instance.aiThrowTechMode = Utility.GetNextEnum(UFE2Manager.instance.aiThrowTechMode);
        }

        public void PreviousAIThrowTechMode()
        {
            UFE2Manager.instance.aiThrowTechMode = Utility.GetPreviousEnum(UFE2Manager.instance.aiThrowTechMode);
        }
    }
}
