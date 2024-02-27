using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class ImageFillAmountPercentTextController : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private Text text;

        private void Update()
        {
            if (image != null
                && text != null)
            {
                text.text = UFE2Manager.instance.cachedStringData.GetPositivePercentStringNumber(Mathf.FloorToInt((float)(image.fillAmount / 1 * 100)));
            }
        }
    }
}