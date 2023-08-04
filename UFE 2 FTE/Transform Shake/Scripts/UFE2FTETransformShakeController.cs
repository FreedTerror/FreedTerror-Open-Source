using System;
using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTETransformShakeController : MonoBehaviour
    {
        private ControlsScript myControlsScript;

        [SerializeField]
        private bool isCameraShakeController;

        private enum Player
        {
            AllPlayers,
            Player1,
            Player2
        }
        [SerializeField]
        private Player player;

        [Serializable]
        private class TransformShakeOptions
        {
            [Header("Transform Options")]
            public Transform transform;
            [HideInInspector]
            public Vector3 originalTransformPosition;
            [HideInInspector]
            public Vector3 originalTransformRotation;
            [HideInInspector]
            public Vector3 originalTransformScale;
            [Header("Rect Transform Options")]
            public RectTransform rectTransform;
            [HideInInspector]
            public Vector3 originalRectTransformPosition;
            [HideInInspector]
            public Vector3 originalRectTransformRotation;
            [HideInInspector]
            public Vector3 originalRectTransformScale;
            [Header("Duration Options")]
            [HideInInspector]
            public float transformShakeDuration;
            [HideInInspector]
            public float transformShakeDurationElapsedTime;
            [Header("Position Options")]
            [HideInInspector]
            public bool useResetTransformXPosition;
            [HideInInspector]
            public bool useResetTransformYPosition;
            [HideInInspector]
            public bool useResetTransformZPosition;
            [HideInInspector]
            public Vector3 transformShakePositionOffset;
            [HideInInspector]
            public bool useTransformShakePositionFadeOutAnimationCurve;
            [HideInInspector]
            public AnimationCurve transformShakePositionFadeOutAnimationCurve;
            [Header("Rotation Options")]
            [HideInInspector]
            public bool useResetTransformXRotation;
            [HideInInspector]
            public bool useResetTransformYRotation;
            [HideInInspector]
            public bool useResetTransformZRotation;
            [HideInInspector]
            public Vector3 transformShakeRotationOffset;
            [HideInInspector]
            public bool useTransformShakeRotationFadeOutAnimationCurve;
            [HideInInspector]
            public AnimationCurve transformShakeRotationFadeOutAnimationCurve;
            [Header("Scale Options")]
            [HideInInspector]
            public bool useResetTransformXScale;
            [HideInInspector]
            public bool useResetTransformYScale;
            [HideInInspector]
            public bool useResetTransformZScale;
            [HideInInspector]
            public Vector3 transformShakeScaleOffset;
            [HideInInspector]
            public bool useTransformShakeScaleFadeOutAnimationCurve;
            [HideInInspector]
            public AnimationCurve transformShakeScaleFadeOutAnimationCurve;
        }
        [SerializeField]
        private TransformShakeOptions transformShakeOptions;

        [Serializable]
        private class TransformShakeScriptableObjectOptions
        {
            public UFE2FTETransformShakeScriptableObject[] transformShakeScriptableObjectArray;
            public bool useOnEnable;
            public bool useOnStart;
            public bool useOnDisable;
            public bool useOnDestroy;
        }
        [SerializeField]
        private TransformShakeScriptableObjectOptions[] transformShakeScriptableObjectOptionsArray;

        private void Awake()
        {
            UFE2FTECameraShakeOptionsManager.LoadUseCameraShakeFromPlayerPrefs(isCameraShakeController);

            SetTransformShakeOptionsOriginalTransformVariables();

            SetTransformShakeOptionsOriginalRectTransformVariables();
        }

        private void OnEnable()
        {
            UFE2FTETransformShakeEventsManager.OnTransformShake += OnTransformShake;

            SetTransformShakeScriptableObjectOptions(true);
        }

        private void Start()
        {
            myControlsScript = GetComponentInParent<ControlsScript>();

            SetTransformShakeScriptableObjectOptions(false, true);
        }

        private void LateUpdate()
        {
            float deltaTime = (float)UFE.fixedDeltaTime;

            SetTransformShakeOptions(deltaTime);
        }

        private void OnDisable()
        {
            UFE2FTETransformShakeEventsManager.OnTransformShake -= OnTransformShake;

            SetTransformShakeScriptableObjectOptions(false, false, true);
        }

        private void OnDestroy()
        {
            SetTransformShakeScriptableObjectOptions(false, false, false, true);
        }

        private void OnTransformShake(UFE2FTETransformShakeScriptableObject transformShakeScriptableObject, UFE2FTETransformShakeScriptableObject[] transformShakeScriptableObjectArray, ControlsScript player)
        {
            HandleTransformShakeVariablesFromTransformShakeScriptableObject(transformShakeScriptableObject, player);

            HandleTransformShakeVariablesFromTransformShakeScriptableObject(transformShakeScriptableObjectArray, player);
        }

        #region Transform Shake Options Methods

        private void SetTransformShakeOptionsOriginalTransformVariables()
        {
            if (transformShakeOptions.transform == null)
            {
                return;
            }

            transformShakeOptions.originalTransformPosition = transformShakeOptions.transform.position;

            transformShakeOptions.originalTransformRotation = transformShakeOptions.transform.eulerAngles;

            transformShakeOptions.originalTransformScale = transformShakeOptions.transform.localScale;
        }

        private void SetTransformShakeOptionsOriginalRectTransformVariables()
        {
            if (transformShakeOptions.rectTransform == null)
            {
                return;
            }

            transformShakeOptions.originalRectTransformPosition = transformShakeOptions.rectTransform.anchoredPosition3D;

            transformShakeOptions.originalRectTransformRotation = transformShakeOptions.rectTransform.eulerAngles;

            transformShakeOptions.originalRectTransformScale = transformShakeOptions.rectTransform.localScale;
        }

        private void ResetTransformShakeOptionsTransform(bool resetPosition, bool resetRotation, bool resetScale)
        {
            if (transformShakeOptions.transform == null)
            {
                return;
            }

            if (resetPosition == true)
            {
                transformShakeOptions.transform.position = GetTransformShakeOptionsOriginalTransformPosition();
            }

            if (resetRotation == true)
            {
                transformShakeOptions.transform.eulerAngles = GetTransformShakeOptionsOriginalTransformRotation();
            }

            if (resetScale == true)
            {
                transformShakeOptions.transform.localScale = GetTransformShakeOptionsOriginalTransformScale();
            }
        }

        private Vector3 GetTransformShakeOptionsOriginalTransformPosition()
        {
            if (transformShakeOptions.transform == null)
            {
                return new Vector3(0, 0, 0);
            }

            Vector3 resetPosition = transformShakeOptions.transform.position;

            if (transformShakeOptions.useResetTransformXPosition == true)
            {
                resetPosition.x = transformShakeOptions.originalTransformPosition.x;
            }

            if (transformShakeOptions.useResetTransformYPosition == true)
            {
                resetPosition.y = transformShakeOptions.originalTransformPosition.y;
            }

            if (transformShakeOptions.useResetTransformZPosition == true)
            {
                resetPosition.z = transformShakeOptions.originalTransformPosition.z;
            }

            return resetPosition;
        }

        private Vector3 GetTransformShakeOptionsOriginalTransformRotation()
        {
            if (transformShakeOptions.transform == null)
            {
                return new Vector3(0, 0, 0);
            }

            Vector3 resetRotation = transformShakeOptions.transform.eulerAngles;

            if (transformShakeOptions.useResetTransformXRotation == true)
            {
                resetRotation.x = transformShakeOptions.originalTransformRotation.x;
            }

            if (transformShakeOptions.useResetTransformYRotation == true)
            {
                resetRotation.y = transformShakeOptions.originalTransformRotation.y;
            }

            if (transformShakeOptions.useResetTransformZRotation == true)
            {
                resetRotation.z = transformShakeOptions.originalTransformRotation.z;
            }

            return resetRotation;
        }

        private Vector3 GetTransformShakeOptionsOriginalTransformScale()
        {
            if (transformShakeOptions.transform == null)
            {
                return new Vector3(0, 0, 0);
            }

            Vector3 resetScale = transformShakeOptions.transform.localScale;

            if (transformShakeOptions.useResetTransformXScale == true)
            {
                resetScale.x = transformShakeOptions.originalTransformScale.x;
            }

            if (transformShakeOptions.useResetTransformYScale == true)
            {
                resetScale.y = transformShakeOptions.originalTransformScale.y;
            }

            if (transformShakeOptions.useResetTransformZScale == true)
            {
                resetScale.z = transformShakeOptions.originalTransformScale.z;
            }

            return resetScale;
        }

        private void ResetTransformShakeOptionsRectTransform(bool resetPosition, bool resetRotation, bool resetScale)
        {
            if (transformShakeOptions.rectTransform == null)
            {
                return;
            }

            if (resetPosition == true)
            {
                transformShakeOptions.rectTransform.anchoredPosition3D = GetTransformShakeOptionsOriginalRectTransformPosition();
            }

            if (resetRotation == true)
            {
                transformShakeOptions.rectTransform.eulerAngles = GetTransformShakeOptionsOriginalRectTransformRotation();
            }

            if (resetScale == true)
            {
                transformShakeOptions.rectTransform.localScale = GetTransformShakeOptionsOriginalRectTransformScale();
            }
        }

        private Vector3 GetTransformShakeOptionsOriginalRectTransformPosition()
        {
            if (transformShakeOptions.rectTransform == null)
            {
                return new Vector3(0, 0, 0);
            }

            Vector3 resetPosition = transformShakeOptions.rectTransform.anchoredPosition3D;

            if (transformShakeOptions.useResetTransformXPosition == true)
            {
                resetPosition.x = transformShakeOptions.originalRectTransformPosition.x;
            }

            if (transformShakeOptions.useResetTransformYPosition == true)
            {
                resetPosition.y = transformShakeOptions.originalRectTransformPosition.y;
            }

            if (transformShakeOptions.useResetTransformZPosition == true)
            {
                resetPosition.z = transformShakeOptions.originalRectTransformPosition.z;
            }

            return resetPosition;
        }

        private Vector3 GetTransformShakeOptionsOriginalRectTransformRotation()
        {
            if (transformShakeOptions.rectTransform == null)
            {
                return new Vector3(0, 0, 0);
            }

            Vector3 resetRotation = transformShakeOptions.rectTransform.eulerAngles;

            if (transformShakeOptions.useResetTransformXRotation == true)
            {
                resetRotation.x = transformShakeOptions.originalRectTransformRotation.x;
            }

            if (transformShakeOptions.useResetTransformYRotation == true)
            {
                resetRotation.y = transformShakeOptions.originalRectTransformRotation.y;
            }

            if (transformShakeOptions.useResetTransformZRotation == true)
            {
                resetRotation.z = transformShakeOptions.originalRectTransformRotation.z;
            }

            return resetRotation;
        }

        private Vector3 GetTransformShakeOptionsOriginalRectTransformScale()
        {
            if (transformShakeOptions.rectTransform == null)
            {
                return new Vector3(0, 0, 0);
            }

            Vector3 resetScale = transformShakeOptions.rectTransform.localScale;

            if (transformShakeOptions.useResetTransformXScale == true)
            {
                resetScale.x = transformShakeOptions.originalRectTransformScale.x;
            }

            if (transformShakeOptions.useResetTransformYScale == true)
            {
                resetScale.y = transformShakeOptions.originalRectTransformScale.y;
            }

            if (transformShakeOptions.useResetTransformZScale == true)
            {
                resetScale.z = transformShakeOptions.originalRectTransformScale.z;
            }

            return resetScale;
        }

        private void HandleTransformShakeVariablesFromTransformShakeScriptableObject(UFE2FTETransformShakeScriptableObject transformShakeScriptableObject, ControlsScript player)
        {
            if (transformShakeScriptableObject == null)
            {
                return;
            }

            if (this.player == Player.AllPlayers)
            {
                SetTransformShakeOptionsVariablesFromTransformShakeScriptableObject(transformShakeScriptableObject);

                return;
            }

            if (player == null)
            {
                return;
            }

            if (player.playerNum == 1)
            {
                if (transformShakeScriptableObject.usePlayer == true
                    && this.player == Player.Player1)
                {
                    SetTransformShakeOptionsVariablesFromTransformShakeScriptableObject(transformShakeScriptableObject);
                }

                if (transformShakeScriptableObject.usePlayerOpponent == true
                    && this.player == Player.Player2)
                {
                    SetTransformShakeOptionsVariablesFromTransformShakeScriptableObject(transformShakeScriptableObject);
                }
            }
            else if (player.playerNum == 2)
            {
                if (transformShakeScriptableObject.usePlayer == true
                    && this.player == Player.Player2)
                {
                    SetTransformShakeOptionsVariablesFromTransformShakeScriptableObject(transformShakeScriptableObject);
                }

                if (transformShakeScriptableObject.usePlayerOpponent == true
                    && this.player == Player.Player1)
                {
                    SetTransformShakeOptionsVariablesFromTransformShakeScriptableObject(transformShakeScriptableObject);
                }
            }
        }

        private void HandleTransformShakeVariablesFromTransformShakeScriptableObject(UFE2FTETransformShakeScriptableObject[] transformShakeScriptableObjectArray, ControlsScript player)
        {
            if (transformShakeScriptableObjectArray == null)
            {
                return;           
            }

            int length = transformShakeScriptableObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                HandleTransformShakeVariablesFromTransformShakeScriptableObject(transformShakeScriptableObjectArray[i], player);
            }
        }

        private void SetTransformShakeOptionsVariablesFromTransformShakeScriptableObject(UFE2FTETransformShakeScriptableObject transformShakeScriptableObject)
        {
            if (transformShakeScriptableObject == null)
            {
                return;
            }

            transformShakeOptions.transformShakeDuration = transformShakeScriptableObject.transformShakeDuration;

            transformShakeOptions.transformShakeDurationElapsedTime = 0;

            transformShakeOptions.useResetTransformXPosition = transformShakeScriptableObject.useResetTransformXPosition;

            transformShakeOptions.useResetTransformYPosition = transformShakeScriptableObject.useResetTransformYPosition;

            transformShakeOptions.useResetTransformZPosition = transformShakeScriptableObject.useResetTransformZPosition;

            transformShakeOptions.transformShakePositionOffset = transformShakeScriptableObject.transformShakePositionOffset;

            transformShakeOptions.useTransformShakePositionFadeOutAnimationCurve = transformShakeScriptableObject.useTransformShakePositionFadeOutAnimationCurve;

            transformShakeOptions.transformShakePositionFadeOutAnimationCurve = transformShakeScriptableObject.transformShakePositionFadeOutAnimationCurve;

            transformShakeOptions.useResetTransformXRotation = transformShakeScriptableObject.useResetTransformXRotation;

            transformShakeOptions.useResetTransformYRotation = transformShakeScriptableObject.useResetTransformYRotation;

            transformShakeOptions.useResetTransformZRotation = transformShakeScriptableObject.useResetTransformZRotation;

            transformShakeOptions.transformShakeRotationOffset = transformShakeScriptableObject.transformShakeRotationOffset;

            transformShakeOptions.useTransformShakeRotationFadeOutAnimationCurve = transformShakeScriptableObject.useTransformShakeRotationFadeOutAnimationCurve;

            transformShakeOptions.transformShakeRotationFadeOutAnimationCurve = transformShakeScriptableObject.transformShakeRotationFadeOutAnimationCurve;

            transformShakeOptions.useResetTransformXScale = transformShakeScriptableObject.useResetTransformXScale;

            transformShakeOptions.useResetTransformYScale = transformShakeScriptableObject.useResetTransformYScale;

            transformShakeOptions.useResetTransformZScale = transformShakeScriptableObject.useResetTransformZScale;

            transformShakeOptions.transformShakeScaleOffset = transformShakeScriptableObject.transformShakeScaleOffset;

            transformShakeOptions.useTransformShakeScaleFadeOutAnimationCurve = transformShakeScriptableObject.useTransformShakeScaleFadeOutAnimationCurve;

            transformShakeOptions.transformShakeScaleFadeOutAnimationCurve = transformShakeScriptableObject.transformShakeScaleFadeOutAnimationCurve;
        }

        private void SetTransformShakeOptions(float deltaTime)
        {
            if (isCameraShakeController == true
                && UFE2FTECameraShakeOptionsManager.useCameraShake == false)
            {
                transformShakeOptions.transformShakeDurationElapsedTime = transformShakeOptions.transformShakeDuration;

                return;
            }
 
            if (UFE.isPaused() == true)
            {
                return;
            }

            if (transformShakeOptions.transformShakeDurationElapsedTime < transformShakeOptions.transformShakeDuration)
            {
                transformShakeOptions.transformShakeDurationElapsedTime += deltaTime;

                float transformShakeDurationPercentageCompleted = transformShakeOptions.transformShakeDurationElapsedTime / transformShakeOptions.transformShakeDuration;

                Vector3 shakePositionOffset = new Vector3(
                    shakePositionOffset.x = UFE2FTEHelperMethodsManager.RandomWithTwoPossibleOutcomes(-1, 1) * transformShakeOptions.transformShakePositionOffset.x,
                    shakePositionOffset.y = UFE2FTEHelperMethodsManager.RandomWithTwoPossibleOutcomes(-1, 1) * transformShakeOptions.transformShakePositionOffset.y,
                    shakePositionOffset.z = UFE2FTEHelperMethodsManager.RandomWithTwoPossibleOutcomes(-1, 1) * transformShakeOptions.transformShakePositionOffset.z);

                if (transformShakeOptions.useTransformShakePositionFadeOutAnimationCurve == true)
                {
                    shakePositionOffset = Vector3.Lerp(shakePositionOffset, new Vector3(0, 0, 0), transformShakeOptions.transformShakePositionFadeOutAnimationCurve.Evaluate(transformShakeDurationPercentageCompleted));
                }

                Vector3 shakeRotationOffset = new Vector3(
                    shakeRotationOffset.x = UFE2FTEHelperMethodsManager.RandomWithTwoPossibleOutcomes(-1, 1) * transformShakeOptions.transformShakeRotationOffset.x,
                    shakeRotationOffset.y = UFE2FTEHelperMethodsManager.RandomWithTwoPossibleOutcomes(-1, 1) * transformShakeOptions.transformShakeRotationOffset.y,
                    shakeRotationOffset.z = UFE2FTEHelperMethodsManager.RandomWithTwoPossibleOutcomes(-1, 1) * transformShakeOptions.transformShakeRotationOffset.z);

                if (transformShakeOptions.useTransformShakeRotationFadeOutAnimationCurve == true)
                {
                    shakeRotationOffset = Vector3.Lerp(shakeRotationOffset, new Vector3(0, 0, 0), transformShakeOptions.transformShakeRotationFadeOutAnimationCurve.Evaluate(transformShakeDurationPercentageCompleted));
                }

                Vector3 shakeScaleOffset = new Vector3(
                    shakeScaleOffset.x = UFE2FTEHelperMethodsManager.RandomWithTwoPossibleOutcomes(-1, 1) * transformShakeOptions.transformShakeScaleOffset.x,
                    shakeScaleOffset.y = UFE2FTEHelperMethodsManager.RandomWithTwoPossibleOutcomes(-1, 1) * transformShakeOptions.transformShakeScaleOffset.y,
                    shakeScaleOffset.z = UFE2FTEHelperMethodsManager.RandomWithTwoPossibleOutcomes(-1, 1) * transformShakeOptions.transformShakeScaleOffset.z);

                if (transformShakeOptions.useTransformShakeScaleFadeOutAnimationCurve == true)
                {
                    shakeScaleOffset = Vector3.Lerp(shakeScaleOffset, new Vector3(0, 0, 0), transformShakeOptions.transformShakeScaleFadeOutAnimationCurve.Evaluate(transformShakeDurationPercentageCompleted));
                }

                if (transformShakeOptions.transform != null)
                {
                    ResetTransformShakeOptionsTransform(true, true, true);

                    transformShakeOptions.transform.position += shakePositionOffset;

                    transformShakeOptions.transform.eulerAngles += shakeRotationOffset;

                    transformShakeOptions.transform.localScale += shakeScaleOffset;
                }

                if (transformShakeOptions.rectTransform != null)
                {
                    ResetTransformShakeOptionsRectTransform(true, true, true);

                    transformShakeOptions.rectTransform.position += shakePositionOffset;

                    transformShakeOptions.rectTransform.eulerAngles += shakeRotationOffset;

                    transformShakeOptions.rectTransform.localScale += shakeScaleOffset;
                }
            }
            else
            {
                ResetTransformShakeOptionsTransform(true, true, true);

                ResetTransformShakeOptionsRectTransform(true, true, true);
            }
        }

        #endregion

        #region Transform Shake Scriptable Object Options Methods

        private void SetTransformShakeScriptableObjectOptions(bool useOnEnable = false, bool useOnStart = false, bool useOnDisable = false, bool useOnDestroy = false)
        {
            int length = transformShakeScriptableObjectOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (transformShakeScriptableObjectOptionsArray[i].useOnEnable == true
                    && useOnEnable == true
                    || (transformShakeScriptableObjectOptionsArray[i].useOnStart == true
                    && useOnStart == true)
                    || (transformShakeScriptableObjectOptionsArray[i].useOnDisable == true
                    && useOnDisable == true)
                    || (transformShakeScriptableObjectOptionsArray[i].useOnDestroy == true
                    && useOnDestroy == true))
                {
                    UFE2FTETransformShakeEventsManager.CallOnTransformShake(null, transformShakeScriptableObjectOptionsArray[i].transformShakeScriptableObjectArray, myControlsScript);
                }
            }
        }

        #endregion
    }
}
