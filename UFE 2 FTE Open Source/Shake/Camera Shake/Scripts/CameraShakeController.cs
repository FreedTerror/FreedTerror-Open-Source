using UFE3D;
using UnityEngine;

namespace UFE2FTE
{
    public class CameraShakeController : MonoBehaviour
    {
        public delegate void CameraShakeHandler(float shakeDuration, Vector3 shakePower);
        public static event CameraShakeHandler OnCameraShakeEvent;

        public static void CallOnCameraShakeEvent(float shakeDuration, Vector3 shakePower)
        {
            if (OnCameraShakeEvent == null)
            {
                return;
            }

            OnCameraShakeEvent(shakeDuration, shakePower);
        }

        [SerializeField]
        private Transform myTransform;
        private float shakeDuration;
        private Vector3 shakePower;

        [System.Serializable]
        private class OnBasicMoveOptions
        {
            public TransformShakeScriptableObject transformShakeScriptableObject;
            public BasicMoveReference[] basicMoveArray;
        }
        [SerializeField]
        private OnBasicMoveOptions[] onBasicMoveOptionsArray;

        [System.Serializable]
        private class OnMoveOptions
        {
            public TransformShakeScriptableObject transformShakeScriptableObject;
            public string[] moveNameArray;
        }
        [SerializeField]
        private OnMoveOptions[] onMoveOptionsArray;

        [System.Serializable]
        private class OnHitOptions
        {
            public TransformShakeScriptableObject transformShakeScriptableObject;
            public HitStrengh hitStrength;
        }
        [SerializeField]
        private OnHitOptions[] onHitOptionsArray;

        private void OnEnable()
        {
            OnCameraShakeEvent += OnCameraShake;
            UFE.OnBasicMove += OnBasicMove;
            UFE.OnMove += OnMove;
            UFE.OnHit += OnHit;
        }

        private void OnDisable()
        {
            OnCameraShakeEvent -= OnCameraShake;
            UFE.OnBasicMove -= OnBasicMove;
            UFE.OnMove -= OnMove;
            UFE.OnHit -= OnHit;
        }

        private void LateUpdate()
        {
            if (UFE.isPaused() == true
                || UFE2FTE.Instance.useCameraShake == false)
            {
                return;
            }

            float deltaTime = (float)UFE.fixedDeltaTime;

            ShakeTransform(deltaTime);
        }

        private void ShakeTransform(float deltaTime)
        {
            float randomX = Random.Range((float)-shakeDuration * shakePower.x, (float)shakeDuration * shakePower.x);
            float randomY = Random.Range((float)-shakeDuration * shakePower.y, (float)shakeDuration * shakePower.y);
            float randomZ = Random.Range((float)-shakeDuration * shakePower.z, (float)shakeDuration * shakePower.z);

            if (myTransform != null)
            {
                myTransform.position += new Vector3(randomX, randomY, randomZ);
            }

            shakeDuration -= deltaTime;
            if (shakeDuration <= 0)
            {
                shakeDuration = 0;
            }
        }

        private void SetShakeVariables(TransformShakeScriptableObject transformShakeScriptableObject)
        {
            if (transformShakeScriptableObject == null)
            {
                return;
            }

            shakeDuration = transformShakeScriptableObject.shakeDuration;
            shakePower = transformShakeScriptableObject.shakePower;
        }

        private void OnCameraShake(float shakeDuration, Vector3 shakePower)
        {
            this.shakeDuration = shakeDuration;
            this.shakePower = shakePower;
        }

        private void OnBasicMove(BasicMoveReference basicMove, ControlsScript player)
        {
            int length = onBasicMoveOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                int lengthA = onBasicMoveOptionsArray[i].basicMoveArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (basicMove != onBasicMoveOptionsArray[i].basicMoveArray[a])
                    {
                        continue;
                    }

                    SetShakeVariables(onBasicMoveOptionsArray[i].transformShakeScriptableObject);
                }
            }
        }

        private void OnMove(MoveInfo move, ControlsScript player)
        {
            if (move == null)
            {
                return;
            }

            int length = onMoveOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                int lengthA = onMoveOptionsArray[i].moveNameArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (move.moveName != onMoveOptionsArray[i].moveNameArray[a])
                    {
                        continue;
                    }

                    SetShakeVariables(onMoveOptionsArray[i].transformShakeScriptableObject);
                }
            }
        }

        private void OnHit(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            int length = onHitOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (hitInfo.hitStrength != onHitOptionsArray[i].hitStrength)
                {
                    continue;
                }

                SetShakeVariables(onHitOptionsArray[i].transformShakeScriptableObject);
            }
        }
    }
}