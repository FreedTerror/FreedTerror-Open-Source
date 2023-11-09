using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class CharacterTotalRoundsImageController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Color32 roundNotWonColor = new Color32(0, 0, 0, 255);
        [SerializeField]
        private Color32 roundWonColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private Image[] roundImageArray;      

        private void Update()
        {
            UpdateRoundImages(UFE2FTE.GetControlsScript(player));
        }

        private void UpdateRoundImages(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            int length = roundImageArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (roundImageArray[i] == null)
                {
                    continue;
                }

                if (i < GetNumberOfPossibleRoundWins())
                {
                    roundImageArray[i].gameObject.SetActive(true);
                }
                else
                {
                    roundImageArray[i].gameObject.SetActive(false);
                }

                if (i >= player.roundsWon)
                {
                    roundImageArray[i].color = roundNotWonColor;
                }
                else
                {
                    roundImageArray[i].color = roundWonColor;
                }
            }
        }

        private static int GetNumberOfPossibleRoundWins()
        {
            if (UFE.config == null)
            {
                return 0;
            }

            return (UFE.config.roundOptions.totalRounds + 1) / 2;
        }
    }
}