using UnityEngine;
using System;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class UFE_2ChallengeModeDisplayUI : MonoBehaviour
    {
        public Canvas myCanvas;
                  
        public Text challengeNameText;

        public Color32 progressImageNormalColor;
        public Color32 progressImageCurrentColor;
        public Color32 progressImageSuccessfulColor;
        public Color32 progressImageUnsuccessfulColor;

        [Serializable]
        public class BasicMoveChallengeDisplayUI
        {
            public GameObject challengeDisplayGameObject;
            public Text progressText;
            public Slider progressSlider1;
            public Slider progressSlider2;
            public Image progressSliderImage1;
            public Image progressSliderImage2;
            public RawImage inputRawImage1;
            public RawImage inputRawImage2;
        }

        public BasicMoveChallengeDisplayUI player1BasicMoveChallengeDisplayUI;

        [Serializable]
        public class MoveInfoChallengeDisplayUI
        {
            public GameObject challengeDisplayGameObject;
            public Image progressImage;
            public Text moveTypeAbbreviationText;          
            public RectTransform inputButtonSequenceRawImage1RectTransform;
            [HideInInspector]
            public Vector2 inputButtonSequenceRawImage1RectTransformOriginalPosition;
            public RawImage inputButtonSequenceRawImage1;   
            public RectTransform inputButtonExecutionRawImage1RectTransform;
            [HideInInspector]
            public Vector2 inputButtonExecutionRawImage1RectTransformOriginalPosition;
            public RawImage inputButtonExecutionRawImage1;          
            public RectTransform inputButtonExecutionRawImage2RectTransform;
            [HideInInspector]
            public Vector2 inputButtonExecutionRawImage2RectTransformOriginalPosition;
            public RawImage inputButtonExecutionRawImage2;
        }

        public MoveInfoChallengeDisplayUI[] player1MoveInfoChallengeDisplayUI;

        private bool isComboChallenge;
        private int unsuccessfulAction;

        private string standMoveTypeAbbreviation = "S";
        private string crouchMoveTypeAbbreviation = "C";
        private string jumpMoveTypeAbbreviation = "J";
        private string dashMoveTypeAbbreviation = "D";
        private string runMoveTypeAbbreviation = "R";

        // Start is called before the first frame update
        void Start()
        {
            int player1MoveInfoChallengeDisplayUILength = player1MoveInfoChallengeDisplayUI.Length;
            for (int i = 0; i < player1MoveInfoChallengeDisplayUILength; i++)
            {
                player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1RectTransformOriginalPosition = player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1RectTransform.anchoredPosition;
                player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1RectTransformOriginalPosition = player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1RectTransform.anchoredPosition;
                player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2RectTransformOriginalPosition = player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2RectTransform.anchoredPosition;
            }
        }

        // Update is called once per frame
        void Update()
        {
            switch (UFE.isPaused())
            {
                case true:
                    myCanvas.enabled = false;
                    break;

                case false:
                    myCanvas.enabled = true;
                    break;
            }

            challengeNameText.text = UFE.GetChallenge(UFE.challengeMode.currentChallenge).challengeName;

            SetPlayer1BasicMoveChallengeDisplayUI();

            SetPlayer1MoveInfoChallengeDisplayUI();  
        }

        #region Set Player 1 Basic Move Challenge Display UI Functions

        public void SetPlayer1BasicMoveChallengeDisplayUI()
        {
            switch (UFE.GetChallenge(UFE.challengeMode.currentChallenge).actionSequence[0].actionType)
            {
                case ActionType.SpecialMove:
                case ActionType.BasicMove:
                    player1BasicMoveChallengeDisplayUI.challengeDisplayGameObject.SetActive(false);
                    return;

                case ActionType.ButtonPress:
                    player1BasicMoveChallengeDisplayUI.challengeDisplayGameObject.SetActive(true);
                    break;     
            }
 
            int challengeModeBasicMovesLength = UFE_2ChallengeModeManager.Instance.challengeModeBasicMoves.Length;
            for (int i = 0; i < challengeModeBasicMovesLength; i++)
            {
                if (UFE.challengeMode.currentChallenge == UFE_2ChallengeModeManager.Instance.challengeModeBasicMoves[i].challengeNumber)
                {
                    switch (UFE_2ChallengeModeManager.Instance.challengeModeBasicMoves[i].basicMoveChallengeType)
                    {
                        case BasicMoveChallengeType.BasicMovePlaying:
                            player1BasicMoveChallengeDisplayUI.progressSlider1.maxValue = UFE_2ChallengeModeManager.Instance.challengeModeBasicMoves[i].player1BasicMovePlayingTimeSuccess;
                            player1BasicMoveChallengeDisplayUI.progressSlider2.maxValue = UFE_2ChallengeModeManager.Instance.challengeModeBasicMoves[i].player1BasicMovePlayingTimeSuccess;

                            player1BasicMoveChallengeDisplayUI.progressSlider1.value = UFE_2ChallengeModeManager.Instance.challengeModeBasicMoves[i].player1BasicMovePlayingTimer;
                            player1BasicMoveChallengeDisplayUI.progressSlider2.value = UFE_2ChallengeModeManager.Instance.challengeModeBasicMoves[i].player1BasicMovePlayingTimer;

                            player1BasicMoveChallengeDisplayUI.progressSliderImage1.color = progressImageSuccessfulColor;
                            player1BasicMoveChallengeDisplayUI.progressSliderImage2.color = progressImageSuccessfulColor;

                            int int_player1BasicMovePlayingTimer = Mathf.FloorToInt(UFE_2ChallengeModeManager.Instance.challengeModeBasicMoves[UFE.challengeMode.currentChallenge].player1BasicMovePlayingTimer);

                            //player1BasicMoveChallengeDisplayUI.progressText.text = GCFreeStringNumbersManager.Instance.GetStringFromStringArray(GCFreeStringNumbersManager.Instance.positiveNumbers, int_player1BasicMovePlayingTimer);
                            player1BasicMoveChallengeDisplayUI.progressText.text = int_player1BasicMovePlayingTimer.ToString();

                            switch (UFE_2ChallengeModeManager.Instance.challengeModeBasicMoves[i].basicMove)
                            {
                                case BasicMoveReference.MoveForward:
                                    switch (UFE.p1ControlsScript.mirror)
                                    {
                                        case -1:
                                            player1BasicMoveChallengeDisplayUI.inputRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon1;
                                            player1BasicMoveChallengeDisplayUI.inputRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon1;
                                            break;

                                        default:
                                            player1BasicMoveChallengeDisplayUI.inputRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon2;
                                            player1BasicMoveChallengeDisplayUI.inputRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon2;
                                            break;
                                    }
                                    break;

                                case BasicMoveReference.MoveBack:
                                    switch (UFE.p1ControlsScript.mirror)
                                    {
                                        case -1:
                                            player1BasicMoveChallengeDisplayUI.inputRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon2;
                                            player1BasicMoveChallengeDisplayUI.inputRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon2;
                                            break;

                                        default:
                                            player1BasicMoveChallengeDisplayUI.inputRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon1;
                                            player1BasicMoveChallengeDisplayUI.inputRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon1;
                                            break;
                                    }
                                    break;

                                case BasicMoveReference.Crouching:
                                    player1BasicMoveChallengeDisplayUI.inputRawImage1.texture = UFE.config.player1_Inputs[1].inputViewerIcon2;
                                    player1BasicMoveChallengeDisplayUI.inputRawImage2.texture = UFE.config.player1_Inputs[1].inputViewerIcon2;
                                    break;
                            }
                            break;

                        case BasicMoveChallengeType.BasicMoveTimesPlayed:
                            player1BasicMoveChallengeDisplayUI.progressSlider1.maxValue = UFE_2ChallengeModeManager.Instance.challengeModeBasicMoves[i].player1BasicMoveTimesPlayedSuccess;
                            player1BasicMoveChallengeDisplayUI.progressSlider2.maxValue = UFE_2ChallengeModeManager.Instance.challengeModeBasicMoves[i].player1BasicMoveTimesPlayedSuccess;

                            player1BasicMoveChallengeDisplayUI.progressSlider1.value = UFE_2ChallengeModeManager.Instance.challengeModeBasicMoves[i].player1BasicMoveTimesPlayed;
                            player1BasicMoveChallengeDisplayUI.progressSlider2.value = UFE_2ChallengeModeManager.Instance.challengeModeBasicMoves[i].player1BasicMoveTimesPlayed;

                            player1BasicMoveChallengeDisplayUI.progressSliderImage1.color = progressImageSuccessfulColor;
                            player1BasicMoveChallengeDisplayUI.progressSliderImage2.color = progressImageSuccessfulColor;

                            //player1BasicMoveChallengeDisplayUI.progressText.text = GCFreeStringNumbersManager.Instance.GetStringFromStringArray(GCFreeStringNumbersManager.Instance.positiveNumbers, UFE_2ChallengeModeManager.Instance.challengeModeBasicMoves[UFE.challengeMode.currentChallenge].player1BasicMoveTimesPlayed);
                            player1BasicMoveChallengeDisplayUI.progressText.text = UFE_2ChallengeModeManager.Instance.challengeModeBasicMoves[UFE.challengeMode.currentChallenge].player1BasicMoveTimesPlayed.ToString();

                            switch (UFE_2ChallengeModeManager.Instance.challengeModeBasicMoves[i].basicMove)
                            {
                                case BasicMoveReference.JumpStraight:
                                    player1BasicMoveChallengeDisplayUI.inputRawImage1.texture = UFE.config.player1_Inputs[1].inputViewerIcon1;
                                    player1BasicMoveChallengeDisplayUI.inputRawImage2.texture = UFE.config.player1_Inputs[1].inputViewerIcon1;
                                    break;

                                case BasicMoveReference.JumpForward:
                                    switch (UFE.p1ControlsScript.mirror)
                                    {
                                        case -1:
                                            player1BasicMoveChallengeDisplayUI.inputRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon3;
                                            player1BasicMoveChallengeDisplayUI.inputRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon3;
                                            break;

                                        default:
                                            player1BasicMoveChallengeDisplayUI.inputRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon4;
                                            player1BasicMoveChallengeDisplayUI.inputRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon4;
                                            break;
                                    }
                                    break;

                                case BasicMoveReference.JumpBack:
                                    switch (UFE.p1ControlsScript.mirror)
                                    {
                                        case -1:
                                            player1BasicMoveChallengeDisplayUI.inputRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon4;
                                            player1BasicMoveChallengeDisplayUI.inputRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon4;
                                            break;

                                        default:
                                            player1BasicMoveChallengeDisplayUI.inputRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon3;
                                            player1BasicMoveChallengeDisplayUI.inputRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon3;
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                }
            }
        }

        #endregion

        #region Set Player 1 Move Info Challenge Display UI Functions

        public void SetPlayer1MoveInfoChallengeDisplayUI()
        {
            switch (UFE.GetChallenge(UFE.challengeMode.currentChallenge).actionSequence[0].actionType)
            {
                case ActionType.ButtonPress:
                case ActionType.BasicMove:
                    int player1MoveInfoChallengeDisplayUILength1 = player1MoveInfoChallengeDisplayUI.Length;
                    for (int i = 0; i < player1MoveInfoChallengeDisplayUILength1; i++)
                    {
                        player1MoveInfoChallengeDisplayUI[i].challengeDisplayGameObject.SetActive(false);
                    }
                    return;
            }

            int player1MoveInfoChallengeDisplayUILength = player1MoveInfoChallengeDisplayUI.Length;
            int actionSequenceLength = UFE.GetChallenge(UFE.challengeMode.currentChallenge).actionSequence.Length;
            int successfulAction = 0;

            //Disable unused gameobjects
            for (int i = actionSequenceLength; i < player1MoveInfoChallengeDisplayUILength; i++)
            {
                player1MoveInfoChallengeDisplayUI[i].challengeDisplayGameObject.SetActive(false);

                player1MoveInfoChallengeDisplayUI[i].progressImage.color = progressImageNormalColor;
            }

            //Set UnsuccessfulColor
            switch (isComboChallenge)
            {
                case true:
                    if (UFE.p1ControlsScript.opControlsScript.stunTime <= 0)
                    {
                        UFE.challengeMode.currentAction = 0;
                    }

                    if (UFE.challengeMode.currentAction != 0)
                    {
                        unsuccessfulAction = UFE.challengeMode.currentAction;
                    }

                    player1MoveInfoChallengeDisplayUI[unsuccessfulAction].progressImage.color = progressImageUnsuccessfulColor;
                    break;
            }

            for (int i = 0; i < player1MoveInfoChallengeDisplayUILength; i++)
            {
                //Set NormalColor
                switch (isComboChallenge)
                {
                    case true:
                        if (i != unsuccessfulAction)
                        {
                            player1MoveInfoChallengeDisplayUI[i].progressImage.color = progressImageNormalColor;
                        }
                        break;

                    case false:
                        player1MoveInfoChallengeDisplayUI[i].progressImage.color = progressImageNormalColor;
                        break;
                }

                //Set CurrentColor
                //Set SuccessfulColor
                switch (UFE.challengeMode.complete)
                {
                    case true:
                        player1MoveInfoChallengeDisplayUI[i].progressImage.color = progressImageSuccessfulColor;
                        break;

                    case false:
                        player1MoveInfoChallengeDisplayUI[UFE.challengeMode.currentAction].progressImage.color = progressImageCurrentColor;

                        if (UFE.challengeMode.currentAction > successfulAction)
                        {
                            player1MoveInfoChallengeDisplayUI[successfulAction].progressImage.color = progressImageSuccessfulColor;
                        }

                        successfulAction++;
                        break;
                }
            }

            for (int i = 0; i < actionSequenceLength; i++)
            {
                switch (UFE.GetChallenge(UFE.challengeMode.currentChallenge).actionSequence[i].actionType)
                {
                    case ActionType.ButtonPress:
                        player1MoveInfoChallengeDisplayUI[i].challengeDisplayGameObject.SetActive(false);

                        player1MoveInfoChallengeDisplayUI[i].progressImage.color = progressImageNormalColor;

                        isComboChallenge = false;
                        break;

                    case ActionType.BasicMove:
                        player1MoveInfoChallengeDisplayUI[i].challengeDisplayGameObject.SetActive(false);

                        player1MoveInfoChallengeDisplayUI[i].progressImage.color = progressImageNormalColor;

                        isComboChallenge = false;
                        break;

                    case ActionType.SpecialMove:
                        player1MoveInfoChallengeDisplayUI[i].challengeDisplayGameObject.SetActive(true);

                        switch (UFE.GetChallenge(UFE.challengeMode.currentChallenge).actionSequence[0].executionOnly)
                        {
                            case true:
                                isComboChallenge = false;
                                break;

                            case false:
                                isComboChallenge = true;
                                break;
                        }

                        if (UFE.GetChallenge(UFE.challengeMode.currentChallenge).actionSequence[i].specialMove.moveName.Contains("Standing"))
                        {
                            player1MoveInfoChallengeDisplayUI[i].moveTypeAbbreviationText.text = standMoveTypeAbbreviation;
                        }
                        else if (UFE.GetChallenge(UFE.challengeMode.currentChallenge).actionSequence[i].specialMove.moveName.Contains("Crouching"))
                        {
                            player1MoveInfoChallengeDisplayUI[i].moveTypeAbbreviationText.text = crouchMoveTypeAbbreviation;
                        }
                        else if (UFE.GetChallenge(UFE.challengeMode.currentChallenge).actionSequence[i].specialMove.moveName.Contains("Air"))
                        {
                            player1MoveInfoChallengeDisplayUI[i].moveTypeAbbreviationText.text = jumpMoveTypeAbbreviation;
                        }
                        else if (UFE.GetChallenge(UFE.challengeMode.currentChallenge).actionSequence[i].specialMove.moveName.Contains("Dash"))
                        {
                            player1MoveInfoChallengeDisplayUI[i].moveTypeAbbreviationText.text = dashMoveTypeAbbreviation;
                        }
                        else if (UFE.GetChallenge(UFE.challengeMode.currentChallenge).actionSequence[i].specialMove.moveName.Contains("Run"))
                        {
                            player1MoveInfoChallengeDisplayUI[i].moveTypeAbbreviationText.text = runMoveTypeAbbreviation;
                        }
                        else
                        {
                            player1MoveInfoChallengeDisplayUI[i].moveTypeAbbreviationText.text = null;
                        }

                        int buttonSequenceLength = UFE.GetChallenge(UFE.challengeMode.currentChallenge).actionSequence[i].specialMove.defaultInputs.buttonSequence.Length;
                        switch (buttonSequenceLength)
                        {
                            case 0:
                                player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.enabled = false;

                                player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1RectTransform.anchoredPosition = player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1RectTransformOriginalPosition;

                                player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2RectTransform.anchoredPosition = player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1RectTransformOriginalPosition;
                                break;

                            default:
                                player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.enabled = true;

                                player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1RectTransform.anchoredPosition = player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1RectTransformOriginalPosition;

                                player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2RectTransform.anchoredPosition = player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2RectTransformOriginalPosition;
                                break;
                        }

                        for (int a = 0; a < buttonSequenceLength; a++)
                        {
                            ButtonPress buttonPressSequence = UFE.GetChallenge(UFE.challengeMode.currentChallenge).actionSequence[i].specialMove.defaultInputs.buttonSequence[a];

                            switch (buttonPressSequence)
                            {
                                case ButtonPress.Forward:
                                    switch (UFE.p1ControlsScript.mirror)
                                    {
                                        case -1:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon1;
                                            break;

                                        default:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon2;
                                            break;
                                    }
                                    break;

                                case ButtonPress.Back:
                                    switch (UFE.p1ControlsScript.mirror)
                                    {
                                        case -1:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon2;
                                            break;

                                        default:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon1;
                                            break;
                                    }
                                    break;

                                case ButtonPress.Up:
                                    player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[1].inputViewerIcon1;
                                    break;

                                case ButtonPress.Down:
                                    player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[1].inputViewerIcon2;
                                    break;

                                case ButtonPress.UpForward:
                                    switch (UFE.p1ControlsScript.mirror)
                                    {
                                        case -1:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon3;
                                            break;

                                        default:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon4;
                                            break;
                                    }
                                    break;

                                case ButtonPress.UpBack:
                                    switch (UFE.p1ControlsScript.mirror)
                                    {
                                        case -1:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon4;
                                            break;

                                        default:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon3;
                                            break;
                                    }
                                    break;

                                case ButtonPress.DownForward:
                                    switch (UFE.p1ControlsScript.mirror)
                                    {
                                        case -1:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon5;
                                            break;

                                        default:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon6;
                                            break;
                                    }
                                    break;

                                case ButtonPress.DownBack:
                                    switch (UFE.p1ControlsScript.mirror)
                                    {
                                        case -1:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon6;
                                            break;

                                        default:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon5;
                                            break;
                                    }
                                    break;

                                case ButtonPress.Button1:
                                    player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[2].inputViewerIcon1;
                                    break;

                                case ButtonPress.Button2:
                                    player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[3].inputViewerIcon1;
                                    break;

                                case ButtonPress.Button3:
                                    player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[4].inputViewerIcon1;
                                    break;

                                case ButtonPress.Button4:
                                    player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[5].inputViewerIcon1;
                                    break;

                                case ButtonPress.Button5:
                                    player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[6].inputViewerIcon1;
                                    break;

                                case ButtonPress.Button6:
                                    player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[7].inputViewerIcon1;
                                    break;

                                case ButtonPress.Button7:
                                    player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[8].inputViewerIcon1;
                                    break;

                                case ButtonPress.Button8:
                                    player1MoveInfoChallengeDisplayUI[i].inputButtonSequenceRawImage1.texture = UFE.config.player1_Inputs[9].inputViewerIcon1;
                                    break;
                            }
                        }

                        int buttonExecutionLength = UFE.GetChallenge(UFE.challengeMode.currentChallenge).actionSequence[i].specialMove.defaultInputs.buttonExecution.Length;
                        switch (buttonExecutionLength)
                        {
                            case 1:
                                player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.enabled = true;
                                player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.enabled = false;
                                break;

                            case 2:
                                player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.enabled = true;
                                player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.enabled = true;
                                break;
                        }

                        for (int a = 0; a < buttonExecutionLength; a++)
                        {
                            ButtonPress buttonPressExecution = UFE.GetChallenge(UFE.challengeMode.currentChallenge).actionSequence[i].specialMove.defaultInputs.buttonExecution[a];

                            switch (buttonPressExecution)
                            {
                                case ButtonPress.Forward:
                                    switch (UFE.p1ControlsScript.mirror)
                                    {
                                        case -1:
                                            switch (a)
                                            {
                                                case 0:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon1;
                                                    break;

                                                default:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon1;
                                                    break;
                                            }
                                            break;

                                        default:
                                            switch (a)
                                            {
                                                case 0:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon2;
                                                    break;

                                                default:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon2;
                                                    break;
                                            }
                                            break;
                                    }
                                    break;

                                case ButtonPress.Back:
                                    switch (UFE.p1ControlsScript.mirror)
                                    {
                                        case -1:
                                            switch (a)
                                            {
                                                case 0:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon2;
                                                    break;

                                                default:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon2;
                                                    break;
                                            }
                                            break;

                                        default:
                                            switch (a)
                                            {
                                                case 0:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon1;
                                                    break;

                                                default:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon1;
                                                    break;
                                            }
                                            break;
                                    }
                                    break;

                                case ButtonPress.Up:
                                    switch (a)
                                    {
                                        case 0:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[1].inputViewerIcon1;
                                            break;

                                        default:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[1].inputViewerIcon1;
                                            break;
                                    }
                                    break;

                                case ButtonPress.Down:
                                    switch (a)
                                    {
                                        case 0:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[1].inputViewerIcon1;
                                            break;

                                        default:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[1].inputViewerIcon1;
                                            break;
                                    }
                                    break;

                                case ButtonPress.UpForward:
                                    switch (UFE.p1ControlsScript.mirror)
                                    {
                                        case -1:
                                            switch (a)
                                            {
                                                case 0:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon3;
                                                    break;

                                                default:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon3;
                                                    break;
                                            }
                                            break;

                                        default:
                                            switch (a)
                                            {
                                                case 0:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon4;
                                                    break;

                                                default:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon4;
                                                    break;
                                            }
                                            break;
                                    }
                                    break;

                                case ButtonPress.UpBack:
                                    switch (UFE.p1ControlsScript.mirror)
                                    {
                                        case -1:
                                            switch (a)
                                            {
                                                case 0:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon4;
                                                    break;

                                                default:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon4;
                                                    break;
                                            }
                                            break;

                                        default:
                                            switch (a)
                                            {
                                                case 0:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon3;
                                                    break;

                                                default:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon3;
                                                    break;
                                            }
                                            break;
                                    }
                                    break;

                                case ButtonPress.DownForward:
                                    switch (UFE.p1ControlsScript.mirror)
                                    {
                                        case -1:
                                            switch (a)
                                            {
                                                case 0:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon5;
                                                    break;

                                                default:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon5;
                                                    break;
                                            }
                                            break;

                                        default:
                                            switch (a)
                                            {
                                                case 0:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon6;
                                                    break;

                                                default:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon6;
                                                    break;
                                            }
                                            break;
                                    }
                                    break;

                                case ButtonPress.DownBack:
                                    switch (UFE.p1ControlsScript.mirror)
                                    {
                                        case -1:
                                            switch (a)
                                            {
                                                case 0:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon6;
                                                    break;

                                                default:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon6;
                                                    break;
                                            }
                                            break;

                                        default:
                                            switch (a)
                                            {
                                                case 0:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[0].inputViewerIcon5;
                                                    break;

                                                default:
                                                    player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[0].inputViewerIcon5;
                                                    break;
                                            }
                                            break;
                                    }
                                    break;

                                case ButtonPress.Button1:
                                    switch (a)
                                    {
                                        case 0:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[2].inputViewerIcon1;
                                            break;

                                        default:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[2].inputViewerIcon1;
                                            break;
                                    }
                                    break;

                                case ButtonPress.Button2:
                                    switch (a)
                                    {
                                        case 0:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[3].inputViewerIcon1;
                                            break;

                                        default:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[3].inputViewerIcon1;
                                            break;
                                    }
                                    break;

                                case ButtonPress.Button3:
                                    switch (a)
                                    {
                                        case 0:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[4].inputViewerIcon1;
                                            break;

                                        default:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[4].inputViewerIcon1;
                                            break;
                                    }
                                    break;

                                case ButtonPress.Button4:
                                    switch (a)
                                    {
                                        case 0:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[5].inputViewerIcon1;
                                            break;

                                        default:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[5].inputViewerIcon1;
                                            break;
                                    }
                                    break;

                                case ButtonPress.Button5:
                                    switch (a)
                                    {
                                        case 0:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[6].inputViewerIcon1;
                                            break;

                                        default:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[6].inputViewerIcon1;
                                            break;
                                    }
                                    break;

                                case ButtonPress.Button6:
                                    switch (a)
                                    {
                                        case 0:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[7].inputViewerIcon1;
                                            break;

                                        default:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[7].inputViewerIcon1;
                                            break;
                                    }
                                    break;

                                case ButtonPress.Button7:
                                    switch (a)
                                    {
                                        case 0:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[8].inputViewerIcon1;
                                            break;

                                        default:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[8].inputViewerIcon1;
                                            break;
                                    }
                                    break;

                                case ButtonPress.Button8:
                                    switch (a)
                                    {
                                        case 0:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage1.texture = UFE.config.player1_Inputs[9].inputViewerIcon1;
                                            break;

                                        default:
                                            player1MoveInfoChallengeDisplayUI[i].inputButtonExecutionRawImage2.texture = UFE.config.player1_Inputs[9].inputViewerIcon1;
                                            break;
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        #endregion
    }
}
