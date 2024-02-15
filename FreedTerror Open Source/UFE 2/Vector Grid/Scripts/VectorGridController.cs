using UnityEngine;

namespace FreedTerror.UFE2
{
    public class VectorGridController : MonoBehaviour
    {
        [SerializeField]
        private VectorGrid vectorGrid;

        private void OnEnable()
        {
            VectorGridManager.AddVectorGrid(vectorGrid);
        }

        private void Update()
        {
            if (vectorGrid == null)
            {
                return;
            }

            if (UFE.IsPaused() == true)
            {
                vectorGrid.enabled = false;
            }
            else
            {
                vectorGrid.enabled = true;
            }
        }

        private void OnDisable()
        {
            VectorGridManager.RemoveVectorGrid(vectorGrid);
        }
    }
}
