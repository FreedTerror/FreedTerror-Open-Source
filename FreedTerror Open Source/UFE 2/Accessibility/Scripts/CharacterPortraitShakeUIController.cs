using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CharacterPortraitShakeUIController : MonoBehaviour
    {
        [SerializeField]
        private Text characterPortraitShakeText;
        private bool previousCharacterPortraitShake;

        private void Start()
        {
            previousCharacterPortraitShake = UFE2Manager.instance.useCharacterPortraitShake;

            if (characterPortraitShakeText != null)
            {
                characterPortraitShakeText.text = Utility.GetStringFromBool(UFE2Manager.instance.useCharacterPortraitShake);
            }
        }

        private void Update()
        {
            if (previousCharacterPortraitShake != UFE2Manager.instance.useCharacterPortraitShake)
            {
                previousCharacterPortraitShake = UFE2Manager.instance.useCharacterPortraitShake;

                if (characterPortraitShakeText != null)
                {
                    characterPortraitShakeText.text = Utility.GetStringFromBool(UFE2Manager.instance.useCharacterPortraitShake);
                }
            }
        }

        public void ToggleCharacterPortraitShake()
        {
            UFE2Manager.instance.useCharacterPortraitShake = !UFE2Manager.instance.useCharacterPortraitShake;
        }
    }
}