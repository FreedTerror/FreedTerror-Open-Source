using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class SliderTextUIController : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        private float previousSliderValue;
        [SerializeField]
        private Text sliderText;

        private void Start()
        {
            if (slider != null)
            {
                previousSliderValue = slider.value;
            }

            if (sliderText != null)
            {
                sliderText.text = previousSliderValue.ToString();
            }
        }

        private void LateUpdate()
        {
            if (slider != null
                && previousSliderValue != slider.value
                && sliderText != null)
            {
                previousSliderValue = slider.value;
                sliderText.text = slider.value.ToString();
            }
        }
    }
}