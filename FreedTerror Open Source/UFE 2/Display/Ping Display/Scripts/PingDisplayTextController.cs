using UFE3D;
using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class PingTextController : MonoBehaviour
    {
        [SerializeField]
        private Text pingText;

        private void Update()
        {
            if (pingText != null)
            {
                pingText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(UFE2Manager.GetPing());
            }
        }
    }
}
