using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class MoveListCharacterAttributesUIController : MonoBehaviour
    {
        [SerializeField]
        private Text characterAttributesText;
 
        private void Start()
        {
            SetCharacterAttributesText(UFE2FTE.GetCharacterInfo(UFE2FTE.Instance.pausedPlayer));
        }

        private void SetCharacterAttributesText(UFE3D.CharacterInfo characterInfo)
        {
            if (characterAttributesText == null
                || characterInfo == null)
            {
                return;
            }

            characterAttributesText.text = GetCharacterAttributesMessage(characterInfo);

            if (characterAttributesText.text == "")
            {
                characterAttributesText.gameObject.SetActive(false);
            }
        }

        private string GetCharacterAttributesMessage(UFE3D.CharacterInfo characterInfo)
        {
            if (UFE.config == null
                || characterInfo == null)
            {
                return "";
            }

            string characterAttributesMessage = "";

            if (UFE2FTE.languageOptions.selectedLanguage.MaxLifePoints != "")
            {
                characterAttributesMessage = 
                    characterAttributesMessage +
                    UFE2FTE.languageOptions.selectedLanguage.MaxLifePoints + 
                    " " +
                    characterInfo.lifePoints +
                    System.Environment.NewLine;
            }

            if (UFE2FTE.languageOptions.selectedLanguage.MaxGaugePoints != "")
            {
                characterAttributesMessage = 
                    characterAttributesMessage +
                    UFE2FTE.languageOptions.selectedLanguage.MaxGaugePoints + 
                    " " +
                    characterInfo.maxGaugePoints + 
                    System.Environment.NewLine;
            }

            if (UFE2FTE.languageOptions.selectedLanguage.MoveForwardSpeed != "")
            {
                characterAttributesMessage = 
                    characterAttributesMessage +
                    UFE2FTE.languageOptions.selectedLanguage.MoveForwardSpeed + 
                    " " +
                    characterInfo.physics._moveForwardSpeed + 
                    System.Environment.NewLine;
            }

            if (UFE2FTE.languageOptions.selectedLanguage.MoveBackSpeed != "")
            {
                characterAttributesMessage = 
                    characterAttributesMessage +
                    UFE2FTE.languageOptions.selectedLanguage.MoveBackSpeed +
                    " " +
                    characterInfo.physics._moveBackSpeed + 
                    System.Environment.NewLine;
            }

            if (UFE2FTE.languageOptions.selectedLanguage.MoveSidewaysSpeed != "")
            {
                switch (UFE.config.gameplayType)
                {
                    case GameplayType._3DFighter:
                    case GameplayType._3DArena:
                        characterAttributesMessage =
                            characterAttributesMessage +
                            UFE2FTE.languageOptions.selectedLanguage.MoveSidewaysSpeed +
                            " " +
                            characterInfo.physics._moveSidewaysSpeed +
                            System.Environment.NewLine;
                        break;
                }
            }

            if (UFE2FTE.languageOptions.selectedLanguage.JumpStartupFrames != "")
            {
                characterAttributesMessage = 
                    characterAttributesMessage +
                    UFE2FTE.languageOptions.selectedLanguage.JumpStartupFrames +
                    " " +
                    characterInfo.physics.jumpDelay + 
                    System.Environment.NewLine;
            }

            if (UFE2FTE.languageOptions.selectedLanguage.JumpLandingFrames != "")
            {
                characterAttributesMessage = 
                    characterAttributesMessage +
                    UFE2FTE.languageOptions.selectedLanguage.JumpLandingFrames +
                    " " +
                    characterInfo.physics.landingDelay + 
                    System.Environment.NewLine;
            }

            if (UFE2FTE.languageOptions.selectedLanguage.JumpForce != "")
            {
                characterAttributesMessage =
                    characterAttributesMessage +
                    UFE2FTE.languageOptions.selectedLanguage.JumpForce +
                    " " +
                    characterInfo.physics._jumpForce +
                    System.Environment.NewLine;
            }

            if (UFE2FTE.languageOptions.selectedLanguage.JumpForwardDistance != "")
            {
                characterAttributesMessage =
                    characterAttributesMessage +
                    UFE2FTE.languageOptions.selectedLanguage.JumpForwardDistance +
                    " " +
                    characterInfo.physics._jumpDistance +
                    System.Environment.NewLine;
            }

            if (UFE2FTE.languageOptions.selectedLanguage.JumpBackDistance != "")
            {
                characterAttributesMessage =
                    characterAttributesMessage +
                    UFE2FTE.languageOptions.selectedLanguage.JumpBackDistance +
                    " " +
                    characterInfo.physics._jumpBackDistance +
                    System.Environment.NewLine;
            }

            if (UFE2FTE.languageOptions.selectedLanguage.Weight != "")
            {
                characterAttributesMessage =
                    characterAttributesMessage +
                    UFE2FTE.languageOptions.selectedLanguage.Weight +
                    " " +
                    characterInfo.physics._weight +
                    System.Environment.NewLine;
            }

            if (UFE2FTE.languageOptions.selectedLanguage.Friction != "")
            {
                characterAttributesMessage =
                    characterAttributesMessage +
                    UFE2FTE.languageOptions.selectedLanguage.Friction +
                    " " +
                    characterInfo.physics._friction;
            }
            //Remove System.Environment.NewLine on last string concat to not add a new line to the bottom

            if (characterAttributesMessage != "")
            {
                characterAttributesMessage = characterAttributesMessage.Insert(0, characterInfo.characterName + " " + UFE2FTE.languageOptions.selectedLanguage.Attributes + System.Environment.NewLine);
            }

            return characterAttributesMessage;
        }
    }
}