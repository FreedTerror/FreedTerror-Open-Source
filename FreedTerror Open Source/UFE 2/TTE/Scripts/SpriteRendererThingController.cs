using UnityEngine;

namespace FreedTerror.TTE
{
    public class SpriteRendererThingController : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer colorSpriteRenderer;
        private Sprite previousColorSprite;
        [SerializeField]
        private SpriteRenderer highlightSpriteRenderer;
        [SerializeField]
        private SpriteRenderer shadowSpriteRender;
        [SerializeField]
        private SpriteReferencesScriptableObject spriteReferencesScriptableObject;

        private void Start()
        {
            previousColorSprite = null;
        }

        private void Update()
        {
            UpdateSpriteRenderers();
        }

        private void UpdateSpriteRenderers()
        {
            if (colorSpriteRenderer == null
                || highlightSpriteRenderer == null
                || shadowSpriteRender == null
                || spriteReferencesScriptableObject == null)
            {
                return;
            }

            if (previousColorSprite != colorSpriteRenderer.sprite)
            {
                previousColorSprite = colorSpriteRenderer.sprite;

                int length = spriteReferencesScriptableObject.dataArray.Length;
                bool foundSprite = false;
                for (int i = 0; i < length; i++)
                {
                    var item = spriteReferencesScriptableObject.dataArray[i];
                    int lengthA = item.colorSpriteArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (colorSpriteRenderer.sprite != item.colorSpriteArray[a])
                        {
                            continue;
                        }

                        if (a < item.highlightSpriteArray.Length)
                        {
                            highlightSpriteRenderer.sprite = item.highlightSpriteArray[a];
                        }

                        if (a < item.shadowSpriteArray.Length)
                        {
                            shadowSpriteRender.sprite = item.shadowSpriteArray[a];
                        }

                        foundSprite = true;

                        break;
                    }
                }

                if (foundSprite == false)
                {
                    highlightSpriteRenderer.sprite = null;
                    shadowSpriteRender.sprite = null;            
                }
            }

            highlightSpriteRenderer.flipX = colorSpriteRenderer.flipX;
            shadowSpriteRender.flipX = colorSpriteRenderer.flipX;

            //Layer order from front to back: highlight, shadow, color.
            if (colorSpriteRenderer.sortingOrder >= 0)
            {
                highlightSpriteRenderer.sortingOrder = colorSpriteRenderer.sortingOrder + 2;
                shadowSpriteRender.sortingOrder = colorSpriteRenderer.sortingOrder + 1;
            }
            else
            {
                highlightSpriteRenderer.sortingOrder = colorSpriteRenderer.sortingOrder - 2;
                shadowSpriteRender.sortingOrder = colorSpriteRenderer.sortingOrder - 1;
            }
        }
    }
}
