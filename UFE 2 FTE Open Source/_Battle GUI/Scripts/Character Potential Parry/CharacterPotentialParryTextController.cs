using FPLibrary;
using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CharacterPotentialParryTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text potentialParryText;
        [SerializeField]
        private bool useFrameNumbers;

        private void Update()
        {
            if (useFrameNumbers == true)
            {
                UFE2FTE.SetTextMessage(potentialParryText, UFE2FTE.languageOptions.GetNormalFrameNumber((int)Fix64.Ceiling(UFE2FTE.GetControlsScript(player).potentialParry * UFE.config.fps)));
            }
            else
            {
                UFE2FTE.SetTextMessage(potentialParryText, UFE2FTE.languageOptions.GetNormalNumber((int)Fix64.Ceiling(UFE2FTE.GetControlsScript(player).potentialParry * UFE.config.fps)));
            }
        }
    }
}