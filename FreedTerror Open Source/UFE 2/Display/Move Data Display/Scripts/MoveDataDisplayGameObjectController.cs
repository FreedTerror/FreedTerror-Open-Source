using UnityEngine;

namespace FreedTerror.UFE2
{
    public class MoveDataDisplayGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] moveDataDisplayGameObjectArray;

        private void Update()
        {
            UFE2Manager.SetGameObjectActive(moveDataDisplayGameObjectArray, UFE2Manager.instance.displayMoveData);
        }
    }
}