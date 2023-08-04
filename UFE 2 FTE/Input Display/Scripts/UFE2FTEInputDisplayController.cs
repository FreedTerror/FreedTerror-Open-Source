using UnityEngine;
using UFE3D;

namespace UFE2FTE
{   
    public class UFE2FTEInputDisplayController : MonoBehaviour
    {
        private RectTransform player1InputDisplayRectTransform;
        private RectTransform player2InputDisplayRectTransform;

        [SerializeField]
        private bool disableInputDisplayOnStart;
        [SerializeField]
        private int inputDisplayOffsetMax;

        private void OnEnable()
        {
            UFE2FTEInputDisplayOptionsManager.useInputDisplay = true;
        }

        private void Start()
        {
            if (UFE.gameMode == GameMode.StoryMode
                && UFE.config.debugOptions.displayInputsStoryMode == false)
            {
                enabled = false;

                return;               
            }
            else if (UFE.gameMode == GameMode.VersusMode
                && UFE.config.debugOptions.displayInputsVersus == false)
            {
                enabled = false;

                return;
            }
            else if (UFE.gameMode == GameMode.TrainingRoom
                && UFE.config.debugOptions.displayInputsTraining == false)
            {
                enabled = false;

                return;
            }
            else if (UFE.gameMode == GameMode.NetworkGame
                && UFE.config.debugOptions.displayInputsNetwork == false)
            {
                enabled = false;

                return;
            }
            else if (UFE.gameMode == GameMode.ChallengeMode
                && UFE.config.debugOptions.displayInputsChallengeMode == false)
            {
                enabled = false;

                return;
            }

            FindAndSetInputDisplayGameObjects();

            if (disableInputDisplayOnStart == true)
            {
                UFE2FTEInputDisplayOptionsManager.useInputDisplay = false;
            }
        }

        private void Update()
        {
            SetActiveInputDisplay(UFE2FTEInputDisplayOptionsManager.useInputDisplay);
        }

        private void OnDestroy()
        {
            UFE2FTEInputDisplayOptionsManager.useInputDisplay = true;
        }

        private void SetActiveInputDisplay(bool active)
        {
            if (player1InputDisplayRectTransform != null)
            {
                player1InputDisplayRectTransform.gameObject.SetActive(active);

                player1InputDisplayRectTransform.offsetMax = new Vector2(0, inputDisplayOffsetMax);
            }

            if (player2InputDisplayRectTransform != null)
            {
                player2InputDisplayRectTransform.gameObject.SetActive(active);

                player2InputDisplayRectTransform.offsetMax = new Vector2(0, inputDisplayOffsetMax);
            }
        }

        private void FindAndSetInputDisplayGameObjects()
        {
            GameObject inputDisplayGameObject = GameObject.Find("Player 1 Input Viewer");
            if (inputDisplayGameObject != null)
            {
                player1InputDisplayRectTransform = inputDisplayGameObject.GetComponent<RectTransform>();
            }

            inputDisplayGameObject = GameObject.Find("Player 2 Input Viewer");
            if (inputDisplayGameObject != null)
            {
                player2InputDisplayRectTransform = inputDisplayGameObject.GetComponent<RectTransform>();
            }
        }

        [NaughtyAttributes.Button]
        private void DisableInputDisplay()
        {
            FindAndSetInputDisplayGameObjects();

            UFE2FTEInputDisplayOptionsManager.useInputDisplay = false;
        }

        [NaughtyAttributes.Button]
        private void EnableInputDisplay()
        {
            FindAndSetInputDisplayGameObjects();

            UFE2FTEInputDisplayOptionsManager.useInputDisplay = true;
        }
    }
}