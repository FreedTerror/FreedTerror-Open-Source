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
            if (UFE.gameMode != GameMode.NetworkGame)
            {
                UFE2Manager.SetTextMessage(pingText, UFE2Manager.GetNormalStringNumber(0));
            }
            else
            {
                UFE2Manager.SetTextMessage(pingText, UFE2Manager.GetNormalStringNumber(UFE2Manager.GetOnlinePing()));
            }
        }
    }
}
