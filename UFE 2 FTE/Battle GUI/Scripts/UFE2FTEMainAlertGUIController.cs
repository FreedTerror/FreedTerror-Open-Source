using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTEMainAlertGUIController : MonoBehaviour
    {
        //From a UFE fourm post
        /*in DefaultBattleGUI.cs At the top of
         * public override void OnShow ()
         * I just added
         * UFE.canvas.renderMode = RenderMode.ScreenSpaceCamera;
         * UFE.canvas.worldCamera = Camera.main;
         * UFE.canvas.sortingLayerName = "UI";
         * The last line there may not be needed for yourself, I was doing it so my characters could jump in front of the UI.
         * If that's what you're going for, I recommend giving all your UI elements a shader with ZTest Always. And if you want some UI elements in front of your characters, you should add a secondary canvas to your BattleGUI prefab with a different sorting layer, that's how I got it to work for me!
         */

        // UFE.CastNewRound(3)
        // This controls the delay of when the characters can move.
        // Can add an option in the editor to control the delay on this.

        private int currentRound;

        [SerializeField]
        private GameObject alertGameObject;
        [SerializeField]
        private Text alertText;
        [SerializeField]
        private float alertDuration;
        private float alertDurationElapsedTime;
        [SerializeField]
        private Color32 roundAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private string finalRoundAlertName = "FINAL ROUND";
        [SerializeField]
        private Color32 finalRoundAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private string trainingModeAlertName = "TRAINING MODE";
        [SerializeField]
        private Color32 trainingModeAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private string fightAlertName = "FIGHT";
        [SerializeField]
        private Color32 fightAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private float fightAlertStartTime;
        [SerializeField]
        private string timeOutAlertName = "TIME OUT";
        [SerializeField]
        private Color32 timeOutAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private string perfectAlertName = "PERFECT";
        [SerializeField]
        private Color32 perfectAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private string koAlertName = "K.O.";
        [SerializeField]
        private Color32 koAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private string doubleKoAlertName = "DOUBLE K.O.";
        [SerializeField]
        private Color32 doubleKoAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private string drawAlertName = "DRAW";
        [SerializeField]
        private Color32 drawAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private float drawAlertStartTime;
        [SerializeField]
        private UFE2FTEGCFreeStringNumbersScriptableObject gCFreeStringNumbersScriptableObject;

        private void OnEnable()
        {
            UFE.OnScreenChanged += OnScreenChanged;
            UFE.OnNewAlert += OnNewAlert;
            UFE.OnRoundBegins += OnRoundBegins;
            UFE.OnRoundEnds += OnRoundEnds;
        }

        private void Start()
        {
            ResetMainAlertGUI();
        }

        private void Update()
        {
            float deltaTime = (float)UFE.fixedDeltaTime;

            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null
                || gCFreeStringNumbersScriptableObject == null)
            {
                return;
            }

            currentRound = UFE.GetPlayer1ControlsScript().roundsWon + UFE.GetPlayer2ControlsScript().roundsWon + 1;

            SetMainAlertGUI(deltaTime);
        }

        private void OnDisable()
        {
            UFE.OnScreenChanged -= OnScreenChanged;
            UFE.OnNewAlert -= OnNewAlert;
            UFE.OnRoundBegins -= OnRoundBegins;
            UFE.OnRoundEnds -= OnRoundEnds;
        }

        private void OnScreenChanged(UFEScreen previousScreen, UFEScreen newScreen)
        {
            if (newScreen == UFE.config.gameGUI.versusModeAfterBattleScreen
                || newScreen == UFE.config.gameGUI.onlineModeAfterBattleScreen
                || newScreen == UFE.config.gameGUI.challengeModeAfterBattleScreen)
            {
                ResetMainAlertGUI();
            }
        }

        private void OnNewAlert(string newString, ControlsScript player)
        {
            ResetMainAlertGUIOnNewAlert(newString);
        }

        private void OnRoundBegins(int newInt)
        {
            switch (UFE.gameMode)
            {
                case GameMode.TrainingRoom:
                    StartMainAlertGUI(trainingModeAlertName, trainingModeAlertColor);
                    return;

                case GameMode.ChallengeMode:
                    break;

                default:
                    if (currentRound == UFE.config.roundOptions.totalRounds)
                    {
                        StartMainAlertGUI(finalRoundAlertName, finalRoundAlertColor);
                    }
                    else
                    {
                        StartMainAlertGUI(UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.roundStringNumberArray, currentRound), roundAlertColor);
                    }
                    break;
            }
        }

        private void OnRoundEnds(ControlsScript winner, ControlsScript loser)
        {
            if (winner == null
                || loser == null)
            {
                if (UFE.timer <= 0)
                {
                    StartMainAlertGUI(timeOutAlertName, timeOutAlertColor);
                }
                else if (UFE.timer > 0
                    && UFE.GetPlayer1ControlsScript().currentLifePoints == 0              
                    && UFE.GetPlayer2ControlsScript().currentLifePoints == 0)
                {
                    StartMainAlertGUI(doubleKoAlertName, doubleKoAlertColor);
                }

                return;
            }

            if (winner != null
                && loser != null)
            {
                switch (UFE.gameMode)
                {
                    case GameMode.StoryMode:
                    case GameMode.VersusMode:
                    case GameMode.TrainingRoom:
                    case GameMode.NetworkGame:
                        if (UFE.timer == 0)
                        {
                            StartMainAlertGUI(timeOutAlertName, timeOutAlertColor);
                        }
                        else if (UFE.timer != 0
                            && winner.currentLifePoints == winner.myInfo.lifePoints)
                        {
                            StartMainAlertGUI(perfectAlertName, perfectAlertColor);
                        }
                        else if (UFE.timer != 0
                            && winner.currentLifePoints != winner.myInfo.lifePoints)
                        {
                            StartMainAlertGUI(koAlertName, koAlertColor);
                        }
                        break;

                    case GameMode.ChallengeMode:
                        break;
                }
            }
        }

        private void StartMainAlertGUI(string message, Color32 color)
        {
            ResetMainAlertGUI();

            SetGameObjectActive(alertGameObject, true);

            SetTextMessage(alertText, message, color);

            alertDurationElapsedTime = 0;
        }

        private void SetMainAlertGUI(float deltaTime)
        {
            if (alertDurationElapsedTime < alertDuration)
            {
                alertDurationElapsedTime += deltaTime;
                
                SetGameObjectActive(alertGameObject, true);
            }
            else
            {
                ResetMainAlertGUI();
            }

            SetTimedAlerts();
        }

        private void SetTimedAlerts()
        {
            if (alertText == null)
            {
                return;
            }

            if (alertText.text == UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.roundStringNumberArray, currentRound)
                && alertDurationElapsedTime >= fightAlertStartTime)
            {
                StartMainAlertGUI(fightAlertName, fightAlertColor);
            }
            else if (alertText.text == finalRoundAlertName
                && alertDurationElapsedTime >= fightAlertStartTime)
            {
                StartMainAlertGUI(fightAlertName, fightAlertColor);
            }
            else if (alertText.text == trainingModeAlertName
                && alertDurationElapsedTime >= fightAlertStartTime)
            {
                StartMainAlertGUI(fightAlertName, fightAlertColor);
            }
            else if (alertText.text == timeOutAlertName
                && alertDurationElapsedTime >= drawAlertStartTime
                && UFE.timer <= 0
                && UFE.GetPlayer1ControlsScript().currentLifePoints == UFE.GetPlayer2ControlsScript().currentLifePoints
                && UFE.GetPlayer2ControlsScript().currentLifePoints == UFE.GetPlayer1ControlsScript().currentLifePoints)
            {
                StartMainAlertGUI(drawAlertName, drawAlertColor);
            }
            else if (alertText.text == doubleKoAlertName
                && alertDurationElapsedTime >= drawAlertStartTime)
            {
                StartMainAlertGUI(drawAlertName, drawAlertColor);
            }
        }

        private void ResetMainAlertGUI()
        {
            SetGameObjectActive(alertGameObject, false);

            SetTextMessage(alertText, "");

            alertDurationElapsedTime = alertDuration;
        }

        private void ResetMainAlertGUIOnNewAlert(string resetMessage)
        {
            if (resetMessage == UFE.config.selectedLanguage.fight
                || resetMessage == UFE.config.selectedLanguage.challengeBegins)
            {
                ResetMainAlertGUI();
            }
        }

        private static void SetGameObjectActive(GameObject gameObject, bool active)
        {
            if (gameObject == null)
            {
                return;
            }

            gameObject.SetActive(active);
        }

        private static void SetTextMessage(Text text, string message, Color32? color = null)
        {
            if (text == null)
            {
                return;
            }

            text.text = message;

            if (color != null)
            {
                text.color = (Color32)color;
            }
        }
    }
}