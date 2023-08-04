using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEComboTextController : MonoBehaviour
    {
        private enum Player
        {
            Player1,
            Player2
        }
        [SerializeField]
        private Player player;
        [SerializeField]
        private Text comboHitsText;
        [SerializeField]
        private Text comboDamagePercentText;
        [SerializeField]
        private Text comboDamagePointsText;
        private int previousComboDamage;
        [SerializeField]
        private Text comboHitDamagePercentText;
        [SerializeField]
        private Text comboHitDamagePointsText;
        [SerializeField]
        private UFE2FTEGCFreeStringNumbersScriptableObject gCFreeStringNumbersScriptableObject;

        private void Update()
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null
                || gCFreeStringNumbersScriptableObject == null)
            {
                return;
            }

            SetComboText();
        }

        private void OnDisable()
        {
            SetTextMessage(comboHitsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, 0));

            SetTextMessage(comboDamagePercentText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray, 0));

            SetTextMessage(comboDamagePointsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, 0));

            SetTextMessage(comboHitDamagePercentText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray, 0));

            SetTextMessage(comboHitDamagePointsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, 0));

            previousComboDamage = 0;
        }

        private void SetComboText()
        {
            if (player == Player.Player1)
            {
                SetTextMessage(comboHitsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, UFE.GetPlayer1ControlsScript().comboHits));

                int percentValue = Mathf.FloorToInt((float)(UFE.GetPlayer1ControlsScript().comboDamage / UFE.GetPlayer1ControlsScript().myInfo.lifePoints * 100));

                SetTextMessage(comboDamagePercentText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray, percentValue));

                SetTextMessage(comboDamagePointsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, Mathf.FloorToInt((float)UFE.GetPlayer1ControlsScript().comboDamage)));

                if (UFE.GetPlayer1ControlsScript().comboDamage > previousComboDamage)
                {
                    int differenceValue = Mathf.FloorToInt((float)UFE.GetPlayer1ControlsScript().comboDamage - previousComboDamage);

                    if (differenceValue > 0)
                    {
                        percentValue = Mathf.FloorToInt((float)differenceValue / UFE.GetPlayer1ControlsScript().myInfo.lifePoints * 100);

                        SetTextMessage(comboHitDamagePercentText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray, percentValue));

                        SetTextMessage(comboHitDamagePointsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, differenceValue));
                    }
                }

                previousComboDamage = Mathf.FloorToInt((float)UFE.GetPlayer1ControlsScript().comboDamage);

                if (UFE.GetPlayer1ControlsScript().comboDamage <= 0)
                {
                    SetTextMessage(comboHitsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, 0));

                    SetTextMessage(comboDamagePercentText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray, 0));

                    SetTextMessage(comboDamagePointsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, 0));

                    SetTextMessage(comboHitDamagePercentText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray, 0));

                    SetTextMessage(comboHitDamagePointsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, 0));
                }
            }
            else if (player == Player.Player2)
            {
                SetTextMessage(comboHitsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, UFE.GetPlayer2ControlsScript().comboHits));

                int percentValue = Mathf.FloorToInt((float)(UFE.GetPlayer2ControlsScript().comboDamage / UFE.GetPlayer2ControlsScript().myInfo.lifePoints * 100));
                
                SetTextMessage(comboDamagePercentText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray, percentValue));

                SetTextMessage(comboDamagePointsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, Mathf.FloorToInt((float)UFE.GetPlayer2ControlsScript().comboDamage)));

                if (UFE.GetPlayer2ControlsScript().comboDamage > previousComboDamage)
                {
                    int differenceValue = Mathf.FloorToInt((float)UFE.GetPlayer2ControlsScript().comboDamage - previousComboDamage);

                    if (differenceValue > 0)
                    {
                        percentValue = Mathf.FloorToInt((float)differenceValue / UFE.GetPlayer2ControlsScript().myInfo.lifePoints * 100);

                        SetTextMessage(comboHitDamagePercentText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray, percentValue));

                        SetTextMessage(comboHitDamagePointsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, differenceValue));
                    }
                }

                previousComboDamage = Mathf.FloorToInt((float)UFE.GetPlayer2ControlsScript().comboDamage);

                if (UFE.GetPlayer2ControlsScript().comboDamage <= 0)
                {
                    SetTextMessage(comboHitsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, 0));

                    SetTextMessage(comboDamagePercentText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray, 0));

                    SetTextMessage(comboDamagePointsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, 0));

                    SetTextMessage(comboHitDamagePercentText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray, 0));

                    SetTextMessage(comboHitDamagePointsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, 0));
                }
            }
        }

        private static void SetTextMessage(Text text, string message, Color32? color = null)
        {
            if (text == null)
            {
                return;
            }

            text.text = message;

            if (color != null)
            {
                text.color = (Color32)color;
            }
        }
    }
}