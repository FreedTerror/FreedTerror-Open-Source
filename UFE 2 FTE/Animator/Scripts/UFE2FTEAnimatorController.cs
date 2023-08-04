using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTEAnimatorController : MonoBehaviour
    {
        private GameObject myGameObject;
        private Animator[] animatorArray;
        private GameObject[] animatorGameObjectArray;   
        [SerializeField]
        private bool disableGameObjectIfAnimatorIsntPlaying = true;

        private void Awake()
        {
            myGameObject = gameObject;
        }

        private void OnEnable()
        {
            SetGameObjectActive(animatorGameObjectArray, true);   
        }

        private void Start()
        {
            animatorArray = GetComponentsInChildren<Animator>();

            int length = animatorArray.Length;
            animatorGameObjectArray = new GameObject[length];
            for (int i = 0; i < length; i++)
            {
                animatorGameObjectArray[i] = animatorArray[i].gameObject;
            }
        }

        private void Update()
        {
            SetAnimator(animatorArray);

            if (disableGameObjectIfAnimatorIsntPlaying == true)
            {
                DisableGameObjectIfAnimatorIsntPlaying(animatorArray, animatorGameObjectArray, myGameObject);
            }
        }

        private static void SetAnimator(Animator animator)
        {
            if (animator == null)
            {
                return;
            }

            if (UFE.isPaused() == true)
            {
                animator.enabled = false;
            }
            else
            {
                animator.enabled = true;

                animator.speed = (float)UFE.timeScale;
            }
        }

        private static void SetAnimator(Animator[] animatorArray)
        {
            if (animatorArray == null)
            {
                return;
            }

            int length = animatorArray.Length;
            for (int i = 0; i < length; i++)
            {
                SetAnimator(animatorArray[i]);
            }
        }

        private static void DisableGameObjectIfAnimatorIsntPlaying(Animator[] animatorArray, GameObject[] animatorGameObjectArray, GameObject gameObjectToDisable)
        {
            if (animatorArray == null
                || animatorGameObjectArray == null
                || gameObjectToDisable == null)
            {
                return;
            }

            int length = animatorArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (animatorArray[i] == null)
                {
                    continue;
                }

                if (animatorArray[i].GetCurrentAnimatorStateInfo(0).normalizedTime > 1
                    && animatorArray[i].IsInTransition(0) == false)
                {
                    SetGameObjectActive(animatorGameObjectArray[i], false);
                }
            }

            for (int i = 0; i < length; i++)
            {
                if (animatorGameObjectArray[i] == null)
                {
                    continue;
                }

                if (animatorGameObjectArray[i].activeInHierarchy == true)
                {
                    return;
                }
            }

            gameObjectToDisable.SetActive(false);
        }

        private static void SetGameObjectActive(GameObject gameObject, bool active)
        {
            if (gameObject == null)
            {
                return;
            }

            gameObject.SetActive(active);
        }

        private static void SetGameObjectActive(GameObject[] gameObjectArray, bool active)
        {
            if (gameObjectArray == null)
            {
                return;
            }

            int length = gameObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                SetGameObjectActive(gameObjectArray[i], active);
            }
        }
    }
}