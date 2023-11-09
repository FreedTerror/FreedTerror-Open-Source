using UFE3D;
using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class PingTextController : MonoBehaviour
    {
        [SerializeField]
        private Text pingText;

        private void Update()
        {
            if (UFE.gameMode != GameMode.NetworkGame)
            {
                UFE2FTE.SetTextMessage(pingText, UFE2FTE.languageOptions.GetNormalNumber(0));
            }
            else
            {
                UFE2FTE.SetTextMessage(pingText, UFE2FTE.languageOptions.GetNormalNumber(UFE2FTE.GetOnlinePing()));
            }
        }
    }
}
