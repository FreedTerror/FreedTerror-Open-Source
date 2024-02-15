using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CameraShakeUIController : MonoBehaviour
    {
        [SerializeField]
        private Text cameraShakeText;
        private bool previousCameraShake;

        private void Start()
        {
            previousCameraShake = UFE2Manager.instance.useCameraShake;

            if (cameraShakeText != null)
            {
                cameraShakeText.text = Utility.GetStringFromBool(UFE2Manager.instance.useCameraShake);
            }
        }

        private void Update()
        {
            if (previousCameraShake != UFE2Manager.instance.useCameraShake)
            {
                previousCameraShake = UFE2Manager.instance.useCameraShake;

                if (cameraShakeText != null)
                {
                    cameraShakeText.text = Utility.GetStringFromBool(UFE2Manager.instance.useCameraShake);
                }
            }
        }

        public void ToggleCameraShake()
        {
            UFE2Manager.instance.useCameraShake = !UFE2Manager.instance.useCameraShake;
        }
    }
}