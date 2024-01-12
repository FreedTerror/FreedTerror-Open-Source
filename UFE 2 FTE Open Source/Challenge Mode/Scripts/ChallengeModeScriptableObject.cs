using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Challenges", menuName = "U.F.E. 2 F.T.E./Challenge Mode/Challenges")]
    public class ChallengeModeScriptableObject : ScriptableObject
    {
        public ChallengeModeController.ChallengeOptions[] challengeOptionsArray;

        [NaughtyAttributes.Button]
        public void StartNextChallenge()
        {
            ChallengeModeController.instance.StartNextChallenge();
        }

        [NaughtyAttributes.Button]
        public void StartPreviousChallenge()
        {
            ChallengeModeController.instance.StartPreviousChallenge();
        }

        [NaughtyAttributes.Button]
        public void RestartCurrentChallenge()
        {
            ChallengeModeController.instance.RestartCurrentChallenge();
        }

        [NaughtyAttributes.Button]
        public void CompleteCurrentChallenge()
        {
            ChallengeModeController.instance.CompleteCurrentChallenge();
        }

        [NaughtyAttributes.Button]
        public void ResetCurrentChallenge()
        {
            ChallengeModeController.instance.ResetCurrentChallenge();
        }

        [NaughtyAttributes.Button]
        public void NextChallengeAction()
        {
            ChallengeModeController.instance.NextChallengeAction();
        }

        [NaughtyAttributes.Button]
        public void PreviousChallengeAction()
        {
            ChallengeModeController.instance.PreviousChallengeAction();
        }

        [NaughtyAttributes.Button]
        public void ResetCurrentChallengeAction()
        {
            ChallengeModeController.instance.ResetCurrentChallengeAction();
        }
    }
}