using UnityEngine;

namespace UFE2FTE
{
    public class TransformShakeController : MonoBehaviour
    {
        [SerializeField]
        private Transform myTransform;
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
            if (myTransform != null)
            {
                originalTransformPosition = myTransform.position;
            }
        }

        private void LateUpdate()
        {
            if (UFE.isPaused() == true)
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

            if (myTransform != null)
            {
                if (resetTransformBeforeEachShake == true)
                {
                    myTransform.position = originalTransformPosition;
                }
 
                myTransform.position += new Vector3(randomX, randomY, randomZ);
            }

            shakeDuration -= deltaTime;
            if (shakeDuration <= 0)
            {
                shakeDuration = 0;

                if (myTransform != null)
                {
                    if (resetTransformAfterShake == true)
                    {
                        myTransform.position = originalTransformPosition;
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