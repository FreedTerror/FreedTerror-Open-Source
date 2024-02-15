using UnityEngine;

namespace FreedTerror.UFE2
{
    public class RectTransformShakeController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform rectTransformToShake;
        private Vector3 originalRectTransformPosition;
        [SerializeField]
        private bool resetTransformBeforeEachShake;
        [SerializeField]
        private bool resetTransformAfterShake;
        [SerializeField]
        private float shakeDuration;
        [SerializeField]
        private Vector3 shakePower;

        private void Start()
        {
            if (rectTransformToShake != null)
            {
                originalRectTransformPosition = rectTransformToShake.anchoredPosition3D;
            }
        }

        private void LateUpdate()
        {
            if (UFE.IsPaused() == true)
            {
                return;
            }

            float deltaTime = (float)UFE.fixedDeltaTime;

            ShakeTransform(deltaTime);
        }

        private void ShakeTransform(float deltaTime)
        {
            float randomX = Random.Range((float)-shakeDuration * shakePower.x, (float)shakeDuration * shakePower.x);
            float randomY = Random.Range((float)-shakeDuration * shakePower.y, (float)shakeDuration * shakePower.y);
            float randomZ = Random.Range((float)-shakeDuration * shakePower.z, (float)shakeDuration * shakePower.z);

            if (rectTransformToShake != null)
            {
                if (resetTransformBeforeEachShake == true)
                {
                    rectTransformToShake.anchoredPosition3D = originalRectTransformPosition;
                }
 
                rectTransformToShake.position += new Vector3(randomX, randomY, randomZ);
            }

            shakeDuration -= deltaTime;
            if (shakeDuration <= 0)
            {
                shakeDuration = 0;

                if (rectTransformToShake != null)
                {
                    if (resetTransformAfterShake == true)
                    {
                        rectTransformToShake.anchoredPosition3D = originalRectTransformPosition;
                    }
                }
            }
        }

        private void SetShakeVariables(TransformShakeScriptableObject transformShakeScriptableObject)
        {
            if (transformShakeScriptableObject == null)
            {
                return;
            }

            shakeDuration = transformShakeScriptableObject.shakeDuration;
            shakePower = transformShakeScriptableObject.shakePower;
        }
    }
}