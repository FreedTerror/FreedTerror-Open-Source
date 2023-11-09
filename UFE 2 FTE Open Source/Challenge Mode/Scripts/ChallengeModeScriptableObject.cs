using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu()]
    public class ChallengeModeScriptableObject : ScriptableObject
    {
        public ChallengeModeController.ChallengeOptions[] challengeOptionsArray;

        [NaughtyAttributes.Button]
        public void StartNextChallenge()
        {
            ChallengeModeController.Instance.StartNextChallenge();
        }

        [NaughtyAttributes.Button]
        public void StartPreviousChallenge()
        {
            ChallengeModeController.Instance.StartPreviousChallenge();
        }

        [NaughtyAttributes.Button]
        public void RestartCurrentChallenge()
        {
            ChallengeModeController.Instance.RestartCurrentChallenge();
        }

        [NaughtyAttributes.Button]
        public void CompleteCurrentChallenge()
        {
            ChallengeModeController.Instance.CompleteCurrentChallenge();
        }

        [NaughtyAttributes.Button]
        public void ResetCurrentChallenge()
        {
            ChallengeModeController.Instance.ResetCurrentChallenge();
        }

        [NaughtyAttributes.Button]
        public void NextChallengeAction()
        {
            ChallengeModeController.Instance.NextChallengeAction();
        }

        [NaughtyAttributes.Button]
        public void PreviousChallengeAction()
        {
            ChallengeModeController.Instance.PreviousChallengeAction();
        }

        [NaughtyAttributes.Button]
        public void ResetCurrentChallengeAction()
        {
            ChallengeModeController.Instance.ResetCurrentChallengeAction();
        }
    }
}