using UnityEngine;

namespace FreedTerror.UFE2
{
    public class TransformShakeController : MonoBehaviour
    {
        [SerializeField]
        private Transform transformToShake;
        private Vector3 originalTransformPosition;
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
            if (transformToShake != null)
            {
                originalTransformPosition = transformToShake.position;
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

            if (transformToShake != null)
            {
                if (resetTransformBeforeEachShake == true)
                {
                    transformToShake.position = originalTransformPosition;
                }
 
                transformToShake.position += new Vector3(randomX, randomY, randomZ);
            }

            shakeDuration -= deltaTime;
            if (shakeDuration <= 0)
            {
                shakeDuration = 0;

                if (transformToShake != null)
                {
                    if (resetTransformAfterShake == true)
                    {
                        transformToShake.position = originalTransformPosition;
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