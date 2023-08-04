using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTEPaletteEditorSpriteMainMenuScreen : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTEPaletteEditorSpriteManager paletteEditorSpriteManagerPrefab;

        public void StartPaletteEditorSpriteScreen()
        {
            if (paletteEditorSpriteManagerPrefab == null) return;

            UFE.currentScreen.gameObject.SetActive(false);

            Instantiate(paletteEditorSpriteManagerPrefab);
        }
    }
}