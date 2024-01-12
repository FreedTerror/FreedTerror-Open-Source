using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CharacterPortraitShakeUIController : MonoBehaviour
    {
        [SerializeField]
        private Text characterPortraitShakeText;

        private void Update()
        {
            UFE2FTE.SetTextMessage(characterPortraitShakeText, UFE2FTE.GetStringFromBool(UFE2FTE.instance.useCharacterPortraitShake));
        }

        public void ToggleCharacterPortraitShake()
        {
            UFE2FTE.instance.useCharacterPortraitShake = UFE2FTE.ToggleBool(UFE2FTE.instance.useCharacterPortraitShake);
        }
    }
}