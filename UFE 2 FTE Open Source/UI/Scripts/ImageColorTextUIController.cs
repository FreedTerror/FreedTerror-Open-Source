using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class ImageColorTextUIController : MonoBehaviour
    {
        [SerializeField]
        private Image colorImage;
        private float previousRedColorValue;
        private float previousGreenColorValue;
        private float previousBlueColorValue;
        private float previousAlphaColorValue;

        [SerializeField]
        private Text redByteText;
        [SerializeField]
        private Text greenByteText;
        [SerializeField]
        private Text blueByteText;
        [SerializeField]
        private Text alphaByteText;

        private void Start()
        {
            if (colorImage == null)
            {
                return;
            }

            previousRedColorValue = colorImage.color.r;
            previousGreenColorValue = colorImage.color.g;
            previousBlueColorValue = colorImage.color.b;
            previousAlphaColorValue = colorImage.color.a;

            Color32 color32 = colorImage.color;
            if (redByteText != null)
            {
                redByteText.text = color32.r.ToString();
            }
            if (greenByteText != null)
            {
                greenByteText.text = color32.g.ToString();
            }
            if (blueByteText != null)
            {
                blueByteText.text = color32.b.ToString();
            }
            if (alphaByteText != null)
            {
                alphaByteText.text = color32.a.ToString();
            }
        }

        private void LateUpdate()
        {
            if (colorImage == null)
            {
                return;
            }

            if (previousRedColorValue != colorImage.color.r)
            {
                previousRedColorValue = colorImage.color.r;

                Color32 color32 = colorImage.color;
                if (redByteText != null)
                {
                    redByteText.text = color32.r.ToString();
                }
            }

            if (previousGreenColorValue != colorImage.color.g)
            {
                previousGreenColorValue = colorImage.color.g;

                Color32 color32 = colorImage.color;
                if (greenByteText != null)
                {
                    greenByteText.text = color32.g.ToString();
                }
            }

            if (previousBlueColorValue != colorImage.color.b)
            {
                previousBlueColorValue = colorImage.color.b;

                Color32 color32 = colorImage.color;
                if (blueByteText != null)
                {
                    blueByteText.text = color32.b.ToString();
                }
            }

            if (previousAlphaColorValue != colorImage.color.a)
            {
                previousAlphaColorValue = colorImage.color.a;

                Color32 color32 = colorImage.color;
                if (alphaByteText != null)
                {
                    alphaByteText.text = color32.a.ToString();
                }
            }
        }
    }
}
