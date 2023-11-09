using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class ChallengeModeDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private GameObject challengeNameGameObject;
        [SerializeField]
        private Text challengeNameText;

        [SerializeField]
        private GameObject challengeDescriptionGameObject;
        [SerializeField]
        private Text challengeDescriptionText;

        [SerializeField]
        private GameObject challengeActionNameGameObject;
        [SerializeField]
        private Text challengeActionNameText;

        [SerializeField]
        private GameObject challengeActionDescriptionGameObject;
        [SerializeField]
        private Text challengeActionDescriptionText;

        [SerializeField]
        private Text[] inputDisplayTextArray;

        [SerializeField]
        private ChallengeModeChallengeActionDisplayUIController[] challengeActionDisplayUIControllerArray;

        private void Update()
        {
            if (ChallengeModeController.Instance.GetCurrentChallenges().Count <= 0)
            {
                return;
            }

            UpdateChallengeName();

            UpdateChallengeDescription();

            UpdateChallengeActionName();

            UpdateChallengeActionDescription();

            UpdateInputDisplay();

            UpdateChallengeActionDisplayUI();
        }

        public void UpdateChallengeName()
        {
            if (challengeNameGameObject != null
                && challengeNameText != null)
            {
                challengeNameText.text = ChallengeModeController.Instance.GetCurrentChallenge().challengeName;

                if (challengeNameText.text == "")
                {
                    challengeNameGameObject.SetActive(false);
                }
                else
                {
                    challengeNameGameObject.SetActive(true);
                }
            }
        }

        public void UpdateChallengeDescription()
        {
            if (challengeDescriptionGameObject != null
                && challengeDescriptionText != null)
            {
                challengeDescriptionText.text = ChallengeModeController.Instance.GetCurrentChallenge().challengeDescription;

                if (challengeDescriptionText.text == "")
                {
                    challengeDescriptionGameObject.SetActive(false);
                }
                else
                {
                    challengeDescriptionGameObject.SetActive(true);
                }
            }
        }

        public void UpdateChallengeActionName()
        {
            if (challengeActionNameGameObject != null
                && challengeActionNameText != null)
            {
                challengeActionNameText.text = ChallengeModeController.Instance.GetCurrentChallengeAction().challengeActionName;

                if (challengeActionNameText.text == "")
                {
                    challengeActionNameGameObject.SetActive(false);
                }
                else
                {
                    challengeActionNameGameObject.SetActive(true);
                }
            }
        }

        public void UpdateChallengeActionDescription()
        {
            if (challengeActionDescriptionGameObject != null
                && challengeActionDescriptionText != null)
            {
                challengeActionDescriptionText.text = ChallengeModeController.Instance.GetCurrentChallengeAction().challengeActionDescription;

                if (challengeActionDescriptionText.text == "")
                {
                    challengeActionDescriptionGameObject.SetActive(false);
                }
                else
                {
                    challengeActionDescriptionGameObject.SetActive(true);
                }
            }
        }

        private void UpdateInputDisplay()
        {
            int arrayBounds = inputDisplayTextArray.Length;
            for (int i = 0; i < arrayBounds; i++)
            {
                if (inputDisplayTextArray[i] == null)
                {
                    continue;
                }

                inputDisplayTextArray[i].gameObject.SetActive(false);
            }

            if (ChallengeModeController.Instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo != null)
            {
                if (ChallengeModeController.Instance.GetCurrentChallengeAction().moveInfoOptions.displayMoveInfoDefaultInputs == true)
                {
                    int length = ChallengeModeController.Instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.defaultInputs.buttonSequence.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (i >= arrayBounds
                            || inputDisplayTextArray[i] == null)
                        {
                            continue;
                        }

                        UFE2FTE.SetInputDisplayRotation(inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].transform, ChallengeModeController.Instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.defaultInputs.buttonSequence[i], ChallengeModeController.Instance.player1ControlsScript);
                        inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].text = UFE2FTE.Instance.inputDisplayScriptableObject.GetInputDisplayStringFromButtonPress(ChallengeModeController.Instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.defaultInputs.buttonSequence[i]);
                        inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].gameObject.SetActive(true);
                    }

                    length = ChallengeModeController.Instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.defaultInputs.buttonExecution.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (i >= arrayBounds
                            || inputDisplayTextArray[i] == null)
                        {
                            continue;
                        }

                        UFE2FTE.SetInputDisplayRotation(inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].transform, ChallengeModeController.Instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.defaultInputs.buttonExecution[i], ChallengeModeController.Instance.player1ControlsScript);
                        inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].text = UFE2FTE.Instance.inputDisplayScriptableObject.GetInputDisplayStringFromButtonPress(ChallengeModeController.Instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.defaultInputs.buttonExecution[i]);
                        inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].gameObject.SetActive(true);
                    }
                }

                if (ChallengeModeController.Instance.GetCurrentChallengeAction().moveInfoOptions.displayMoveInfoAlternativeInputs == true)
                {
                    int length = ChallengeModeController.Instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.altInputs.buttonSequence.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (i >= arrayBounds
                            || inputDisplayTextArray[i] == null)
                        {
                            continue;
                        }

                        UFE2FTE.SetInputDisplayRotation(inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].transform, ChallengeModeController.Instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.altInputs.buttonSequence[i], ChallengeModeController.Instance.player1ControlsScript);
                        inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].text = UFE2FTE.Instance.inputDisplayScriptableObject.GetInputDisplayStringFromButtonPress(ChallengeModeController.Instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.altInputs.buttonSequence[i]);
                        inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].gameObject.SetActive(true);
                    }

                    length = ChallengeModeController.Instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.altInputs.buttonExecution.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (i >= arrayBounds
                            || inputDisplayTextArray[i] == null)
                        {
                            continue;
                        }

                        UFE2FTE.SetInputDisplayRotation(inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].transform, ChallengeModeController.Instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.altInputs.buttonExecution[i], ChallengeModeController.Instance.player1ControlsScript);
                        inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].text = UFE2FTE.Instance.inputDisplayScriptableObject.GetInputDisplayStringFromButtonPress(ChallengeModeController.Instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.altInputs.buttonExecution[i]);
                        inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].gameObject.SetActive(true);
                    }
                }
            }

            int length1 = ChallengeModeController.Instance.GetCurrentChallengeAction().inputDisplayOptions.inputDisplayButtonPressArray.Length;
            for (int i = 0; i < length1; i++)
            {
                if (i >= arrayBounds
                    || inputDisplayTextArray[i] == null)
                {
                    continue;
                }

                UFE2FTE.SetInputDisplayRotation(inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].transform, ChallengeModeController.Instance.GetCurrentChallengeAction().inputDisplayOptions.inputDisplayButtonPressArray[i], ChallengeModeController.Instance.player1ControlsScript);
                inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].text = UFE2FTE.Instance.inputDisplayScriptableObject.GetInputDisplayStringFromButtonPress(ChallengeModeController.Instance.GetCurrentChallengeAction().inputDisplayOptions.inputDisplayButtonPressArray[i]);
                inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].gameObject.SetActive(true);
            }

            int GetFirstDisabledGameObjectIndex()
            {
                int length = inputDisplayTextArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (inputDisplayTextArray[i] == null
                        || inputDisplayTextArray[i].gameObject.activeInHierarchy == true)
                    {
                        continue;
                    }

                    return i;
                }

                return 0;
            }
        }

        private void UpdateChallengeActionDisplayUI()
        {
            int arrayBounds = ChallengeModeController.Instance.GetCurrentChallengeActions().Length;
            int challengeActionDisplayLength = challengeActionDisplayUIControllerArray.Length;
            for (int i = 0; i < challengeActionDisplayLength; i++)
            {
                if (challengeActionDisplayUIControllerArray[i] == null)
                {
                    continue;
                }

                if (i < arrayBounds)
                {
                    challengeActionDisplayUIControllerArray[i].gameObject.SetActive(true);

                    if (challengeActionDisplayUIControllerArray[i].challengeActionNameGameObject != null)
                    {
                        if (challengeActionDisplayUIControllerArray[i].challengeActionNameText != null)
                        {
                            challengeActionDisplayUIControllerArray[i].challengeActionNameText.text = ChallengeModeController.Instance.GetCurrentChallengeActions()[i].challengeActionName;

                            if (challengeActionDisplayUIControllerArray[i].challengeActionNameText.text != "")
                            {
                                challengeActionDisplayUIControllerArray[i].challengeActionNameGameObject.SetActive(true);
                            }
                            else
                            {
                                challengeActionDisplayUIControllerArray[i].challengeActionNameGameObject.SetActive(false);
                            }
                        }
                    }

                    if (challengeActionDisplayUIControllerArray[i].challengeActionDescriptionGameObject != null)
                    {
                        if (challengeActionDisplayUIControllerArray[i].challengeActionDescriptionText != null)
                        {
                            challengeActionDisplayUIControllerArray[i].challengeActionDescriptionText.text = ChallengeModeController.Instance.GetCurrentChallengeActions()[i].challengeActionDescription;

                            if (challengeActionDisplayUIControllerArray[i].challengeActionDescriptionText.text != "")
                            {
                                challengeActionDisplayUIControllerArray[i].challengeActionDescriptionGameObject.SetActive(true);
                            }
                            else
                            {
                                challengeActionDisplayUIControllerArray[i].challengeActionDescriptionGameObject.SetActive(false);
                            }
                        }
                    }

                    if (challengeActionDisplayUIControllerArray[i].moveInfoMoveNameGameObject != null)
                    {
                        if (challengeActionDisplayUIControllerArray[i].moveInfoMoveNameText != null)
                        {
                            if (ChallengeModeController.Instance.GetCurrentChallengeActions()[i].moveInfoOptions.displayMoveInfoMoveName == true
                                && ChallengeModeController.Instance.GetCurrentChallengeActions()[i].moveInfoOptions.moveInfo != null)
                            {
                                challengeActionDisplayUIControllerArray[i].moveInfoMoveNameText.text = ChallengeModeController.Instance.GetCurrentChallengeActions()[i].moveInfoOptions.moveInfo.moveName;
                            }
                            else if (ChallengeModeController.Instance.GetCurrentChallengeActions()[i].moveInfoOptions.displayMoveInfoMoveName == false
                                || ChallengeModeController.Instance.GetCurrentChallengeActions()[i].moveInfoOptions.moveInfo == null)
                            {
                                challengeActionDisplayUIControllerArray[i].moveInfoMoveNameText.text = "";
                            }

                            if (challengeActionDisplayUIControllerArray[i].moveInfoMoveNameText.text != "")
                            {
                                challengeActionDisplayUIControllerArray[i].moveInfoMoveNameGameObject.SetActive(true);
                            }
                            else
                            {
                                challengeActionDisplayUIControllerArray[i].moveInfoMoveNameGameObject.SetActive(false);
                            }
                        }
                    }
                }
                else
                {
                    challengeActionDisplayUIControllerArray[i].gameObject.SetActive(false);
                }

                if (i < ChallengeModeController.Instance.currentChallengeActionNumber)
                {
                    if (challengeActionDisplayUIControllerArray[i].challengeActionProgressImage != null)
                    {
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressImage.color = ChallengeModeController.Instance.challengeActionCompleteColor;
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressImage.fillAmount = 1;
                    }

                    if (challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage != null)
                    {
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage.color = ChallengeModeController.Instance.challengeActionCompleteColor;
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage.fillAmount = 0;
                    }
                }
                else if (i == ChallengeModeController.Instance.currentChallengeActionNumber)
                {
                    if (challengeActionDisplayUIControllerArray[i].challengeActionProgressImage != null)
                    {
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressImage.color = ChallengeModeController.Instance.challengeActionCompleteColor;
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressImage.fillAmount = (float)ChallengeModeController.Instance.currentChallengeActionProgress / (float)ChallengeModeController.Instance.GetCurrentChallengeAction().successValue;
                    }

                    if (challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage != null)
                    {
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage.color = ChallengeModeController.Instance.challengeActionCurrentColor;
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage.fillAmount = 1 - challengeActionDisplayUIControllerArray[i].challengeActionProgressImage.fillAmount;
                    }
                }
                else if (i > ChallengeModeController.Instance.currentChallengeActionNumber
                    && i != ChallengeModeController.Instance.highestChallengeActionNumber)
                {
                    if (challengeActionDisplayUIControllerArray[i].challengeActionProgressImage != null)
                    {
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressImage.color = ChallengeModeController.Instance.challengeActionIncompleteColor;
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressImage.fillAmount = 0;
                    }

                    if (challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage != null)
                    {
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage.color = ChallengeModeController.Instance.challengeActionIncompleteColor;
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage.fillAmount = 1;
                    }
                }
            }
        }
    }
}