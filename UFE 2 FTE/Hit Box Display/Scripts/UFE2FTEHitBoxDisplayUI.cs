using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEHitBoxDisplayUI : MonoBehaviour
    {
        [SerializeField]
        private string offName = "OFF";
        [SerializeField]
        private string spriteRenderer2DInfrontName = "2D INFRONT";
        [SerializeField]
        private string spriteRenderer2DBehindName = "2D BEHIND";
        [SerializeField]
        private string meshRenderer3DName = "3D";
        [SerializeField]
        private string popcronGizmos2DName = "POPCRON GIZMOS 2D";
        [SerializeField]
        private string popcronGizmos3DName = "POPCRON GIZMOS 3D";
        [SerializeField]
        private Text displayModeText;
        [SerializeField]
        private UFE2FTEHitBoxDisplayOptionsManager.DisplayMode[] displayModeOrder =
            { UFE2FTEHitBoxDisplayOptionsManager.DisplayMode.Off,
            UFE2FTEHitBoxDisplayOptionsManager.DisplayMode.SpriteRenderer2DInfront,
            UFE2FTEHitBoxDisplayOptionsManager.DisplayMode.SpriteRenderer2DBehind,
            UFE2FTEHitBoxDisplayOptionsManager.DisplayMode.MeshRenderer3D,
            UFE2FTEHitBoxDisplayOptionsManager.DisplayMode.PopcronGizmos2D,
            UFE2FTEHitBoxDisplayOptionsManager.DisplayMode.PopcronGizmos3D };
        private int displayModeOrderIndex;

        [SerializeField]
        private Text alphaValueText;
        [Range(0, 255)]
        [SerializeField]
        private int alphaValueMin = 32;
        [Range(0, 255)]
        [SerializeField]
        private int alphaValueMax = 255;
        [SerializeField]
        private int incrementAlphaValueAmount = 32;
        [SerializeField]
        private int decrementAlphaValueAmount = 32;

        [SerializeField]
        private Toggle projectileTotalHitsTextToggle;

        private void Start()
        {
            SetDisplayModeOrderIndex();

            SetTextMessage(displayModeText, GetDisplayModeNameFromDisplayMode(UFE2FTEHitBoxDisplayOptionsManager.displayMode));

            SetTextMessage(alphaValueText, UFE2FTEHitBoxDisplayOptionsManager.alphaValue.ToString());
            
            SetToggleIsOn(projectileTotalHitsTextToggle, UFE2FTEHitBoxDisplayOptionsManager.useProjectileTotalHitsText);
        }

        private void Update()
        {
            SetToggleIsOn(projectileTotalHitsTextToggle, UFE2FTEHitBoxDisplayOptionsManager.useProjectileTotalHitsText);         
        }

        public void NextDisplayMode()
        {
            displayModeOrderIndex++;

            if (displayModeOrderIndex > displayModeOrder.Length - 1)
            {
                displayModeOrderIndex = 0;   
            }

            UFE2FTEHitBoxDisplayOptionsManager.displayMode = displayModeOrder[displayModeOrderIndex];

            SetTextMessage(displayModeText, GetDisplayModeNameFromDisplayMode(UFE2FTEHitBoxDisplayOptionsManager.displayMode));

            UFE2FTEHitBoxDisplayEventsManager.CallOnHitBoxDisplay(UFE2FTEHitBoxDisplayOptionsManager.displayMode, UFE2FTEHitBoxDisplayOptionsManager.alphaValue, UFE2FTEHitBoxDisplayOptionsManager.useProjectileTotalHitsText);
        }

        public void PreviousDisplayMode()
        {
            displayModeOrderIndex--;

            if (displayModeOrderIndex < 0)
            {
                displayModeOrderIndex = displayModeOrder.Length - 1;
            }

            UFE2FTEHitBoxDisplayOptionsManager.displayMode = displayModeOrder[displayModeOrderIndex];

            SetTextMessage(displayModeText, GetDisplayModeNameFromDisplayMode(UFE2FTEHitBoxDisplayOptionsManager.displayMode));

            UFE2FTEHitBoxDisplayEventsManager.CallOnHitBoxDisplay(UFE2FTEHitBoxDisplayOptionsManager.displayMode, UFE2FTEHitBoxDisplayOptionsManager.alphaValue, UFE2FTEHitBoxDisplayOptionsManager.useProjectileTotalHitsText);
        }

        private void SetDisplayModeOrderIndex()
        {
            int length = displayModeOrder.Length;
            for (int i = 0; i < length; i++)
            {
                if (UFE2FTEHitBoxDisplayOptionsManager.displayMode != displayModeOrder[i])
                {
                    continue;
                }

                displayModeOrderIndex = i;

                break;
            }
        }

        private string GetDisplayModeNameFromDisplayMode(UFE2FTEHitBoxDisplayOptionsManager.DisplayMode displayMode)
        {
            switch (displayMode)
            {
                case UFE2FTEHitBoxDisplayOptionsManager.DisplayMode.Off: 
                    return offName;

                case UFE2FTEHitBoxDisplayOptionsManager.DisplayMode.SpriteRenderer2DInfront: 
                    return spriteRenderer2DInfrontName;

                case UFE2FTEHitBoxDisplayOptionsManager.DisplayMode.SpriteRenderer2DBehind: 
                    return spriteRenderer2DBehindName;

                case UFE2FTEHitBoxDisplayOptionsManager.DisplayMode.MeshRenderer3D: 
                    return meshRenderer3DName;

                case UFE2FTEHitBoxDisplayOptionsManager.DisplayMode.PopcronGizmos2D:
                    return popcronGizmos2DName;

                case UFE2FTEHitBoxDisplayOptionsManager.DisplayMode.PopcronGizmos3D:
                    return popcronGizmos3DName;

                default: return "";
            }
        }

        public void NextAlphaValue()
        {
            int alphaValue = UFE2FTEHitBoxDisplayOptionsManager.alphaValue;

            if (alphaValue == alphaValueMax)
            {
                alphaValue = alphaValueMin;

                UFE2FTEHitBoxDisplayOptionsManager.alphaValue = (byte)alphaValue;

                SetTextMessage(alphaValueText, UFE2FTEHitBoxDisplayOptionsManager.alphaValue.ToString());

                UFE2FTEHitBoxDisplayEventsManager.CallOnHitBoxDisplay(UFE2FTEHitBoxDisplayOptionsManager.displayMode, UFE2FTEHitBoxDisplayOptionsManager.alphaValue, UFE2FTEHitBoxDisplayOptionsManager.useProjectileTotalHitsText);

                return;
            }

            alphaValue += incrementAlphaValueAmount;

            if (alphaValue > alphaValueMax)
            {
                alphaValue = alphaValueMax;
            }

            UFE2FTEHitBoxDisplayOptionsManager.alphaValue = (byte)alphaValue;

            SetTextMessage(alphaValueText, UFE2FTEHitBoxDisplayOptionsManager.alphaValue.ToString());

            UFE2FTEHitBoxDisplayEventsManager.CallOnHitBoxDisplay(UFE2FTEHitBoxDisplayOptionsManager.displayMode, UFE2FTEHitBoxDisplayOptionsManager.alphaValue, UFE2FTEHitBoxDisplayOptionsManager.useProjectileTotalHitsText);
        }

        public void PreviousAlphaValue()
        {
            int alphaValue = UFE2FTEHitBoxDisplayOptionsManager.alphaValue;

            if (alphaValue == alphaValueMin)
            {
                alphaValue = alphaValueMax;

                UFE2FTEHitBoxDisplayOptionsManager.alphaValue = (byte)alphaValue;

                SetTextMessage(alphaValueText, UFE2FTEHitBoxDisplayOptionsManager.alphaValue.ToString());

                UFE2FTEHitBoxDisplayEventsManager.CallOnHitBoxDisplay(UFE2FTEHitBoxDisplayOptionsManager.displayMode, UFE2FTEHitBoxDisplayOptionsManager.alphaValue, UFE2FTEHitBoxDisplayOptionsManager.useProjectileTotalHitsText);

                return;
            }

            alphaValue -= decrementAlphaValueAmount;

            if (alphaValue < alphaValueMin)
            {
                alphaValue = alphaValueMin;
            }

            UFE2FTEHitBoxDisplayOptionsManager.alphaValue = (byte)alphaValue;

            SetTextMessage(alphaValueText, UFE2FTEHitBoxDisplayOptionsManager.alphaValue.ToString());

            UFE2FTEHitBoxDisplayEventsManager.CallOnHitBoxDisplay(UFE2FTEHitBoxDisplayOptionsManager.displayMode, UFE2FTEHitBoxDisplayOptionsManager.alphaValue, UFE2FTEHitBoxDisplayOptionsManager.useProjectileTotalHitsText);
        }

        public void SetUseProjectileTotalHitsText(bool useProjectileTotalHitsText)
        {
            UFE2FTEHitBoxDisplayOptionsManager.useProjectileTotalHitsText = useProjectileTotalHitsText;
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

        private static void SetToggleIsOn(Toggle toggle, bool isOn)
        {
            if (toggle == null)
            {
                return;
            }

            toggle.isOn = isOn;
        }
    }
}
