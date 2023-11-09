using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CharacterPotentialParryImageController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Image potentialParryImage;

        private void OnEnable()
        {
            if (potentialParryImage == null
                || UFE.config == null
                || UFE2FTE.GetControlsScript(player) == null)
            {
                return;
            }

            potentialParryImage.fillAmount = (float)UFE2FTE.GetControlsScript(player).potentialParry / (float)UFE.config.blockOptions._parryTiming;
        }

        private void Update()
        {
            if (potentialParryImage == null
                || UFE.config == null
                || UFE2FTE.GetControlsScript(player) == null)
            {
                return;
            }

            potentialParryImage.fillAmount = (float)UFE2FTE.GetControlsScript(player).potentialParry / (float)UFE.config.blockOptions._parryTiming;
        }
    }
}
