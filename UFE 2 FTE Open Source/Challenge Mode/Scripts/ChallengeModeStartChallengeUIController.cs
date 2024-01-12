using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class ChallengeModeStartChallengeUIController : MonoBehaviour
    {       
        [HideInInspector]
        public ChallengeModeController.ChallengeOptions challengeOptions;
        [SerializeField]
        private Text challengeNameText;

        private void Update()
        {
            SetChallengeNameText();
        }

        public void StartChallenge()
        {
            ChallengeModeController.instance.StartChallenge(challengeOptions);
        }

        private void SetChallengeNameText()
        {
            if (challengeNameText == null
                || challengeOptions == null)
            {
                return;
            }

            challengeNameText.text = challengeOptions.challengeName;
        }
    }
}