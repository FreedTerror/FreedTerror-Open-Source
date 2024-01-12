using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Input Display", menuName = "U.F.E. 2 F.T.E./Display/Input Display")]
    public class InputDisplayScriptableObject : ScriptableObject
    {
        //Add to DefaultBattleGUI
        //Sprite sprite = UFE2FTE.UFE2FTE.GetInputDisplaySpriteFromTexture(inputRef.activeIcon);

        public Vector2 inputDisplayButtonIconSizeDelta = new Vector2(80, 80);
        public Vector2 inputDisplayOffsetMax;
        public string inputDisplayButtonIconControllerPrefabPath = "Input Display Button Icon Controller Prefab";
        public InputDisplayButtonIconController GetInputDisplayButtonIconControllerPrefab()
        {
            return Resources.Load<InputDisplayButtonIconController>(inputDisplayButtonIconControllerPrefabPath);
        }

        [Header("Sprite Options")]
        public Sprite inputDisplayForwardSprite;
        public Sprite inputDisplayBackSprite;
        public Sprite inputDisplayUpSprite;
        public Sprite inputDisplayUpForwardSprite;
        public Sprite inputDisplayUpBackSprite;
        public Sprite inputDisplayDownSprite;
        public Sprite inputDisplayDownForwardSprite;
        public Sprite inputDisplayDownBackSprite;
        public Sprite inputDisplayButton1Sprite;
        public Sprite inputDisplayButton2Sprite;
        public Sprite inputDisplayButton3Sprite;
        public Sprite inputDisplayButton4Sprite;
        public Sprite inputDisplayButton5Sprite;
        public Sprite inputDisplayButton6Sprite;
        public Sprite inputDisplayButton7Sprite;
        public Sprite inputDisplayButton8Sprite;
        public Sprite inputDisplayButton9Sprite;
        public Sprite inputDisplayButton10Sprite;
        public Sprite inputDisplayButton11Sprite;
        public Sprite inputDisplayButton12Sprite;

        [Header("Texture Options")]
        public Texture inputDisplayForwardTexture;
        public Texture inputDisplayBackTexture;
        public Texture inputDisplayUpTexture;
        public Texture inputDisplayUpForwardTexture;
        public Texture inputDisplayUpBackTexture;
        public Texture inputDisplayDownTexture;
        public Texture inputDisplayDownForwardTexture;
        public Texture inputDisplayDownBackTexture;
        public Texture inputDisplayButton1Texture;
        public Texture inputDisplayButton2Texture;
        public Texture inputDisplayButton3Texture;
        public Texture inputDisplayButton4Texture;
        public Texture inputDisplayButton5Texture;
        public Texture inputDisplayButton6Texture;
        public Texture inputDisplayButton7Texture;
        public Texture inputDisplayButton8Texture;
        public Texture inputDisplayButton9Texture;
        public Texture inputDisplayButton10Texture;
        public Texture inputDisplayButton11Texture;
        public Texture inputDisplayButton12Texture;

        public ButtonPress GetButtonPressFromInputDisplaySprite(Sprite sprite)
        {
            if (sprite == null)
            {
                return ButtonPress.Forward;
            }

            if (sprite == inputDisplayForwardSprite)
            {
                return ButtonPress.Forward;
            }
            else if (sprite == inputDisplayBackSprite)
            {
                return ButtonPress.Back;
            }
            else if (sprite == inputDisplayUpSprite)
            {
                return ButtonPress.Up;
            }
            else if (sprite == inputDisplayUpForwardSprite)
            {
                return ButtonPress.UpForward;
            }
            else if (sprite == inputDisplayUpBackSprite)
            {
                return ButtonPress.UpBack;
            }
            else if (sprite == inputDisplayDownSprite)
            {
                return ButtonPress.Down;
            }
            else if (sprite == inputDisplayDownForwardSprite)
            {
                return ButtonPress.DownForward;
            }
            else if (sprite == inputDisplayDownBackSprite)
            {
                return ButtonPress.DownBack;
            }
            else if (sprite == inputDisplayButton1Sprite)
            {
                return ButtonPress.Button1;
            }
            else if (sprite == inputDisplayButton2Sprite)
            {
                return ButtonPress.Button2;
            }
            else if (sprite == inputDisplayButton3Sprite)
            {
                return ButtonPress.Button3;
            }
            else if (sprite == inputDisplayButton4Sprite)
            {
                return ButtonPress.Button4;
            }
            else if (sprite == inputDisplayButton5Sprite)
            {
                return ButtonPress.Button5;
            }
            else if (sprite == inputDisplayButton6Sprite)
            {
                return ButtonPress.Button6;
            }
            else if (sprite == inputDisplayButton7Sprite)
            {
                return ButtonPress.Button7;
            }
            else if (sprite == inputDisplayButton8Sprite)
            {
                return ButtonPress.Button8;
            }
            else if (sprite == inputDisplayButton9Sprite)
            {
                return ButtonPress.Button9;
            }
            else if (sprite == inputDisplayButton10Sprite)
            {
                return ButtonPress.Button10;
            }
            else if (sprite == inputDisplayButton11Sprite)
            {
                return ButtonPress.Button11;
            }
            else if (sprite == inputDisplayButton12Sprite)
            {
                return ButtonPress.Button12;
            }

            return ButtonPress.Forward;
        }

        #region Input Display Methods

        public bool CanDisplayInputs()
        {
            if (UFE.config == null)
            {
                return false;
            }

            if (UFE.gameMode == GameMode.StoryMode
               && UFE.config.debugOptions.displayInputsStoryMode == false)
            {
                return false;
            }
            else if (UFE.gameMode == GameMode.VersusMode
                && UFE.config.debugOptions.displayInputsVersus == false)
            {
                return false;
            }
            else if (UFE.gameMode == GameMode.TrainingRoom
                && UFE.config.debugOptions.displayInputsTraining == false)
            {
                return false;
            }
            else if (UFE.gameMode == GameMode.NetworkGame
                && UFE.config.debugOptions.displayInputsNetwork == false)
            {
                return false;
            }
            else if (UFE.gameMode == GameMode.ChallengeMode
                && UFE.config.debugOptions.displayInputsChallengeMode == false)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Input Display String Methods

        public string GetInputDisplayStringFromButtonPress(ButtonPress buttonPress)
        {
            return "?";

            /*switch (buttonPress)
            {
                case ButtonPress.Forward:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayForward;

                case ButtonPress.Back:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayBack;

                case ButtonPress.Down:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayDown;

                case ButtonPress.DownForward:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayDownForward;

                case ButtonPress.DownBack:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayDownBack;

                case ButtonPress.Up:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayUp;

                case ButtonPress.UpForward:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayUpForward;

                case ButtonPress.UpBack:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayUpBack;

                case ButtonPress.Button1:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton1;

                case ButtonPress.Button2:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton2;

                case ButtonPress.Button3:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton3;

                case ButtonPress.Button4:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton4;

                case ButtonPress.Button5:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton5;

                case ButtonPress.Button6:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton6;

                case ButtonPress.Button7:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton7;

                case ButtonPress.Button8:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton8;

                case ButtonPress.Button9:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton9;

                case ButtonPress.Button10:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton10;

                case ButtonPress.Button11:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton11;

                case ButtonPress.Button12:
                    return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton12;

                default:
                    return "";
            }*/
        }

        public string GetInputDisplayStringFromMoveInputs(MoveInputs moveInputs)
        {
            if (moveInputs == null)
            {
                return "";
            }

            if (moveInputs.onPressExecution == true)
            {
                return "Press";
            }

            if (moveInputs.onReleaseExecution == true)
            {
                return "Release";
            }

            return "";
        }

        public string GetInputDisplayStringFromSprite(Sprite sprite)
        {
            if (sprite == null)
            {
                return "";
            }

            return "?";

            /*if (sprite == inputDisplayForwardSprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayForward;
            }
            else if (sprite == inputDisplayBackSprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayBack;
            }
            else if (sprite == inputDisplayUpSprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayUp;
            }
            else if (sprite == inputDisplayUpForwardSprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayUpForward;
            }
            else if (sprite == inputDisplayUpBackSprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayUpBack;
            }
            else if (sprite == inputDisplayDownSprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayDown;
            }
            else if (sprite == inputDisplayDownForwardSprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayDownForward;
            }
            else if (sprite == inputDisplayDownBackSprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayDownBack;
            }
            else if (sprite == inputDisplayButton1Sprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton1;
            }
            else if (sprite == inputDisplayButton2Sprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton2;
            }
            else if (sprite == inputDisplayButton3Sprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton3;
            }
            else if (sprite == inputDisplayButton4Sprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton4;
            }
            else if (sprite == inputDisplayButton5Sprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton5;
            }
            else if (sprite == inputDisplayButton6Sprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton6;
            }
            else if (sprite == inputDisplayButton7Sprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton7;
            }
            else if (sprite == inputDisplayButton8Sprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton8;
            }
            else if (sprite == inputDisplayButton9Sprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton9;
            }
            else if (sprite == inputDisplayButton10Sprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton10;
            }
            else if (sprite == inputDisplayButton11Sprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton11;
            }
            else if (sprite == inputDisplayButton12Sprite)
            {
                return UFE2FTE.languageOptions.selectedLanguage.InputDisplayButton12;
            }

            return "";*/
        }

        #endregion

        #region Input Display Sprite Methods

        public Sprite GetInputDisplaySpriteFromButtonPress(ButtonPress buttonPress)
        {
            switch (buttonPress)
            {
                case ButtonPress.Forward:
                    return inputDisplayForwardSprite;

                case ButtonPress.Back:
                    return inputDisplayBackSprite;

                case ButtonPress.Up:
                    return inputDisplayUpSprite;

                case ButtonPress.UpForward:
                    return inputDisplayUpForwardSprite;

                case ButtonPress.UpBack:
                    return inputDisplayUpBackSprite;

                case ButtonPress.Down:
                    return inputDisplayDownSprite;

                case ButtonPress.DownForward:
                    return inputDisplayDownForwardSprite;

                case ButtonPress.DownBack:
                    return inputDisplayDownBackSprite;

                case ButtonPress.Button1:
                    return inputDisplayButton1Sprite;

                case ButtonPress.Button2:
                    return inputDisplayButton2Sprite;

                case ButtonPress.Button3:
                    return inputDisplayButton3Sprite;

                case ButtonPress.Button4:
                    return inputDisplayButton4Sprite;

                case ButtonPress.Button5:
                    return inputDisplayButton5Sprite;

                case ButtonPress.Button6:
                    return inputDisplayButton6Sprite;

                case ButtonPress.Button7:
                    return inputDisplayButton7Sprite;

                case ButtonPress.Button8:
                    return inputDisplayButton8Sprite;

                case ButtonPress.Button9:
                    return inputDisplayButton9Sprite;

                case ButtonPress.Button10:
                    return inputDisplayButton10Sprite;

                case ButtonPress.Button11:
                    return inputDisplayButton11Sprite;

                case ButtonPress.Button12:
                    return inputDisplayButton12Sprite;

                default:
                    return null;
            }
        }

        public Sprite GetInputDisplaySpriteFromTexture(Texture texture)
        {
            //Add to DefaultBattleGUI
            //Sprite sprite = UFE2FTE.UFE2FTE.GetInputDisplaySpriteFromTexture(inputRef.activeIcon);

            if (texture == null)
            {
                return null;
            }

            if (texture == inputDisplayForwardTexture)
            {
                return inputDisplayForwardSprite;
            }
            else if (texture == inputDisplayBackTexture)
            {
                return inputDisplayBackSprite;
            }
            else if (texture == inputDisplayUpTexture)
            {
                return inputDisplayUpSprite;
            }
            else if (texture == inputDisplayUpForwardTexture)
            {
                return inputDisplayUpForwardSprite;
            }
            else if (texture == inputDisplayUpBackTexture)
            {
                return inputDisplayUpBackSprite;
            }
            else if (texture == inputDisplayDownTexture)
            {
                return inputDisplayDownSprite;
            }
            else if (texture == inputDisplayDownForwardTexture)
            {
                return inputDisplayDownForwardSprite;
            }
            else if (texture == inputDisplayDownBackTexture)
            {
                return inputDisplayDownBackSprite;
            }
            else if (texture == inputDisplayButton1Texture)
            {
                return inputDisplayButton1Sprite;
            }
            else if (texture == inputDisplayButton2Texture)
            {
                return inputDisplayButton2Sprite;
            }
            else if (texture == inputDisplayButton3Texture)
            {
                return inputDisplayButton3Sprite;
            }
            else if (texture == inputDisplayButton4Texture)
            {
                return inputDisplayButton4Sprite;
            }
            else if (texture == inputDisplayButton5Texture)
            {
                return inputDisplayButton5Sprite;
            }
            else if (texture == inputDisplayButton6Texture)
            {
                return inputDisplayButton6Sprite;
            }
            else if (texture == inputDisplayButton7Texture)
            {
                return inputDisplayButton7Sprite;
            }
            else if (texture == inputDisplayButton8Texture)
            {
                return inputDisplayButton8Sprite;
            }
            else if (texture == inputDisplayButton9Texture)
            {
                return inputDisplayButton9Sprite;
            }
            else if (texture == inputDisplayButton10Texture)
            {
                return inputDisplayButton10Sprite;
            }
            else if (texture == inputDisplayButton11Texture)
            {
                return inputDisplayButton11Sprite;
            }
            else if (texture == inputDisplayButton12Texture)
            {
                return inputDisplayButton12Sprite;
            }

            return null;
        }

        #endregion
    }
}