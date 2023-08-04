using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEPaletteEditorSpriteQuitScreen : MonoBehaviour
    {
        [SerializeField]
        private Button firstSelectedButton;

        // Start is called before the first frame update
        void Start()
        {
            SelectButton(firstSelectedButton);
        }

        private void SelectButton(Button button)
        {
            if (button == null) return;

            button.Select();
        }

        public void StartMainMenuScreen()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || UFE2FTEPaletteEditorSpriteManager.instance == null) return;

            Destroy(gameObject);

            Destroy(UFE2FTEPaletteEditorSpriteManager.instance.gameObject);

            UFE.StartMainMenuScreen();
        }

        public void StartPaletteEditorSpriteScreen()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || UFE2FTEPaletteEditorSpriteManager.instance == null) return;

            Destroy(gameObject);

            UFE2FTEPaletteEditorSpriteManager.instance.gameObject.SetActive(true);
        }
    }
}