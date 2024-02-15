using UnityEngine;

namespace FreedTerror
{
    public class RectTransformController : MonoBehaviour
    {
        [System.Serializable]
        private class Vector2Options
        {
            [SerializeField]
            private Vector2 vector2;
            [SerializeField]
            private bool useX;
            [SerializeField]
            private bool useY;

            public Vector2 GetVector2()
            {
                Vector2 vector = Vector2.zero;

                if (useX == true)
                {
                    vector.x = vector2.x;
                }

                if (useY == true)
                {
                    vector.y = vector2.y;
                }

                return vector;
            }

            public Vector2 GetVector2(Vector2 defaultVector)
            {
                Vector2 vector = defaultVector;

                if (useX == true)
                {
                    vector.x = vector2.x;
                }

                if (useY == true)
                {
                    vector.y = vector2.y;
                }

                return vector;
            }
        }

        [SerializeField]
        private RectTransform rectTransform;
        [Header("X = Left Y = Bottom")]
        [SerializeField]
        private Vector2Options offsetMinOptions;
        [Header("X = Right Y = Top")]
        [SerializeField]
        private Vector2Options offsetMaxOptions;
        [SerializeField]
        private Vector2Options sizeDeltaOptions;

        private void Update()
        {
            SetRectTransform(rectTransform);
        }

        private void SetRectTransform(RectTransform rectTransform)
        {
            if (rectTransform == null)
            {
                return;
            }

            rectTransform.offsetMin = offsetMinOptions.GetVector2(rectTransform.offsetMin);

            rectTransform.offsetMax = offsetMaxOptions.GetVector2(rectTransform.offsetMax);

            rectTransform.sizeDelta = sizeDeltaOptions.GetVector2(rectTransform.sizeDelta);
        }
    }
}