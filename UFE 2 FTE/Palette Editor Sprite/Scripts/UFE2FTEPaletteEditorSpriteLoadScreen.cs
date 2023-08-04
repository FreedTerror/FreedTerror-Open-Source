using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEPaletteEditorSpriteLoadScreen : MonoBehaviour
    {
        [SerializeField]
        private Button firstSelectedButton;

        [SerializeField]
        private Text loadedSwapColorsText;

        // Start is called before the first frame update
        void Start()
        {
            SelectButton(firstSelectedButton);

            SetLoadedSwapColorsNameText();
        }

        private void SelectButton(Button button)
        {
            if (button == null) return;

            button.Select();
        }

        public void StartPaletteEditorSpriteScreen()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || UFE2FTEPaletteEditorSpriteManager.instance == null) return;

            Destroy(gameObject);

            UFE2FTEPaletteEditorSpriteManager.instance.gameObject.SetActive(true);
        }

        public void NextLoadedSwapColors()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || UFE2FTEPaletteEditorSpriteManager.instance == null
                || loadedSwapColorsText == null) return;

            UFE2FTEPaletteEditorSpriteManager.instance.NextLoadedSwapColors();

            SetLoadedSwapColorsNameText();
        }

        public void PreviousLoadedSwapColors()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || UFE2FTEPaletteEditorSpriteManager.instance == null
                || loadedSwapColorsText == null) return;

            UFE2FTEPaletteEditorSpriteManager.instance.PreviousLoadedSwapColors();

            SetLoadedSwapColorsNameText();
        }

        private void SetLoadedSwapColorsNameText()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || UFE2FTEPaletteEditorSpriteManager.instance == null
                || loadedSwapColorsText == null) return;

            loadedSwapColorsText.text = UFE2FTEPaletteSwapSpriteManager.instance.GetLoadedSwapColorsName();
        }

        public void OpenCharacterCustomSwapColorsFilesFolder()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || UFE2FTEPaletteEditorSpriteManager.instance == null) return;

            UFE2FTEPaletteSwapSpriteManager.instance.OpenCharacterCustomSwapColorsFilesFolder(UFE2FTEPaletteEditorSpriteManager.instance.characterGameObject.characterName);
        }
    }
}