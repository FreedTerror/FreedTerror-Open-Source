using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace FreedTerror.UFE2
{
    public class MoveListCharacterAttributesUIController : MonoBehaviour
    {
        [SerializeField]
        private Text characterAttributesText;

        private void Start()
        {
            SetCharacterAttributesText(UFE2Manager.GetCharacterInfo(UFE2Manager.instance.pausedPlayer));
        }

        private void SetCharacterAttributesText(UFE3D.CharacterInfo characterInfo)
        {
            if (characterAttributesText == null
                || characterInfo == null)
            {
                return;
            }

            characterAttributesText.text = GetCharacterAttributesMessage(characterInfo);
        }

        private string GetCharacterAttributesMessage(UFE3D.CharacterInfo characterInfo)
        {
            if (UFE.config == null
                || characterInfo == null)
            {
                return "";
            }

            string moveSideWaysSpeed = "";
            switch (UFE.config.gameplayType)
            {
                case GameplayType._3DFighter:
                case GameplayType._3DArena:
                    moveSideWaysSpeed =
                        "Move Sideways Speed: " +
                        characterInfo.physics._moveSidewaysSpeed +
                        System.Environment.NewLine;
                    break;
            }

            return "Attributes" +
                System.Environment.NewLine +
                "Life Points: " +
                characterInfo.lifePoints +
                System.Environment.NewLine +
                "Move Forward Speed: " +
                characterInfo.physics._moveForwardSpeed +
                System.Environment.NewLine +
                "Move Backward Speed: " +
                characterInfo.physics._moveBackSpeed +
                System.Environment.NewLine +
                moveSideWaysSpeed +
                "Jump Startup Frames: " +
                characterInfo.physics.jumpDelay +
                System.Environment.NewLine +
                "Jump Landing Frames: " +
                characterInfo.physics.landingDelay +
                System.Environment.NewLine +
                "Jump Force: " +
                characterInfo.physics._jumpForce +
                System.Environment.NewLine +
                "Jump Forward Distance: " +
                characterInfo.physics._jumpDistance +
                System.Environment.NewLine +
                "Jump Backward Distance: " +
                characterInfo.physics._jumpBackDistance +
                System.Environment.NewLine +
                "Weight: " +
                characterInfo.physics._weight +
                System.Environment.NewLine +
                "Friction: " +
                characterInfo.physics._friction;
        }
    }
}