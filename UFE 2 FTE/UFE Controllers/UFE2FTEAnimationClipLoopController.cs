using System;
using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTEAnimationClipLoopController : MonoBehaviour
    {
        private ControlsScript myControlsScript;

        [Serializable]
        private class AnimationClipLoopOptions
        {
            public bool useAnimationClipLoopName;
            public string animationClipLoopName;
            public bool useAnimationClipLoopSprite;
            public Sprite animationClipLoopSprite;
            public float animationClipLoopStartTime;
        }
        [SerializeField]
        private AnimationClipLoopOptions[] animationClipLoopOptionsArray;

        private void Start()
        {
            myControlsScript = GetComponentInParent<ControlsScript>();
        }

        private void Update()
        {
            SetAnimationClipLoopOptions();
        }

        private void SetAnimationClipLoopOptions()
        {
            if (myControlsScript == null)
            {
                return;
            }

            int length = animationClipLoopOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (animationClipLoopOptionsArray[i].useAnimationClipLoopName == true
                    && myControlsScript.MoveSet.GetCurrentClipName() == animationClipLoopOptionsArray[i].animationClipLoopName)
                {
                    myControlsScript.MoveSet.PlayAnimation(animationClipLoopOptionsArray[i].animationClipLoopName, 0, animationClipLoopOptionsArray[i].animationClipLoopStartTime);
                }

                if (animationClipLoopOptionsArray[i].useAnimationClipLoopSprite == true
                    && animationClipLoopOptionsArray[i].animationClipLoopSprite != null
                    && myControlsScript.mySpriteRenderer != null
                    && myControlsScript.mySpriteRenderer.sprite == animationClipLoopOptionsArray[i].animationClipLoopSprite)
                {
                    myControlsScript.MoveSet.PlayAnimation(animationClipLoopOptionsArray[i].animationClipLoopName, 0, animationClipLoopOptionsArray[i].animationClipLoopStartTime);
                }
            }
        }
    }
}
