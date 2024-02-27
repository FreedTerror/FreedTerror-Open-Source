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
            UpdateCharacterPortrait(UFE2Manager.GetControlsScript(player));
        }

        private void UpdateCharacterPortrait(ControlsScript player)
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