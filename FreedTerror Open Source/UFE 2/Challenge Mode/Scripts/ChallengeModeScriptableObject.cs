using UnityEngine;

namespace FreedTerror.UFE2
{
    [CreateAssetMenu(menuName = "FreedTerror/UFE 2/Challenge Mode/Challenges", fileName = "New Challenges")]
    public class ChallengeModeScriptableObject : ScriptableObject
    {
        public ChallengeModeController.ChallengeOptions[] challengeOptionsArray;

        [NaughtyAttributes.Button]
        private void StartNextChallenge()
        {
            ChallengeModeController.instance.StartNextChallenge();
        }

        [NaughtyAttributes.Button]
        private void StartPreviousChallenge()
        {
            ChallengeModeController.instance.StartPreviousChallenge();
        }

        [NaughtyAttributes.Button]
        private void CompleteCurrentChallenge()
        {
            ChallengeModeController.instance.CompleteCurrentChallenge();
        }

        [NaughtyAttributes.Button]
        private void ResetCurrentChallenge()
        {
            ChallengeModeController.instance.ResetCurrentChallenge();
        }

        [NaughtyAttributes.Button]
        private void RestartCurrentChallenge()
        {
            ChallengeModeController.instance.RestartCurrentChallenge();
        }

        [NaughtyAttributes.Button]
        private void NextChallengeAction()
        {
            ChallengeModeController.instance.NextChallengeAction();
        }

        [NaughtyAttributes.Button]
        private void PreviousChallengeAction()
        {
            ChallengeModeController.instance.PreviousChallengeAction();
        }

        [NaughtyAttributes.Button]
        private void ResetCurrentChallengeAction()
        {
            ChallengeModeController.instance.ResetCurrentChallengeAction();
        }
    }
}