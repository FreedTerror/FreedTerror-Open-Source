using System;
using System.Collections.Generic;
using System.IO;
using UFE3D;
using Unity.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace UFE2FTE
{
    public class PaletteSwapSpriteController : MonoBehaviour
    {
        //TODO add a swap texture pool

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

        [SerializeField]
        private SpriteRenderer mySpriteRenderer;
        private Texture2D mySwapTexture;
        private ControlsScript myControlsScript;
        private ProjectileMoveScript myProjectileMoveScript;

        [SerializeField]
        private UFE3D.CharacterInfo characterInfo;
        [HideInInspector]
        public UFE2FTE.Player player;

        public string[] palettePartNameArray;
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

#if UNITY_EDITOR
        [SerializeField]
        private TextAsset paletteTextAsset;
        [SerializeField]
        private Color32[] paletteTextureColorsArray;
#endif

        public string paletteSwapSpriteEditorName;
        public Sprite[] paletteSwapEditorSpriteArray;

        private void Awake()
        {
            InitializeSwapTexture();
        }

        private void Start()
        {
            SetAllCustomSwapColorsArraysRandomSwapColors();

            LoadCustomSwapColorsArray();

            SaveCustomSwapColorsArray();

            myControlsScript = GetComponentInParent<ControlsScript>();
            if (myControlsScript != null)
            {
                player = (UFE2FTE.Player)myControlsScript.playerNum - 1;

                LoadSwapColorsVariables();

                ApplyCurrentSwapColors();
            }

            myProjectileMoveScript = GetComponent<ProjectileMoveScript>();
            if (myProjectileMoveScript != null
                && myProjectileMoveScript.myControlsScript != null)
            {
                player = (UFE2FTE.Player)myProjectileMoveScript.myControlsScript.playerNum - 1;

                //LoadSwapColorsVariables();

                //ApplyCurrentSwapColors();
            }
        }

        private void OnDestroy()
        {
            Destroy(mySwapTexture);
        }

        [NaughtyAttributes.Button]
        private void AutoConfiguration()
        {
            SetPalettePartNameArrayFromPaletteTextAsset();
            SetAllSwapColorsArraysFromPaletteTexture();
            SetPaletteTextureColorsArrayFromPaletteTextAsset();
        }

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

            mySwapTexture = new Texture2D(paletteTexture.width, 1, TextureFormat.RGBA32, false, false);
            mySwapTexture.filterMode = FilterMode.Point;
            mySwapTexture.wrapMode = TextureWrapMode.Clamp;
            mySwapTexture.SetPixels32(paletteTexture.GetPixels32());
            mySwapTexture.Apply();

            if (Application.isPlaying == true)
            {
                mySpriteRenderer.material.SetTexture(swapTextureID, mySwapTexture);
            }    
            else
            {
                mySpriteRenderer.GetPropertyBlock(materialPropertyBlock);
                materialPropertyBlock.SetTexture(swapTextureID, mySwapTexture);
                mySpriteRenderer.SetPropertyBlock(materialPropertyBlock);
            }
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

        #endregion

        #region Save And Load Methods

        [NaughtyAttributes.Button]
        public void SaveCustomSwapColorsArray()
        {
            if (characterInfo == null)
            {
                return;
            }

            try
            {
                ES3.Save(characterInfo.characterName + nameof(customSwapColorsArray), customSwapColorsArray);
            }
            catch (Exception exception)
            {
                Debug.Log(exception);
            }
        }

        [NaughtyAttributes.Button]
        public void LoadCustomSwapColorsArray()
        {
            if (characterInfo == null)
            {
                return;
            }

            try
            {
                SwapColors[] loadedSwapColorsArray = ES3.Load<SwapColors[]>(characterInfo.characterName + nameof(customSwapColorsArray));
                if (loadedSwapColorsArray == null)
                {
                    return;
                }

                int length = loadedSwapColorsArray.Length;
                int arrayBounds = customSwapColorsArray.Length - 1;
                for (int i = 0; i < length; i++)
                {
                    if (i > arrayBounds)
                    {
                        break;
                    }

                    CopyAndPaste(loadedSwapColorsArray[i].swapColorsArray, customSwapColorsArray[i].swapColorsArray);
                }
            }
            catch (Exception exception)
            {
                Debug.Log(exception);
            }
        }

        public void SaveSwapColorsVariables()
        {
            if (characterInfo == null)
            {
                return;
            }

            PlayerPrefs.SetInt(characterInfo.characterName + player + nameof(swapColorsType), (int)swapColorsType);
            PlayerPrefs.SetInt(characterInfo.characterName + player + nameof(swapColorsArrayIndex), swapColorsArrayIndex);
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

        #region Palette Text Asset Methods

        private void SetPalettePartNameArrayFromPaletteTextAsset()
        {
            List<string> stringList = GetListFromTextAsset(paletteTextAsset);
            if (stringList == null)
            {
                return;
            }
            int count = stringList.Count;
            palettePartNameArray = new string[count];
            for (int i = 0; i < count; i++)
            {
                palettePartNameArray[i] = GetPalettePartNameFromString(stringList[i]);
            }
        }

        [NaughtyAttributes.Button]
        private void SetPaletteTextureColorsArrayFromPaletteTextAsset()
        {
            List<string> stringList = GetListFromTextAsset(paletteTextAsset);
            if (stringList == null)
            {
                return;
            }
            int count = stringList.Count;
            paletteTextureColorsArray = new Color32[count];
            for (int i = 0; i < count; i++)
            {
                paletteTextureColorsArray[i] = GetPaletteColorFromString(stringList[i]);
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

        private string GetPalettePartNameFromString(string aText)
        {
            if (aText.StartsWith("RGBA(") == false)
            {
                return "";
            }

            // Cut "RGBA(" and split at ")"
            string[] S = aText.Substring(5).Split(')');

            // Read the colorname and remove leading or trailing spaces
            string colorName = S[1].Trim();

            return colorName;
        }

        private Color32 GetPaletteColorFromString(string aText)
        {
            if (aText.StartsWith("RGBA(") == false)
            {
                return new Color32(0, 0, 0, 255);
            }

            // Cut "RGBA(" and split at ")"
            string[] S = aText.Substring(5).Split(')');

            // Remove all spaces and split the 4 color values
            string[] values = S[0].Replace(" ", "").Split(',');

            // Parse the 4 strings into bytes and create the color value
            Color32 col = new Color32(byte.Parse(values[0]), byte.Parse(values[1]), byte.Parse(values[2]), byte.Parse(values[3]));

            return col;
        }

        #endregion

        #region  Palette Texture Methods

        [NaughtyAttributes.Button]
        private void SetPaletteTextureColorsArrayFromPaletteTexture()
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

            paletteTextureColorsArray = paletteTexture.GetPixels32();
        }

        [NaughtyAttributes.Button]
        private void OverwritePaletteTextureWithPaletteTextureColorsArray()
        {
            if (mySpriteRenderer == null
                || paletteTextureColorsArray == null
                || paletteTextureColorsArray.Length <= 0)
            {
                return;
            }

            Texture2D paletteTexture = (Texture2D)mySpriteRenderer.sharedMaterial.GetTexture(paletteTextureID);
            if (paletteTexture == null)
            {
                return;
            }

            Texture2D newPaletteTexture = new Texture2D(paletteTextureColorsArray.Length, 1, TextureFormat.RGBA32, false, false);
            newPaletteTexture.SetPixels32(paletteTextureColorsArray);
            File.WriteAllBytes(AssetDatabase.GetAssetPath(paletteTexture), newPaletteTexture.EncodeToPNG());
            AssetDatabase.Refresh();
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
            int arrayBounds = pasteTo.Length - 1;
            for (int i = 0; i < length; i++)
            {
                if (i > arrayBounds)
                {
                    break;
                }

                pasteTo[i] = copyFrom[i];
            }
        }

        private void SetAllSwapColorsArraysFromPaletteTexture()
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
            SetDefaultSwapColorsArrayElement0();

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

        private void SetDefaultSwapColorsArrayElement0()
        {
            if (mySpriteRenderer == null
                || defaultSwapColorsArray.Length <= 0)
            {
                return;
            }

            Texture2D paletteTexture = (Texture2D)mySpriteRenderer.sharedMaterial.GetTexture(paletteTextureID);
            if (paletteTexture == null)
            {
                return;
            }

            defaultSwapColorsArray[0].swapColorsArray = paletteTexture.GetPixels32();
        }

        private void SetAllCustomSwapColorsArraysRandomSwapColors()
        {
            int length = customSwapColorsArray.Length;
            for (int i = 0; i < length; i++)
            {
                int lengthA = customSwapColorsArray[i].swapColorsArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    customSwapColorsArray[i].swapColorsArray[a] = GetDefaultRandomColor32();
                }
            }
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

        public Color32 GetDefaultColor32()
        {
            return new Color32(0, 0, 0, 255);
        }

        public Color32 GetDefaultRandomColor32()
        {
            return new Color32((byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), 255);
        }

        private Color32[] GetDefaultColor32Array(int length)
        {
            Color32[] newArray = new Color32[length];
            for (int i = 0; i < length; i++)
            {
                newArray[i] = GetDefaultColor32();
            }
            return newArray;
        }

        #endregion

        #region Palette Swap Sprite Controller Tools Options

#if UNITY_EDITOR
        [System.Serializable]
        public class PaletteSwapSpriteControllerToolsOptions
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
                "paletteSwapControllerTools",
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
            private bool paletteSwapControllerToolsMethodButtons;
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
    }
}