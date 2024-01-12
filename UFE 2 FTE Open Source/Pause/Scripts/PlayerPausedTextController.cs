using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class PlayerPausedTextController : MonoBehaviour
    {
        [SerializeField]
        private Text playerPausedTextController;

        private void Update()
        {
            UFE2FTE.SetTextMessage(playerPausedTextController, UFE2FTE.GetStringFromEnum(UFE2FTE.instance.pausedPlayer));
        }
    }
}