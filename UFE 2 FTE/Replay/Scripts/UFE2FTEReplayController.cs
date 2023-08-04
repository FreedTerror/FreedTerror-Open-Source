using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTEReplayController : MonoBehaviour
    {
        [SerializeField]
        private GameMode[] excludedGameModeArray;

        [SerializeField]
        private ButtonPress[] excludedButtonPressArray;

        private void OnEnable()
        {
            UFE.OnScreenChanged += OnScreenChanged;
            UFE.OnRoundBegins += OnRoundBegins;
        }

        private void FixedUpdate()
        {
            UFE2FTEReplayOptionsManager.excludedGameModeArray = excludedGameModeArray;

            UFE2FTEReplayOptionsManager.excludedButtonPressArray = excludedButtonPressArray;

            UFE2FTEReplayOptionsManager.Record();

            UFE2FTEReplayOptionsManager.Playback();
        }

        private void OnDisable()
        {
            UFE.OnScreenChanged -= OnScreenChanged;
            UFE.OnRoundBegins -= OnRoundBegins;
        }

        private void OnScreenChanged(UFEScreen previousScreen, UFEScreen newScreen)
        {
            if (newScreen == UFE.GetCharacterSelectionScreen()
                || newScreen == UFE.GetConnectionLostScreen()
                || newScreen == UFE.GetCreditsScreen()
                || newScreen == UFE.GetHostGameScreen()
                || newScreen == UFE.GetIntroScreen()
                || newScreen == UFE.GetJoinGameScreen()
                || newScreen == UFE.GetLoadingBattleScreen()
                || newScreen == UFE.GetMainMenuScreen()
                || newScreen == UFE.GetNetworkGameScreen()
                || newScreen == UFE.GetOptionsScreen()
                || newScreen == UFE.GetStageSelectionScreen()
                || newScreen == UFE.GetStoryModeCongratulationsScreen()
                || newScreen == UFE.GetStoryModeContinueScreen()
                || newScreen == UFE.GetStoryModeGameOverScreen()
                //|| newScreen == UFE.GetVersusModeAfterBattleScreen()
                || newScreen == UFE.GetVersusModeScreen())
            {
                UFE2FTEReplayOptionsManager.replaySaved = false;

                UFE2FTEReplayOptionsManager.ResetRecordedReplayData();
            }

            if (newScreen == UFE.GetCharacterSelectionScreen()
                || newScreen == UFE.GetConnectionLostScreen()
                || newScreen == UFE.GetCreditsScreen()
                || newScreen == UFE.GetHostGameScreen()
                || newScreen == UFE.GetIntroScreen()
                || newScreen == UFE.GetJoinGameScreen()
                //|| newScreen == UFE.GetLoadingBattleScreen()
                || newScreen == UFE.GetMainMenuScreen()
                || newScreen == UFE.GetNetworkGameScreen()
                || newScreen == UFE.GetOptionsScreen()
                || newScreen == UFE.GetStageSelectionScreen()
                || newScreen == UFE.GetStoryModeCongratulationsScreen()
                || newScreen == UFE.GetStoryModeContinueScreen()
                || newScreen == UFE.GetStoryModeGameOverScreen()
                //|| newScreen == UFE.GetVersusModeAfterBattleScreen()
                || newScreen == UFE.GetVersusModeScreen())
            {
                UFE2FTEReplayOptionsManager.ResetLoadedReplayData();
            }
        }

        private void OnRoundBegins(int roundNumber)
        {
            UFE2FTEReplayOptionsManager.ResetReplayVariables();

            UFE2FTEReplayOptionsManager.CreateNewRecordedRoundData();
        }
    }
}