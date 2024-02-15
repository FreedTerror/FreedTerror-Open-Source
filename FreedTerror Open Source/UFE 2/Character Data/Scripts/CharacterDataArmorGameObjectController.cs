using UnityEngine;

namespace FreedTerror.UFE2
{
    public class CharacterDataArmorGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private CharacterAlertController characterAlertController;
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private GameObject[] gameObjectArray;

        private void Update()
        {
            if (characterAlertController == null)
            {
                return;
            }

            UFE2Manager.SetGameObjectActive(gameObjectArray, characterAlertController.GetCharacterData(player).armor);
        }
    }
}