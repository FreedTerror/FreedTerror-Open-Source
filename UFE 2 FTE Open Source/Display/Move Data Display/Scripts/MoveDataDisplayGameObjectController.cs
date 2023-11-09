using UnityEngine;

namespace UFE2FTE
{
    public class MoveDataDisplayGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] moveDataDisplayGameObjectArray;

        private void Update()
        {
            UFE2FTE.SetGameObjectActive(moveDataDisplayGameObjectArray, UFE2FTE.Instance.displayMoveData);
        }
    }
}