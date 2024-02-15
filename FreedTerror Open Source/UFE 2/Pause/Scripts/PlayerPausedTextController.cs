using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class PlayerPausedTextController : MonoBehaviour
    {
        [SerializeField]
        private Text playerPausedText;
        private UFE2Manager.Player previousPausedPlayer;

        private void Start()
        {
            previousPausedPlayer = UFE2Manager.instance.pausedPlayer;

            if (playerPausedText != null)
            {
                playerPausedText.text = Utility.AddSpacesBeforeNumbers(System.Enum.GetName(typeof(UFE2Manager.Player), UFE2Manager.instance.pausedPlayer) + " Paused");
            }
        }

        private void Update()
        {
            if (previousPausedPlayer != UFE2Manager.instance.pausedPlayer)
            {
                previousPausedPlayer = UFE2Manager.instance.pausedPlayer;

                if (playerPausedText != null)
                {
                    playerPausedText.text = Utility.AddSpacesBeforeNumbers(System.Enum.GetName(typeof(UFE2Manager.Player), UFE2Manager.instance.pausedPlayer) + " Paused");
                }
            }
        }
    }
}