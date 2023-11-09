using System.Collections.Generic;
using UnityEngine;

namespace UFE2FTE
{
    public class LineRendererTest : MonoBehaviour
    {
        private Transform myTransform;

        [SerializeField]
        private LineRenderer lineRenderer;
        [SerializeField]
        private float lineWidth = 0.1f;
        [SerializeField]
        private Color32 lineColor;
        [SerializeField]
        private List<Vector3> positionList = new List<Vector3>();
        private bool drawLines;

        private void Awake()
        {
            myTransform = transform;

            if (lineRenderer == null)
            {
                lineRenderer = gameObject.AddComponent<LineRenderer>();
                if (lineRenderer != null)
                {
                    //lineRenderer.materia
                }
            }
        }

        private void LateUpdate()
        {
            if (lineRenderer == null)
            {
                return;
            }

            positionList.Add(myTransform.position);

            int count = positionList.Count;
            lineRenderer.positionCount = count;
            for (int i = 0; i < count; i++)
            {
                lineRenderer.SetPosition(i, positionList[i]);
            }

            if (count > 500)
            {
                positionList.Clear();
            }

            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;

            lineRenderer.startColor = lineColor;
            lineRenderer.endColor = lineColor;
        }
    }
}