using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CharacterComboHitsTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Text comboHitsText;

        private void Update()
        {
            if (comboHitsText != null)
            {
                comboHitsText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(UFE2Manager.GetControlsScript(player).opControlsScript.comboHits);
            }
        }

        private void OnDisable()
        {
            if (comboHitsText != null)
            {
                comboHitsText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(0);
            }
        }
    }
}