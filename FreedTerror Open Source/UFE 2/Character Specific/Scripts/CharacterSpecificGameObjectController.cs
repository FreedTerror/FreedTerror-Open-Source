using UnityEngine;

namespace FreedTerror.UFE2
{
    public class CharacterSpecificGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private UFE3D.CharacterInfo characterInfo;
        [SerializeField]
        private GameObject[] gameObjectArray;

        private void Update()
        {
            if (characterInfo == null
                || UFE2Manager.GetCharacterInfo(player) == null
                || characterInfo.characterName != UFE2Manager.GetCharacterInfo(player).characterName)
            {
                Utility.SetGameObjectActive(gameObjectArray, false);
            }
            else if (characterInfo.characterName == UFE2Manager.GetCharacterInfo(player).characterName)
            {
                Utility.SetGameObjectActive(gameObjectArray, true);
            }
        }
    }
}