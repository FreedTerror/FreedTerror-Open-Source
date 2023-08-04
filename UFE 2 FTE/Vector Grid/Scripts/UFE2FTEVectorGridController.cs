using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTEVectorGridController : MonoBehaviour
    {
        public VectorGrid[] vectorGrids;

        private void OnEnable()
        {
            UFE2FTEVectorGridManager.AddVectorGridToVectorGridsList(vectorGrids);
        }

        private void OnDisable()
        {

        }

        private void OnDestroy()
        {
            UFE2FTEVectorGridManager.RemoveVectorGridFromVectorGridsList(vectorGrids);
        }
    }
}
