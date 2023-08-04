using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class SliderText : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        private float oldSliderValue;

        [SerializeField]
        private Text sliderText;

        private void Start()
        {
            InitializeSliderText();
        }

        private void Update()
        {
            SetSliderText();
        }

        private void InitializeSliderText()
        {
            if (slider == null
                || sliderText == null)
            {
                return;
            }

            oldSliderValue = slider.value;

            sliderText.text = Mathf.RoundToInt(slider.value * 100).ToString();
        }

        private void SetSliderText()
        {
            if (slider == null
                || sliderText == null)
            {
                return;
            }

            if (oldSliderValue != slider.value)
            {
                oldSliderValue = slider.value;

                sliderText.text = Mathf.RoundToInt(slider.value * 100).ToString();
            }
        }
    }
}
