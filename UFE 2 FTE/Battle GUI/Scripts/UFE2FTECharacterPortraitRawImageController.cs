using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTECharacterPortraitRawImageController : MonoBehaviour
    {
        private enum Player
        {
            Player1,
            Player2
        }
        [SerializeField]
        private Player player;
        [SerializeField]
        private RawImage characterPortraitRawImage;

        private void Start()
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            SetCharacterPortraitRawImage();
        }

        private void SetCharacterPortraitRawImage()
        {
            if (characterPortraitRawImage == null)
            {
                return;
            }

            if (player == Player.Player1)
            {
                characterPortraitRawImage.texture = UFE.GetPlayer1ControlsScript().myInfo.profilePictureSmall;
            }
            else if (player == Player.Player2)
            {
                characterPortraitRawImage.texture = UFE.GetPlayer2ControlsScript().myInfo.profilePictureSmall;
            }
        }
    }
}