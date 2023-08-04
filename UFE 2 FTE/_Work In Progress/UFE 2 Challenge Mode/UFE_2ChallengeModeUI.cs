using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class UFE_2ChallengeModeUI : MonoBehaviour
    {
        [SerializeField]
        private DefaultPauseScreen defaultPauseScreen;

        [SerializeField]
        private Button previousChallengeButton;

        [SerializeField]
        private Button nextChallengeButton;

        // Start is called before the first frame update
        void Start()
        {
            switch (UFE.gameMode)
            {
                case GameMode.ChallengeMode:
                    if (previousChallengeButton != null
                        && UFE.challengeMode.currentChallenge == 0)
                    {
                        previousChallengeButton.interactable = false;
                    }

                    if (nextChallengeButton != null
                        && UFE.challengeMode.currentChallenge + 1 == UFE.config.challengeModeOptions.Length)
                    {
                        nextChallengeButton.interactable = false;
                    }
                    break;
            }
        }

        /*public void NextChallenge()
        {
            defaultPauseScreen.ResumeGame();
            UFE_2ChallengeModeManager.Instance.defaultChallengeModeGUI.NextChallenge();
        }

        public void PreviousChallenge()
        {
            defaultPauseScreen.ResumeGame();
            UFE_2ChallengeModeManager.Instance.defaultChallengeModeGUI.PreviousChallenge();
        }

        public void RepeatChallenge()
        {
            defaultPauseScreen.ResumeGame();
            UFE_2ChallengeModeManager.Instance.defaultChallengeModeGUI.RepeatChallenge();
        }

        public void CompleteChallenge()
        {
            defaultPauseScreen.ResumeGame();
            UFE_2ChallengeModeManager.Instance.defaultChallengeModeGUI.CompleteChallenge();
        }*/
    }
}
