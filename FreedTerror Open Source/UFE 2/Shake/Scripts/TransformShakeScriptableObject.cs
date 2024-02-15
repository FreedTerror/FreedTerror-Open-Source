using UnityEngine;

namespace FreedTerror.UFE2
{
    [CreateAssetMenu(menuName = "FreedTerror/UFE 2/Shake/Transform Shake", fileName = "New Transform Shake")]
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
            CharacterPortraitShakeController.CallOnCharacterPortraitShakeEvent(shakeDuration, shakePower, UFE.p1ControlsScript);
            CharacterPortraitShakeController.CallOnCharacterPortraitShakeEvent(shakeDuration, shakePower, UFE.p2ControlsScript);
        }
    }
}
