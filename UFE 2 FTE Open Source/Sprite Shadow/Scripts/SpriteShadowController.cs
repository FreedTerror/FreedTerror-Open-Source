using UnityEngine;

namespace UFE2FTE
{
    public class SpriteShadowController : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRendererToSet;
        [SerializeField]
        private SpriteRenderer spriteRendererToCopy;
        private enum OrderInLayerMode
        {
            Infront,
            Behind
        }
        [SerializeField]
        private OrderInLayerMode spriteRendererToCopyOrderInLayerMode = OrderInLayerMode.Behind;

        private void Update()
        {
            SetSpriteRenderer();
        }

        private void SetSpriteRenderer()
        {
            if (spriteRendererToSet == null
                || spriteRendererToCopy == null)
            {
                return;
            }

            spriteRendererToSet.sprite = spriteRendererToCopy.sprite;
            spriteRendererToSet.flipX = spriteRendererToCopy.flipX;
            spriteRendererToSet.flipY = spriteRendererToCopy.flipY;
            switch(spriteRendererToCopyOrderInLayerMode)
            {
                case OrderInLayerMode.Infront:
                    spriteRendererToSet.sortingOrder = spriteRendererToCopy.sortingOrder + 1;
                    break;

                case OrderInLayerMode.Behind:
                    spriteRendererToSet.sortingOrder = spriteRendererToCopy.sortingOrder - 1;
                    break;
            }
        }
    }
}