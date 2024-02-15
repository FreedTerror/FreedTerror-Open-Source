using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
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
            if (ChallengeModeController.instance.GetCurrentChallenges().Count <= 0)
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

        private void UpdateChallengeName()
        {
            if (challengeNameGameObject != null
                && challengeNameText != null)
            {
                challengeNameText.text = ChallengeModeController.instance.GetCurrentChallenge().challengeName;

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

        private void UpdateChallengeDescription()
        {
            if (challengeDescriptionGameObject != null
                && challengeDescriptionText != null)
            {
                challengeDescriptionText.text = ChallengeModeController.instance.GetCurrentChallenge().challengeDescription;

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

        private void UpdateChallengeActionName()
        {
            if (challengeActionNameGameObject != null
                && challengeActionNameText != null)
            {
                challengeActionNameText.text = ChallengeModeController.instance.GetCurrentChallengeAction().challengeActionName;

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

        private void UpdateChallengeActionDescription()
        {
            if (challengeActionDescriptionGameObject != null
                && challengeActionDescriptionText != null)
            {
                challengeActionDescriptionText.text = ChallengeModeController.instance.GetCurrentChallengeAction().challengeActionDescription;

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
            int bounds = inputDisplayTextArray.Length;
            for (int i = 0; i < bounds; i++)
            {
                if (inputDisplayTextArray[i] == null)
                {
                    continue;
                }

                inputDisplayTextArray[i].gameObject.SetActive(false);
            }

            if (ChallengeModeController.instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo != null)
            {
                if (ChallengeModeController.instance.GetCurrentChallengeAction().moveInfoOptions.displayMoveInfoDefaultInputs == true)
                {
                    int length = ChallengeModeController.instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.defaultInputs.buttonSequence.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (i >= bounds
                            || inputDisplayTextArray[i] == null)
                        {
                            continue;
                        }

                        UFE2Manager.SetInputDisplayRotation(inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].transform, ChallengeModeController.instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.defaultInputs.buttonSequence[i], UFE.p1ControlsScript);
                        inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].text = UFE2Manager.instance.inputDisplayScriptableObject.GetInputDisplayStringFromButtonPress(ChallengeModeController.instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.defaultInputs.buttonSequence[i]);
                        inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].gameObject.SetActive(true);
                    }

                    length = ChallengeModeController.instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.defaultInputs.buttonExecution.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (i >= bounds
                            || inputDisplayTextArray[i] == null)
                        {
                            continue;
                        }

                        UFE2Manager.SetInputDisplayRotation(inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].transform, ChallengeModeController.instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.defaultInputs.buttonExecution[i], UFE.p1ControlsScript);
                        inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].text = UFE2Manager.instance.inputDisplayScriptableObject.GetInputDisplayStringFromButtonPress(ChallengeModeController.instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.defaultInputs.buttonExecution[i]);
                        inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].gameObject.SetActive(true);
                    }
                }

                if (ChallengeModeController.instance.GetCurrentChallengeAction().moveInfoOptions.displayMoveInfoAlternativeInputs == true)
                {
                    int length = ChallengeModeController.instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.altInputs.buttonSequence.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (i >= bounds
                            || inputDisplayTextArray[i] == null)
                        {
                            continue;
                        }

                        UFE2Manager.SetInputDisplayRotation(inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].transform, ChallengeModeController.instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.altInputs.buttonSequence[i], UFE.p1ControlsScript);
                        inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].text = UFE2Manager.instance.inputDisplayScriptableObject.GetInputDisplayStringFromButtonPress(ChallengeModeController.instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.altInputs.buttonSequence[i]);
                        inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].gameObject.SetActive(true);
                    }

                    length = ChallengeModeController.instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.altInputs.buttonExecution.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (i >= bounds
                            || inputDisplayTextArray[i] == null)
                        {
                            continue;
                        }

                        UFE2Manager.SetInputDisplayRotation(inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].transform, ChallengeModeController.instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.altInputs.buttonExecution[i], UFE.p1ControlsScript);
                        inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].text = UFE2Manager.instance.inputDisplayScriptableObject.GetInputDisplayStringFromButtonPress(ChallengeModeController.instance.GetCurrentChallengeAction().moveInfoOptions.moveInfo.altInputs.buttonExecution[i]);
                        inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].gameObject.SetActive(true);
                    }
                }
            }

            int length1 = ChallengeModeController.instance.GetCurrentChallengeAction().inputDisplayButtonPressArray.Length;
            for (int i = 0; i < length1; i++)
            {
                if (i >= bounds
                    || inputDisplayTextArray[i] == null)
                {
                    continue;
                }

                UFE2Manager.SetInputDisplayRotation(inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].transform, ChallengeModeController.instance.GetCurrentChallengeAction().inputDisplayButtonPressArray[i], UFE.p1ControlsScript);
                inputDisplayTextArray[GetFirstDisabledGameObjectIndex()].text = UFE2Manager.instance.inputDisplayScriptableObject.GetInputDisplayStringFromButtonPress(ChallengeModeController.instance.GetCurrentChallengeAction().inputDisplayButtonPressArray[i]);
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
            int bounds = ChallengeModeController.instance.GetCurrentChallengeActions().Length;
            int challengeActionDisplayLength = challengeActionDisplayUIControllerArray.Length;
            for (int i = 0; i < challengeActionDisplayLength; i++)
            {
                if (challengeActionDisplayUIControllerArray[i] == null)
                {
                    continue;
                }

                if (i < bounds)
                {
                    challengeActionDisplayUIControllerArray[i].gameObject.SetActive(true);

                    if (challengeActionDisplayUIControllerArray[i].challengeActionNameGameObject != null)
                    {
                        if (challengeActionDisplayUIControllerArray[i].challengeActionNameText != null)
                        {
                            challengeActionDisplayUIControllerArray[i].challengeActionNameText.text = ChallengeModeController.instance.GetCurrentChallengeActions()[i].challengeActionName;

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
                            challengeActionDisplayUIControllerArray[i].challengeActionDescriptionText.text = ChallengeModeController.instance.GetCurrentChallengeActions()[i].challengeActionDescription;

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
                            if (ChallengeModeController.instance.GetCurrentChallengeActions()[i].moveInfoOptions.displayMoveInfoMoveName == true
                                && ChallengeModeController.instance.GetCurrentChallengeActions()[i].moveInfoOptions.moveInfo != null)
                            {
                                challengeActionDisplayUIControllerArray[i].moveInfoMoveNameText.text = ChallengeModeController.instance.GetCurrentChallengeActions()[i].moveInfoOptions.moveInfo.moveName;
                            }
                            else if (ChallengeModeController.instance.GetCurrentChallengeActions()[i].moveInfoOptions.displayMoveInfoMoveName == false
                                || ChallengeModeController.instance.GetCurrentChallengeActions()[i].moveInfoOptions.moveInfo == null)
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

                if (i < ChallengeModeController.instance.currentChallengeActionNumber)
                {
                    if (challengeActionDisplayUIControllerArray[i].challengeActionProgressImage != null)
                    {
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressImage.color = ChallengeModeController.instance.challengeActionCompleteColor;
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressImage.fillAmount = 1;
                    }

                    if (challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage != null)
                    {
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage.color = ChallengeModeController.instance.challengeActionCompleteColor;
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage.fillAmount = 0;
                    }
                }
                else if (i == ChallengeModeController.instance.currentChallengeActionNumber)
                {
                    if (challengeActionDisplayUIControllerArray[i].challengeActionProgressImage != null)
                    {
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressImage.color = ChallengeModeController.instance.challengeActionCompleteColor;
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressImage.fillAmount = (float)ChallengeModeController.instance.currentChallengeActionProgress / (float)ChallengeModeController.instance.GetCurrentChallengeAction().successValue;
                    }

                    if (challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage != null)
                    {
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage.color = ChallengeModeController.instance.challengeActionCurrentColor;
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage.fillAmount = 1 - challengeActionDisplayUIControllerArray[i].challengeActionProgressImage.fillAmount;
                    }
                }
                else if (i > ChallengeModeController.instance.currentChallengeActionNumber
                    && i != ChallengeModeController.instance.highestChallengeActionNumber)
                {
                    if (challengeActionDisplayUIControllerArray[i].challengeActionProgressImage != null)
                    {
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressImage.color = ChallengeModeController.instance.challengeActionIncompleteColor;
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressImage.fillAmount = 0;
                    }

                    if (challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage != null)
                    {
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage.color = ChallengeModeController.instance.challengeActionIncompleteColor;
                        challengeActionDisplayUIControllerArray[i].challengeActionProgressRemainderImage.fillAmount = 1;
                    }
                }
            }
        }
    }
}