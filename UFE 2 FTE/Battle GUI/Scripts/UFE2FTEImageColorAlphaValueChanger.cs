using System;
using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEImageColorAlphaValueChanger : MonoBehaviour
    {
        [Serializable]
        private class ImageColorAlphaValueChangerOptions
        {      
            [Range(0, 1)]
            public float minAlpha;
            [Range(0, 1)]
            public float maxAlpha;
            [HideInInspector]
            public float current;
            [HideInInspector]
            public float target;
            public enum CurrentTarget
            {
                Min,
                Max
            }
            [HideInInspector]
            public CurrentTarget currentTarget;
            public float alphaChangeSpeed;
            public Image[] imageArray;

            public static void SetImageColorAlphaValuesSharedOptions(ImageColorAlphaValueChangerOptions imageColorAlphaValueChangerOptions, float deltaTime)
            {
                if (imageColorAlphaValueChangerOptions == null)
                {
                    return;
                }

                imageColorAlphaValueChangerOptions.current = Mathf.MoveTowards(imageColorAlphaValueChangerOptions.current, imageColorAlphaValueChangerOptions.target, imageColorAlphaValueChangerOptions.alphaChangeSpeed * deltaTime);

                if (imageColorAlphaValueChangerOptions.currentTarget == CurrentTarget.Min)
                {
                    imageColorAlphaValueChangerOptions.target = imageColorAlphaValueChangerOptions.minAlpha;

                    if (imageColorAlphaValueChangerOptions.current == imageColorAlphaValueChangerOptions.target)
                    {
                        imageColorAlphaValueChangerOptions.currentTarget = CurrentTarget.Max;
                    }
                }
                else if (imageColorAlphaValueChangerOptions.currentTarget == CurrentTarget.Max)
                {
                    imageColorAlphaValueChangerOptions.target = imageColorAlphaValueChangerOptions.maxAlpha;

                    if (imageColorAlphaValueChangerOptions.current == imageColorAlphaValueChangerOptions.target)
                    {
                        imageColorAlphaValueChangerOptions.currentTarget = CurrentTarget.Min;
                    }
                }

                int length = imageColorAlphaValueChangerOptions.imageArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (imageColorAlphaValueChangerOptions.imageArray[i] == null)
                    {
                        continue;
                    }

                    imageColorAlphaValueChangerOptions.imageArray[i].color = new Color(imageColorAlphaValueChangerOptions.imageArray[i].color.r, imageColorAlphaValueChangerOptions.imageArray[i].color.g, imageColorAlphaValueChangerOptions.imageArray[i].color.b, imageColorAlphaValueChangerOptions.current);
                }
            }

            public static void SetImageColorAlphaValuesSharedOptions(ImageColorAlphaValueChangerOptions[] imageColorAlphaValueChangerOptionsArray, float deltaTime)
            {
                if (imageColorAlphaValueChangerOptionsArray == null)
                {
                    return;
                }

                int length = imageColorAlphaValueChangerOptionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    SetImageColorAlphaValuesSharedOptions(imageColorAlphaValueChangerOptionsArray[i], deltaTime);
                }
            }
        }
        [SerializeField]
        private ImageColorAlphaValueChangerOptions[] imageColorAlphaValueChangerOptionsArray;

        private void Update()
        {
            float deltaTime = (float)UFE.fixedDeltaTime;

            ImageColorAlphaValueChangerOptions.SetImageColorAlphaValuesSharedOptions(imageColorAlphaValueChangerOptionsArray, deltaTime);
        }
    }
}