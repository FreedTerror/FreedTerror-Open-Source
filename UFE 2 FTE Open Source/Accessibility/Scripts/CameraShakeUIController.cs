using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CameraShakeUIController : MonoBehaviour
    {
        [SerializeField]
        private Text cameraShakeText;

        private void Update()
        {
            UFE2FTE.SetTextMessage(cameraShakeText, UFE2FTE.GetStringFromBool(UFE2FTE.instance.useCameraShake));
        }

        public void ToggleCameraShake()
        {
            UFE2FTE.instance.useCameraShake = UFE2FTE.ToggleBool(UFE2FTE.instance.useCameraShake);
        }
    }
}