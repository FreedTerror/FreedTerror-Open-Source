using UnityEngine;

namespace FreedTerror.UFE2
{
    public class CharacterAlertArmorGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private CharacterAlertController characterAlertController;
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private GameObject[] gameObjectArray;

        private void Update()
        {
            if (characterAlertController != null)
            {
                Utility.SetGameObjectActive(gameObjectArray, characterAlertController.GetCharacterData(player).armor);
            }
        }
    }
}