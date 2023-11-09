using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class ImageFillAmountTextController : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private Text text;
        [SerializeField]
        private bool usePercentNumbers;

        private void Update()
        {
            if (image == null
                || text == null)
            {
                return;
            }

            if (usePercentNumbers == true)
            {
                text.text = UFE2FTE.languageOptions.GetNormalPercentNumber(Mathf.FloorToInt((float)(image.fillAmount / 1 * 100)));
            }
            else
            {
                text.text = UFE2FTE.languageOptions.GetNormalNumber(Mathf.FloorToInt((float)(image.fillAmount / 1 * 100)));
            }
        }
    }
}