using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CharacterSpriteRendererRawImageController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private RawImage rawImage;
  
        private void Start()
        {
            SetRawImage(UFE2FTE.GetControlsScript(player));
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