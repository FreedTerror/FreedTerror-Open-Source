using UnityEngine;

namespace UFE2FTE
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