using UnityEngine;

namespace FreedTerror.UFE2
{   
    public class FrameAdvantageDisplayGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] frameAdvantageDisplayGameObjectArray;

        private void Update()
        {
            UFE2Manager.SetGameObjectActive(frameAdvantageDisplayGameObjectArray, UFE2Manager.instance.displayFrameAdvantage);
        }
    }
}