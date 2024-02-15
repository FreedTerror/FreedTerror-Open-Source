using UnityEngine;

namespace FreedTerror.UFE2
{
    public class CameraController : MonoBehaviour
    {
        private Camera myCamera;
        private Transform myCameraTransform;
        [SerializeField]
        private Vector3 cameraPosition;
        [SerializeField]
        private Vector3 cameraRotation;

        private void Start()
        {
            myCamera = Camera.main;
            if (myCamera != null)
            {
                myCameraTransform = myCamera.transform;
            }
        }

        private void Update()
        {
            if (myCamera != null)
            {
                if (myCameraTransform != null)
                {
                    myCameraTransform.position = cameraPosition;
                    myCameraTransform.eulerAngles = cameraRotation;
                }
            }
        }
    }
}
