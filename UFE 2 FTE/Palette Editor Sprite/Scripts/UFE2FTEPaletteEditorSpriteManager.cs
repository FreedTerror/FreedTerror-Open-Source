using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEPaletteEditorSpriteManager : MonoBehaviour
    {
        public static UFE2FTEPaletteEditorSpriteManager instance;

        #region UI Options

        [Header("UI OPTIONS")]
        [SerializeField]
        private Button firstSelectedButtonOnEnable;

        [SerializeField]
        private Text characterNameText;
        private int characterNameIndex;

        [SerializeField]
        private Text customSwapColorsNameText;
        private int customSwapColorsNameIndex;

        [SerializeField]
        private Text palettePartNameText;
        private int palettePartNameIndex;

        [SerializeField]
        private Button saveButton;
        [SerializeField]
        private Button quitButton;
        [SerializeField]
        private Button loadButton;

        [SerializeField]
        private UFE2FTEPaletteEditorSpriteSaveScreen saveScreenPrefab;
        [SerializeField]
        private UFE2FTEPaletteEditorSpriteQuitScreen quitScreenPrefab;
        [SerializeField]
        private UFE2FTEPaletteEditorSpriteLoadScreen loadScreenPrefab;

        #endregion

        #region Character Prefabs Options

        [Header("CHARACTER PREFABS OPTIONS")]
        [SerializeField]
        private List<UFE2FTEPaletteSwapSpriteController> characterPrefabs = new List<UFE2FTEPaletteSwapSpriteController>();
        [SerializeField]
        private Transform characterPrefabsParentTransform;
        [SerializeField]
        private bool useMainCameraAsCharacterPrefabsParentTransform;
        [SerializeField]
        private Vector3 characterPrefabsSpawnPosition;
        [HideInInspector]
        public UFE2FTEPaletteSwapSpriteController characterGameObject;
        private Vector3 characterGameObjectStoredPosition;

        #endregion

        #region Move Character Options

        [Header("MOVE CHARACTER OPTIONS")] 
        [SerializeField]
        private float moveCharacterXAxisAmount;
        [SerializeField]
        private float moveCharacterYAxisAmount;
        [SerializeField]
        private float moveCharacterZAxisAmount;

        #endregion

        #region Color Variables

        [Header("DEFAULT COLOR CHOICE OPTIONS")]
        [SerializeField]
        private Text colorNameTypeText;

        [SerializeField]
        private LetterCaseType colorNameTypeLetterCaseType;

        [SerializeField]
        private ColorNameType[] colorNameTypeOrder =
            { ColorNameType.BlackAndGreyColors,
            ColorNameType.WhiteColors,
            ColorNameType.RedColors,
            ColorNameType.GreenColors,
            ColorNameType.BlueColors,
            ColorNameType.OrangeColors,
            ColorNameType.YellowColors,
            ColorNameType.PurpleColors,
            ColorNameType.PinkColors,
            ColorNameType.BrownColors,
            ColorNameType.CyanColors,
            ColorNameType.GoldColors };

        private int colorNameTypeOrderIndex;

        [SerializeField]
        private Text colorNameText;
        [SerializeField]
        private string noColorName = "NONE";
        [SerializeField]
        private LetterCaseType colorNameLetterCaseType;

        [SerializeField]
        private ColorNameBlackAndGray[] colorNameBlackAndGreyOrder =
            { ColorNameBlackAndGray.Black,
            ColorNameBlackAndGray.DarkSlateGray,
            ColorNameBlackAndGray.DimGray,
            ColorNameBlackAndGray.SlateGray,
            ColorNameBlackAndGray.Gray,
            ColorNameBlackAndGray.LightSlateGray,
            ColorNameBlackAndGray.DarkGray,
            ColorNameBlackAndGray.Silver,
            ColorNameBlackAndGray.LightGray,
            ColorNameBlackAndGray.Gainsboro };

        [SerializeField]
        private ColorNameWhite[] colorNameWhiteOrder =
            { ColorNameWhite.White,
            ColorNameWhite.Ivory,
            ColorNameWhite.Snow,
            ColorNameWhite.MintCream,
            ColorNameWhite.Azure,
            ColorNameWhite.FloralWhite,
            ColorNameWhite.Honeydew,
            ColorNameWhite.GhostWhite,
            ColorNameWhite.Seashell,
            ColorNameWhite.AliceBlue,
            ColorNameWhite.OldLace,
            ColorNameWhite.LavenderBlush,
            ColorNameWhite.WhiteSmoke,
            ColorNameWhite.Beige, 
            ColorNameWhite.Linen,
            ColorNameWhite.AntiqueWhite,
            ColorNameWhite.MistyRose };

        [SerializeField]
        private ColorNameRed[] colorNameRedOrder =
            { ColorNameRed.Red,
            ColorNameRed.DarkRed,
            ColorNameRed.LightRed,
            ColorNameRed.Firebrick,
            ColorNameRed.Crimson,
            ColorNameRed.IndianRed,
            ColorNameRed.LightCoral,
            ColorNameRed.Salmon,
            ColorNameRed.DarkSalmon,
            ColorNameRed.LightSalmon,
            ColorNameRed.Cardinal,
            ColorNameRed.Carmine,
            ColorNameRed.RustyRed,
            ColorNameRed.ImperialRed,
            ColorNameRed.FireEngineRed,
            ColorNameRed.Vermilion,
            ColorNameRed.Cinnabar,
            ColorNameRed.Jasper };

        [SerializeField]
        private ColorNameGreen[] colorNameGreenOrder =
            { ColorNameGreen.Green,
            ColorNameGreen.DarkGreen,
            ColorNameGreen.LightGreen,
            ColorNameGreen.DarkOliveGreen,
            ColorNameGreen.ForestGreen,
            ColorNameGreen.SeaGreen,
            ColorNameGreen.Olive,
            ColorNameGreen.OliveDrab,
            ColorNameGreen.MediumSeaGreen,
            ColorNameGreen.LimeGreen,
            ColorNameGreen.Lime,
            ColorNameGreen.SpringGreen,
            ColorNameGreen.MediumSpringGreen,
            ColorNameGreen.DarkSeaGreen,
            ColorNameGreen.YellowGreen,
            ColorNameGreen.LawnGreen,
            ColorNameGreen.Chartreuse,
            ColorNameGreen.GreenYellow,
            ColorNameGreen.PaleGreen };

        [SerializeField]
        private ColorNameBlue[] colorNameBlueOrder =
            { ColorNameBlue.Blue,
            ColorNameBlue.DarkBlue,
            ColorNameBlue.LightBlue,
            ColorNameBlue.Navy,
            ColorNameBlue.MediumBlue,
            ColorNameBlue.MidnightBlue,
            ColorNameBlue.RoyalBlue,
            ColorNameBlue.SteelBlue,
            ColorNameBlue.DodgerBlue,
            ColorNameBlue.DeepSkyBlue,
            ColorNameBlue.CornflowerBlue,
            ColorNameBlue.SkyBlue,
            ColorNameBlue.LightSkyBlue,
            ColorNameBlue.LightSteelBlue,
            ColorNameBlue.PowderBlue };

        [SerializeField]
        private ColorNameOrange[] colorNameOrangeOrder =
            { ColorNameOrange.Orange,
            ColorNameOrange.OrangeRed,
            ColorNameOrange.DarkOrange,
            ColorNameOrange.Tomato,
            ColorNameOrange.Coral,
            ColorNameOrange.SafetyOrange,
            ColorNameOrange.AerospaceOrange,
            ColorNameOrange.Xanthous,
            ColorNameOrange.CarrotOrange,
            ColorNameOrange.OrangePeel,
            ColorNameOrange.Tangerine,
            ColorNameOrange.Pumpkin,
            ColorNameOrange.Tangelo,
            ColorNameOrange.Saffron,
            ColorNameOrange.Persimmon };

        [SerializeField]
        private ColorNameYellow[] colorNameYellowOrder =
            { ColorNameYellow.Yellow,
            ColorNameYellow.DarkKhaki,
            ColorNameYellow.Khaki,
            ColorNameYellow.PeachPuff,
            ColorNameYellow.PaleGoldenrod,
            ColorNameYellow.Moccasin,
            ColorNameYellow.Papayawhip,
            ColorNameYellow.LightGoldenrodYellow,
            ColorNameYellow.LemonChiffon,
            ColorNameYellow.LightYellow,
            ColorNameYellow.LightYellow1,
            ColorNameYellow.LightYellow2,
            ColorNameYellow.LightYellow3,
            ColorNameYellow.LightYellow4,
            ColorNameYellow.DarkYellow1,
            ColorNameYellow.DarkYellow2,
            ColorNameYellow.DarkYellow3,
            ColorNameYellow.DarkYellow4 };

        [SerializeField]
        private ColorNamePurple[] colorNamePurpleOrder =
            { ColorNamePurple.Purple,
            ColorNamePurple.Indigo,
            ColorNamePurple.DarkMagenta,
            ColorNamePurple.DarkViolet,
            ColorNamePurple.DarkSlateBlue,
            ColorNamePurple.BlueViolet,
            ColorNamePurple.DarkOrchid,
            ColorNamePurple.Magenta,
            ColorNamePurple.SlateBlue,
            ColorNamePurple.MediumSlateBlue,
            ColorNamePurple.MediumOrchid,
            ColorNamePurple.MediumPurple,
            ColorNamePurple.Orchid,
            ColorNamePurple.Violet,
            ColorNamePurple.Plum,
            ColorNamePurple.Thistle,
            ColorNamePurple.Lavender };

        [SerializeField]
        private ColorNamePink[] colorNamePinkOrder =
            { ColorNamePink.Pink,
            ColorNamePink.LightPink,
            ColorNamePink.HotPink,
            ColorNamePink.PaleVioletRed,
            ColorNamePink.DeepPink,
            ColorNamePink.MediumVioletRed,
            ColorNamePink.PinkLace,
            ColorNamePink.PiggyPink,
            ColorNamePink.BabyPink,
            ColorNamePink.OrchidPink,
            ColorNamePink.CherryBlossomPink,
            ColorNamePink.LightHotPink,
            ColorNamePink.CoralPink,
            ColorNamePink.FandangoPink,
            ColorNamePink.PersianPink,
            ColorNamePink.LightDeepPink,
            ColorNamePink.UltraPink,
            ColorNamePink.ShockingPink,
            ColorNamePink.SuperPink,
            ColorNamePink.SteelPink,
            ColorNamePink.BubblegumPink };

        [SerializeField]
        private ColorNameBrown[] colorNameBrownOrder =
            { ColorNameBrown.Brown,
            ColorNameBrown.Maroon,
            ColorNameBrown.SaddleBrown,
            ColorNameBrown.Sienna,
            ColorNameBrown.Chocolate,
            ColorNameBrown.DarkGoldenrod,
            ColorNameBrown.Peru,
            ColorNameBrown.RosyBrown,
            ColorNameBrown.Goldenrod,
            ColorNameBrown.SandyBrown,
            ColorNameBrown.Tan,
            ColorNameBrown.Burlywood,
            ColorNameBrown.Wheat,
            ColorNameBrown.NavajoWhite,
            ColorNameBrown.Bisque,
            ColorNameBrown.BlanchedAlmond,
            ColorNameBrown.Cornsilk };

        [SerializeField]
        private ColorNameCyan[] colorNameCyanOrder =
            { ColorNameCyan.Cyan,
            ColorNameCyan.Teal,
            ColorNameCyan.DarkCyan,
            ColorNameCyan.LightSeaGreen,
            ColorNameCyan.CadetBlue,
            ColorNameCyan.DarkTurquoise,
            ColorNameCyan.MediumTurquoise,
            ColorNameCyan.Turquoise,
            ColorNameCyan.Aquamarine,
            ColorNameCyan.MediumAquamarine,
            ColorNameCyan.PaleTurquoise,
            ColorNameCyan.LightCyan };

        [SerializeField]
        private ColorNameGold[] colorNameGoldOrder =
            { ColorNameGold.Gold,
            ColorNameGold.GoldenYellow,
            ColorNameGold.MetallicGold,
            ColorNameGold.OldGold,
            ColorNameGold.VegasGold,
            ColorNameGold.PaleGold,
            ColorNameGold.GoldenBrown };

        private int colorNameOrderIndex;

        #endregion

        #region Color Picker Variables

        [Header("COLOR PICKER OPTIONS")]
        [SerializeField]
        private GameObject[] hiddenColorPickerGameObjects;
        [SerializeField]
        private GameObject colorPickerGameObject;
        [SerializeField]
        private Button firstSelectedColorPickerButtonOnEnable;
        [SerializeField]
        private Button firstSelectedColorPickerButtonOnDisable;
        [SerializeField]
        private Image colorPickerOutputImage;
        [SerializeField]
        private Slider redColorPickerSlider;
        [SerializeField]
        private Text redColorPickerSliderValueText;
        [SerializeField]
        private Slider greenColorPickerSlider;
        [SerializeField]
        private Text greenColorPickerSliderValueText;
        [SerializeField]
        private Slider blueColorPickerSlider;
        [SerializeField]
        private Text blueColorPickerSliderValueText;
        private bool ignoreColorPickerSliderOnValueChanged;
        [Range(0, 255)]
        [SerializeField]
        private int incrementAndDecrementColorPickerSliderAmount;

        #endregion

        void Awake()
        {
            instance = this;
        }

        void OnEnable()
        {
            SetCharacterPrefabsParentTransform();

            SetCharacterPrefabsList();

            SelectButton(firstSelectedButtonOnEnable);

            MoveCharacterToCharacterGameObjectStoredPosition();

            ResetLoadedSwapColorsVariables();
        }

        // Start is called before the first frame update
        void Start()
        {
            SetCharacterPrefabsParentTransform();

            SetCharacterPrefabsList();

            SetColorNameTypeText();
       
            colorNameOrderIndex = -1;

            SetColorNameText();
        }

        // Update is called once per frame
        void Update()
        {
            SetCharacterPrefabsList();

            SetCharacterNameText();

            SetCharacterGameObjectStoredPosition();

            SetCustomSwapColorsNameText();

            SetPalettePartNameText();

            SetAllColorPickerSliderValues();
        }

        void OnDestroy()
        {
            DestroyCharacterGameObject();
        }

        private void ResetLoadedSwapColorsVariables()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null) return;

            UFE2FTEPaletteSwapSpriteManager.instance.ResetLoadedSwapColorsVariables();
        }

        #region Screen Methods

        private void SelectButton(Button button)
        {
            if (button == null) return;

            button.Select();
        }

        public void StartSaveScreen()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || saveScreenPrefab == null) return;

            gameObject.SetActive(false);

            if (saveButton != null)
            {
                firstSelectedButtonOnEnable = saveButton;
            }

            ResetCharacterPosition();

            Instantiate(saveScreenPrefab);
        }

        public void StartQuitScreen()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || quitScreenPrefab == null) return;

            gameObject.SetActive(false);

            if (saveButton != null)
            {
                firstSelectedButtonOnEnable = quitButton;
            }

            ResetCharacterPosition();

            Instantiate(quitScreenPrefab);
        }

        public void StartLoadScreen()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || loadScreenPrefab == null
                || characterGameObject == null) return;

            gameObject.SetActive(false);

            if (loadButton != null)
            {
                firstSelectedButtonOnEnable = loadButton;
            }

            ResetCharacterPosition();

            Instantiate(loadScreenPrefab);

            UFE2FTEPaletteSwapSpriteManager.instance.LoadLoadedSwapColors(characterGameObject.characterName);
        }

        #endregion

        #region Character Prefabs And GameObject Methods

        private void SetCharacterPrefabsParentTransform()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null) return;

            if (useMainCameraAsCharacterPrefabsParentTransform == true)
            {
                characterPrefabsParentTransform = Camera.main.transform;
            }
        }

        private void SetCharacterPrefabsList()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null) return;

            int count = characterPrefabs.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                if (characterPrefabs[i] != null) continue;

                characterPrefabs.RemoveAt(i);
            }
        }

        private void CreateAndDestroyCharacterGameObjectByCharacterName(string characterName)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null) return;

            int count = characterPrefabs.Count;
            for (int i = 0; i < count; i++)
            {
                if (characterPrefabs[i] == null) continue;

                if (characterGameObject == null
                    && characterName == characterPrefabs[i].characterName)
                {
                    SpawnCharacterGameObject(characterPrefabs[i]);

                    break;
                }
                else if (characterGameObject != null
                    && characterName != characterGameObject.characterName)
                {
                    DestroyCharacterGameObject();

                    if (characterName == characterPrefabs[i].characterName)
                    {
                        SpawnCharacterGameObject(characterPrefabs[i]);

                        break;
                    }
                }
            }
        }

        private void SpawnCharacterGameObject(UFE2FTEPaletteSwapSpriteController characterPrefab)
        {
            if (characterPrefab == null) return;

            characterGameObject = Instantiate(characterPrefab, characterPrefabsParentTransform);

            if (characterPrefabsParentTransform == null)
            {
                characterGameObject.transform.position = characterPrefabsSpawnPosition;
            }
            else
            {
                characterGameObject.transform.localPosition = characterPrefabsSpawnPosition;
            }

            characterGameObject.transform.localEulerAngles = new Vector3(0, 0, 0);

            characterGameObject.transform.localScale = characterPrefab.transform.localScale;

            ApplySavedColors();
        }

        private void DestroyCharacterGameObject()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            Destroy(characterGameObject.gameObject);
        }

        #endregion

        #region Character Position Methods

        private void SetCharacterGameObjectStoredPosition()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            if (characterGameObject.transform.parent == null)
            {
                characterGameObjectStoredPosition = characterGameObject.transform.position;
            }
            else
            {
                characterGameObjectStoredPosition = characterGameObject.transform.localPosition;
            }
        }

        private void MoveCharacterToCharacterGameObjectStoredPosition()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            if (characterGameObject.transform.parent == null)
            {
                characterGameObject.transform.position = characterGameObjectStoredPosition;
            }
            else
            {
                characterGameObject.transform.localPosition = characterGameObjectStoredPosition;
            }
        }

        public void ResetCharacterPosition()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            if (characterGameObject.transform.parent == null)
            {
                characterGameObject.transform.position = characterPrefabsSpawnPosition;
            }
            else
            {
                characterGameObject.transform.localPosition = characterPrefabsSpawnPosition;
            }
        }

        public void MoveCharacterXAxisPositive()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            Vector3 newPosition = characterGameObject.transform.position;

            newPosition.x += moveCharacterXAxisAmount;

            characterGameObject.transform.position = newPosition;
        }

        public void MoveCharacterXAxisNegative()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            Vector3 newPosition = characterGameObject.transform.position;

            newPosition.x -= moveCharacterXAxisAmount;

            characterGameObject.transform.position = newPosition;
        }

        public void MoveCharacterYAxisPositive()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            Vector3 newPosition = characterGameObject.transform.position;

            newPosition.y += moveCharacterYAxisAmount;

            characterGameObject.transform.position = newPosition;
        }

        public void MoveCharacterYAxisNegative()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            Vector3 newPosition = characterGameObject.transform.position;

            newPosition.y -= moveCharacterYAxisAmount;

            characterGameObject.transform.position = newPosition;
        }

        public void MoveCharacterZAxisPositive()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            Vector3 newPosition = characterGameObject.transform.position;

            newPosition.z += moveCharacterZAxisAmount;

            characterGameObject.transform.position = newPosition;
        }

        public void MoveCharacterZAxisNegative()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            Vector3 newPosition = characterGameObject.transform.position;

            newPosition.z -= moveCharacterZAxisAmount;

            characterGameObject.transform.position = newPosition;
        }

        #endregion

        #region Character Name Methods

        private void SetCharacterNameText()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterPrefabs == null
                || characterPrefabs.Count == 0
                || characterNameText == null) return;

            characterNameText.text = characterPrefabs[characterNameIndex].characterName;

            CreateAndDestroyCharacterGameObjectByCharacterName(characterNameText.text);    
        }

        public void NextCharacterName()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterPrefabs.Count == 1
                || characterNameText == null) return;

            characterNameIndex++;

            palettePartNameIndex = 0;

            if (characterNameIndex > characterPrefabs.Count - 1)
            {
                characterNameIndex = 0;
            }
        }

        public void PreviousCharacterName()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterPrefabs.Count == 1
                || characterNameText == null) return;

            characterNameIndex--;

            palettePartNameIndex = 0;

            if (characterNameIndex < 0)
            {
                characterNameIndex = characterPrefabs.Count - 1;
            }
        }

        #endregion

        #region Custom Swap Colors Name Methods

        private void SetCustomSwapColorsNameText()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || customSwapColorsNameText == null) return;

            customSwapColorsNameText.text = UFE2FTEPaletteSwapSpriteManager.instance.swapColorsSaveDataOptions.customSwapColorNames[customSwapColorsNameIndex];
        }

        public void NextCustomSwapColorsName()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || customSwapColorsNameText == null) return;

            customSwapColorsNameIndex++;

            if (customSwapColorsNameIndex > UFE2FTEPaletteSwapSpriteManager.instance.swapColorsSaveDataOptions.customSwapColorNames.Length - 1)
            {
                customSwapColorsNameIndex = 0;
            }

            ApplySavedColors();
        }

        public void PreviousCustomSwapColorsName()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || customSwapColorsNameText == null) return;

            customSwapColorsNameIndex--;

            if (customSwapColorsNameIndex < 0)
            {
                customSwapColorsNameIndex = UFE2FTEPaletteSwapSpriteManager.instance.swapColorsSaveDataOptions.customSwapColorNames.Length - 1;
            }

            ApplySavedColors();
        }

        #endregion

        #region Palette Part Name Methods

        private void SetPalettePartNameText()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || palettePartNameText == null) return;

            palettePartNameText.text = GetPalettePartName();
        }

        public void NextPalettePartName()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            palettePartNameIndex = UFE2FTEPaletteSwapSpriteManager.instance.PaletteEditorSpriteGetNextPalettePartIndex(characterGameObject.characterName, palettePartNameIndex);
        }

        public void PreviousPalettePartName()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            palettePartNameIndex = UFE2FTEPaletteSwapSpriteManager.instance.PaletteEditorSpriteGetNextPalettePartIndex(characterGameObject.characterName, palettePartNameIndex);
        }

        private string GetPalettePartName()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return "";

            return UFE2FTEPaletteSwapSpriteManager.instance.PaletteEditorSpriteGetPalettePartName(characterGameObject.characterName, palettePartNameIndex);
        }

        #endregion

        #region Color Enums

        private enum ColorNameType
        {
            BlackAndGreyColors,
            WhiteColors,
            RedColors,
            GreenColors,
            BlueColors,
            OrangeColors,
            YellowColors,
            PurpleColors,
            PinkColors,
            BrownColors,
            CyanColors,
            GoldColors
        }

        private enum ColorNameBlackAndGray
        {
            Black,
            DarkSlateGray,
            DimGray,
            SlateGray,
            Gray,
            LightSlateGray,
            DarkGray,
            Silver,
            LightGray,
            Gainsboro
        }

        private enum ColorNameWhite
        {
            White,
            Ivory,
            Snow,
            MintCream,
            Azure,
            FloralWhite,
            Honeydew,
            GhostWhite,
            Seashell,
            AliceBlue,
            OldLace,
            LavenderBlush,
            WhiteSmoke,
            Beige,
            Linen,
            AntiqueWhite,
            MistyRose
        }

        private enum ColorNameRed
        {
            Red,
            DarkRed,
            LightRed,
            Firebrick,
            Crimson,
            IndianRed,
            LightCoral,
            Salmon,
            DarkSalmon,
            LightSalmon,
            Cardinal,
            Carmine,
            RustyRed,
            ImperialRed,
            FireEngineRed,
            Vermilion,
            Cinnabar,
            Jasper
        }

        private enum ColorNameGreen
        {
            Green,
            DarkGreen,
            LightGreen,
            DarkOliveGreen,
            ForestGreen,
            SeaGreen,
            Olive,
            OliveDrab,
            MediumSeaGreen,
            LimeGreen,
            Lime,
            SpringGreen,
            MediumSpringGreen,
            DarkSeaGreen,
            YellowGreen,
            LawnGreen,
            Chartreuse,
            GreenYellow,
            PaleGreen
        }

        private enum ColorNameBlue
        {
            Blue,
            DarkBlue,
            LightBlue,
            Navy,
            MediumBlue,
            MidnightBlue,
            RoyalBlue,
            SteelBlue,
            DodgerBlue,
            DeepSkyBlue,
            CornflowerBlue,
            SkyBlue,
            LightSkyBlue,
            LightSteelBlue,
            PowderBlue
        }

        private enum ColorNameOrange
        {
            Orange,
            OrangeRed,
            DarkOrange,
            Tomato,
            Coral,
            SafetyOrange,
            AerospaceOrange,
            Xanthous,
            CarrotOrange,
            OrangePeel,
            Tangerine,
            Pumpkin,
            Tangelo,
            Saffron,
            Persimmon
        }

        private enum ColorNameYellow
        {
            Yellow,
            DarkKhaki,
            Khaki,
            PeachPuff,
            PaleGoldenrod,
            Moccasin,
            Papayawhip,
            LightGoldenrodYellow,
            LemonChiffon,
            LightYellow,
            LightYellow1,
            LightYellow2,
            LightYellow3,
            LightYellow4,
            DarkYellow1,
            DarkYellow2,
            DarkYellow3,
            DarkYellow4
        }

        private enum ColorNamePurple
        {
            Purple,
            Indigo,
            DarkMagenta,
            DarkViolet,
            DarkSlateBlue,
            BlueViolet,
            DarkOrchid,
            Magenta,
            SlateBlue,
            MediumSlateBlue,
            MediumOrchid,
            MediumPurple,
            Orchid,
            Violet,
            Plum,
            Thistle,
            Lavender
        }

        private enum ColorNamePink
        {
            Pink,
            LightPink,
            HotPink,
            PaleVioletRed,
            DeepPink,
            MediumVioletRed,
            PinkLace,
            PiggyPink,
            BabyPink,
            OrchidPink,
            CherryBlossomPink,
            LightHotPink,
            CoralPink,
            FandangoPink,
            PersianPink,
            LightDeepPink,
            UltraPink,
            ShockingPink,
            SuperPink,
            SteelPink,
            BubblegumPink
        }

        private enum ColorNameBrown
        {
            Brown,
            Maroon,
            SaddleBrown,
            Sienna,
            Chocolate,
            DarkGoldenrod,
            Peru,
            RosyBrown,
            Goldenrod,
            SandyBrown,
            Tan,
            Burlywood,
            Wheat,
            NavajoWhite,
            Bisque,
            BlanchedAlmond,
            Cornsilk
        }

        private enum ColorNameCyan
        {
            Cyan,
            Teal,
            DarkCyan,
            LightSeaGreen,
            CadetBlue,
            DarkTurquoise,
            MediumTurquoise,
            Turquoise,
            Aquamarine,
            MediumAquamarine,
            PaleTurquoise,
            LightCyan
        }

        private enum ColorNameGold
        {
            Gold,
            GoldenYellow,
            MetallicGold,
            OldGold,
            VegasGold,
            PaleGold,
            GoldenBrown
        }

        #endregion

        #region Color Name Type Methods

        private void SetColorNameTypeText()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || colorNameTypeText == null) return;

            colorNameTypeText.text = GetColorNameTypeString(colorNameTypeOrder[colorNameTypeOrderIndex]);
        }

        public void NextColorNameType()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || colorNameTypeText == null
                || colorNameText == null) return;

            colorNameTypeOrderIndex++;

            if (colorNameTypeOrderIndex > colorNameTypeOrder.Length - 1)
            {
                colorNameTypeOrderIndex = 0;
            }

            SetColorNameTypeText();

            colorNameOrderIndex = -1;

            SetColorNameText();
        }

        public void PreviousColorNameType()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || colorNameTypeText == null
                || colorNameText == null) return;

            colorNameTypeOrderIndex--;

            if (colorNameTypeOrderIndex < 0)
            {
                colorNameTypeOrderIndex = colorNameTypeOrder.Length - 1;
            }

            SetColorNameTypeText();

            colorNameOrderIndex = -1;

            SetColorNameText();
        }

        private string GetColorNameTypeString(ColorNameType colorNameType)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null) return "";

            switch (colorNameType)
            {
                case ColorNameType.BlackAndGreyColors:
                    return ConvertStringByLetterCaseType("Black Colors", colorNameTypeLetterCaseType);

                case ColorNameType.WhiteColors:
                    return ConvertStringByLetterCaseType("White Colors", colorNameTypeLetterCaseType);

                case ColorNameType.RedColors:
                    return ConvertStringByLetterCaseType("Red Colors", colorNameTypeLetterCaseType);

                case ColorNameType.GreenColors:
                    return ConvertStringByLetterCaseType("Green Colors", colorNameTypeLetterCaseType);

                case ColorNameType.BlueColors:
                    return ConvertStringByLetterCaseType("Blue Colors", colorNameTypeLetterCaseType);

                case ColorNameType.OrangeColors:
                    return ConvertStringByLetterCaseType("Orange Colors", colorNameTypeLetterCaseType);

                case ColorNameType.YellowColors:
                    return ConvertStringByLetterCaseType("Yellow Colors", colorNameTypeLetterCaseType);

                case ColorNameType.PurpleColors:
                    return ConvertStringByLetterCaseType("Purple Colors", colorNameTypeLetterCaseType);

                case ColorNameType.PinkColors:
                    return ConvertStringByLetterCaseType("Pink Colors", colorNameTypeLetterCaseType);

                case ColorNameType.BrownColors:
                    return ConvertStringByLetterCaseType("Brown Colors", colorNameTypeLetterCaseType);

                case ColorNameType.CyanColors:
                    return ConvertStringByLetterCaseType("Cyan Colors", colorNameTypeLetterCaseType);

                case ColorNameType.GoldColors:
                    return ConvertStringByLetterCaseType("Gold Colors", colorNameTypeLetterCaseType);
            }

            return "";
        }

        #endregion

        #region Color Name Methods

        private void SetColorNameText()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                && colorNameText == null) return;

            colorNameText.text = GetColorName(colorNameTypeOrder[colorNameTypeOrderIndex]);
        }

        public void NextColorName()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || colorNameTypeText == null
                || colorNameText == null) return;

            colorNameOrderIndex++;

            switch (colorNameTypeOrder[colorNameTypeOrderIndex])
            {
                case ColorNameType.BlackAndGreyColors:
                    if (colorNameOrderIndex > colorNameBlackAndGreyOrder.Length - 1)
                    {
                        colorNameOrderIndex = 0;
                    }
                    break;

                case ColorNameType.WhiteColors:
                    if (colorNameOrderIndex > colorNameWhiteOrder.Length - 1)
                    {
                        colorNameOrderIndex = 0;
                    }
                    break;

                case ColorNameType.RedColors:
                    if (colorNameOrderIndex > colorNameRedOrder.Length - 1)
                    {
                        colorNameOrderIndex = 0;
                    }
                    break;

                case ColorNameType.GreenColors:
                    if (colorNameOrderIndex > colorNameGreenOrder.Length - 1)
                    {
                        colorNameOrderIndex = 0;
                    }
                    break;

                case ColorNameType.BlueColors:
                    if (colorNameOrderIndex > colorNameBlueOrder.Length - 1)
                    {
                        colorNameOrderIndex = 0;
                    }
                    break;

                case ColorNameType.OrangeColors:
                    if (colorNameOrderIndex > colorNameOrangeOrder.Length - 1)
                    {
                        colorNameOrderIndex = 0;
                    }
                    break;

                case ColorNameType.YellowColors:
                    if (colorNameOrderIndex > colorNameYellowOrder.Length - 1)
                    {
                        colorNameOrderIndex = 0;
                    }
                    break;

                case ColorNameType.PurpleColors:
                    if (colorNameOrderIndex > colorNamePurpleOrder.Length - 1)
                    {
                        colorNameOrderIndex = 0;
                    }
                    break;

                case ColorNameType.PinkColors:
                    if (colorNameOrderIndex > colorNamePinkOrder.Length - 1)
                    {
                        colorNameOrderIndex = 0;
                    }
                    break;

                case ColorNameType.BrownColors:
                    if (colorNameOrderIndex > colorNameBrownOrder.Length - 1)
                    {
                        colorNameOrderIndex = 0;
                    }
                    break;

                case ColorNameType.CyanColors:
                    if (colorNameOrderIndex > colorNameCyanOrder.Length - 1)
                    {
                        colorNameOrderIndex = 0;
                    }
                    break;

                case ColorNameType.GoldColors:
                    if (colorNameOrderIndex > colorNameGoldOrder.Length - 1)
                    {
                        colorNameOrderIndex = 0;
                    }
                    break;
            }

            SetColorNameText();

            ApplySingleColor(GetColor(colorNameTypeOrder[colorNameTypeOrderIndex]));
        }

        public void PreviousColorName()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || colorNameTypeText == null
                || colorNameText == null) return;

            colorNameOrderIndex--;

            switch (colorNameTypeOrder[colorNameTypeOrderIndex])
            {
                case ColorNameType.BlackAndGreyColors:
                    if (colorNameOrderIndex < 0)
                    {
                        colorNameOrderIndex = colorNameBlackAndGreyOrder.Length - 1;
                    }
                    break;

                case ColorNameType.WhiteColors:
                    if (colorNameOrderIndex < 0)
                    {
                        colorNameOrderIndex = colorNameWhiteOrder.Length - 1;
                    }
                    break;

                case ColorNameType.RedColors:
                    if (colorNameOrderIndex < 0)
                    {
                        colorNameOrderIndex = colorNameRedOrder.Length - 1;
                    }
                    break;

                case ColorNameType.GreenColors:
                    if (colorNameOrderIndex < 0)
                    {
                        colorNameOrderIndex = colorNameGreenOrder.Length - 1;
                    }
                    break;

                case ColorNameType.BlueColors:
                    if (colorNameOrderIndex < 0)
                    {
                        colorNameOrderIndex = colorNameBlueOrder.Length - 1;
                    }
                    break;

                case ColorNameType.OrangeColors:
                    if (colorNameOrderIndex < 0)
                    {
                        colorNameOrderIndex = colorNameOrangeOrder.Length - 1;
                    }
                    break;

                case ColorNameType.YellowColors:
                    if (colorNameOrderIndex < 0)
                    {
                        colorNameOrderIndex = colorNameYellowOrder.Length - 1;
                    }
                    break;

                case ColorNameType.PurpleColors:
                    if (colorNameOrderIndex < 0)
                    {
                        colorNameOrderIndex = colorNamePurpleOrder.Length - 1;
                    }
                    break;

                case ColorNameType.PinkColors:
                    if (colorNameOrderIndex < 0)
                    {
                        colorNameOrderIndex = colorNamePinkOrder.Length - 1;
                    }
                    break;

                case ColorNameType.BrownColors:
                    if (colorNameOrderIndex < 0)
                    {
                        colorNameOrderIndex = colorNameBrownOrder.Length - 1;
                    }
                    break;

                case ColorNameType.CyanColors:
                    if (colorNameOrderIndex < 0)
                    {
                        colorNameOrderIndex = colorNameCyanOrder.Length - 1;
                    }
                    break;

                case ColorNameType.GoldColors:
                    if (colorNameOrderIndex < 0)
                    {
                        colorNameOrderIndex = colorNameGoldOrder.Length - 1;
                    }
                    break;
            }

            SetColorNameText();

            ApplySingleColor(GetColor(colorNameTypeOrder[colorNameTypeOrderIndex]));
        }

        private string GetColorName(ColorNameType colorNameType)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null) return "";

            if (colorNameOrderIndex < 0)
            {
                return noColorName;
            }

            switch (colorNameType)
            {
                case ColorNameType.BlackAndGreyColors:
                    switch (colorNameBlackAndGreyOrder[colorNameOrderIndex])
                    {
                        case ColorNameBlackAndGray.Black:
                            return ConvertStringByLetterCaseType("Black", colorNameLetterCaseType);

                        case ColorNameBlackAndGray.DarkSlateGray:
                            return ConvertStringByLetterCaseType("Dark Slate Gray", colorNameLetterCaseType);

                        case ColorNameBlackAndGray.DimGray:
                            return ConvertStringByLetterCaseType("Dim Gray", colorNameLetterCaseType);

                        case ColorNameBlackAndGray.SlateGray:
                            return ConvertStringByLetterCaseType("Slate Gray", colorNameLetterCaseType);

                        case ColorNameBlackAndGray.Gray:
                            return ConvertStringByLetterCaseType("Gray", colorNameLetterCaseType);

                        case ColorNameBlackAndGray.LightSlateGray:
                            return ConvertStringByLetterCaseType("Light Slate Gray", colorNameLetterCaseType);

                        case ColorNameBlackAndGray.DarkGray:
                            return ConvertStringByLetterCaseType("Dark Gray", colorNameLetterCaseType);

                        case ColorNameBlackAndGray.Silver:
                            return ConvertStringByLetterCaseType("Silver", colorNameLetterCaseType);

                        case ColorNameBlackAndGray.LightGray:
                            return ConvertStringByLetterCaseType("Light Gray", colorNameLetterCaseType);

                        case ColorNameBlackAndGray.Gainsboro:
                            return ConvertStringByLetterCaseType("Gainsboro", colorNameLetterCaseType);
                    }
                    break;

                case ColorNameType.WhiteColors:
                    switch (colorNameWhiteOrder[colorNameOrderIndex])
                    {
                        case ColorNameWhite.White:
                            return ConvertStringByLetterCaseType("White", colorNameLetterCaseType);

                        case ColorNameWhite.Ivory:
                            return ConvertStringByLetterCaseType("Ivory", colorNameLetterCaseType);

                        case ColorNameWhite.Snow:
                            return ConvertStringByLetterCaseType("Snow", colorNameLetterCaseType);

                        case ColorNameWhite.MintCream:
                            return ConvertStringByLetterCaseType("Mint Cream", colorNameLetterCaseType);

                        case ColorNameWhite.Azure:
                            return ConvertStringByLetterCaseType("Azure", colorNameLetterCaseType);

                        case ColorNameWhite.FloralWhite:
                            return ConvertStringByLetterCaseType("Floral White", colorNameLetterCaseType);

                        case ColorNameWhite.Honeydew:
                            return ConvertStringByLetterCaseType("Honeydew", colorNameLetterCaseType);

                        case ColorNameWhite.GhostWhite:
                            return ConvertStringByLetterCaseType("Ghost White", colorNameLetterCaseType);

                        case ColorNameWhite.Seashell:
                            return ConvertStringByLetterCaseType("Seashell", colorNameLetterCaseType);

                        case ColorNameWhite.AliceBlue:
                            return ConvertStringByLetterCaseType("Alice Blue", colorNameLetterCaseType);

                        case ColorNameWhite.OldLace:
                            return ConvertStringByLetterCaseType("Old Lace", colorNameLetterCaseType);

                        case ColorNameWhite.LavenderBlush:
                            return ConvertStringByLetterCaseType("Lavender Blush", colorNameLetterCaseType);

                        case ColorNameWhite.WhiteSmoke:
                            return ConvertStringByLetterCaseType("White Smoke", colorNameLetterCaseType);

                        case ColorNameWhite.Beige:
                            return ConvertStringByLetterCaseType("Beige", colorNameLetterCaseType);

                        case ColorNameWhite.Linen:
                            return ConvertStringByLetterCaseType("Linen", colorNameLetterCaseType);

                        case ColorNameWhite.AntiqueWhite:
                            return ConvertStringByLetterCaseType("Antique White", colorNameLetterCaseType);

                        case ColorNameWhite.MistyRose:
                            return ConvertStringByLetterCaseType("Misty Rose", colorNameLetterCaseType);
                    }
                    break;

                case ColorNameType.RedColors:
                    switch (colorNameRedOrder[colorNameOrderIndex])
                    {
                        case ColorNameRed.Red:
                            return ConvertStringByLetterCaseType("Red", colorNameLetterCaseType);

                        case ColorNameRed.DarkRed:
                            return ConvertStringByLetterCaseType("Dark Red", colorNameLetterCaseType);

                        case ColorNameRed.LightRed:
                            return ConvertStringByLetterCaseType("Light Red", colorNameLetterCaseType);

                        case ColorNameRed.Firebrick:
                            return ConvertStringByLetterCaseType("Firebrick", colorNameLetterCaseType);

                        case ColorNameRed.Crimson:
                            return ConvertStringByLetterCaseType("Crimson", colorNameLetterCaseType);

                        case ColorNameRed.IndianRed:
                            return ConvertStringByLetterCaseType("Indian Red", colorNameLetterCaseType);

                        case ColorNameRed.LightCoral:
                            return ConvertStringByLetterCaseType("Light Coral", colorNameLetterCaseType);

                        case ColorNameRed.Salmon:
                            return ConvertStringByLetterCaseType("Salmon", colorNameLetterCaseType);

                        case ColorNameRed.DarkSalmon:
                            return ConvertStringByLetterCaseType("Dark Salmon", colorNameLetterCaseType);

                        case ColorNameRed.LightSalmon:
                            return ConvertStringByLetterCaseType("Light Salmon", colorNameLetterCaseType);

                        case ColorNameRed.Cardinal:
                            return ConvertStringByLetterCaseType("Cardinal", colorNameLetterCaseType);

                        case ColorNameRed.Carmine:
                            return ConvertStringByLetterCaseType("Carmine", colorNameLetterCaseType);

                        case ColorNameRed.RustyRed:
                            return ConvertStringByLetterCaseType("Rusty Red", colorNameLetterCaseType);

                        case ColorNameRed.ImperialRed:
                            return ConvertStringByLetterCaseType("Imperial", colorNameLetterCaseType);

                        case ColorNameRed.FireEngineRed:
                            return ConvertStringByLetterCaseType("Fire Engine Red", colorNameLetterCaseType);

                        case ColorNameRed.Vermilion:
                            return ConvertStringByLetterCaseType("Vermilion", colorNameLetterCaseType);

                        case ColorNameRed.Cinnabar:
                            return ConvertStringByLetterCaseType("Cinnabar", colorNameLetterCaseType);

                        case ColorNameRed.Jasper:
                            return ConvertStringByLetterCaseType("Jasper", colorNameLetterCaseType);
                    }
                    break;

                case ColorNameType.GreenColors:
                    switch (colorNameGreenOrder[colorNameOrderIndex])
                    {
                        case ColorNameGreen.Green:
                            return ConvertStringByLetterCaseType("Green", colorNameLetterCaseType);

                        case ColorNameGreen.DarkGreen:
                            return ConvertStringByLetterCaseType("Dark Green", colorNameLetterCaseType);

                        case ColorNameGreen.LightGreen:
                            return ConvertStringByLetterCaseType("Light Green", colorNameLetterCaseType);

                        case ColorNameGreen.DarkOliveGreen:
                            return ConvertStringByLetterCaseType("Dark Olive Green", colorNameLetterCaseType);

                        case ColorNameGreen.ForestGreen:
                            return ConvertStringByLetterCaseType("Forest Green", colorNameLetterCaseType);

                        case ColorNameGreen.SeaGreen:
                            return ConvertStringByLetterCaseType("Sea Green", colorNameLetterCaseType);

                        case ColorNameGreen.Olive:
                            return ConvertStringByLetterCaseType("Olive", colorNameLetterCaseType);

                        case ColorNameGreen.OliveDrab:
                            return ConvertStringByLetterCaseType("Olive Drab", colorNameLetterCaseType);

                        case ColorNameGreen.MediumSeaGreen:
                            return ConvertStringByLetterCaseType("Medium Sea Green", colorNameLetterCaseType);

                        case ColorNameGreen.LimeGreen:
                            return ConvertStringByLetterCaseType("Lime Green", colorNameLetterCaseType);

                        case ColorNameGreen.Lime:
                            return ConvertStringByLetterCaseType("Lime", colorNameLetterCaseType);

                        case ColorNameGreen.SpringGreen:
                            return ConvertStringByLetterCaseType("Spring Green", colorNameLetterCaseType);

                        case ColorNameGreen.MediumSpringGreen:
                            return ConvertStringByLetterCaseType("Medium Spring Green", colorNameLetterCaseType);

                        case ColorNameGreen.DarkSeaGreen:
                            return ConvertStringByLetterCaseType("Dark Sea Green", colorNameLetterCaseType);

                        case ColorNameGreen.YellowGreen:
                            return ConvertStringByLetterCaseType("Yellow Green", colorNameLetterCaseType);

                        case ColorNameGreen.LawnGreen:
                            return ConvertStringByLetterCaseType("Lawn Green", colorNameLetterCaseType);

                        case ColorNameGreen.Chartreuse:
                            return ConvertStringByLetterCaseType("Chartreuse", colorNameLetterCaseType);

                        case ColorNameGreen.GreenYellow:
                            return ConvertStringByLetterCaseType("Green Yellow", colorNameLetterCaseType);

                        case ColorNameGreen.PaleGreen:
                            return ConvertStringByLetterCaseType("Pale Green", colorNameLetterCaseType);
                    }
                    break;

                case ColorNameType.BlueColors:
                    switch (colorNameBlueOrder[colorNameOrderIndex])
                    {
                        case ColorNameBlue.Blue:
                            return ConvertStringByLetterCaseType("Blue", colorNameLetterCaseType);

                        case ColorNameBlue.DarkBlue:
                            return ConvertStringByLetterCaseType("Dark Blue", colorNameLetterCaseType);

                        case ColorNameBlue.LightBlue:
                            return ConvertStringByLetterCaseType("Light Blue", colorNameLetterCaseType);

                        case ColorNameBlue.Navy:
                            return ConvertStringByLetterCaseType("Navy", colorNameLetterCaseType);

                        case ColorNameBlue.MediumBlue:
                            return ConvertStringByLetterCaseType("Medium Blue", colorNameLetterCaseType);

                        case ColorNameBlue.MidnightBlue:
                            return ConvertStringByLetterCaseType("Midnight Blue", colorNameLetterCaseType);

                        case ColorNameBlue.RoyalBlue:
                            return ConvertStringByLetterCaseType("Royal Blue", colorNameLetterCaseType);

                        case ColorNameBlue.SteelBlue:
                            return ConvertStringByLetterCaseType("Steel Blue", colorNameLetterCaseType);

                        case ColorNameBlue.DodgerBlue:
                            return ConvertStringByLetterCaseType("Dodger Blue", colorNameLetterCaseType);

                        case ColorNameBlue.DeepSkyBlue:
                            return ConvertStringByLetterCaseType("Deep Sky Blue", colorNameLetterCaseType);

                        case ColorNameBlue.CornflowerBlue:
                            return ConvertStringByLetterCaseType("Cornflower Blue", colorNameLetterCaseType);

                        case ColorNameBlue.SkyBlue:
                            return ConvertStringByLetterCaseType("Sky Blue", colorNameLetterCaseType);

                        case ColorNameBlue.LightSkyBlue:
                            return ConvertStringByLetterCaseType("Light Sky Blue", colorNameLetterCaseType);

                        case ColorNameBlue.LightSteelBlue:
                            return ConvertStringByLetterCaseType("Light Steel Blue", colorNameLetterCaseType);

                        case ColorNameBlue.PowderBlue:
                            return ConvertStringByLetterCaseType("Powder Blue", colorNameLetterCaseType);
                    }
                    break;

                case ColorNameType.OrangeColors:
                    switch (colorNameOrangeOrder[colorNameOrderIndex])
                    {
                        case ColorNameOrange.Orange:
                            return ConvertStringByLetterCaseType("Orange", colorNameLetterCaseType);

                        case ColorNameOrange.OrangeRed:
                            return ConvertStringByLetterCaseType("Orange Red", colorNameLetterCaseType);

                        case ColorNameOrange.DarkOrange:
                            return ConvertStringByLetterCaseType("Dark Orange", colorNameLetterCaseType);

                        case ColorNameOrange.Tomato:
                            return ConvertStringByLetterCaseType("Tomato", colorNameLetterCaseType);

                        case ColorNameOrange.Coral:
                            return ConvertStringByLetterCaseType("Coral", colorNameLetterCaseType);

                        case ColorNameOrange.SafetyOrange:
                            return ConvertStringByLetterCaseType("Safety Orange", colorNameLetterCaseType);

                        case ColorNameOrange.AerospaceOrange:
                            return ConvertStringByLetterCaseType("Areospace Orange", colorNameLetterCaseType);

                        case ColorNameOrange.Xanthous:
                            return ConvertStringByLetterCaseType("Xanthous", colorNameLetterCaseType);

                        case ColorNameOrange.CarrotOrange:
                            return ConvertStringByLetterCaseType("Carrot Orange", colorNameLetterCaseType);

                        case ColorNameOrange.OrangePeel:
                            return ConvertStringByLetterCaseType("Orange Peel", colorNameLetterCaseType);

                        case ColorNameOrange.Tangerine:
                            return ConvertStringByLetterCaseType("Tangerine", colorNameLetterCaseType);

                        case ColorNameOrange.Pumpkin:
                            return ConvertStringByLetterCaseType("Pumpkin", colorNameLetterCaseType);

                        case ColorNameOrange.Tangelo:
                            return ConvertStringByLetterCaseType("Tangelo", colorNameLetterCaseType);

                        case ColorNameOrange.Saffron:
                            return ConvertStringByLetterCaseType("Saffron", colorNameLetterCaseType);

                        case ColorNameOrange.Persimmon:
                            return ConvertStringByLetterCaseType("Persimmon", colorNameLetterCaseType);
                    }
                    break;

                case ColorNameType.YellowColors:
                    switch (colorNameYellowOrder[colorNameOrderIndex])
                    {
                        case ColorNameYellow.Yellow:
                            return ConvertStringByLetterCaseType("Yellow", colorNameLetterCaseType);

                        case ColorNameYellow.DarkKhaki:
                            return ConvertStringByLetterCaseType("Dark Khaki", colorNameLetterCaseType);

                        case ColorNameYellow.Khaki:
                            return ConvertStringByLetterCaseType("Khaki", colorNameLetterCaseType);

                        case ColorNameYellow.PeachPuff:
                            return ConvertStringByLetterCaseType("Peach Puff", colorNameLetterCaseType);

                        case ColorNameYellow.PaleGoldenrod:
                            return ConvertStringByLetterCaseType("Pale Goldenrod", colorNameLetterCaseType);

                        case ColorNameYellow.Moccasin:
                            return ConvertStringByLetterCaseType("Moccasin", colorNameLetterCaseType);

                        case ColorNameYellow.Papayawhip:
                            return ConvertStringByLetterCaseType("Papayawhip", colorNameLetterCaseType);

                        case ColorNameYellow.LightGoldenrodYellow:
                            return ConvertStringByLetterCaseType("Light Goldenrod Yellow", colorNameLetterCaseType);

                        case ColorNameYellow.LemonChiffon:
                            return ConvertStringByLetterCaseType("Lemon Chiffon", colorNameLetterCaseType);

                        case ColorNameYellow.LightYellow:
                            return ConvertStringByLetterCaseType("Light Yellow", colorNameLetterCaseType);

                        case ColorNameYellow.LightYellow1:
                            return ConvertStringByLetterCaseType("Light Yellow 1", colorNameLetterCaseType);

                        case ColorNameYellow.LightYellow2:
                            return ConvertStringByLetterCaseType("Light Yellow 2", colorNameLetterCaseType);

                        case ColorNameYellow.LightYellow3:
                            return ConvertStringByLetterCaseType("Light Yellow 3", colorNameLetterCaseType);

                        case ColorNameYellow.LightYellow4:
                            return ConvertStringByLetterCaseType("Light Yellow 4", colorNameLetterCaseType);

                        case ColorNameYellow.DarkYellow1:
                            return ConvertStringByLetterCaseType("Dark Yellow 1", colorNameLetterCaseType);

                        case ColorNameYellow.DarkYellow2:
                            return ConvertStringByLetterCaseType("Dark Yellow 2", colorNameLetterCaseType);

                        case ColorNameYellow.DarkYellow3:
                            return ConvertStringByLetterCaseType("Dark Yellow 3", colorNameLetterCaseType);

                        case ColorNameYellow.DarkYellow4:
                            return ConvertStringByLetterCaseType("Dark Yellow 4", colorNameLetterCaseType);
                    }
                    break;

                case ColorNameType.PurpleColors:
                    switch (colorNamePurpleOrder[colorNameOrderIndex])
                    {
                        case ColorNamePurple.Purple:
                            return ConvertStringByLetterCaseType("Purple", colorNameLetterCaseType);

                        case ColorNamePurple.Indigo:
                            return ConvertStringByLetterCaseType("Indigo", colorNameLetterCaseType);

                        case ColorNamePurple.DarkMagenta:
                            return ConvertStringByLetterCaseType("Dark Magenta", colorNameLetterCaseType);

                        case ColorNamePurple.DarkViolet:
                            return ConvertStringByLetterCaseType("Dark Violet", colorNameLetterCaseType);

                        case ColorNamePurple.DarkSlateBlue:
                            return ConvertStringByLetterCaseType("Dark Slate Blue", colorNameLetterCaseType);

                        case ColorNamePurple.BlueViolet:
                            return ConvertStringByLetterCaseType("Blue Violet", colorNameLetterCaseType);

                        case ColorNamePurple.DarkOrchid:
                            return ConvertStringByLetterCaseType("Dark Orchid", colorNameLetterCaseType);

                        case ColorNamePurple.Magenta:
                            return ConvertStringByLetterCaseType("Magenta", colorNameLetterCaseType);

                        case ColorNamePurple.SlateBlue:
                            return ConvertStringByLetterCaseType("Slate Blue", colorNameLetterCaseType);

                        case ColorNamePurple.MediumSlateBlue:
                            return ConvertStringByLetterCaseType("Medium Slate Blue", colorNameLetterCaseType);

                        case ColorNamePurple.MediumOrchid:
                            return ConvertStringByLetterCaseType("Medium Orchid", colorNameLetterCaseType);

                        case ColorNamePurple.MediumPurple:
                            return ConvertStringByLetterCaseType("Medium Purple", colorNameLetterCaseType);

                        case ColorNamePurple.Orchid:
                            return ConvertStringByLetterCaseType("Orchid", colorNameLetterCaseType);

                        case ColorNamePurple.Violet:
                            return ConvertStringByLetterCaseType("Violet", colorNameLetterCaseType);

                        case ColorNamePurple.Plum:
                            return ConvertStringByLetterCaseType("Plum", colorNameLetterCaseType);

                        case ColorNamePurple.Thistle:
                            return ConvertStringByLetterCaseType("Thistle", colorNameLetterCaseType);

                        case ColorNamePurple.Lavender:
                            return ConvertStringByLetterCaseType("Lavender", colorNameLetterCaseType);
                    }
                    break;

                case ColorNameType.PinkColors:
                    switch (colorNamePinkOrder[colorNameOrderIndex])
                    {
                        case ColorNamePink.Pink:
                            return ConvertStringByLetterCaseType("Pink", colorNameLetterCaseType);

                        case ColorNamePink.LightPink:
                            return ConvertStringByLetterCaseType("Light Pink", colorNameLetterCaseType);

                        case ColorNamePink.HotPink:
                            return ConvertStringByLetterCaseType("Hot Pink", colorNameLetterCaseType);

                        case ColorNamePink.PaleVioletRed:
                            return ConvertStringByLetterCaseType("Pale Violet Red", colorNameLetterCaseType);

                        case ColorNamePink.DeepPink:
                            return ConvertStringByLetterCaseType("Deep Pink", colorNameLetterCaseType);

                        case ColorNamePink.MediumVioletRed:
                            return ConvertStringByLetterCaseType("Medium Violet Red", colorNameLetterCaseType);

                        case ColorNamePink.PinkLace:
                            return ConvertStringByLetterCaseType("Pink Lace", colorNameLetterCaseType);

                        case ColorNamePink.PiggyPink:
                            return ConvertStringByLetterCaseType("Piggy Pink", colorNameLetterCaseType);

                        case ColorNamePink.BabyPink:
                            return ConvertStringByLetterCaseType("Baby Pink", colorNameLetterCaseType);

                        case ColorNamePink.OrchidPink:
                            return ConvertStringByLetterCaseType("Orchid Pink", colorNameLetterCaseType);

                        case ColorNamePink.CherryBlossomPink:
                            return ConvertStringByLetterCaseType("Cherry Blossom Pink", colorNameLetterCaseType);

                        case ColorNamePink.LightHotPink:
                            return ConvertStringByLetterCaseType("Light Hot Pink", colorNameLetterCaseType);

                        case ColorNamePink.CoralPink:
                            return ConvertStringByLetterCaseType("Coral Pink", colorNameLetterCaseType);

                        case ColorNamePink.FandangoPink:
                            return ConvertStringByLetterCaseType("Fandango Pink", colorNameLetterCaseType);

                        case ColorNamePink.PersianPink:
                            return ConvertStringByLetterCaseType("Persian Pink", colorNameLetterCaseType);

                        case ColorNamePink.LightDeepPink:
                            return ConvertStringByLetterCaseType("Light Deep Pink", colorNameLetterCaseType);

                        case ColorNamePink.UltraPink:
                            return ConvertStringByLetterCaseType("Ultra Pink", colorNameLetterCaseType);

                        case ColorNamePink.ShockingPink:
                            return ConvertStringByLetterCaseType("Shocking Pink", colorNameLetterCaseType);

                        case ColorNamePink.SuperPink:
                            return ConvertStringByLetterCaseType("Super Pink", colorNameLetterCaseType);

                        case ColorNamePink.SteelPink:
                            return ConvertStringByLetterCaseType("Steel Pink", colorNameLetterCaseType);

                        case ColorNamePink.BubblegumPink:
                            return ConvertStringByLetterCaseType("Bubblegum Pink", colorNameLetterCaseType);
                    }
                    break;

                case ColorNameType.BrownColors:
                    switch (colorNameBrownOrder[colorNameOrderIndex])
                    {
                        case ColorNameBrown.Brown:
                            return ConvertStringByLetterCaseType("Brown", colorNameLetterCaseType);

                        case ColorNameBrown.Maroon:
                            return ConvertStringByLetterCaseType("Maroon", colorNameLetterCaseType);

                        case ColorNameBrown.SaddleBrown:
                            return ConvertStringByLetterCaseType("Saddle Brown", colorNameLetterCaseType);

                        case ColorNameBrown.Sienna:
                            return ConvertStringByLetterCaseType("Sienna", colorNameLetterCaseType);

                        case ColorNameBrown.Chocolate:
                            return ConvertStringByLetterCaseType("Chocolate", colorNameLetterCaseType);

                        case ColorNameBrown.DarkGoldenrod:
                            return ConvertStringByLetterCaseType("Dark Goldenrod", colorNameLetterCaseType);

                        case ColorNameBrown.Peru:
                            return ConvertStringByLetterCaseType("Peru", colorNameLetterCaseType);

                        case ColorNameBrown.RosyBrown:
                            return ConvertStringByLetterCaseType("Rosy Brown", colorNameLetterCaseType);

                        case ColorNameBrown.Goldenrod:
                            return ConvertStringByLetterCaseType("Goldenrod", colorNameLetterCaseType);

                        case ColorNameBrown.SandyBrown:
                            return ConvertStringByLetterCaseType("Sandy Brown", colorNameLetterCaseType);

                        case ColorNameBrown.Tan:
                            return ConvertStringByLetterCaseType("Tan", colorNameLetterCaseType);

                        case ColorNameBrown.Burlywood:
                            return ConvertStringByLetterCaseType("Burlywood", colorNameLetterCaseType);

                        case ColorNameBrown.Wheat:
                            return ConvertStringByLetterCaseType("Wheat", colorNameLetterCaseType);

                        case ColorNameBrown.NavajoWhite:
                            return ConvertStringByLetterCaseType("Navajo White", colorNameLetterCaseType);

                        case ColorNameBrown.Bisque:
                            return ConvertStringByLetterCaseType("Bisque", colorNameLetterCaseType);

                        case ColorNameBrown.BlanchedAlmond:
                            return ConvertStringByLetterCaseType("Blanched Almond", colorNameLetterCaseType);

                        case ColorNameBrown.Cornsilk:
                            return ConvertStringByLetterCaseType("Cornsilk", colorNameLetterCaseType);
                    }
                    break;

                case ColorNameType.CyanColors:
                    switch (colorNameCyanOrder[colorNameOrderIndex])
                    {
                        case ColorNameCyan.Cyan:
                            return ConvertStringByLetterCaseType("Cyan", colorNameLetterCaseType);

                        case ColorNameCyan.Teal:
                            return ConvertStringByLetterCaseType("Teal", colorNameLetterCaseType);

                        case ColorNameCyan.DarkCyan:
                            return ConvertStringByLetterCaseType("Dark Cyan", colorNameLetterCaseType);

                        case ColorNameCyan.LightSeaGreen:
                            return ConvertStringByLetterCaseType("Light Sea Green", colorNameLetterCaseType);

                        case ColorNameCyan.CadetBlue:
                            return ConvertStringByLetterCaseType("Cadet Blue", colorNameLetterCaseType);

                        case ColorNameCyan.DarkTurquoise:
                            return ConvertStringByLetterCaseType("Dark Turquoise", colorNameLetterCaseType);

                        case ColorNameCyan.MediumTurquoise:
                            return ConvertStringByLetterCaseType("Medium Turquoise", colorNameLetterCaseType);

                        case ColorNameCyan.Turquoise:
                            return ConvertStringByLetterCaseType("Turquoise", colorNameLetterCaseType);

                        case ColorNameCyan.Aquamarine:
                            return ConvertStringByLetterCaseType("Aquamarine", colorNameLetterCaseType);

                        case ColorNameCyan.MediumAquamarine:
                            return ConvertStringByLetterCaseType("Medium Aquamarine", colorNameLetterCaseType);

                        case ColorNameCyan.PaleTurquoise:
                            return ConvertStringByLetterCaseType("Pale Turquoise", colorNameLetterCaseType);

                        case ColorNameCyan.LightCyan:
                            return ConvertStringByLetterCaseType("Light Cyan", colorNameLetterCaseType);
                    }
                    break;

                case ColorNameType.GoldColors:
                    switch (colorNameGoldOrder[colorNameOrderIndex])
                    {
                        case ColorNameGold.Gold:
                            return ConvertStringByLetterCaseType("Gold", colorNameLetterCaseType);

                        case ColorNameGold.GoldenYellow:
                            return ConvertStringByLetterCaseType("Golden Yellow", colorNameLetterCaseType);

                        case ColorNameGold.MetallicGold:
                            return ConvertStringByLetterCaseType("Metallic Gold", colorNameLetterCaseType);

                        case ColorNameGold.OldGold:
                            return ConvertStringByLetterCaseType("Old Gold", colorNameLetterCaseType);

                        case ColorNameGold.VegasGold:
                            return ConvertStringByLetterCaseType("Vegas Gold", colorNameLetterCaseType);

                        case ColorNameGold.PaleGold:
                            return ConvertStringByLetterCaseType("Pale Gold", colorNameLetterCaseType);

                        case ColorNameGold.GoldenBrown:
                            return ConvertStringByLetterCaseType("Golden Brown", colorNameLetterCaseType);
                    }
                    break;
            }

            return "";
        }

        private Color32 GetColor(ColorNameType colorNameType)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null) return new Color32(0, 0, 0, 255);

            Color32 previewColor = new Color32(0, 0, 0, 255);

            switch (colorNameType)
            {
                case ColorNameType.BlackAndGreyColors:
                    switch (colorNameBlackAndGreyOrder[colorNameOrderIndex])
                    {
                        case ColorNameBlackAndGray.Black:
                            previewColor.r = 0;
                            previewColor.g = 0;
                            previewColor.b = 0;
                            break;

                        case ColorNameBlackAndGray.DarkSlateGray:
                            previewColor.r = 47;
                            previewColor.g = 79;
                            previewColor.b = 79;
                            break;

                        case ColorNameBlackAndGray.DimGray:
                            previewColor.r = 105;
                            previewColor.g = 105;
                            previewColor.b = 105;
                            break;

                        case ColorNameBlackAndGray.SlateGray:
                            previewColor.r = 112;
                            previewColor.g = 128;
                            previewColor.b = 144;
                            break;

                        case ColorNameBlackAndGray.Gray:
                            previewColor.r = 128;
                            previewColor.g = 128;
                            previewColor.b = 128;
                            break;

                        case ColorNameBlackAndGray.LightSlateGray:
                            previewColor.r = 119;
                            previewColor.g = 136;
                            previewColor.b = 153;
                            break;

                        case ColorNameBlackAndGray.DarkGray:
                            previewColor.r = 169;
                            previewColor.g = 169;
                            previewColor.b = 169;
                            break;

                        case ColorNameBlackAndGray.Silver:
                            previewColor.r = 192;
                            previewColor.g = 192;
                            previewColor.b = 192;
                            break;

                        case ColorNameBlackAndGray.LightGray:
                            previewColor.r = 211;
                            previewColor.g = 211;
                            previewColor.b = 211;
                            break;

                        case ColorNameBlackAndGray.Gainsboro:
                            previewColor.r = 220;
                            previewColor.g = 220;
                            previewColor.b = 220;
                            break;
                    }
                    break;

                case ColorNameType.WhiteColors:
                    switch (colorNameWhiteOrder[colorNameOrderIndex])
                    {
                        case ColorNameWhite.White:
                            previewColor.r = 255;
                            previewColor.g = 255;
                            previewColor.b = 255;
                            break;

                        case ColorNameWhite.Ivory:
                            previewColor.r = 255;
                            previewColor.g = 255;
                            previewColor.b = 240;
                            break;

                        case ColorNameWhite.Snow:
                            previewColor.r = 250;
                            previewColor.g = 250;
                            previewColor.b = 250;
                            break;

                        case ColorNameWhite.MintCream:
                            previewColor.r = 245;
                            previewColor.g = 255;
                            previewColor.b = 250;
                            break;

                        case ColorNameWhite.Azure:
                            previewColor.r = 240;
                            previewColor.g = 255;
                            previewColor.b = 255;
                            break;

                        case ColorNameWhite.FloralWhite:
                            previewColor.r = 255;
                            previewColor.g = 250;
                            previewColor.b = 240;
                            break;

                        case ColorNameWhite.Honeydew:
                            previewColor.r = 240;
                            previewColor.g = 255;
                            previewColor.b = 240;
                            break;

                        case ColorNameWhite.GhostWhite:
                            previewColor.r = 248;
                            previewColor.g = 248;
                            previewColor.b = 255;
                            break;

                        case ColorNameWhite.Seashell:
                            previewColor.r = 255;
                            previewColor.g = 245;
                            previewColor.b = 238;
                            break;

                        case ColorNameWhite.AliceBlue:
                            previewColor.r = 240;
                            previewColor.g = 248;
                            previewColor.b = 255;
                            break;

                        case ColorNameWhite.OldLace:
                            previewColor.r = 253;
                            previewColor.g = 245;
                            previewColor.b = 230;
                            break;

                        case ColorNameWhite.LavenderBlush:
                            previewColor.r = 255;
                            previewColor.g = 240;
                            previewColor.b = 245;
                            break;

                        case ColorNameWhite.WhiteSmoke:
                            previewColor.r = 245;
                            previewColor.g = 245;
                            previewColor.b = 245;
                            break;

                        case ColorNameWhite.Beige:
                            previewColor.r = 245;
                            previewColor.g = 245;
                            previewColor.b = 220;
                            break;

                        case ColorNameWhite.Linen:
                            previewColor.r = 250;
                            previewColor.g = 240;
                            previewColor.b = 230;
                            break;

                        case ColorNameWhite.AntiqueWhite:
                            previewColor.r = 250;
                            previewColor.g = 235;
                            previewColor.b = 215;
                            break;

                        case ColorNameWhite.MistyRose:
                            previewColor.r = 255;
                            previewColor.g = 228;
                            previewColor.b = 225;
                            break;
                    }
                    break;

                case ColorNameType.RedColors:
                    switch (colorNameRedOrder[colorNameOrderIndex])
                    {
                        case ColorNameRed.Red:
                            previewColor.r = 255;
                            previewColor.g = 0;
                            previewColor.b = 0;
                            break;

                        case ColorNameRed.DarkRed:
                            previewColor.r = 139;
                            previewColor.g = 0;
                            previewColor.b = 0;
                            break;

                        case ColorNameRed.LightRed:
                            previewColor.r = 255;
                            previewColor.g = 127;
                            previewColor.b = 127;
                            break;

                        case ColorNameRed.Firebrick:
                            previewColor.r = 178;
                            previewColor.g = 34;
                            previewColor.b = 34;
                            break;

                        case ColorNameRed.Crimson:
                            previewColor.r = 220;
                            previewColor.g = 20;
                            previewColor.b = 60;
                            break;

                        case ColorNameRed.IndianRed:
                            previewColor.r = 205;
                            previewColor.g = 92;
                            previewColor.b = 92;
                            break;

                        case ColorNameRed.LightCoral:
                            previewColor.r = 240;
                            previewColor.g = 128;
                            previewColor.b = 128;
                            break;

                        case ColorNameRed.Salmon:
                            previewColor.r = 250;
                            previewColor.g = 128;
                            previewColor.b = 114;
                            break;

                        case ColorNameRed.DarkSalmon:
                            previewColor.r = 233;
                            previewColor.g = 150;
                            previewColor.b = 122;
                            break;

                        case ColorNameRed.LightSalmon:
                            previewColor.r = 255;
                            previewColor.g = 160;
                            previewColor.b = 122;
                            break;

                        case ColorNameRed.Cardinal:
                            previewColor.r = 196;
                            previewColor.g = 30;
                            previewColor.b = 58;
                            break;

                        case ColorNameRed.Carmine:
                            previewColor.r = 150;
                            previewColor.g = 0;
                            previewColor.b = 24;
                            break;

                        case ColorNameRed.RustyRed:
                            previewColor.r = 218;
                            previewColor.g = 44;
                            previewColor.b = 67;
                            break;

                        case ColorNameRed.ImperialRed:
                            previewColor.r = 237;
                            previewColor.g = 41;
                            previewColor.b = 57;
                            break;

                        case ColorNameRed.FireEngineRed:
                            previewColor.r = 206;
                            previewColor.g = 32;
                            previewColor.b = 41;
                            break;

                        case ColorNameRed.Vermilion:
                            previewColor.r = 227;
                            previewColor.g = 66;
                            previewColor.b = 52;
                            break;

                        case ColorNameRed.Cinnabar:
                            previewColor.r = 228;
                            previewColor.g = 77;
                            previewColor.b = 48;
                            break;

                        case ColorNameRed.Jasper:
                            previewColor.r = 208;
                            previewColor.g = 83;
                            previewColor.b = 64;
                            break;
                    }
                    break;

                case ColorNameType.GreenColors:
                    switch (colorNameGreenOrder[colorNameOrderIndex])
                    {
                        case ColorNameGreen.Green:
                            previewColor.r = 0;
                            previewColor.g = 128;
                            previewColor.b = 0;
                            break;

                        case ColorNameGreen.DarkGreen:
                            previewColor.r = 0;
                            previewColor.g = 100;
                            previewColor.b = 0;
                            break;

                        case ColorNameGreen.LightGreen:
                            previewColor.r = 144;
                            previewColor.g = 238;
                            previewColor.b = 144;
                            break;

                        case ColorNameGreen.DarkOliveGreen:
                            previewColor.r = 85;
                            previewColor.g = 107;
                            previewColor.b = 47;
                            break;

                        case ColorNameGreen.ForestGreen:
                            previewColor.r = 34;
                            previewColor.g = 139;
                            previewColor.b = 34;
                            break;

                        case ColorNameGreen.SeaGreen:
                            previewColor.r = 46;
                            previewColor.g = 139;
                            previewColor.b = 87;
                            break;

                        case ColorNameGreen.Olive:
                            previewColor.r = 128;
                            previewColor.g = 128;
                            previewColor.b = 0;
                            break;

                        case ColorNameGreen.OliveDrab:
                            previewColor.r = 107;
                            previewColor.g = 142;
                            previewColor.b = 35;
                            break;

                        case ColorNameGreen.MediumSeaGreen:
                            previewColor.r = 60;
                            previewColor.g = 179;
                            previewColor.b = 113;
                            break;

                        case ColorNameGreen.LimeGreen:
                            previewColor.r = 50;
                            previewColor.g = 205;
                            previewColor.b = 50;
                            break;

                        case ColorNameGreen.Lime:
                            previewColor.r = 0;
                            previewColor.g = 255;
                            previewColor.b = 0;
                            break;

                        case ColorNameGreen.SpringGreen:
                            previewColor.r = 0;
                            previewColor.g = 255;
                            previewColor.b = 127;
                            break;

                        case ColorNameGreen.MediumSpringGreen:
                            previewColor.r = 0;
                            previewColor.g = 250;
                            previewColor.b = 154;
                            break;

                        case ColorNameGreen.DarkSeaGreen:
                            previewColor.r = 143;
                            previewColor.g = 188;
                            previewColor.b = 143;
                            break;

                        case ColorNameGreen.YellowGreen:
                            previewColor.r = 154;
                            previewColor.g = 205;
                            previewColor.b = 50;
                            break;

                        case ColorNameGreen.LawnGreen:
                            previewColor.r = 124;
                            previewColor.g = 252;
                            previewColor.b = 0;
                            break;

                        case ColorNameGreen.Chartreuse:
                            previewColor.r = 127;
                            previewColor.g = 255;
                            previewColor.b = 0;
                            break;

                        case ColorNameGreen.GreenYellow:
                            previewColor.r = 173;
                            previewColor.g = 255;
                            previewColor.b = 47;
                            break;

                        case ColorNameGreen.PaleGreen:
                            previewColor.r = 152;
                            previewColor.g = 251;
                            previewColor.b = 152;
                            break;
                    }
                    break;

                case ColorNameType.BlueColors:
                    switch (colorNameBlueOrder[colorNameOrderIndex])
                    {
                        case ColorNameBlue.Blue:
                            previewColor.r = 0;
                            previewColor.g = 0;
                            previewColor.b = 255;
                            break;

                        case ColorNameBlue.DarkBlue:
                            previewColor.r = 0;
                            previewColor.g = 0;
                            previewColor.b = 139;
                            break;

                        case ColorNameBlue.LightBlue:
                            previewColor.r = 173;
                            previewColor.g = 216;
                            previewColor.b = 230;
                            break;

                        case ColorNameBlue.Navy:
                            previewColor.r = 0;
                            previewColor.g = 0;
                            previewColor.b = 128;
                            break;

                        case ColorNameBlue.MediumBlue:
                            previewColor.r = 0;
                            previewColor.g = 0;
                            previewColor.b = 205;
                            break;

                        case ColorNameBlue.MidnightBlue:
                            previewColor.r = 25;
                            previewColor.g = 25;
                            previewColor.b = 112;
                            break;

                        case ColorNameBlue.RoyalBlue:
                            previewColor.r = 65;
                            previewColor.g = 105;
                            previewColor.b = 225;
                            break;

                        case ColorNameBlue.SteelBlue:
                            previewColor.r = 70;
                            previewColor.g = 130;
                            previewColor.b = 180;
                            break;

                        case ColorNameBlue.DodgerBlue:
                            previewColor.r = 30;
                            previewColor.g = 144;
                            previewColor.b = 255;
                            break;

                        case ColorNameBlue.DeepSkyBlue:
                            previewColor.r = 0;
                            previewColor.g = 191;
                            previewColor.b = 255;
                            break;

                        case ColorNameBlue.CornflowerBlue:
                            previewColor.r = 100;
                            previewColor.g = 149;
                            previewColor.b = 237;
                            break;

                        case ColorNameBlue.SkyBlue:
                            previewColor.r = 135;
                            previewColor.g = 206;
                            previewColor.b = 235;
                            break;

                        case ColorNameBlue.LightSkyBlue:
                            previewColor.r = 135;
                            previewColor.g = 206;
                            previewColor.b = 250;
                            break;

                        case ColorNameBlue.LightSteelBlue:
                            previewColor.r = 176;
                            previewColor.g = 196;
                            previewColor.b = 222;
                            break;

                        case ColorNameBlue.PowderBlue:
                            previewColor.r = 176;
                            previewColor.g = 224;
                            previewColor.b = 230;
                            break;
                    }
                    break;

                case ColorNameType.OrangeColors:
                    switch (colorNameOrangeOrder[colorNameOrderIndex])
                    {
                        case ColorNameOrange.Orange:
                            previewColor.r = 255;
                            previewColor.g = 165;
                            previewColor.b = 0;
                            break;

                        case ColorNameOrange.OrangeRed:
                            previewColor.r = 255;
                            previewColor.g = 69;
                            previewColor.b = 0;
                            break;

                        case ColorNameOrange.DarkOrange:
                            previewColor.r = 255;
                            previewColor.g = 140;
                            previewColor.b = 0;
                            break;

                        case ColorNameOrange.Tomato:
                            previewColor.r = 255;
                            previewColor.g = 99;
                            previewColor.b = 71;
                            break;

                        case ColorNameOrange.Coral:
                            previewColor.r = 255;
                            previewColor.g = 127;
                            previewColor.b = 80;
                            break;

                        case ColorNameOrange.SafetyOrange:
                            previewColor.r = 255;
                            previewColor.g = 121;
                            previewColor.b = 0;
                            break;

                        case ColorNameOrange.AerospaceOrange:
                            previewColor.r = 255;
                            previewColor.g = 79;
                            previewColor.b = 0;
                            break;

                        case ColorNameOrange.Xanthous:
                            previewColor.r = 241;
                            previewColor.g = 180;
                            previewColor.b = 47;
                            break;

                        case ColorNameOrange.CarrotOrange:
                            previewColor.r = 237;
                            previewColor.g = 145;
                            previewColor.b = 33;
                            break;

                        case ColorNameOrange.OrangePeel:
                            previewColor.r = 255;
                            previewColor.g = 159;
                            previewColor.b = 0;
                            break;

                        case ColorNameOrange.Tangerine:
                            previewColor.r = 242;
                            previewColor.g = 133;
                            previewColor.b = 0;
                            break;

                        case ColorNameOrange.Pumpkin:
                            previewColor.r = 255;
                            previewColor.g = 117;
                            previewColor.b = 24;
                            break;

                        case ColorNameOrange.Tangelo:
                            previewColor.r = 249;
                            previewColor.g = 77;
                            previewColor.b = 0;
                            break;

                        case ColorNameOrange.Saffron:
                            previewColor.r = 244;
                            previewColor.g = 196;
                            previewColor.b = 48;
                            break;

                        case ColorNameOrange.Persimmon:
                            previewColor.r = 236;
                            previewColor.g = 88;
                            previewColor.b = 0;
                            break;
                    }
                    break;

                case ColorNameType.YellowColors:
                    switch (colorNameYellowOrder[colorNameOrderIndex])
                    {
                        case ColorNameYellow.Yellow:
                            previewColor.r = 255;
                            previewColor.g = 255;
                            previewColor.b = 0;
                            break;

                        case ColorNameYellow.DarkKhaki:
                            previewColor.r = 189;
                            previewColor.g = 183;
                            previewColor.b = 107;
                            break;

                        case ColorNameYellow.Khaki:
                            previewColor.r = 240;
                            previewColor.g = 230;
                            previewColor.b = 140;
                            break;

                        case ColorNameYellow.PeachPuff:
                            previewColor.r = 255;
                            previewColor.g = 218;
                            previewColor.b = 185;
                            break;

                        case ColorNameYellow.PaleGoldenrod:
                            previewColor.r = 238;
                            previewColor.g = 232;
                            previewColor.b = 170;
                            break;

                        case ColorNameYellow.Moccasin:
                            previewColor.r = 255;
                            previewColor.g = 228;
                            previewColor.b = 181;
                            break;

                        case ColorNameYellow.Papayawhip:
                            previewColor.r = 255;
                            previewColor.g = 239;
                            previewColor.b = 213;
                            break;

                        case ColorNameYellow.LightGoldenrodYellow:
                            previewColor.r = 250;
                            previewColor.g = 250;
                            previewColor.b = 210;
                            break;

                        case ColorNameYellow.LemonChiffon:
                            previewColor.r = 255;
                            previewColor.g = 250;
                            previewColor.b = 205;
                            break;

                        case ColorNameYellow.LightYellow:
                            previewColor.r = 255;
                            previewColor.g = 255;
                            previewColor.b = 224;
                            break;

                        case ColorNameYellow.LightYellow1:
                            previewColor.r = 255;
                            previewColor.g = 255;
                            previewColor.b = 204;
                            break;

                        case ColorNameYellow.LightYellow2:
                            previewColor.r = 255;
                            previewColor.g = 255;
                            previewColor.b = 153;
                            break;

                        case ColorNameYellow.LightYellow3:
                            previewColor.r = 255;
                            previewColor.g = 255;
                            previewColor.b = 102;
                            break;

                        case ColorNameYellow.LightYellow4:
                            previewColor.r = 255;
                            previewColor.g = 255;
                            previewColor.b = 51;
                            break;

                        case ColorNameYellow.DarkYellow1:
                            previewColor.r = 204;
                            previewColor.g = 204;
                            previewColor.b = 0;
                            break;

                        case ColorNameYellow.DarkYellow2:
                            previewColor.r = 153;
                            previewColor.g = 153;
                            previewColor.b = 0;
                            break;

                        case ColorNameYellow.DarkYellow3:
                            previewColor.r = 102;
                            previewColor.g = 102;
                            previewColor.b = 0;
                            break;

                        case ColorNameYellow.DarkYellow4:
                            previewColor.r = 51;
                            previewColor.g = 51;
                            previewColor.b = 0;
                            break;
                    }
                    break;

                case ColorNameType.PurpleColors:
                    switch (colorNamePurpleOrder[colorNameOrderIndex])
                    {
                        case ColorNamePurple.Purple:
                            previewColor.r = 128;
                            previewColor.g = 0;
                            previewColor.b = 128;
                            break;

                        case ColorNamePurple.Indigo:
                            previewColor.r = 75;
                            previewColor.g = 0;
                            previewColor.b = 130;
                            break;

                        case ColorNamePurple.DarkMagenta:
                            previewColor.r = 139;
                            previewColor.g = 0;
                            previewColor.b = 139;
                            break;

                        case ColorNamePurple.DarkViolet:
                            previewColor.r = 148;
                            previewColor.g = 0;
                            previewColor.b = 211;
                            break;

                        case ColorNamePurple.DarkSlateBlue:
                            previewColor.r = 72;
                            previewColor.g = 61;
                            previewColor.b = 139;
                            break;

                        case ColorNamePurple.BlueViolet:
                            previewColor.r = 148;
                            previewColor.g = 0;
                            previewColor.b = 226;
                            break;

                        case ColorNamePurple.DarkOrchid:
                            previewColor.r = 153;
                            previewColor.g = 50;
                            previewColor.b = 204;
                            break;

                        case ColorNamePurple.Magenta:
                            previewColor.r = 255;
                            previewColor.g = 0;
                            previewColor.b = 255;
                            break;

                        case ColorNamePurple.SlateBlue:
                            previewColor.r = 106;
                            previewColor.g = 90;
                            previewColor.b = 205;
                            break;

                        case ColorNamePurple.MediumSlateBlue:
                            previewColor.r = 123;
                            previewColor.g = 104;
                            previewColor.b = 238;
                            break;

                        case ColorNamePurple.MediumOrchid:
                            previewColor.r = 186;
                            previewColor.g = 85;
                            previewColor.b = 211;
                            break;

                        case ColorNamePurple.MediumPurple:
                            previewColor.r = 147;
                            previewColor.g = 112;
                            previewColor.b = 219;
                            break;

                        case ColorNamePurple.Orchid:
                            previewColor.r = 218;
                            previewColor.g = 112;
                            previewColor.b = 214;
                            break;

                        case ColorNamePurple.Violet:
                            previewColor.r = 238;
                            previewColor.g = 130;
                            previewColor.b = 238;
                            break;

                        case ColorNamePurple.Plum:
                            previewColor.r = 221;
                            previewColor.g = 160;
                            previewColor.b = 221;
                            break;

                        case ColorNamePurple.Thistle:
                            previewColor.r = 216;
                            previewColor.g = 191;
                            previewColor.b = 216;
                            break;

                        case ColorNamePurple.Lavender:
                            previewColor.r = 230;
                            previewColor.g = 230;
                            previewColor.b = 250;
                            break;
                    }
                    break;

                case ColorNameType.PinkColors:
                    switch (colorNamePinkOrder[colorNameOrderIndex])
                    {
                        case ColorNamePink.Pink:
                            previewColor.r = 255;
                            previewColor.g = 192;
                            previewColor.b = 203;
                            break;

                        case ColorNamePink.LightPink:
                            previewColor.r = 255;
                            previewColor.g = 182;
                            previewColor.b = 193;
                            break;

                        case ColorNamePink.HotPink:
                            previewColor.r = 255;
                            previewColor.g = 105;
                            previewColor.b = 180;
                            break;

                        case ColorNamePink.PaleVioletRed:
                            previewColor.r = 219;
                            previewColor.g = 112;
                            previewColor.b = 147;
                            break;

                        case ColorNamePink.DeepPink:
                            previewColor.r = 255;
                            previewColor.g = 20;
                            previewColor.b = 147;
                            break;

                        case ColorNamePink.MediumVioletRed:
                            previewColor.r = 199;
                            previewColor.g = 21;
                            previewColor.b = 133;
                            break;

                        case ColorNamePink.PinkLace:
                            previewColor.r = 255;
                            previewColor.g = 221;
                            previewColor.b = 244;
                            break;

                        case ColorNamePink.PiggyPink:
                            previewColor.r = 253;
                            previewColor.g = 221;
                            previewColor.b = 230;
                            break;

                        case ColorNamePink.BabyPink:
                            previewColor.r = 244;
                            previewColor.g = 194;
                            previewColor.b = 194;
                            break;

                        case ColorNamePink.OrchidPink:
                            previewColor.r = 242;
                            previewColor.g = 189;
                            previewColor.b = 205;
                            break;

                        case ColorNamePink.CherryBlossomPink:
                            previewColor.r = 255;
                            previewColor.g = 183;
                            previewColor.b = 197;
                            break;

                        case ColorNamePink.LightHotPink:
                            previewColor.r = 255;
                            previewColor.g = 179;
                            previewColor.b = 222;
                            break;

                        case ColorNamePink.CoralPink:
                            previewColor.r = 248;
                            previewColor.g = 131;
                            previewColor.b = 121;
                            break;

                        case ColorNamePink.FandangoPink:
                            previewColor.r = 222;
                            previewColor.g = 82;
                            previewColor.b = 133;
                            break;

                        case ColorNamePink.PersianPink:
                            previewColor.r = 247;
                            previewColor.g = 127;
                            previewColor.b = 190;
                            break;

                        case ColorNamePink.LightDeepPink:
                            previewColor.r = 255;
                            previewColor.g = 92;
                            previewColor.b = 205;
                            break;

                        case ColorNamePink.UltraPink:
                            previewColor.r = 255;
                            previewColor.g = 111;
                            previewColor.b = 255;
                            break;

                        case ColorNamePink.ShockingPink:
                            previewColor.r = 252;
                            previewColor.g = 15;
                            previewColor.b = 192;
                            break;

                        case ColorNamePink.SuperPink:
                            previewColor.r = 207;
                            previewColor.g = 107;
                            previewColor.b = 169;
                            break;

                        case ColorNamePink.SteelPink:
                            previewColor.r = 204;
                            previewColor.g = 51;
                            previewColor.b = 204;
                            break;

                        case ColorNamePink.BubblegumPink:
                            previewColor.r = 245;
                            previewColor.g = 128;
                            previewColor.b = 146;
                            break;
                    }
                    break;

                case ColorNameType.BrownColors:
                    switch (colorNameBrownOrder[colorNameOrderIndex])
                    {
                        case ColorNameBrown.Brown:
                            previewColor.r = 165;
                            previewColor.g = 42;
                            previewColor.b = 42;
                            break;

                        case ColorNameBrown.Maroon:
                            previewColor.r = 128;
                            previewColor.g = 0;
                            previewColor.b = 0;
                            break;

                        case ColorNameBrown.SaddleBrown:
                            previewColor.r = 139;
                            previewColor.g = 69;
                            previewColor.b = 19;
                            break;

                        case ColorNameBrown.Sienna:
                            previewColor.r = 160;
                            previewColor.g = 82;
                            previewColor.b = 45;
                            break;

                        case ColorNameBrown.Chocolate:
                            previewColor.r = 210;
                            previewColor.g = 105;
                            previewColor.b = 30;
                            break;

                        case ColorNameBrown.DarkGoldenrod:
                            previewColor.r = 184;
                            previewColor.g = 134;
                            previewColor.b = 11;
                            break;

                        case ColorNameBrown.Peru:
                            previewColor.r = 205;
                            previewColor.g = 133;
                            previewColor.b = 63;
                            break;

                        case ColorNameBrown.RosyBrown:
                            previewColor.r = 188;
                            previewColor.g = 143;
                            previewColor.b = 143;
                            break;

                        case ColorNameBrown.Goldenrod:
                            previewColor.r = 218;
                            previewColor.g = 165;
                            previewColor.b = 32;
                            break;

                        case ColorNameBrown.SandyBrown:
                            previewColor.r = 244;
                            previewColor.g = 164;
                            previewColor.b = 96;
                            break;

                        case ColorNameBrown.Tan:
                            previewColor.r = 210;
                            previewColor.g = 180;
                            previewColor.b = 140;
                            break;

                        case ColorNameBrown.Burlywood:
                            previewColor.r = 222;
                            previewColor.g = 184;
                            previewColor.b = 135;
                            break;

                        case ColorNameBrown.Wheat:
                            previewColor.r = 245;
                            previewColor.g = 222;
                            previewColor.b = 179;
                            break;

                        case ColorNameBrown.NavajoWhite:
                            previewColor.r = 255;
                            previewColor.g = 222;
                            previewColor.b = 173;
                            break;

                        case ColorNameBrown.Bisque:
                            previewColor.r = 255;
                            previewColor.g = 228;
                            previewColor.b = 196;
                            break;

                        case ColorNameBrown.BlanchedAlmond:
                            previewColor.r = 255;
                            previewColor.g = 235;
                            previewColor.b = 205;
                            break;

                        case ColorNameBrown.Cornsilk:
                            previewColor.r = 255;
                            previewColor.g = 248;
                            previewColor.b = 220;
                            break;
                    }
                    break;

                case ColorNameType.CyanColors:
                    switch (colorNameCyanOrder[colorNameOrderIndex])
                    {
                        case ColorNameCyan.Cyan:
                            previewColor.r = 0;
                            previewColor.g = 255;
                            previewColor.b = 255;
                            break;

                        case ColorNameCyan.Teal:
                            previewColor.r = 0;
                            previewColor.g = 128;
                            previewColor.b = 128;
                            break;

                        case ColorNameCyan.DarkCyan:
                            previewColor.r = 0;
                            previewColor.g = 139;
                            previewColor.b = 139;
                            break;

                        case ColorNameCyan.LightSeaGreen:
                            previewColor.r = 32;
                            previewColor.g = 178;
                            previewColor.b = 170;
                            break;

                        case ColorNameCyan.CadetBlue:
                            previewColor.r = 95;
                            previewColor.g = 158;
                            previewColor.b = 160;
                            break;

                        case ColorNameCyan.DarkTurquoise:
                            previewColor.r = 0;
                            previewColor.g = 206;
                            previewColor.b = 209;
                            break;

                        case ColorNameCyan.MediumTurquoise:
                            previewColor.r = 72;
                            previewColor.g = 209;
                            previewColor.b = 204;
                            break;

                        case ColorNameCyan.Turquoise:
                            previewColor.r = 64;
                            previewColor.g = 224;
                            previewColor.b = 208;
                            break;

                        case ColorNameCyan.Aquamarine:
                            previewColor.r = 127;
                            previewColor.g = 255;
                            previewColor.b = 212;
                            break;

                        case ColorNameCyan.MediumAquamarine:
                            previewColor.r = 102;
                            previewColor.g = 221;
                            previewColor.b = 170;
                            break;

                        case ColorNameCyan.PaleTurquoise:
                            previewColor.r = 175;
                            previewColor.g = 238;
                            previewColor.b = 238;
                            break;

                        case ColorNameCyan.LightCyan:
                            previewColor.r = 224;
                            previewColor.g = 255;
                            previewColor.b = 255;
                            break;
                    }
                    break;

                case ColorNameType.GoldColors:
                    switch (colorNameGoldOrder[colorNameOrderIndex])
                    {
                        case ColorNameGold.Gold:
                            previewColor.r = 255;
                            previewColor.g = 215;
                            previewColor.b = 0;
                            break;

                        case ColorNameGold.GoldenYellow:
                            previewColor.r = 255;
                            previewColor.g = 223;
                            previewColor.b = 0;
                            break;

                        case ColorNameGold.MetallicGold:
                            previewColor.r = 212;
                            previewColor.g = 175;
                            previewColor.b = 55;
                            break;

                        case ColorNameGold.OldGold:
                            previewColor.r = 207;
                            previewColor.g = 181;
                            previewColor.b = 59;
                            break;

                        case ColorNameGold.VegasGold:
                            previewColor.r = 197;
                            previewColor.g = 179;
                            previewColor.b = 88;
                            break;

                        case ColorNameGold.PaleGold:
                            previewColor.r = 230;
                            previewColor.g = 190;
                            previewColor.b = 138;
                            break;

                        case ColorNameGold.GoldenBrown:
                            previewColor.r = 153;
                            previewColor.g = 101;
                            previewColor.b = 21;
                            break;
                    }
                    break;
            }

            return previewColor;
        }

        #endregion

        #region Convert String By Letter Case Type Methods

        private enum LetterCaseType
        {
            None,
            Uppercase,
            Lowercase
        }

        private string ConvertStringByLetterCaseType(string anyString, LetterCaseType letterCaseType)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null) return anyString;

            switch (letterCaseType)
            {
                case LetterCaseType.None:
                    return anyString;

                case LetterCaseType.Uppercase:
                    return anyString.ToUpper();

                case LetterCaseType.Lowercase:
                    return anyString.ToLower();
            }

            return anyString;
        }

        #endregion

        #region Apply, Save Colors Methods

        public void ApplySavedColors()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null
                || UFE2FTEPaletteSwapSpriteManager.instance.PaletteEditorSpriteGetCustomSwapColors(characterGameObject.characterName, customSwapColorsNameIndex) == null) return;

            characterGameObject.SwapAllSpriteColorsWithSwapColorsArray(UFE2FTEPaletteSwapSpriteManager.instance.PaletteEditorSpriteGetCustomSwapColors(characterGameObject.characterName, customSwapColorsNameIndex), true);
        }

        public void ApplyDefaultColors()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null
                || UFE2FTEPaletteSwapSpriteManager.instance.PaletteEditorSpriteGetDefaultSwapColors(characterGameObject.characterName) == null) return;

            characterGameObject.SwapAllSpriteColorsWithSwapColorsArray(UFE2FTEPaletteSwapSpriteManager.instance.PaletteEditorSpriteGetDefaultSwapColors(characterGameObject.characterName), true);
        }

        public void ApplyRandomColorCurrentPart()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            characterGameObject.SwapSingleSpriteColorWithSwapColor(palettePartNameIndex, new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255), true);
        }

        public void ApplyRandomColorOtherParts()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null
                || UFE2FTEPaletteSwapSpriteManager.instance.PaletteEditorSpriteGetDefaultSwapColors(characterGameObject.characterName) == null) return;

            int length = UFE2FTEPaletteSwapSpriteManager.instance.PaletteEditorSpriteGetDefaultSwapColors(characterGameObject.characterName).Length;
            for (int i = 0; i < length; i++)
            {
                if (i == palettePartNameIndex) continue;

                characterGameObject.SwapSingleSpriteColorWithSwapColor(i, new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255), true);
            }
        }

        public void ApplyRandomColorAllParts()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null
                || UFE2FTEPaletteSwapSpriteManager.instance.PaletteEditorSpriteGetDefaultSwapColors(characterGameObject.characterName) == null) return;

            Color32[] newColors = new Color32[UFE2FTEPaletteSwapSpriteManager.instance.PaletteEditorSpriteGetDefaultSwapColors(characterGameObject.characterName).Length];

            int lengthA = newColors.Length;
            for (int a = 0; a < lengthA; a++)
            {
                newColors[a] = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);
            }

            characterGameObject.SwapAllSpriteColorsWithSwapColorsArray(newColors, true);
        }

        public void ApplySingleColor(Color32 color)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            characterGameObject.SwapSingleSpriteColorWithSwapColor(palettePartNameIndex, color, true);
        }

        public void SaveSwapColorsSaveData()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            UFE2FTEPaletteSwapSpriteManager.instance.PaletteEditorSpriteSetCustomSwapColors(characterGameObject.characterName, customSwapColorsNameIndex, characterGameObject.mySwapColors);
          
            UFE2FTEPaletteSwapSpriteManager.instance.SaveSwapColorsSaveData();

            UFE2FTEPaletteSwapSpriteManager.instance.SaveCharacterCustomSwapColors(characterGameObject.characterName, customSwapColorsNameIndex);
        }

        #endregion

        #region Color Picker Methods

        private bool IsColorPickerSetupValid()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || colorPickerGameObject == null
                || colorPickerOutputImage == null
                || redColorPickerSlider == null
                || redColorPickerSliderValueText == null
                || greenColorPickerSlider == null
                || greenColorPickerSliderValueText == null
                || blueColorPickerSlider == null
                || blueColorPickerSliderValueText == null) return false;

            return true;
        }

        public void EnableColorPicker()
        {
            if (IsColorPickerSetupValid() == false) return;

            SetActiveHiddenColorPickerGameObjects(false);

            colorPickerGameObject.SetActive(true);

            SelectButton(firstSelectedColorPickerButtonOnEnable);
        }

        public void DisableColorPicker()
        {
            if (IsColorPickerSetupValid() == false) return;

            SetActiveHiddenColorPickerGameObjects(true);

            colorPickerGameObject.SetActive(false);

            SelectButton(firstSelectedColorPickerButtonOnDisable);
        }

        private void SetActiveHiddenColorPickerGameObjects(bool value)
        {
            int length = hiddenColorPickerGameObjects.Length;
            for (int i = 0; i < length; i++)
            {
                if (hiddenColorPickerGameObjects == null) continue;

                hiddenColorPickerGameObjects[i].SetActive(value);
            }
        }

        private void SetColorPickerOutputImage()
        {
            if (IsColorPickerSetupValid() == false) return;

            Color32 color = new Color32((byte)redColorPickerSlider.value, (byte)greenColorPickerSlider.value, (byte)blueColorPickerSlider.value, 255);

            colorPickerOutputImage.color = color;

            if (ignoreColorPickerSliderOnValueChanged == false)
            {
                ApplySingleColor(color);
            }
        }

        private void SetAllColorPickerSliderValues()
        {
            if (IsColorPickerSetupValid() == false
                || characterGameObject == null) return;

            ignoreColorPickerSliderOnValueChanged = true;

            redColorPickerSlider.value = characterGameObject.mySwapColors[palettePartNameIndex].r;

            greenColorPickerSlider.value = characterGameObject.mySwapColors[palettePartNameIndex].g;

            blueColorPickerSlider.value = characterGameObject.mySwapColors[palettePartNameIndex].b;

            ignoreColorPickerSliderOnValueChanged = false;
        }

        public void SetRedColorPickerSliderValueText()
        {
            if (IsColorPickerSetupValid() == false) return;

            redColorPickerSliderValueText.text = redColorPickerSlider.value.ToString();

            SetColorPickerOutputImage();
        }

        public void SetGreenColorPickerSliderValueText()
        {
            if (IsColorPickerSetupValid() == false) return;

            greenColorPickerSliderValueText.text = greenColorPickerSlider.value.ToString();

            SetColorPickerOutputImage();
        }

        public void SetBlueColorPickerSliderValueText()
        {
            if (IsColorPickerSetupValid() == false) return;

            blueColorPickerSliderValueText.text = blueColorPickerSlider.value.ToString();

            SetColorPickerOutputImage();
        }

        public void IncrementRedColorPickerSlider()
        {
            if (IsColorPickerSetupValid() == false) return;

            float sliderValue = redColorPickerSlider.value += incrementAndDecrementColorPickerSliderAmount;

            if (sliderValue > 255)
            {
                sliderValue = 255;
            }

            redColorPickerSlider.value = sliderValue;
        }

        public void DecrementRedColorPickerSlider()
        {
            if (IsColorPickerSetupValid() == false) return;

            float sliderValue = redColorPickerSlider.value -= incrementAndDecrementColorPickerSliderAmount;

            if (sliderValue < 0)
            {
                sliderValue = 0;
            }

            redColorPickerSlider.value = sliderValue;
        }

        public void IncrementGreenColorPickerSlider()
        {
            if (IsColorPickerSetupValid() == false) return;

            float sliderValue = greenColorPickerSlider.value += incrementAndDecrementColorPickerSliderAmount;

            if (sliderValue > 255)
            {
                sliderValue = 255;
            }

            greenColorPickerSlider.value = sliderValue;
        }

        public void DecrementGreenColorPickerSlider()
        {
            if (IsColorPickerSetupValid() == false) return;

            float sliderValue = greenColorPickerSlider.value -= incrementAndDecrementColorPickerSliderAmount;

            if (sliderValue < 0)
            {
                sliderValue = 0;
            }

            greenColorPickerSlider.value = sliderValue;
        }

        public void IncrementBlueColorPickerSlider()
        {
            if (IsColorPickerSetupValid() == false) return;

            float sliderValue = blueColorPickerSlider.value += incrementAndDecrementColorPickerSliderAmount;

            if (sliderValue > 255)
            {
                sliderValue = 255;
            }

            blueColorPickerSlider.value = sliderValue;
        }

        public void DecrementBlueColorPickerSlider()
        {
            if (IsColorPickerSetupValid() == false) return;

            float sliderValue = blueColorPickerSlider.value -= incrementAndDecrementColorPickerSliderAmount;

            if (sliderValue < 0)
            {
                sliderValue = 0;
            }

            blueColorPickerSlider.value = sliderValue;
        }

        #endregion

        #region Loaded Swap Colors Methods

        public void NextLoadedSwapColors()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            UFE2FTEPaletteSwapSpriteManager.instance.NextLoadedSwapColors();

            if (UFE2FTEPaletteSwapSpriteManager.instance.GetLoadedSwapColors() == null) return;

            characterGameObject.SwapAllSpriteColorsWithSwapColorsArray(UFE2FTEPaletteSwapSpriteManager.instance.GetLoadedSwapColors(), true);
        }

        public void PreviousLoadedSwapColors()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || characterGameObject == null) return;

            UFE2FTEPaletteSwapSpriteManager.instance.PreviousLoadedSwapColors();

            if (UFE2FTEPaletteSwapSpriteManager.instance.GetLoadedSwapColors() == null) return;

            characterGameObject.SwapAllSpriteColorsWithSwapColorsArray(UFE2FTEPaletteSwapSpriteManager.instance.GetLoadedSwapColors(), true);
        }

        #endregion
    }
}
