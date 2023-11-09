using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CharacterPortraitRawImageController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private RawImage characterPortraitRawImage;

        private void Start()
        {
            SetCharacterPortrait(UFE2FTE.GetControlsScript(player));
        }

        private void SetCharacterPortrait(ControlsScript player)
        {
            if (player == null
                || player.myInfo == null)
            {
                return;
            }

            if (characterPortraitRawImage != null)
            {
                characterPortraitRawImage.texture = player.myInfo.profilePictureSmall;
            }
        }
    }
}