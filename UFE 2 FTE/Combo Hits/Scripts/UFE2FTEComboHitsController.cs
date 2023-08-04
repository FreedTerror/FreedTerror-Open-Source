using System;
using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTEComboHitsController : MonoBehaviour
    {
        [Serializable]
        private class ComboHitsRangeOptions
        {
            public int minComboHits;
            public int maxComboHits;
            public GameObject[] disabledGameObjectArray;
            public GameObject[] enabledGameObjectArray;

            public static bool IsInComboHitsRange(ControlsScript player, ComboHitsRangeOptions comboHitsRangeOptions)
            {
                if (player == null
                    || comboHitsRangeOptions == null)
                {
                    return false;
                }

                if (player.comboHits >= comboHitsRangeOptions.minComboHits
                    && player.comboHits <= comboHitsRangeOptions.maxComboHits)
                {
                    return true;
                }
                
                return false;
            }

            public static void SetComboHitsRangeOptions(ControlsScript player, ComboHitsRangeOptions comboHitsRangeOptions)
            {
                if (player == null
                    || comboHitsRangeOptions == null)
                {
                    return;
                }

                int length = comboHitsRangeOptions.disabledGameObjectArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (comboHitsRangeOptions.disabledGameObjectArray[i] == null
                        || IsInComboHitsRange(player, comboHitsRangeOptions) == false)
                    {
                        continue;
                    }

                    comboHitsRangeOptions.disabledGameObjectArray[i].SetActive(false);
                }

                length = comboHitsRangeOptions.enabledGameObjectArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (comboHitsRangeOptions.enabledGameObjectArray[i] == null
                        || IsInComboHitsRange(player, comboHitsRangeOptions) == false)
                    {
                        continue;
                    }

                    comboHitsRangeOptions.enabledGameObjectArray[i].SetActive(true);
                }
            }

            public static void SetComboHitsRangeOptions(ControlsScript player, ComboHitsRangeOptions[] comboHitsRangeOptionsArray)
            {
                if (player == null
                    || comboHitsRangeOptionsArray == null)
                {
                    return;
                }

                int length = comboHitsRangeOptionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    SetComboHitsRangeOptions(player, comboHitsRangeOptionsArray[i]);
                }
            }
        }

        [Serializable]
        private class ComboHitsOptions
        {
            private enum Player
            {
                Player1,
                Player2
            }
            [SerializeField]
            private Player player;
            private static ControlsScript GetControlsScriptFromPlayer(Player player)
            {
                if (player == Player.Player1)
                {
                    return UFE.GetPlayer1ControlsScript();
                }
                if (player == Player.Player2)
                {
                    return UFE.GetPlayer2ControlsScript();
                }

                return null;
            }
            public ComboHitsRangeOptions[] comboHitsRangeOptionsArray;

            public static void SetComboHitsOptions(ComboHitsOptions comboHitsOptions)
            {
                if (comboHitsOptions == null)
                {
                    return;
                }

                ComboHitsRangeOptions.SetComboHitsRangeOptions(GetControlsScriptFromPlayer(comboHitsOptions.player), comboHitsOptions.comboHitsRangeOptionsArray);
            }

            public static void SetComboHitsOptions(ComboHitsOptions[] comboHitsOptionsArray)
            {
                if (comboHitsOptionsArray == null)
                {
                    return;
                }

                int length = comboHitsOptionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    ComboHitsRangeOptions.SetComboHitsRangeOptions(GetControlsScriptFromPlayer(comboHitsOptionsArray[i].player), comboHitsOptionsArray[i].comboHitsRangeOptionsArray);
                }
            }
        }
        [SerializeField]
        private ComboHitsOptions[] comboHitsOptionsArray;

        private void Update()
        {
            ComboHitsOptions.SetComboHitsOptions(comboHitsOptionsArray);
        }
    }
}
