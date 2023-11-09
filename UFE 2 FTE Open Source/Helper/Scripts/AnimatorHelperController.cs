using UnityEngine;

namespace UFE2FTE
{
    public class AnimatorHelperController : MonoBehaviour
    {
        private GameObject myGameObject;
        private Animator[] animatorArray;
        private GameObject[] animatorGameObjectArray;   
        [SerializeField]
        private bool disableGameObjectIfAnimatorIsntPlaying;

        private void Awake()
        {
            myGameObject = gameObject;
        }

        private void OnEnable()
        {
            UFE2FTE.SetGameObjectActive(animatorGameObjectArray, true);   
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

        private static void SetAnimator(Animator[] animator)
        {
            if (animator == null)
            {
                return;
            }

            int length = animator.Length;
            for (int i = 0; i < length; i++)
            {
                SetAnimator(animator[i]);
            }
        }

        private static void DisableGameObjectIfAnimatorIsntPlaying(Animator[] animator, GameObject[] animatorGameObjectArray, GameObject gameObjectToDisable)
        {
            if (animator == null
                || animatorGameObjectArray == null
                || gameObjectToDisable == null)
            {
                return;
            }

            int length = animator.Length;
            for (int i = 0; i < length; i++)
            {
                if (animator[i] == null)
                {
                    continue;
                }

                if (animator[i].GetCurrentAnimatorStateInfo(0).normalizedTime > 1
                    && animator[i].IsInTransition(0) == false)
                {
                    UFE2FTE.SetGameObjectActive(animatorGameObjectArray[i], false);
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
    }
}