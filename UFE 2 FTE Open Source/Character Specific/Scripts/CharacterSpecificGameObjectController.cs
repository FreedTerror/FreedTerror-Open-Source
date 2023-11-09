using UnityEngine;

namespace UFE2FTE
{
    public class CharacterSpecificGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private UFE3D.CharacterInfo characterInfo;
        [SerializeField]
        private GameObject[] gameObjectArray;

        private void Update()
        {
            if (characterInfo == null
                || UFE2FTE.GetCharacterInfo(player) == null
                || characterInfo.characterName != UFE2FTE.GetCharacterInfo(player).characterName)
            {
                UFE2FTE.SetGameObjectActive(gameObjectArray, false);
            }
            else if (characterInfo.characterName == UFE2FTE.GetCharacterInfo(player).characterName)
            {
                UFE2FTE.SetGameObjectActive(gameObjectArray, true);
            }
        }
    }
}