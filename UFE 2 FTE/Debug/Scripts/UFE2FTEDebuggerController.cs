using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTEDebuggerController : MonoBehaviour
    {
        private bool debuggerFindAttempted;
        private enum ActiveMode
        {
            Disabled,
            Enabled
        }
        [SerializeField]
        private ActiveMode debuggerActiveMode = ActiveMode.Enabled;
        private RectTransform player1DebuggerRectTransform;
        [SerializeField]
        private Vector2 player1DebuggerAnchoredPosition;
        private RectTransform player2DebuggerRectTransform;
        [SerializeField]
        private Vector2 player2DebuggerAnchoredPosition;

        private void Update()
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                debuggerFindAttempted = false;

                return;
            }

            SetAllDebuggersPosition();
        }

        private void SetAllDebuggersPosition()
        {
            if (UFE.config.debugOptions.debugMode == true
                && UFE.config.debugOptions.trainingModeDebugger == false)
            {
                if (debuggerFindAttempted == false)
                {
                    debuggerFindAttempted = true;

                    player1DebuggerRectTransform = GameObject.Find("Debugger1").GetComponent<RectTransform>();

                    player2DebuggerRectTransform = GameObject.Find("Debugger2").GetComponent<RectTransform>();
                }

                if (player1DebuggerRectTransform != null)
                {
                    player1DebuggerRectTransform.anchoredPosition = player1DebuggerAnchoredPosition;
                }

                if (player2DebuggerRectTransform != null)
                {
                    player2DebuggerRectTransform.anchoredPosition = player2DebuggerAnchoredPosition;
                }
            }
            else if (UFE.config.debugOptions.debugMode == true
                && UFE.config.debugOptions.trainingModeDebugger == true
                && UFE.gameMode == GameMode.TrainingRoom)
            {
                if (debuggerFindAttempted == false)
                {
                    debuggerFindAttempted = true;

                    player1DebuggerRectTransform = GameObject.Find("Debugger1").GetComponent<RectTransform>();

                    player2DebuggerRectTransform = GameObject.Find("Debugger2").GetComponent<RectTransform>();
                }

                if (player1DebuggerRectTransform != null)
                {
                    player1DebuggerRectTransform.anchoredPosition = player1DebuggerAnchoredPosition;
                }

                if (player2DebuggerRectTransform != null)
                {
                    player2DebuggerRectTransform.anchoredPosition = player2DebuggerAnchoredPosition;
                }
            }

            if (debuggerActiveMode == ActiveMode.Disabled)
            {
                SetActiveAllDebuggers(false);
            }
            else if (debuggerActiveMode == ActiveMode.Enabled)
            {
                SetActiveAllDebuggers(true);
            }
        }

        private void SetActiveAllDebuggers(bool active)
        {
            if (player1DebuggerRectTransform != null)
            {
                SetGameObjectActive(player1DebuggerRectTransform.gameObject, active);
            }

            if (player2DebuggerRectTransform != null)
            {
                SetGameObjectActive(player2DebuggerRectTransform.gameObject, active);
            }
        }

        [NaughtyAttributes.Button]
        private void DisableAllDebuggers()
        {
            debuggerActiveMode = ActiveMode.Disabled;

            SetActiveAllDebuggers(false);
        }

        [NaughtyAttributes.Button]
        private void EnableAllDebuggers()
        {
            debuggerActiveMode = ActiveMode.Enabled;

            SetActiveAllDebuggers(true);
        }

        private static void SetGameObjectActive(GameObject gameObject, bool active)
        {
            if (gameObject == null)
            {
                return;
            }

            gameObject.SetActive(active);
        }
    }
}
