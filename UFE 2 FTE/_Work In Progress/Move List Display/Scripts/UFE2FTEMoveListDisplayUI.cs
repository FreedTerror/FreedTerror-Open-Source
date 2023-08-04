using System;
using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTEMoveListDisplayUI : MonoBehaviour
    {
        #region Variables

        [Serializable]
        private class MoveListDisplayCharacterInfoOptionsUI
        {
            public Vector2 anchoredPosition;
            public Text characterNameText;
            public RawImage characterIconLeftRawImage;
            public RawImage characterIconRightRawImage;
        }
        [Header("CHARACTER INFO OPTIONS")]
        [SerializeField]
        private MoveListDisplayCharacterInfoOptionsUI characterInfoOptions;

        [Serializable]
        private class CategoryOptionsUI
        {
            public Text categoryNameText;
        }
        [Header("CATEGORY INFO OPTIONS")]
        [SerializeField]
        private CategoryOptionsUI categoryInfoOptions;

        [Serializable]
        private class MoveListDisplayMoveInfoOptionsUI
        {
            public string pressName = "PRESS";
            public string releaseName = "RELEASE";

            public Text inputButtonSequenceText;
            public RawImage inputButtonSequenceRawImage;

            public Text inputButtonExecutionDirectionText;
            public RawImage inputButtonExecutionDirectionRawImage;

            public Text inputButtonExecutionButtonText;
            public RawImage inputButtonExecutionButtonRawImage;

            public Text moveNameText;
            public Text moveDescriptionText;
        }
        [Header("MOVE INFO OPTIONS")]
        [SerializeField]
        private MoveListDisplayMoveInfoOptionsUI moveInfoOptions;

        #endregion

        #region Set Character Info UI Methods

        public void SetCharacterInfoUI(ControlsScript controlsScript, RectTransform rectTransform)
        {
            if (controlsScript == null) return;

            if (characterInfoOptions.characterNameText != null)
            {
                characterInfoOptions.characterNameText.text = controlsScript.myInfo.characterName;
            }

            if (characterInfoOptions.characterIconLeftRawImage != null)
            {
                characterInfoOptions.characterIconLeftRawImage.texture = controlsScript.myInfo.profilePictureSmall;
            }

            if (characterInfoOptions.characterIconLeftRawImage != null)
            {
                characterInfoOptions.characterIconRightRawImage.texture = controlsScript.myInfo.profilePictureSmall;
            }

            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = characterInfoOptions.anchoredPosition;
            }
        }

        #endregion

        #region Set Category Info UI Methods

        public void SetCategoryInfoUI(string categoryName)
        {
            if (categoryInfoOptions.categoryNameText != null)
            {
                categoryInfoOptions.categoryNameText.text = categoryName;
            }
        }

        #endregion

        #region Set Move Info UI Methods

        public void SetMoveInfoUI
            (ControlsScript controlsScript,
            MoveInputs defaultInputs,
            bool useCustomButtonSequenceName,
            string customButtonSequenceName,
            bool useCustomButtonExecutionDirectionName,
            string customButtonExecutionDirectionName,
            bool useCustomButtonExecutionButtonName,
            string customButtonExecutionButtonName,
            string moveName,
            string moveDescription)
        {
            if (controlsScript == null) return;

            if (defaultInputs.buttonSequence.Length != 0)
            {
                if (useCustomButtonSequenceName == true)
                {
                    moveInfoOptions.inputButtonSequenceText.text = customButtonSequenceName;
                }
                else
                {
                    moveInfoOptions.inputButtonSequenceText.text = moveInfoOptions.pressName;
                }

                int length = defaultInputs.buttonSequence.Length;
                for (int i = 0; i < length; i++)
                {
                    switch (defaultInputs.buttonSequence[i])
                    {
                        case ButtonPress.Forward:
                            if (controlsScript.playerNum == 1)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon1;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon2;
                                }                           
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon1;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon2;
                                }
                            }
                            break;

                        case ButtonPress.Back:
                            if (controlsScript.playerNum == 1)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon2;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon1;
                                }
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon2;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon1;
                                }
                            }
                            break;

                        case ButtonPress.Up:
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[1].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[1].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Down:
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[1].inputViewerIcon2;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[1].inputViewerIcon2;
                            }
                            break;

                        case ButtonPress.UpForward:
                            if (controlsScript.playerNum == 1)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon3;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon4;
                                }
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon3;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon4;
                                }
                            }
                            break;

                        case ButtonPress.UpBack:
                            if (controlsScript.playerNum == 1)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon4;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon3;
                                }
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon4;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon3;
                                }
                            }
                            break;

                        case ButtonPress.DownForward:
                            if (controlsScript.playerNum == 1)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon5;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon6;
                                }
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon5;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon6;
                                }
                            }
                            break;

                        case ButtonPress.DownBack:
                            if (controlsScript.playerNum == 1)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon6;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon5;
                                }
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon6;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                    moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon5;
                                }
                            }
                            break;

                        case ButtonPress.Button1:
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[2].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[2].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button2:
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[3].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[3].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button3:
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[4].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[4].inputViewerIcon1;
                            }

                            break;

                        case ButtonPress.Button4:
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[5].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[5].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button5:
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[6].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[6].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button6:
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[7].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[7].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button7:
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[8].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[8].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button8:
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[9].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[9].inputViewerIcon1;
                            }         
                            break;

                        case ButtonPress.Button9:
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[10].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[10].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button10:
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[11].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[11].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button11:
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[12].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[12].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button12:
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player1_Inputs[13].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonSequenceRawImage.enabled = true;
                                moveInfoOptions.inputButtonSequenceRawImage.texture = UFE.config.player2_Inputs[13].inputViewerIcon1;
                            }
                            break;
                    }
                }
            }

            if (defaultInputs.buttonExecution.Length != 0)
            {
                int length = defaultInputs.buttonExecution.Length;
                for (int i = 0; i < length; i++)
                {
                    switch (defaultInputs.buttonExecution[i])
                    {
                        case ButtonPress.Forward:
                            if (useCustomButtonExecutionDirectionName == true)
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = customButtonExecutionDirectionName;
                            }
                            else
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = moveInfoOptions.pressName;
                            }

                            if (controlsScript.playerNum == 1)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon1;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon2;
                                }
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon1;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon2;
                                }
                            }
                            break;

                        case ButtonPress.Back:
                            if (useCustomButtonExecutionDirectionName == true)
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = customButtonExecutionDirectionName;
                            }
                            else
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = moveInfoOptions.pressName;
                            }

                            if (controlsScript.playerNum == 1)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon2;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon1;
                                }
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon2;

                                }
                                else
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon1;
                                }
                            }      
                            break;

                        case ButtonPress.Up:
                            if (useCustomButtonExecutionDirectionName == true)
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = customButtonExecutionDirectionName;
                            }
                            else
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = moveInfoOptions.pressName;
                            }

                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player1_Inputs[1].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player2_Inputs[1].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Down:
                            if (useCustomButtonExecutionDirectionName == true)
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = customButtonExecutionDirectionName;
                            }
                            else
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = moveInfoOptions.pressName;
                            }

                            if (useCustomButtonExecutionDirectionName == true)
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = customButtonExecutionDirectionName;
                            }
                            else
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = moveInfoOptions.pressName;
                            }

                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player1_Inputs[1].inputViewerIcon2;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player2_Inputs[1].inputViewerIcon2;
                            }
                            break;

                        case ButtonPress.UpForward:
                            if (useCustomButtonExecutionDirectionName == true)
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = customButtonExecutionDirectionName;
                            }
                            else
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = moveInfoOptions.pressName;
                            }

                            if (controlsScript.playerNum == 1)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon3;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon4;
                                }
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon3;
                                }
                                else
                                {         
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon4;
                                }
                            }
                            break;

                        case ButtonPress.UpBack:
                            if (useCustomButtonExecutionDirectionName == true)
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = customButtonExecutionDirectionName;
                            }
                            else
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = moveInfoOptions.pressName;
                            }

                            if (controlsScript.playerNum == 1)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon4;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon3;
                                }
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon4;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon3;
                                }
                            }
                            break;

                        case ButtonPress.DownForward:
                            if (useCustomButtonExecutionDirectionName == true)
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = customButtonExecutionDirectionName;
                            }
                            else
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = moveInfoOptions.pressName;
                            }

                            if (controlsScript.playerNum == 1)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon5;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon6;
                                }
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon5;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon6;
                                }
                            }
                            break;

                        case ButtonPress.DownBack:
                            if (useCustomButtonExecutionDirectionName == true)
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = customButtonExecutionDirectionName;
                            }
                            else
                            {
                                moveInfoOptions.inputButtonExecutionDirectionText.text = moveInfoOptions.pressName;
                            }

                            if (controlsScript.playerNum == 1)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon6;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player1_Inputs[0].inputViewerIcon5;
                                }
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                if (controlsScript.mirror == -1)
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon6;
                                }
                                else
                                {
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.enabled = true;
                                    moveInfoOptions.inputButtonExecutionDirectionRawImage.texture = UFE.config.player2_Inputs[0].inputViewerIcon5;
                                }
                            }
                            break;

                        case ButtonPress.Button1:
                            if (useCustomButtonExecutionButtonName == true)
                            {
                                moveInfoOptions.inputButtonExecutionButtonText.text = customButtonExecutionButtonName;
                            }
                            else
                            {
                                if (defaultInputs.onPressExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.pressName;
                                }

                                if (defaultInputs.onReleaseExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.releaseName;
                                }
                            }

                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player1_Inputs[2].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player2_Inputs[2].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button2:
                            if (useCustomButtonExecutionButtonName == true)
                            {
                                moveInfoOptions.inputButtonExecutionButtonText.text = customButtonExecutionButtonName;
                            }
                            else
                            {
                                if (defaultInputs.onPressExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.pressName;
                                }

                                if (defaultInputs.onReleaseExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.releaseName;
                                }
                            }
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player1_Inputs[3].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player2_Inputs[3].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button3:
                            if (useCustomButtonExecutionButtonName == true)
                            {
                                moveInfoOptions.inputButtonExecutionButtonText.text = customButtonExecutionButtonName;
                            }
                            else
                            {
                                if (defaultInputs.onPressExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.pressName;
                                }

                                if (defaultInputs.onReleaseExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.releaseName;
                                }
                            }

                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player1_Inputs[4].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player2_Inputs[4].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button4:
                            if (useCustomButtonExecutionButtonName == true)
                            {
                                moveInfoOptions.inputButtonExecutionButtonText.text = customButtonExecutionButtonName;
                            }
                            else
                            {
                                if (defaultInputs.onPressExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.pressName;
                                }

                                if (defaultInputs.onReleaseExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.releaseName;
                                }
                            }

                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player1_Inputs[5].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player2_Inputs[5].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button5:
                            if (useCustomButtonExecutionButtonName == true)
                            {
                                moveInfoOptions.inputButtonExecutionButtonText.text = customButtonExecutionButtonName;
                            }
                            else
                            {
                                if (defaultInputs.onPressExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.pressName;
                                }

                                if (defaultInputs.onReleaseExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.releaseName;
                                }
                            }
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player1_Inputs[6].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player2_Inputs[6].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button6:
                            if (useCustomButtonExecutionButtonName == true)
                            {
                                moveInfoOptions.inputButtonExecutionButtonText.text = customButtonExecutionButtonName;
                            }
                            else
                            {
                                if (defaultInputs.onPressExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.pressName;
                                }

                                if (defaultInputs.onReleaseExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.releaseName;
                                }
                            }

                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player1_Inputs[7].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player2_Inputs[7].inputViewerIcon1;

                            }
                            break;

                        case ButtonPress.Button7:
                            if (useCustomButtonExecutionButtonName == true)
                            {
                                moveInfoOptions.inputButtonExecutionButtonText.text = customButtonExecutionButtonName;
                            }
                            else
                            {
                                if (defaultInputs.onPressExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.pressName;
                                }

                                if (defaultInputs.onReleaseExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.releaseName;
                                }
                            }

                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player1_Inputs[8].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player2_Inputs[8].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button8:
                            if (useCustomButtonExecutionButtonName == true)
                            {
                                moveInfoOptions.inputButtonExecutionButtonText.text = customButtonExecutionButtonName;
                            }
                            else
                            {
                                if (defaultInputs.onPressExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.pressName;
                                }

                                if (defaultInputs.onReleaseExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.releaseName;
                                }
                            }

                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player1_Inputs[9].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player2_Inputs[9].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button9:
                            if (useCustomButtonExecutionButtonName == true)
                            {
                                moveInfoOptions.inputButtonExecutionButtonText.text = customButtonExecutionButtonName;
                            }
                            else
                            {
                                if (defaultInputs.onPressExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.pressName;
                                }

                                if (defaultInputs.onReleaseExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.releaseName;
                                }
                            }

                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player1_Inputs[10].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player2_Inputs[10].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button10:
                            if (useCustomButtonExecutionButtonName == true)
                            {
                                moveInfoOptions.inputButtonExecutionButtonText.text = customButtonExecutionButtonName;
                            }
                            else
                            {
                                if (defaultInputs.onPressExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.pressName;
                                }

                                if (defaultInputs.onReleaseExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.releaseName;
                                }
                            }
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player1_Inputs[11].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player2_Inputs[11].inputViewerIcon1;
                            }
                            break;

                        case ButtonPress.Button11:
                            if (useCustomButtonExecutionButtonName == true)
                            {
                                moveInfoOptions.inputButtonExecutionButtonText.text = customButtonExecutionButtonName;
                            }
                            else
                            {
                                if (defaultInputs.onPressExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.pressName;
                                }

                                if (defaultInputs.onReleaseExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.releaseName;
                                }
                            }

                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player1_Inputs[12].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player2_Inputs[12].inputViewerIcon1;
                            }    
                            break;

                        case ButtonPress.Button12:
                            if (useCustomButtonExecutionButtonName == true)
                            {
                                moveInfoOptions.inputButtonExecutionButtonText.text = customButtonExecutionButtonName;
                            }
                            else
                            {
                                if (defaultInputs.onPressExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.pressName;
                                }

                                if (defaultInputs.onReleaseExecution == true)
                                {
                                    moveInfoOptions.inputButtonExecutionButtonText.text = moveInfoOptions.releaseName;
                                }
                            }
                            if (controlsScript.playerNum == 1)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player1_Inputs[13].inputViewerIcon1;
                            }
                            else if (controlsScript.playerNum == 2)
                            {
                                moveInfoOptions.inputButtonExecutionButtonRawImage.enabled = true;
                                moveInfoOptions.inputButtonExecutionButtonRawImage.texture = UFE.config.player2_Inputs[13].inputViewerIcon1;
                            }   
                            break;
                    }
                }
            }

            moveInfoOptions.moveNameText.text = moveName;
            moveInfoOptions.moveDescriptionText.text = moveDescription;
        }

        #endregion
    }
}
