using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTECameraShakeUI : MonoBehaviour
    {
        [SerializeField]
        private Toggle cameraShakeToggle;

        private void Update()
        {
            SetToggleIsOn(cameraShakeToggle, UFE2FTECameraShakeOptionsManager.useCameraShake);
        }

        public void SetUseCameraShake(bool useCameraShake)
        {
            UFE2FTECameraShakeOptionsManager.SetUseCameraShakeWithPlayerPrefs(useCameraShake);
        }

        private static void SetToggleIsOn(Toggle toggle, bool isOn)
        {
            if (toggle == null)
            {
                return;
            }

            toggle.isOn = isOn;
        }
    }
}
