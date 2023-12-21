using UFE3D;
using UnityEngine;

namespace UFE2FTE
{
    public class CharacterPortraitShakeController : MonoBehaviour
    {
        public delegate void CharacterPortraitShakeHandler(float shakeDuration, Vector3 shakePower, ControlsScript player);
        public static event CharacterPortraitShakeHandler OnCharacterPortraitShakeEvent;

        public static void CallOnCharacterPortraitShakeEvent(float shakeDuration, Vector3 shakePower, ControlsScript player)
        {
            if (OnCharacterPortraitShakeEvent == null)
            {
                return;
            }

            OnCharacterPortraitShakeEvent(shakeDuration, shakePower, player);
        }

        [SerializeField]
        private RectTransform myRectTransform;
        private Vector3 originalRectTransformPosition;
        [SerializeField]
        private bool resetTransformBeforeEachShake;
        [SerializeField]
        private bool resetTransformAfterShake;
        private float shakeDuration;
        private Vector3 shakePower;

        [SerializeField]
        private UFE2FTE.Player player;

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
            OnCharacterPortraitShakeEvent += OnCharacterPortraitShake;
            UFE.OnBasicMove += OnBasicMove;
            UFE.OnMove += OnMove;
            UFE.OnHit += OnHit;
        }

        private void Start()
        {
            if (myRectTransform != null)
            {
                originalRectTransformPosition = myRectTransform.anchoredPosition3D;
            }
        }

        private void OnDisable()
        {
            OnCharacterPortraitShakeEvent -= OnCharacterPortraitShake;
            UFE.OnBasicMove -= OnBasicMove;
            UFE.OnMove -= OnMove;
            UFE.OnHit -= OnHit;
        }

        private void LateUpdate()
        {
            if (UFE.IsPaused() == true
                || UFE2FTE.Instance.useCharacterPortraitShake == false)
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

            if (myRectTransform != null)
            {
                if (resetTransformBeforeEachShake == true)
                {
                    myRectTransform.anchoredPosition3D = originalRectTransformPosition;
                }
 
                myRectTransform.position += new Vector3(randomX, randomY, randomZ);
            }

            shakeDuration -= deltaTime;
            if (shakeDuration <= 0)
            {
                shakeDuration = 0;

                if (myRectTransform != null)
                {
                    if (resetTransformAfterShake == true)
                    {
                        myRectTransform.anchoredPosition3D = originalRectTransformPosition;
                    }
                }
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

        private void OnCharacterPortraitShake(float shakeDuration, Vector3 shakePower, ControlsScript player)
        {
            if (player != UFE2FTE.GetControlsScript(this.player))
            {
                return;
            }

            this.shakeDuration = shakeDuration;
            this.shakePower = shakePower;
        }

        private void OnBasicMove(BasicMoveReference basicMove, ControlsScript player)
        {
            if (player != UFE2FTE.GetControlsScript(this.player))
            {
                return;
            }

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
            if (move == null
                || player != UFE2FTE.GetControlsScript(this.player))
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
            if (player == UFE2FTE.GetControlsScript(this.player))
            {
                return;
            }

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