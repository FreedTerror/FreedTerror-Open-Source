using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTECharacterSelectionScreenSelectedCharacterUI : MonoBehaviour
    {
        [SerializeField]
        private DefaultCharacterSelectionScreen defaultCharacterSelectionScreen;

        private int player1HoverIndex;
        private int player2HoverIndex;

        [SerializeField]
        private GameObject player1CharacterSelectGameObject;
        [SerializeField]
        private GameObject player2CharacterSelectGameObject;
        [SerializeField]
        private GameObject bothPlayersCharacterSelectGameObject;

        [SerializeField]
        private Image player1CharacterSelectImage;
        [SerializeField]
        private Image player2CharacterSelectImage;
        [SerializeField]
        private Image player1BothPlayersCharacterSelectImage;
        [SerializeField]
        private Image player2BothPlayersCharacterSelectImage;

        [SerializeField]
        private Color32 unselectedCharacterColor;
        [SerializeField]
        private Color32 selectedCharacterColor;

        // Update is called once per frame
        void Update()
        {
            if (defaultCharacterSelectionScreen == null
                || player1CharacterSelectGameObject == null
                || player2CharacterSelectGameObject == null
                || bothPlayersCharacterSelectGameObject == null
                || player1CharacterSelectImage == null
                || player2CharacterSelectImage == null
                || player1BothPlayersCharacterSelectImage == null
                || player2BothPlayersCharacterSelectImage == null) return;

            player1HoverIndex = defaultCharacterSelectionScreen.GetHoverIndex(1);
            player2HoverIndex = defaultCharacterSelectionScreen.GetHoverIndex(2);

            if (player1HoverIndex == player2HoverIndex)
            {
                player1CharacterSelectGameObject.SetActive(false);
                player2CharacterSelectGameObject.SetActive(false);
                bothPlayersCharacterSelectGameObject.SetActive(true);
            }
            else
            {
                player1CharacterSelectGameObject.SetActive(true);
                player2CharacterSelectGameObject.SetActive(true);
                bothPlayersCharacterSelectGameObject.SetActive(false);
            }

            if (UFE.config.player1Character != null)
            {
                player1CharacterSelectImage.color = selectedCharacterColor;
                player1BothPlayersCharacterSelectImage.color = selectedCharacterColor;
            }
            else
            {
                player1CharacterSelectImage.color = unselectedCharacterColor;
                player1BothPlayersCharacterSelectImage.color = unselectedCharacterColor;
            }

            if (UFE.config.player2Character != null)
            {
                player2CharacterSelectImage.color = selectedCharacterColor;
                player2BothPlayersCharacterSelectImage.color = selectedCharacterColor;
            }
            else
            {
                player2CharacterSelectImage.color = unselectedCharacterColor;
                player2BothPlayersCharacterSelectImage.color = unselectedCharacterColor;
            }
        }
    }
}
