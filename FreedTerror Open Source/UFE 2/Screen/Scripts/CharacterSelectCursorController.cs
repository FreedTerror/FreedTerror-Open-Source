using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CharacterSelectCursorController : MonoBehaviour
    {
        //TODO figure out a more scalable soulution for the all players cursor system
        //current system is not scalable at all for more than 2 players.

        [SerializeField]
        private DefaultCharacterSelectionScreen defaultCharacterSelectionScreen;

        [SerializeField]
        private Color32 unselectedCharacterColor;
        [SerializeField]
        private Color32 selectedCharacterColor;

        [SerializeField]
        private RectTransform allPlayersRectTransform;
        [SerializeField]
        private Image allPlayers1Image;
        [SerializeField]
        private Image allPlayers2Image;

        [SerializeField]
        private RectTransform player1RectTransform;
        [SerializeField]
        private Image player1Image;

        [SerializeField]
        private RectTransform player2RectTransform;
        [SerializeField]
        private Image player2Image;

        private void Update()
        {
            if (defaultCharacterSelectionScreen == null)
            {
                return;
            }

            if (defaultCharacterSelectionScreen.GetHoverIndex(1) == defaultCharacterSelectionScreen.GetHoverIndex(2))
            {
                if (allPlayersRectTransform != null)
                {
                    allPlayersRectTransform.gameObject.SetActive(true);
                    allPlayersRectTransform.anchoredPosition = defaultCharacterSelectionScreen.characters[defaultCharacterSelectionScreen.GetHoverIndex(1)].rectTransform.anchoredPosition;
                    allPlayersRectTransform.offsetMin = defaultCharacterSelectionScreen.characters[defaultCharacterSelectionScreen.GetHoverIndex(1)].rectTransform.offsetMin;
                    allPlayersRectTransform.offsetMax = defaultCharacterSelectionScreen.characters[defaultCharacterSelectionScreen.GetHoverIndex(1)].rectTransform.offsetMax;
                }

                if (UFE.GetPlayer1() == null)
                {
                    if (allPlayers1Image != null)
                    {
                        allPlayers1Image.color = unselectedCharacterColor;
                    }
                }
                else
                {
                    if (allPlayers1Image != null)
                    {
                        allPlayers1Image.color = selectedCharacterColor;
                    }
                }

                if (UFE.GetPlayer2() == null)
                {
                    if (allPlayers2Image != null)
                    {
                        allPlayers2Image.color = unselectedCharacterColor;
                    }
                }
                else
                {
                    if (allPlayers2Image != null)
                    {
                        allPlayers2Image.color = selectedCharacterColor;
                    }
                }

                if (player1RectTransform != null)
                {
                    player1RectTransform.gameObject.SetActive(false);
                }

                if (player2RectTransform != null)
                {
                    player2RectTransform.gameObject.SetActive(false);
                }
            }
            else
            {
                if (allPlayersRectTransform != null)
                {
                    allPlayersRectTransform.gameObject.SetActive(true);
                }

                if (player1RectTransform != null)
                {
                    player1RectTransform.gameObject.SetActive(true);
                    player1RectTransform.anchoredPosition = defaultCharacterSelectionScreen.characters[defaultCharacterSelectionScreen.GetHoverIndex(1)].rectTransform.anchoredPosition;
                    player1RectTransform.offsetMin = defaultCharacterSelectionScreen.characters[defaultCharacterSelectionScreen.GetHoverIndex(1)].rectTransform.offsetMin;
                    player1RectTransform.offsetMax = defaultCharacterSelectionScreen.characters[defaultCharacterSelectionScreen.GetHoverIndex(1)].rectTransform.offsetMax;
                }

                if (UFE.GetPlayer1() == null)
                {
                    if (player1Image != null)
                    {
                        player1Image.color = unselectedCharacterColor;
                    }
                }
                else
                {
                    if (player1Image != null)
                    {
                        player1Image.color = selectedCharacterColor;
                    }
                }

                if (player2RectTransform != null)
                {
                    player2RectTransform.gameObject.SetActive(true);
                    player2RectTransform.anchoredPosition = defaultCharacterSelectionScreen.characters[defaultCharacterSelectionScreen.GetHoverIndex(2)].rectTransform.anchoredPosition;
                    player2RectTransform.offsetMin = defaultCharacterSelectionScreen.characters[defaultCharacterSelectionScreen.GetHoverIndex(2)].rectTransform.offsetMin;
                    player2RectTransform.offsetMax = defaultCharacterSelectionScreen.characters[defaultCharacterSelectionScreen.GetHoverIndex(2)].rectTransform.offsetMax;
                }

                if (UFE.GetPlayer2() == null)
                {
                    if (player2Image != null)
                    {
                        player2Image.color = unselectedCharacterColor;
                    }
                }
                else
                {
                    if (player2Image != null)
                    {
                        player2Image.color = selectedCharacterColor;
                    }
                }
            }
        }
    }
}