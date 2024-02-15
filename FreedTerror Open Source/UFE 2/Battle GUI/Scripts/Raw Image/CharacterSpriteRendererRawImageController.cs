using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CharacterSpriteRendererRawImageController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private RawImage rawImage;
  
        private void Start()
        {
            SetRawImage(UFE2Manager.GetControlsScript(player));
        }

        private void SetRawImage(ControlsScript player)
        {
            if (player == null
                || player.mySpriteRenderer == null)
            {
                return;
            }

            rawImage.material = player.mySpriteRenderer.material;
        }
    }
}