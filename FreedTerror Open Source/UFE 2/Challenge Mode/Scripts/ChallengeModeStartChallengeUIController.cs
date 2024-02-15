using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class ChallengeModeStartChallengeUIController : MonoBehaviour
    {       
        [HideInInspector]
        public ChallengeModeController.ChallengeOptions challengeOptions;
        [SerializeField]
        private Text challengeNameText;

        private void Update()
        {
            if (challengeOptions != null
                && challengeNameText != null)
            {
                challengeNameText.text = challengeOptions.challengeName;
            }      
        }

        public void StartChallenge()
        {
            ChallengeModeController.instance.StartChallenge(challengeOptions);
        }
    }
}