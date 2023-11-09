using UnityEngine;

namespace UFE2FTE
{
    public class CharacterDataArmorGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private CharacterAlertController characterAlertController;
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private GameObject[] gameObjectArray;

        private void Update()
        {
            if (characterAlertController == null)
            {
                return;
            }

            UFE2FTE.SetGameObjectActive(gameObjectArray, characterAlertController.GetCharacterData(player).armor);
        }
    }
}