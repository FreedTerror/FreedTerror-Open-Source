using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CharacterAirJuggleHitsTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text airJuggleHitsText;

        private void Update()
        {
            SetAirJuggleHitsText(UFE2FTE.GetControlsScript(player));   
        }

        private void SetAirJuggleHitsText(ControlsScript player)
        {
            if (player == null
                || player.opControlsScript == null)
            {
                return;
            }

            UFE2FTE.SetTextMessage(airJuggleHitsText, UFE2FTE.languageOptions.GetNormalNumber(player.opControlsScript.airJuggleHits));
        }
    }
}