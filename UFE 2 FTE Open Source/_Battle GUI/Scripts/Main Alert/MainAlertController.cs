using FPLibrary;
using UFE3D;
using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class MainAlertController : MonoBehaviour
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

        public delegate void StringHandler(string message);
        public static event StringHandler OnMainAlertEvent;
        public static void CallOnMainAlertEvent(string message)
        {
            if (OnMainAlertEvent == null)
            {
                return;
            }

            OnMainAlertEvent(message);
        }

        [SerializeField]
        private GameObject alertGameObject;
        [SerializeField]
        private Text alertText;
        public Fix64 alertDuration = 2;
        private Fix64 alertDurationElapsedTime;

        private void OnEnable()
        {
            OnMainAlertEvent += OnMainAlert;
            UFE.OnRoundBegins += OnRoundBegins;
            UFE.OnRoundEnds += OnRoundEnds;
        }

        private void Start()
        {
            ResetMainAlert();
        }

        private void Update()
        {
            Fix64 deltaTime = UFE.fixedDeltaTime;

            UpdateMainAlert(deltaTime);
        }

        private void OnDisable()
        {
            OnMainAlertEvent -= OnMainAlert;
            UFE.OnRoundBegins -= OnRoundBegins;
            UFE.OnRoundEnds -= OnRoundEnds;
        }

        #region Main Alert Methods

        private void OnMainAlert(string message)
        {
            StartMainAlert(message);
        }

        private void StartMainAlert(string message)
        {
            if (message == "")
            {
                return;
            }

            alertText.text = message;
            alertDurationElapsedTime = 0;
        }

        private void UpdateMainAlert(Fix64 deltaTime)
        {
            alertDurationElapsedTime += deltaTime;
            if (alertDurationElapsedTime < alertDuration)
            {
                alertGameObject.SetActive(true);
            }
            else
            {
                alertGameObject.SetActive(false);
            }
        }

        private void ResetMainAlert()
        {
            alertGameObject.SetActive(false);
            alertText.text = "";
            alertDurationElapsedTime = alertDuration;
        }

        #endregion

        #region On Round Methods

        private void OnRoundBegins(int newInt)
        {
            StartMainAlert("Ready");
        }

        private void OnRoundEnds(ControlsScript winner, ControlsScript loser)
        {
            if (winner == null
                || loser == null)
            {
                if (UFE.timer <= 0
                   && UFE.GetPlayer1ControlsScript().currentLifePoints == UFE.GetPlayer2ControlsScript().currentLifePoints
                   && UFE.GetPlayer2ControlsScript().currentLifePoints == UFE.GetPlayer1ControlsScript().currentLifePoints)
                {
                    StartMainAlert("Draw");
                }
                else if (UFE.timer <= 0
                    && UFE.GetPlayer1ControlsScript().currentLifePoints != UFE.GetPlayer2ControlsScript().currentLifePoints
                    && UFE.GetPlayer2ControlsScript().currentLifePoints != UFE.GetPlayer1ControlsScript().currentLifePoints)
                {
                    StartMainAlert("TimeOut");
                }
                else if (UFE.timer > 0
                    && UFE.GetPlayer1ControlsScript().currentLifePoints <= 0
                    && UFE.GetPlayer2ControlsScript().currentLifePoints <= 0)
                {
                    StartMainAlert("DoubleKo");
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
                        if (UFE.timer <= 0)
                        {
                            StartMainAlert("TimeOut");
                        }
                        else if (UFE.timer > 0
                            && winner.currentLifePoints == winner.myInfo.lifePoints)
                        {
                            StartMainAlert("Perfect");
                        }
                        else if (UFE.timer > 0
                            && winner.currentLifePoints != winner.myInfo.lifePoints)
                        {
                            StartMainAlert("Ko");
                        }
                        break;

                    case GameMode.ChallengeMode:
                        break;
                }
            }
        }

        #endregion
    }
}