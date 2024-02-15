using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CharacterPortraitRawImageController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private RawImage characterPortraitRawImage;

        private void Start()
        {
            SetCharacterPortrait(UFE2Manager.GetControlsScript(player));
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