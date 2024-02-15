using System;
using System.Collections.Generic;
using UFE3D;
using Unity.Collections;
using UnityEngine;

namespace FreedTerror.UFE2
{
    public class PaletteSwapSpriteController : MonoBehaviour
    {
        //TODO add code to force alpha values to 255?

        //GetPixels32(); //GC. Recommended if GC isnt an issue.
        //GetRawTextureData<Color32>(); //0 GC. Recommended if GC is an issue. Test for correct results before using.

        private static readonly int paletteTextureID = Shader.PropertyToID("_PaletteTex");
        private static readonly int swapTextureID = Shader.PropertyToID("_SwapTex");

        private static MaterialPropertyBlock _materialPropertyBlock;
        private static MaterialPropertyBlock materialPropertyBlock
        {
            get 
            {
                if (_materialPropertyBlock == null)
                {
                    _materialPropertyBlock = new MaterialPropertyBlock();
                }

                return _materialPropertyBlock;
            }
        }

        public SpriteRenderer mySpriteRenderer;
        private Texture2D mySwapTexture;
        private ControlsScript myControlsScript;
        private ProjectileMoveScript myProjectileMoveScript;

        [SerializeField]
        private UFE3D.CharacterInfo characterInfo;
        [SerializeField]
        private UFE3D.CharacterInfo[] linkedCharacterInfoArray;
        [HideInInspector]
        public UFE2Manager.Player player;

        [SerializeField]
        private TextAsset paletteTextAsset;

        [System.Serializable]
        public class SwapColors
        {
            public Color32[] swapColorsArray;
        }
        public SwapColors[] defaultSwapColorsArray;
        public SwapColors[] customSwapColorsArray;
        private Color32[] resetSwapColorsArray;
        public enum SwapColorsType
        {
            Default,
            Custom
        }
        private SwapColorsType swapColorsType;
        private int swapColorsArrayIndex;

        [System.Serializable]
        public class PaletteSwapSpriteEditorOptions
        {
            public string objectName;
            public string[] objectPartNameArray;
            public Sprite[] objectSpriteArray;
        }
        public PaletteSwapSpriteEditorOptions paletteSwapSpriteEditorOptions;

        private void Awake()
        {
            InitializeSwapTexture();
        }

        private void Start()
        {
            SetAllCustomSwapColorsArraysRandomSwapColors();

            LoadSwapColors();

            SaveSwapColors();

            myControlsScript = GetComponentInParent<ControlsScript>();
            if (myControlsScript != null)
            {
                player = (UFE2Manager.Player)myControlsScript.playerNum - 1;

                LoadSwapColorsVariables();

                ApplyCurrentSwapColors();
            }

            myProjectileMoveScript = GetComponent<ProjectileMoveScript>();
            if (myProjectileMoveScript != null
                && myProjectileMoveScript.myControlsScript != null)
            {
                player = (UFE2Manager.Player)myProjectileMoveScript.myControlsScript.playerNum - 1;

                //LoadSwapColorsVariables();

                //ApplyCurrentSwapColors();
            }
        }

        private void OnDestroy()
        {
            if (PaletteSwapSpriteSwapManager.IsSwapTextureInPool(mySwapTexture) == false)
            {
                Destroy(mySwapTexture);
            }
        }

        #region Configuration Methods

        [NaughtyAttributes.Button]
        private void AutoConfiguration()
        {
            if (characterInfo != null)
            {
                paletteSwapSpriteEditorOptions.objectName = characterInfo.characterName;
            }

            List<string> newStringList = GetListFromTextAsset(paletteTextAsset);
            if (newStringList != null)
            {
                int count = newStringList.Count;
                paletteSwapSpriteEditorOptions.objectPartNameArray = new string[count];
                for (int i = 0; i < count; i++)
                {
                    paletteSwapSpriteEditorOptions.objectPartNameArray[i] = GetPalettePartNameFromString(newStringList[i]);
                }
            }

            if (mySpriteRenderer != null)
            {
                Texture2D paletteTexture = (Texture2D)mySpriteRenderer.sharedMaterial.GetTexture(paletteTextureID);
                if (paletteTexture != null)
                {
                    int paletteTextureWidth = paletteTexture.width;

                    if (defaultSwapColorsArray.Length <= 0)
                    {
                        SwapColors newSwapColors = new SwapColors();
                        newSwapColors.swapColorsArray = GetDefaultColor32Array(paletteTextureWidth);
                        defaultSwapColorsArray = new SwapColors[1];
                        defaultSwapColorsArray[0] = newSwapColors;
                    }
                    int length = defaultSwapColorsArray.Length;
                    for (int i = 0; i < length; i++)
                    {
                        Color32[] newColorArray = GetDefaultColor32Array(paletteTextureWidth);
                        CopyAndPaste(defaultSwapColorsArray[i].swapColorsArray, newColorArray);
                        defaultSwapColorsArray[i].swapColorsArray = newColorArray;
                    }
                    Texture2D duplicateTexture = GetDuplicateTexture(paletteTexture);
                    defaultSwapColorsArray[0].swapColorsArray = duplicateTexture.GetPixels32();
                    DestroyImmediate(duplicateTexture);

                    if (customSwapColorsArray.Length <= 0)
                    {
                        SwapColors newSwapColors = new SwapColors();
                        newSwapColors.swapColorsArray = GetDefaultColor32Array(paletteTextureWidth);
                        customSwapColorsArray = new SwapColors[1];
                        customSwapColorsArray[0] = newSwapColors;
                    }
                    length = customSwapColorsArray.Length;
                    for (int i = 0; i < length; i++)
                    {
                        Color32[] newColorArray = GetDefaultColor32Array(paletteTextureWidth);
                        CopyAndPaste(customSwapColorsArray[i].swapColorsArray, newColorArray);
                        customSwapColorsArray[i].swapColorsArray = newColorArray;
                    }
                    SetAllCustomSwapColorsArraysRandomSwapColors();
                }
            }
        }

        #endregion

        #region Swap Color Methods

        [NaughtyAttributes.Button]
        private void InitializeSwapTexture()
        {
            if (mySpriteRenderer == null)
            {
                return;
            }

            Texture2D paletteTexture = (Texture2D)mySpriteRenderer.sharedMaterial.GetTexture(paletteTextureID);
            if (paletteTexture == null)
            {
                return;
            }

            if (characterInfo != null)
            {
                mySwapTexture = PaletteSwapSpriteSwapManager.GetSwapTexture(characterInfo.characterName, this);
            }

            if (mySwapTexture == null)
            {
                mySwapTexture = new Texture2D(paletteTexture.width, 1, TextureFormat.RGBA32, false, false);
                mySwapTexture.filterMode = FilterMode.Point;
                mySwapTexture.wrapMode = TextureWrapMode.Clamp;
                Texture2D duplicateTexture = GetDuplicateTexture(paletteTexture);
                mySwapTexture.SetPixels32(duplicateTexture.GetPixels32());
                DestroyImmediate(duplicateTexture);
                mySwapTexture.Apply();

                if (characterInfo != null)
                {
                    PaletteSwapSpriteSwapManager.AddSwapTexture(mySwapTexture, characterInfo.characterName, this);
                }
            }

            mySpriteRenderer.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetTexture(swapTextureID, mySwapTexture);
            mySpriteRenderer.SetPropertyBlock(materialPropertyBlock);
        }

        [NaughtyAttributes.Button]
        public void NextSwapColors()
        {
            swapColorsArrayIndex += 1;

            if (swapColorsType == SwapColorsType.Default)
            {
                if (swapColorsArrayIndex > defaultSwapColorsArray.Length - 1)
                {
                    swapColorsType = SwapColorsType.Custom;
                    swapColorsArrayIndex = 0;
                }
            }
            else
            {
                if (swapColorsArrayIndex > customSwapColorsArray.Length - 1)
                {
                    swapColorsType = SwapColorsType.Default;
                    swapColorsArrayIndex = 0;
                }
            }

            ApplyCurrentSwapColors();
        }

        [NaughtyAttributes.Button]
        public void PreviousSwapColors()
        {
            swapColorsArrayIndex -= 1;

            if (swapColorsType == SwapColorsType.Default)
            {
                if (swapColorsArrayIndex < 0)
                {
                    swapColorsType = SwapColorsType.Custom;
                    swapColorsArrayIndex = customSwapColorsArray.Length - 1;
                }
            }
            else
            {
                if (swapColorsArrayIndex < 0)
                {
                    swapColorsType = SwapColorsType.Default;
                    swapColorsArrayIndex = defaultSwapColorsArray.Length - 1;
                }
            }

            ApplyCurrentSwapColors();
        }

        public void ApplyCurrentSwapColors()
        {
            SwapAllSpriteColors(GetCurrentSwapColors());

            resetSwapColorsArray = GetCurrentSwapColors();

            SaveSwapColorsVariables();
        }

        public void SwapSingleSpriteColor(int paletteIndex, Color32 swapColor)
        {
            if (mySwapTexture == null)
            {
                return;
            }

            NativeArray<Color32> colorArray = mySwapTexture.GetRawTextureData<Color32>();
            if (paletteIndex < 0
                || paletteIndex >= colorArray.Length)
            {
                return;
            }
            colorArray[paletteIndex] = swapColor;

            mySwapTexture.SetPixelData(colorArray, 0);
            mySwapTexture.Apply();
        }

        public void SwapSingleSpriteColor(int[] paletteIndex, Color32 swapColor)
        {
            if (mySwapTexture == null
                || paletteIndex == null)
            {
                return;
            }

            int length = paletteIndex.Length;
            for (int i = 0; i < length; i++)
            {
                SwapSingleSpriteColor(paletteIndex[i], swapColor);
            }
        }

        public void SwapAllSpriteColors(Color32 swapColor)
        {
            if (mySwapTexture == null)
            {
                return;
            }

            NativeArray<Color32> colorArray = mySwapTexture.GetRawTextureData<Color32>();
            int length = colorArray.Length;
            for (int i = 0; i < length; i++)
            {
                colorArray[i] = swapColor;
            }

            mySwapTexture.SetPixelData(colorArray, 0);
            mySwapTexture.Apply();
        }

        public void SwapAllSpriteColors(Color32[] swapColor)
        {
            if (mySwapTexture == null
                || swapColor == null)
            {
                return;
            }

            NativeArray<Color32> colorArray = mySwapTexture.GetRawTextureData<Color32>();
            if (swapColor.Length != colorArray.Length)
            {
                return;
            }

            mySwapTexture.SetPixels32(swapColor);
            mySwapTexture.Apply();
        }

        public void TintSingleSpriteColor(int paletteIndex, Color32 tintColor, float tintAmount)
        {
            if (mySwapTexture == null)
            {
                return;
            }

            ResetSingleSpriteColor(paletteIndex);

            NativeArray<Color32> colorArray = mySwapTexture.GetRawTextureData<Color32>();
            if (paletteIndex < 0
                || paletteIndex >= colorArray.Length)
            {
                return;
            }
            colorArray[paletteIndex] = Color32.Lerp(colorArray[paletteIndex], tintColor, tintAmount);

            mySwapTexture.SetPixelData(colorArray, 0);
            mySwapTexture.Apply();
        }

        public void TintSingleSpriteColor(int[] paletteIndex, Color32 tintColor, float tintAmount)
        {
            if (mySwapTexture == null
                || paletteIndex == null)
            {
                return;
            }

            int length = paletteIndex.Length;
            for (int i = 0; i < length; i++)
            {
                TintSingleSpriteColor(paletteIndex[i], tintColor, tintAmount);
            }
        }

        public void TintAllSpriteColors(Color32 tintColor, float tintAmount)
        {
            if (mySwapTexture == null)
            {
                return;
            }

            ResetAllSpriteColors();

            NativeArray<Color32> colorArray = mySwapTexture.GetRawTextureData<Color32>();
            int length = colorArray.Length;
            for (int i = 0; i < length; i++)
            {
                colorArray[i] = Color32.Lerp(colorArray[i], tintColor, tintAmount);
            }

            mySwapTexture.SetPixelData(colorArray, 0);
            mySwapTexture.Apply();
        }

        public void TintAllSpriteColors(Color32[] tintColor, float tintAmount)
        {
            if (mySwapTexture == null
                || tintColor == null)
            {
                return;
            }

            NativeArray<Color32> colorArray = mySwapTexture.GetRawTextureData<Color32>();
            if (tintColor.Length != colorArray.Length)
            {
                return;
            }
            int length = colorArray.Length;
            for (int i = 0; i < length; i++)
            {
                colorArray[i] = Color32.Lerp(colorArray[i], tintColor[i], tintAmount);
            }

            mySwapTexture.SetPixelData(colorArray, 0);
            mySwapTexture.Apply();
        }

        public void ResetSingleSpriteColor(int paletteIndex)
        {
            if (mySwapTexture == null)
            {
                return;
            }

            NativeArray<Color32> colorArray = mySwapTexture.GetRawTextureData<Color32>();
            if (paletteIndex < 0
                || paletteIndex >= colorArray.Length)
            {
                return;
            }
            colorArray[paletteIndex] = resetSwapColorsArray[paletteIndex];

            mySwapTexture.SetPixelData(colorArray, 0);
            mySwapTexture.Apply();
        }

        public void ResetSingleSpriteColor(int[] paletteIndex)
        {
            if (mySwapTexture == null
                || paletteIndex == null)
            {
                return;
            }

            int length = paletteIndex.Length;
            for (int i = 0; i < length; i++)
            {
                ResetSingleSpriteColor(paletteIndex[i]);
            }
        }

        public void ResetAllSpriteColors()
        {
            if (mySwapTexture == null)
            {
                return;
            }

            mySwapTexture.SetPixels32(resetSwapColorsArray);
            mySwapTexture.Apply();
        }

        private Texture2D GetDuplicateTexture(Texture2D source)
        {
            RenderTexture renderTex = RenderTexture.GetTemporary(
                source.width,
                source.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear);

            Graphics.Blit(source, renderTex);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTex;
            Texture2D readableText = new Texture2D(source.width, source.height);
            readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableText.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);
            return readableText;
        }

        #endregion

        #region Save And Load Methods

        [NaughtyAttributes.Button]
        public void SaveSwapColors()
        {
            if (characterInfo == null)
            {
                return;
            }

            try
            {
                ES3.Save("", customSwapColorsArray, "Swap Colors/" + characterInfo.characterName + " Swap Colors/" + characterInfo.characterName + " Swap Colors" + ".es3");

                int length = customSwapColorsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    ES3.Save("", customSwapColorsArray[i].swapColorsArray, "Swap Colors/" + characterInfo.characterName + " Swap Colors/" + characterInfo.characterName + " Swap Colors " + i + ".es3");
                }
            }
            catch (Exception exception)
            {
#if UNITY_EDITOR
                Debug.Log(exception);
#endif
            }
        }

        [NaughtyAttributes.Button]
        public void LoadSwapColors()
        {
            if (characterInfo == null)
            {
                return;
            }

            try
            {
                SwapColors[] loadedSwapColorsArray = ES3.Load<SwapColors[]>("", "Swap Colors/" + characterInfo.characterName + " Swap Colors/" + characterInfo.characterName + " Swap Colors" + ".es3");
                if (loadedSwapColorsArray != null)
                {
                    int length = loadedSwapColorsArray.Length;
                    int bounds = customSwapColorsArray.Length - 1;
                    for (int i = 0; i < length; i++)
                    {
                        if (i > bounds)
                        {
                            break;
                        }

                        CopyAndPaste(loadedSwapColorsArray[i].swapColorsArray, customSwapColorsArray[i].swapColorsArray);
                    }
                }
            }
            catch (Exception exception)
            {
#if UNITY_EDITOR
                Debug.Log(exception);
#endif
            }
        }

        public List<SwapColors> GetSavedSwapColors()
        {
            if (characterInfo == null
                || ES3.DirectoryExists("Swap Colors/" + characterInfo.characterName + " Swap Colors/") == false)
            {
                return null;
            }

            List<SwapColors> newSwapColorsList = new List<SwapColors>();
            foreach (var fileName in ES3.GetFiles("Swap Colors/" + characterInfo.characterName + " Swap Colors/"))
            {
                try
                {
                    SwapColors newSwapColors = new SwapColors();
                    newSwapColors.swapColorsArray = ES3.Load<Color32[]>("", "Swap Colors/" + characterInfo.characterName + " Swap Colors/" + fileName);
                    if (newSwapColors.swapColorsArray != null)
                    {
                        newSwapColorsList.Add(newSwapColors);
                    }
                }
                catch (Exception exception)
                {
#if UNITY_EDITOR
                    Debug.Log(exception);
#endif
                }            
            }

            return newSwapColorsList;
        }

        [NaughtyAttributes.Button]
        public void OpenSaveDataLocation()
        {
            if (characterInfo == null)
            {
                return;
            }

            Application.OpenURL(Application.persistentDataPath + "\\" + "Swap Colors" + "\\" + characterInfo.characterName + " Swap Colors");
        }

        public void SaveSwapColorsVariables()
        {
            if (characterInfo == null)
            {
                return;
            }

            PlayerPrefs.SetInt(characterInfo.characterName + player + nameof(swapColorsType), (int)swapColorsType);
            PlayerPrefs.SetInt(characterInfo.characterName + player + nameof(swapColorsArrayIndex), swapColorsArrayIndex);

            int length = linkedCharacterInfoArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (linkedCharacterInfoArray[i] == null)
                {
                    continue;
                }

                PlayerPrefs.SetInt(linkedCharacterInfoArray[i].characterName + player + nameof(swapColorsType), (int)swapColorsType);
                PlayerPrefs.SetInt(linkedCharacterInfoArray[i].characterName + player + nameof(swapColorsArrayIndex), swapColorsArrayIndex);
            }
        }

        public void LoadSwapColorsVariables()
        {
            if (characterInfo == null)
            {
                return;
            }

            swapColorsType = (SwapColorsType)PlayerPrefs.GetInt(characterInfo.characterName + player + nameof(swapColorsType));
            if (System.Enum.IsDefined(typeof(SwapColorsType), swapColorsType) == false)
            {
                swapColorsType = SwapColorsType.Default;
            }

            swapColorsArrayIndex = PlayerPrefs.GetInt(characterInfo.characterName + player + nameof(swapColorsArrayIndex));
            if (swapColorsType == SwapColorsType.Default)
            {
                if (swapColorsArrayIndex >= defaultSwapColorsArray.Length)
                {
                    swapColorsArrayIndex = 0;
                }
            }
            else
            {
                if (swapColorsArrayIndex >= customSwapColorsArray.Length)
                {
                    swapColorsArrayIndex = 0;
                }
            }
        }

        #endregion

        #region Helper Methods

        public void CopyAndPaste(Color32[] copyFrom, Color32[] pasteTo)
        {
            if (copyFrom == null
                || pasteTo == null)
            {
                return;
            }

            int length = copyFrom.Length;
            int bounds = pasteTo.Length - 1;
            for (int i = 0; i < length; i++)
            {
                if (i > bounds)
                {
                    break;
                }

                pasteTo[i] = copyFrom[i];
            }
        }

        private List<string> GetListFromTextAsset(TextAsset textAsset)
        {
            if (textAsset == null)
            {
                return null;
            }

            return new List<string>(textAsset.text.Split(System.Environment.NewLine));
        }

        private Color32 GetPaletteColorFromString(string paletteColorMessage)
        {
            if (paletteColorMessage.StartsWith("RGBA(") == false)
            {
                return new Color32(0, 0, 0, 255);
            }

            // Cut "RGBA(" and split at ")"
            string[] S = paletteColorMessage.Substring(5).Split(')');

            // Remove all spaces and split the 4 color values
            string[] values = S[0].Replace(" ", "").Split(',');

            // Parse the 4 strings into bytes and create the color value
            Color32 color = new Color32(byte.Parse(values[0]), byte.Parse(values[1]), byte.Parse(values[2]), byte.Parse(values[3]));

            return color;
        }

        private string GetPalettePartNameFromString(string palettePartMessage)
        {
            if (palettePartMessage.StartsWith("RGBA(") == false)
            {
                return "";
            }

            // Cut "RGBA(" and split at ")"
            string[] S = palettePartMessage.Substring(5).Split(')');

            // Read the colorname and remove leading or trailing spaces
            string colorName = S[1].Trim();

            return colorName;
        }

        private Color32[] GetCurrentSwapColors()
        {
            if (swapColorsType == SwapColorsType.Default)
            {
                return defaultSwapColorsArray[swapColorsArrayIndex].swapColorsArray;
            }
            else
            {
                return customSwapColorsArray[swapColorsArrayIndex].swapColorsArray;
            }
        }

        private Color32[] GetDefaultColor32Array(int length)
        {
            Color32[] newArray = new Color32[length];
            for (int i = 0; i < length; i++)
            {
                newArray[i] = new Color32((byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), 255);
            }
            return newArray;
        }

        private void SetAllCustomSwapColorsArraysRandomSwapColors()
        {
            int length = customSwapColorsArray.Length;
            for (int i = 0; i < length; i++)
            {
                int lengthA = customSwapColorsArray[i].swapColorsArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    customSwapColorsArray[i].swapColorsArray[a] = new Color32((byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), 255);
                }
            }
        }

        #endregion

        #region Palette Swap Sprite Controller Tools Options

#if UNITY_EDITOR
        [System.Serializable]
        private class PaletteSwapSpriteControllerToolsOptions
        {
            [SerializeField]
            private PaletteSwapSpriteController paletteSwapSpriteController;

            [Header("Swap Color")]
            [SerializeField]
            private Color32 swapColor;
            [SerializeField]
            private int swapColorPaletteIndex;
            [SerializeField]
            private int[] swapColorPaletteIndexArray;

            [Header("Tint Color")]
            [SerializeField]
            private Color32 tintColor;
            [Range(0, 1)]
            [SerializeField]
            private float tintAmount;
            [SerializeField]
            private int tintColorPaletteIndex;
            [SerializeField]
            private int[] tintColorPaletteIndexArray;

            [Header("Reset Color")]
            [SerializeField]
            private int resetColorPaletteIndex;
            [SerializeField]
            private int[] resetColorPaletteIndexArray;

#if UNITY_EDITOR
            [MethodButton(
                "paletteSwapSpriteControllerToolsOptions",
                nameof(InitializeSwapTexture),
                nameof(SwapSingleSpriteColorPaletteIndex),
                nameof(SwapSingleSpriteColorPaletteIndexArray),
                nameof(SwapAllSpriteColors),
                nameof(TintSingleSpriteColorPaletteIndex),
                nameof(TintSingleSpriteColorPaletteIndexArray),
                nameof(TintAllSpriteColors),
                nameof(ResetSingleSpriteColorPaletteIndex),
                nameof(ResetSingleSpriteColorPaletteIndexArray),
                nameof(ResetAllSpriteColors))]
            [SerializeField]
            private bool paletteSwapSpriteControllerToolsOptionsMethodButtons;
#endif

            private void InitializeSwapTexture()
            {
                if (paletteSwapSpriteController == null)
                {
                    return;
                }

                paletteSwapSpriteController.InitializeSwapTexture();
            }

            private void SwapSingleSpriteColorPaletteIndex()
            {
                if (paletteSwapSpriteController == null)
                {
                    return;
                }

                paletteSwapSpriteController.SwapSingleSpriteColor(swapColorPaletteIndex, swapColor);
            }

            private void SwapSingleSpriteColorPaletteIndexArray()
            {
                if (paletteSwapSpriteController == null)
                {
                    return;
                }

                paletteSwapSpriteController.SwapSingleSpriteColor(swapColorPaletteIndexArray, swapColor);
            }

            private void SwapAllSpriteColors()
            {
                if (paletteSwapSpriteController == null)
                {
                    return;
                }

                paletteSwapSpriteController.SwapAllSpriteColors(swapColor);
            }

            private void TintSingleSpriteColorPaletteIndex()
            {
                if (paletteSwapSpriteController == null)
                {
                    return;
                }

                paletteSwapSpriteController.TintSingleSpriteColor(tintColorPaletteIndex, tintColor, tintAmount);
            }

            private void TintSingleSpriteColorPaletteIndexArray()
            {
                if (paletteSwapSpriteController == null)
                {
                    return;
                }

                paletteSwapSpriteController.TintSingleSpriteColor(tintColorPaletteIndexArray, tintColor, tintAmount);
            }

            private void TintAllSpriteColors()
            {
                if (paletteSwapSpriteController == null)
                {
                    return;
                }

                paletteSwapSpriteController.TintAllSpriteColors(tintColor, tintAmount);
            }

            private void ResetSingleSpriteColorPaletteIndex()
            {
                if (paletteSwapSpriteController == null)
                {
                    return;
                }

                paletteSwapSpriteController.ResetSingleSpriteColor(resetColorPaletteIndex);
            }

            private void ResetSingleSpriteColorPaletteIndexArray()
            {
                if (paletteSwapSpriteController == null)
                {
                    return;
                }

                paletteSwapSpriteController.ResetSingleSpriteColor(resetColorPaletteIndexArray);
            }

            private void ResetAllSpriteColors()
            {
                if (paletteSwapSpriteController == null)
                {
                    return;
                }

                paletteSwapSpriteController.ResetAllSpriteColors();
            }
        }
        [SerializeField]
        private PaletteSwapSpriteControllerToolsOptions paletteSwapSpriteControllerToolsOptions;
#endif

        #endregion

        #region Palette Texture Tools Options

#if UNITY_EDITOR
        [System.Serializable]
        private class PaletteTextureToolsOptions
        {
            [SerializeField]
            private PaletteSwapSpriteController paletteSwapSpriteController;
            [SerializeField]
            private Texture2D paletteTexture;
            [SerializeField]
            private Texture2D sampleTexture;
            [SerializeField]
            private Color32[] paletteTextureColorsArray;

#if UNITY_EDITOR
            [MethodButton(
                "paletteTextureToolsOptions",
                nameof(SetPaletteTextureColorsArrayFromPaletteTextAsset),
                nameof(SetPaletteTextureColorsArrayFromPaletteTexture),    
                nameof(SetPaletteTextureColorsArrayFromSampleTexture),
                nameof(OverwritePaletteTexture))]
            [SerializeField]
            private bool paletteTextureToolsOptionsMethodButtons;
#endif

            private void SetPaletteTextureColorsArrayFromPaletteTextAsset()
            {
                if (paletteSwapSpriteController == null)
                {
                    return;
                }

                List<string> newStringList = paletteSwapSpriteController.GetListFromTextAsset(paletteSwapSpriteController.paletteTextAsset);
                if (newStringList == null)
                {
                    return;
                }
                int count = newStringList.Count;
                Color32[] newColorArray = new Color32[count];
                for (int i = 0; i < count; i++)
                {
                    newColorArray[i] = paletteSwapSpriteController.GetPaletteColorFromString(newStringList[i]);
                }
                paletteTextureColorsArray = newColorArray;
            }

            private void SetPaletteTextureColorsArrayFromPaletteTexture()
            {
                if (paletteTexture == null)
                {
                    return;
                }

                Texture2D duplicateTexture = GetDuplicateTexture(paletteTexture);     
                int width = duplicateTexture.width;
                int height = duplicateTexture.height;
                List<Color32> newColorList = new List<Color32>();
                for (int i = height - 1; i >= 0; i--)
                {
                    for (int a = 0; a < width; a++)
                    {
                        Color32 color = duplicateTexture.GetPixel(a, i);
                        if (color.a != 255)
                        {
                            continue;
                        }
                        newColorList.Add(color);
                    }
                }
                int count = newColorList.Count;
                Color32[] newColorArray = new Color32[count];
                for (int i = 0; i < count; i++)
                {
                    newColorArray[i] = newColorList[i];
                }
                paletteTextureColorsArray = newColorArray;
                DestroyImmediate(duplicateTexture);  
            }

            private void SetPaletteTextureColorsArrayFromSampleTexture()
            {
                if (sampleTexture == null)
                {
                    return;
                }

                Texture2D duplicateTexture = GetDuplicateTexture(sampleTexture);
                int width = duplicateTexture.width;
                int height = duplicateTexture.height;
                List<Color32> newColorList = new List<Color32>();
                for (int i = height - 1; i >= 0; i--)
                {
                    for (int a = 0; a < width; a++)
                    {
                        Color32 color = duplicateTexture.GetPixel(a, i);
                        if (color.a != 255)
                        {
                            continue;
                        }
                        newColorList.Add(color);
                    }
                }
                for (int i = 0; i < newColorList.Count; i++)
                {
                    RemoveDuplicateColor(newColorList[i]);
                }
                int count = newColorList.Count;
                Color32[] newColorArray = new Color32[count];
                for (int i = 0; i < count; i++)
                {
                    newColorArray[i] = newColorList[i];
                }
                paletteTextureColorsArray = newColorArray;
                DestroyImmediate(duplicateTexture);

                void RemoveDuplicateColor(Color32 colorToRemove)
                {
                    bool ignoreFirstMatch = true;

                    for (int i = 0; i < newColorList.Count; i++)
                    {
                        if (ignoreFirstMatch == true
                            && colorToRemove.r == newColorList[i].r
                            && colorToRemove.g == newColorList[i].g
                            && colorToRemove.b == newColorList[i].b
                            && colorToRemove.a == newColorList[i].a)
                        {
                            ignoreFirstMatch = false;
                        }
                        else if (ignoreFirstMatch == false
                            && colorToRemove.r == newColorList[i].r
                            && colorToRemove.g == newColorList[i].g
                            && colorToRemove.b == newColorList[i].b
                            && colorToRemove.a == newColorList[i].a)
                        {
                            newColorList.RemoveAt(i);
                            i -= 1;
                        }
                    }
                }
            }

            private void OverwritePaletteTexture()
            {
                if (paletteTexture == null
                    || paletteTextureColorsArray.Length <= 0)
                {
                    return;
                }

                Texture2D newPaletteTexture = new Texture2D(paletteTextureColorsArray.Length, 1, TextureFormat.RGBA32, false, false);
                newPaletteTexture.SetPixels32(paletteTextureColorsArray);
                System.IO.File.WriteAllBytes(UnityEditor.AssetDatabase.GetAssetPath(paletteTexture), newPaletteTexture.EncodeToPNG());
                UnityEditor.AssetDatabase.Refresh();
            }

            private Texture2D GetDuplicateTexture(Texture2D source)
            {
                RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

                Graphics.Blit(source, renderTex);
                RenderTexture previous = RenderTexture.active;
                RenderTexture.active = renderTex;
                Texture2D readableText = new Texture2D(source.width, source.height);
                readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
                readableText.Apply();
                RenderTexture.active = previous;
                RenderTexture.ReleaseTemporary(renderTex);
                return readableText;
            }
        }
        [SerializeField]
        private PaletteTextureToolsOptions paletteTextureToolsOptions;
#endif

        #endregion
    }
}