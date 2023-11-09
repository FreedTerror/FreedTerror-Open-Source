using UnityEngine;

namespace UFE2FTE
{
    public class HitDataDisplayGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] hitDataDisplayGameObjectArray;

        private void Update()
        {
            UFE2FTE.SetGameObjectActive(hitDataDisplayGameObjectArray, UFE2FTE.Instance.displayHitData);
        }
    }
}