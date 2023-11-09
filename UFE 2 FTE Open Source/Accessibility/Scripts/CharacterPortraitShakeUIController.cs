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
            UFE2FTE.SetTextMessage(characterPortraitShakeText, UFE2FTE.GetStringFromBool(UFE2FTE.Instance.useCharacterPortraitShake));
        }

        public void ToggleCharacterPortraitShake()
        {
            UFE2FTE.Instance.useCharacterPortraitShake = UFE2FTE.ToggleBool(UFE2FTE.Instance.useCharacterPortraitShake);
        }
    }
}