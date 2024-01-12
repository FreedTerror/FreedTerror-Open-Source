using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Transform Shake", menuName = "U.F.E. 2 F.T.E./Shake/Transform Shake")]
    public class TransformShakeScriptableObject : ScriptableObject
    {     
        public float shakeDuration;
        public Vector3 shakePower;

        [NaughtyAttributes.Button]
        private void CallOnCameraShakeEvent()
        {
            CameraShakeController.CallOnCameraShakeEvent(shakeDuration, shakePower);
        }

        [NaughtyAttributes.Button]
        private void CallOnCharacterPortraitShakeEvent()
        {
            CharacterPortraitShakeController.CallOnCharacterPortraitShakeEvent(shakeDuration, shakePower, UFE.GetPlayer1ControlsScript());
            CharacterPortraitShakeController.CallOnCharacterPortraitShakeEvent(shakeDuration, shakePower, UFE.GetPlayer2ControlsScript());
        }
    }
}
