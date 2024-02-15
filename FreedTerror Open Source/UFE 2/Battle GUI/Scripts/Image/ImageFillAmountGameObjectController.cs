using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class ImageFillAmountGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        [System.Serializable]
        private class ImageFillAmountOptions
        {
            [Range(0, 1)]
            public float minFillAmount;
            [Range(0, 1)]
            public float maxFillAmount;
            public GameObject[] gameObjectArray;
        }
        [SerializeField]
        private ImageFillAmountOptions[] imageFillAmountOptionsArray;

        private void Update()
        {
            if (image == null)
            {
                return;
            }

            int length = imageFillAmountOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                UFE2Manager.SetGameObjectActive(imageFillAmountOptionsArray[i].gameObjectArray, Utility.IsInMinMaxRange(image.fillAmount, imageFillAmountOptionsArray[i].minFillAmount, imageFillAmountOptionsArray[i].maxFillAmount));
            }
        }
    }
}