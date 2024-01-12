using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class AIUIController : MonoBehaviour
    {
        [SerializeField]
        private Text aiModeText;
        [SerializeField]
        private Text aiThrowTechModeText;

        private void Update()
        {
            UFE2FTE.SetTextMessage(aiModeText, System.Enum.GetName(typeof(UFE2FTE.AIMode), UFE2FTE.instance.aiMode));

            UFE2FTE.SetTextMessage(aiThrowTechModeText, UFE2FTE.GetStringFromEnum(UFE2FTE.instance.aiThrowTechMode));
        }

        public void NextAIMode()
        {
            UFE2FTE.instance.aiMode = UFE2FTE.GetNextEnum(UFE2FTE.instance.aiMode);
        }

        public void PreviousAIMode()
        {
            UFE2FTE.instance.aiMode = UFE2FTE.GetPreviousEnum(UFE2FTE.instance.aiMode);
        }

        public void NextAIThrowTechMode()
        {
            UFE2FTE.instance.aiThrowTechMode = UFE2FTE.GetNextEnum(UFE2FTE.instance.aiThrowTechMode);
        }

        public void PreviousAIThrowTechMode()
        {
            UFE2FTE.instance.aiThrowTechMode = UFE2FTE.GetPreviousEnum(UFE2FTE.instance.aiThrowTechMode);
        }
    }
}
