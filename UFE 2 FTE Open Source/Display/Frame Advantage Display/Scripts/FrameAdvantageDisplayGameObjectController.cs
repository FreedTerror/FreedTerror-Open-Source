using UnityEngine;

namespace UFE2FTE
{   
    public class FrameAdvantageDisplayGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] frameAdvantageDisplayGameObjectArray;

        private void Update()
        {
            UFE2FTE.SetGameObjectActive(frameAdvantageDisplayGameObjectArray, UFE2FTE.Instance.displayFrameAdvantage);
        }
    }
}