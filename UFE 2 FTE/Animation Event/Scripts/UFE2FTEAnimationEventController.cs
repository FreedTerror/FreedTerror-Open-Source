using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTEAnimationEventController : MonoBehaviour
    {
        private Transform myTransform;
        private ControlsScript myControlsScript;

        private void Awake()
        {
            myTransform = transform;
        }

        private void Start()
        {
            myControlsScript = GetComponentInParent<ControlsScript>();
        }

        public void AnimationEventScriptableObject(UFE2FTEAnimationEventScriptableObject animationEventScriptableObject)
        {
            if (animationEventScriptableObject == null)
            {
                return;
            }

            animationEventScriptableObject.AnimationEventScriptableObject(myTransform, myControlsScript);
        }
    }
}