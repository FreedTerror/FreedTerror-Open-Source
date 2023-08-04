namespace UFE2FTE
{
    public static class UFE2FTEPlayerPausedOptionsManager
    {
        public enum Player
        {
            Player1,
            Player2
        }

        public static Player playerPaused;
    }
}

/*// In that case, we can process pause menu events
     //UFE.PauseGame(!UFE.isPaused());

     if (player1CurrentStartButton && !player1PreviousStartButton)
     {
         UFE2FTE.UFE2FTEPlayerPausedOptionsManager.playerPaused = UFE2FTE.UFE2FTEPlayerPausedOptionsManager.Player.Player1Paused;
         UFE.PauseGame(!UFE.isPaused());
     }
     else if (player2CurrentStartButton && !player2PreviousStartButton)
     {
         UFE2FTE.UFE2FTEPlayerPausedOptionsManager.playerPaused = UFE2FTE.UFE2FTEPlayerPausedOptionsManager.Player.Player2Paused;
         UFE.PauseGame(!UFE.isPaused());
     }*/
