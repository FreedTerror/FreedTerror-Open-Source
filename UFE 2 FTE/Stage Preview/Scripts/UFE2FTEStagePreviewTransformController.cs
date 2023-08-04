using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTEStagePreviewTransformController : MonoBehaviour
    {
        [SerializeField]
        private bool useMainCameraAsParentTransform;
        [SerializeField]
        private Vector3 initialPosition;

        private void Start()
        {
            if (useMainCameraAsParentTransform == true)
            {
                transform.parent = Camera.main.transform;

                transform.localPosition = initialPosition;

                return;
            }

            transform.position = initialPosition;
        }
    }
}