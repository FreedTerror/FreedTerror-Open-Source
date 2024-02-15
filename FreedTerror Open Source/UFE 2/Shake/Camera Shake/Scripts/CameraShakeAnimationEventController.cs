using UnityEngine;

namespace FreedTerror.UFE2
{
    public class CameraShakeAnimationEventController : MonoBehaviour
    {
        public void CallOnCameraShakeEvent(TransformShakeScriptableObject transformShakeScriptableObject)
        {
            if (transformShakeScriptableObject == null)
            {
                return;
            }

            CameraShakeController.CallOnCameraShakeEvent(transformShakeScriptableObject.shakeDuration, transformShakeScriptableObject.shakePower);
        }
    }
}