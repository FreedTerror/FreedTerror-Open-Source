using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTERoundImageController : MonoBehaviour
    {
        public Color32 timeOutAlertColor = new Color32(255, 255, 255, 255);
        public Color32 perfectAlertColor = new Color32(255, 255, 255, 255);
        public Color32 koAlertColor = new Color32(255, 255, 255, 255);
        private enum Player
        {
            Player1,
            Player2
        }
        [SerializeField]
        private Player player;
        [SerializeField]
        private Image[] roundImageArray;

        private void OnEnable()
        {
            UFE.OnRoundEnds += OnRoundEnds;
        }

        private void Start()
        {
            SetActiveRoundImages();       
        }

        private void OnDisable()
        {
            UFE.OnRoundEnds -= OnRoundEnds;
        }

        private void OnRoundEnds(ControlsScript winner, ControlsScript loser)
        {
            if (winner != null
                && loser != null)
            {
                switch (UFE.gameMode)
                {
                    case GameMode.StoryMode:
                    case GameMode.VersusMode:
                    case GameMode.TrainingRoom:
                    case GameMode.NetworkGame:
                        if (UFE.timer == 0)
                        {
                            SetRoundImageColor(winner, winner.roundsWon - 1, timeOutAlertColor);
                        }
                        else if (UFE.timer != 0
                            && winner.currentLifePoints == winner.myInfo.lifePoints)
                        {
                            SetRoundImageColor(winner, winner.roundsWon - 1, perfectAlertColor);
                        }
                        else if (UFE.timer != 0
                            && winner.currentLifePoints != winner.myInfo.lifePoints)
                        {
                            SetRoundImageColor(winner, winner.roundsWon - 1, koAlertColor);
                        }
                        break;

                    case GameMode.ChallengeMode:
                        break;
                }
            }
        }

        private void SetActiveRoundImages()
        {
            int length = roundImageArray.Length;
            for (int i = GetNumberOfPossibleRoundWins(); i < length; i++)
            {
                if (roundImageArray[i] == null)
                {
                    continue;
                }
                
                roundImageArray[i].gameObject.SetActive(false);
            }
        }

        private void SetRoundImageColor(ControlsScript player, int index, Color32 color)
        {
            if (this.player == Player.Player1
                && player == UFE.GetPlayer1ControlsScript())
            {
                int length = roundImageArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (roundImageArray[i] == null
                        || index != i)
                    {
                        continue;
                    }

                    roundImageArray[i].color = color;

                    break;
                }
            }
            else if (this.player == Player.Player2
                && player == UFE.GetPlayer2ControlsScript())
            {
                int length = roundImageArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (roundImageArray[i] == null
                        || index != i)
                    {
                        continue;
                    }

                    roundImageArray[i].color = color;

                    break;
                }
            }
        }

        private int GetNumberOfPossibleRoundWins()
        {
            return (UFE.config.roundOptions.totalRounds + 1) / 2;
        }
    }
}