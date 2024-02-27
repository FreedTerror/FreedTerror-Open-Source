using UnityEngine;

namespace FreedTerror.UFE2
{
    public class HitDataDisplayGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] hitDataDisplayGameObjectArray;

        private void Update()
        {
            Utility.SetGameObjectActive(hitDataDisplayGameObjectArray, UFE2Manager.instance.displayHitData);
        }
    }
}