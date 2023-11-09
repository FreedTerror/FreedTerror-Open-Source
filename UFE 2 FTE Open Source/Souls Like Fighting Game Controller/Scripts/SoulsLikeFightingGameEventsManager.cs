using UFE3D;

namespace UFE2FTE
{
    public static class UFE2FTESoulsLikeFightingGameEventsManager
    {
        public delegate void ControlsScriptHandler(ControlsScript player);
        public static event ControlsScriptHandler OnBlockBreak;

        public static void CallOnBlockBreak(ControlsScript player)
        {
            if (OnBlockBreak == null) return;

            OnBlockBreak(player);
        }
    }
}