using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTESpriteShadowController : MonoBehaviour
    {
        private enum OrderInLayerMode
        {
            Infront,
            Behind
        }

        [SerializeField]
        private SpriteRenderer spriteRendererToSet;
        [SerializeField]
        private SpriteRenderer spriteRendererToCopy;
        [SerializeField]
        private OrderInLayerMode spriteRendererToCopyOrderInLayerMode = OrderInLayerMode.Behind;

        private void Update()
        {
            SetSpriteRenderers();
        }

        private void SetSpriteRenderers()
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