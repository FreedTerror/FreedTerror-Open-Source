using System;
using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEImageFillAmountController : MonoBehaviour
    {
        [Serializable]
        private class ImageFillAmountRangeOptions
        {
            [Range(0, 1)]
            public float minImageFillAmount;
            [Range(0, 1)]
            public float maxImageFillAmount;          
            public bool useImageColor;
            public Color32 imageColor = new Color32(255, 255, 255, 255); 
            public Text[] textArray;
            public string textMessage;
            public bool useTextColor;
            public Color32 textColor = new Color32(255, 255, 255, 255);
            public GameObject[] disabledGameObjectArray;
            public GameObject[] enabledGameObjectArray;

            public static bool IsInImageFillAmountRange(Image image, ImageFillAmountRangeOptions imageFillAmountRangeOptions)
            {
                if (image == null
                    || imageFillAmountRangeOptions == null)
                {
                    return false;
                }

                if (image.fillAmount >= imageFillAmountRangeOptions.minImageFillAmount
                    && image.fillAmount <= imageFillAmountRangeOptions.maxImageFillAmount)
                {
                    return true;
                }

                return false;
            }

            public static void SetImageFillAmountRangeOptions(ImageFillAmountOptions imageFillAmountOptions, ImageFillAmountRangeOptions imageFillAmountRangeOptions)
            {
                if (imageFillAmountOptions == null
                    || imageFillAmountOptions.image == null
                    || imageFillAmountRangeOptions == null)
                {
                    return;
                }

                if (imageFillAmountRangeOptions.useImageColor == true)
                {
                    imageFillAmountOptions.image.color = imageFillAmountRangeOptions.imageColor;
                }

                int length = imageFillAmountRangeOptions.textArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (imageFillAmountRangeOptions.textArray[i] == null
                        || IsInImageFillAmountRange(imageFillAmountOptions.image, imageFillAmountRangeOptions) == false)
                    {
                        continue;
                    }

                    imageFillAmountRangeOptions.textArray[i].text = imageFillAmountRangeOptions.textMessage;

                    if (imageFillAmountRangeOptions.useTextColor == true)
                    {
                        imageFillAmountRangeOptions.textArray[i].color = imageFillAmountRangeOptions.textColor;
                    }                   
                }

                length = imageFillAmountRangeOptions.disabledGameObjectArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (imageFillAmountRangeOptions.disabledGameObjectArray[i] == null
                        || IsInImageFillAmountRange(imageFillAmountOptions.image, imageFillAmountRangeOptions) == false)
                    {
                        continue;
                    }

                    imageFillAmountRangeOptions.disabledGameObjectArray[i].SetActive(false);
                }

                length = imageFillAmountRangeOptions.enabledGameObjectArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (imageFillAmountRangeOptions.enabledGameObjectArray[i] == null
                        || IsInImageFillAmountRange(imageFillAmountOptions.image, imageFillAmountRangeOptions) == false)
                    {
                        continue;
                    }

                    imageFillAmountRangeOptions.enabledGameObjectArray[i].SetActive(true);
                }
            }

            public static void SetImageFillAmountRangeOptions(ImageFillAmountOptions imageFillAmountOptions, ImageFillAmountRangeOptions[] imageFillAmountRangeOptionsArray)
            {
                if (imageFillAmountRangeOptionsArray == null)
                {
                    return;
                }

                int length = imageFillAmountRangeOptionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    SetImageFillAmountRangeOptions(imageFillAmountOptions, imageFillAmountRangeOptionsArray[i]);
                }
            }
        }

        [Serializable]
        private class ImageFillAmountOptions
        {
            public Image image;
            public ImageFillAmountRangeOptions[] imageFillAmountRangeOptionsArray;

            public static void SetImageFillAmountOptions(ImageFillAmountOptions imageFillAmountOptions)
            {
                if (imageFillAmountOptions == null)
                {
                    return;
                }

                ImageFillAmountRangeOptions.SetImageFillAmountRangeOptions(imageFillAmountOptions, imageFillAmountOptions.imageFillAmountRangeOptionsArray);
            }

            public static void SetImageFillAmountOptions(ImageFillAmountOptions[] imageFillAmountOptionsArray)
            {
                if (imageFillAmountOptionsArray == null)
                {
                    return;
                }

                int length = imageFillAmountOptionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    SetImageFillAmountOptions(imageFillAmountOptionsArray[i]);
                }
            }
        }
        [SerializeField]
        private ImageFillAmountOptions[] imageFillAmountOptionsArray;

        private void Update()
        {
            ImageFillAmountOptions.SetImageFillAmountOptions(imageFillAmountOptionsArray);
        }
    }
}