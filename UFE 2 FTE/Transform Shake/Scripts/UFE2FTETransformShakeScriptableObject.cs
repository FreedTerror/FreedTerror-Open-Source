using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Transform Shake", menuName = "U.F.E. 2 F.T.E./Transform Shake/Transform Shake")]
    public class UFE2FTETransformShakeScriptableObject : ScriptableObject
    {
        public bool usePlayer;
        public bool usePlayerOpponent;
        [Header("Duration Options")]
        public float transformShakeDuration;
        [Header("Position Options")]
        public bool useResetTransformXPosition;
        public bool useResetTransformYPosition;
        public bool useResetTransformZPosition;
        public Vector3 transformShakePositionOffset;
        public bool useTransformShakePositionFadeOutAnimationCurve;   
        public AnimationCurve transformShakePositionFadeOutAnimationCurve;
        [Header("Rotation Options")]
        public bool useResetTransformXRotation;
        public bool useResetTransformYRotation;
        public bool useResetTransformZRotation;
        public Vector3 transformShakeRotationOffset;
        public bool useTransformShakeRotationFadeOutAnimationCurve;
        public AnimationCurve transformShakeRotationFadeOutAnimationCurve;
        [Header("Scale Options")]
        public bool useResetTransformXScale;
        public bool useResetTransformYScale;
        public bool useResetTransformZScale;
        public Vector3 transformShakeScaleOffset;
        public bool useTransformShakeScaleFadeOutAnimationCurve;
        public AnimationCurve transformShakeScaleFadeOutAnimationCurve;

        [NaughtyAttributes.Button("Call On Transform Shake Event")]
        private void CallOnTransformShake()
        {
            UFE2FTETransformShakeEventsManager.CallOnTransformShake(this, null, null);
        }
    }
}
