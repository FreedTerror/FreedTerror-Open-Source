using UnityEngine;

namespace FreedTerror
{
    public class SpriteShadowController : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRendererToSet;
        [SerializeField]
        private SpriteRenderer spriteRendererToCopy;

        private void Update()
        {
            UpdateSpriteRenderer();
        }

        private void UpdateSpriteRenderer()
        {
            if (spriteRendererToSet == null
                || spriteRendererToCopy == null)
            {
                return;
            }

            spriteRendererToSet.sprite = spriteRendererToCopy.sprite;
            spriteRendererToSet.flipX = spriteRendererToCopy.flipX;
            spriteRendererToSet.flipY = spriteRendererToCopy.flipY;
            spriteRendererToSet.sortingOrder = spriteRendererToCopy.sortingOrder - 1;
        }
    }
}