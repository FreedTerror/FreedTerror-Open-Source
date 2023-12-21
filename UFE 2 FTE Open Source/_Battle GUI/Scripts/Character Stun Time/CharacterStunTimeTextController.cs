using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CharacterStunTimeTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text stunTimeText;

        private void Update()
        {
            SetStunTimeText(UFE2FTE.GetControlsScript(player));
        }

        private void OnDisable()
        {
            UFE2FTE.SetTextMessage(stunTimeText, UFE2FTE.languageOptions.GetNormalFrameNumber(0));
        }

        private void SetStunTimeText(ControlsScript player)
        {
            if (player == null
                || player.isDead == true
                || player.opControlsScript == null       
                || player.opControlsScript.isDead == true
                || UFE.IsPaused())
            {
                return;
            }
            
            UFE2FTE.SetTextMessage(stunTimeText, UFE2FTE.languageOptions.GetNormalFrameNumber((int)(player.opControlsScript.stunTime * UFE.config.fps)));  
        }
    }
}