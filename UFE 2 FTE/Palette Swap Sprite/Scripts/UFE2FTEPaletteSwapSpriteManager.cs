using System;
using System.Collections.Generic;
using UnityEngine;
using UFE3D;
using Photon.Pun;

namespace UFE2FTE
{
    public class UFE2FTEPaletteSwapSpriteManager : MonoBehaviour
    {
        public static UFE2FTEPaletteSwapSpriteManager instance;

        #region Material Property Block

        private MaterialPropertyBlock myMaterialPropertyBlock;
        public MaterialPropertyBlock GetMaterialPropertyBlock()
        {
            if (myMaterialPropertyBlock == null)
            {
                myMaterialPropertyBlock = new MaterialPropertyBlock();
            }

            return myMaterialPropertyBlock;
        }

        #endregion

        #region Screen Type

        public enum ScreenType
        {
            None,
            MainMenuScreen,
            CharacterSelectionScreen,
            LoadingBattleScreen
        }
        public ScreenType screenType;

        #endregion

        #region Swap Colors Save Data Options

        [Serializable]
        public class SwapColorsSaveDataOptions
        {
            public string defaultSwapColorName = "DEFAULT ";
            [HideInInspector]
            public string[] defaultSwapColorNames;
            public int customSwapColorsSlots = 10;
            public string customSwapColorName = "CUSTOM ";
            [HideInInspector]
            public string[] customSwapColorNames;
        }
        [Header("SWAP COLORS SAVE DATA OPTIONS")]
        public SwapColorsSaveDataOptions swapColorsSaveDataOptions;

        #endregion

        #region Swap Colors Save Data

        [Serializable]
        public class SwapColorsSaveData
        {
            [Serializable]
            public class SwapColorsData
            {
                public string characterName;

                public enum UsingSwapColors
                {
                    Default,
                    Custom
                }
                [HideInInspector]
                public UsingSwapColors player1UsingSwapColors;
                [HideInInspector]
                public UsingSwapColors player2UsingSwapColors;

                [HideInInspector]
                public int player1DefaultSwapColorsIndex;
                [HideInInspector]
                public int player1CustomSwapColorsIndex;

                [HideInInspector]
                public int player2DefaultSwapColorsIndex;
                [HideInInspector]
                public int player2CustomSwapColorsIndex;

                public string[] partNames;
                   
                [Serializable]
                public class SwapColors
                {
                    public Color32[] swapColors;
                    [HideInInspector]
                    public Vector3[] swapColorsRGBColorBytes;
                }
                public SwapColors[] defaultSwapColors;
                [HideInInspector]
                public SwapColors[] customSwapColors;
            }
            public SwapColorsData[] swapColorsData;
        }
        [Header("SWAP COLORS SAVE DATA")]
        public SwapColorsSaveData swapColorsSaveData;

        #endregion

        #region Loaded Swap Colors Options

        [Serializable]
        public class LoadedSwapColorsOptions
        {
            [Serializable]
            public class LoadedSwapColors
            {
                public Color32[] swapColors;
            }
            [HideInInspector]
            public List<LoadedSwapColors> loadedSwapColors;
            public List<LoadedSwapColors> GetLoadedSwapColors()
            {
                if (loadedSwapColors == null)
                {
                    loadedSwapColors = new List<LoadedSwapColors>();
                }

                return loadedSwapColors;
            }
            [HideInInspector]
            public int loadedSwapColorsIndex;
            public string noLoadedSwapColorsName = "NONE";
            public string loadedSwapColorsName = "CUSTOM ";   
        }
        [Header("LOADED SWAP COLORS OPTIONS")]
        public LoadedSwapColorsOptions loadedSwapColorsOptions;

        #endregion

        #region Character Prefab Screen Options

        [Serializable]
        public class CharacterPrefabScreenOptions
        {
            public List<UFE2FTEPaletteSwapSpriteController> characterPrefabs = new List<UFE2FTEPaletteSwapSpriteController>();
            public Transform characterPrefabsParentTransform;
            public bool useMainCameraAsCharacterPrefabsParentTransform;       
            public Vector3 player1CharacterPrefabSpawnPosition;
            public Vector3 player2CharacterPrefabSpawnPosition;
            [HideInInspector]
            public UFE2FTEPaletteSwapSpriteController player1CharacterGameObject;
            [HideInInspector]
            public UFE2FTEPaletteSwapSpriteController player2CharacterGameObject;
        }
        [Header("CHARACTER PREFAB SCREEN OPTIONS")]
        public CharacterPrefabScreenOptions characterPrefabScreenOptions;

        #endregion

        #region Main Menu Screen Options

        [Serializable]
        public class MainMenuScreenOptions
        {
            public float characterSwitchDelay;
            [HideInInspector]
            public float characterSwitchTimer;
        }
        [Header("MAIN MENU SCREEN OPTIONS")]
        public MainMenuScreenOptions mainMenuScreenOptions;

        #endregion

        #region Character Selection Screen Options

        [Serializable]
        public class CharacterSelectionScreenOptions
        {
            public ButtonPress nextSwapColorsButtonPress;
            public ButtonPress previousSwapColorsButtonPress;

            [HideInInspector]
            public bool player1NextSwapColorsButtonPressed;
            [HideInInspector]
            public bool player1PreviousSwapColorsButtonPressed;

            [HideInInspector]
            public bool player2NextSwapColorsButtonPressed;
            [HideInInspector]
            public bool player2PreviousSwapColorsButtonPressed;
        }
        [Header("CHARACTER SELECTION SCREEN OPTIONS")]
        public CharacterSelectionScreenOptions characterSelectionScreenOptions;

        #endregion

        #region Palette Texture Object Pool Options

        [Serializable]
        public class PaletteTextureObjectPoolOptions
        {
            [Serializable]
            public class PaletteTextureInfo
            {
                public int poolSize;
                public bool usePoolCanGrow;
                public string characterName;
                public Texture2D paletteTexture;
                private List<PooledPaletteTextureInfo> pooledPaletteTextureInfos;
                public List<PooledPaletteTextureInfo> GetPooledPaletteTextureInfos()
                {
                    if (pooledPaletteTextureInfos == null)
                    {
                        pooledPaletteTextureInfos = new List<PooledPaletteTextureInfo>();
                    }

                    return pooledPaletteTextureInfos;
                }
            }
            public List<PaletteTextureInfo> paletteTextureInfos = new List<PaletteTextureInfo>();

            [Serializable]
            public class PooledPaletteTextureInfo
            {
                public string characterName;
                public Color32[] swapColors;
                public Texture2D paletteTexture;
                public UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController;
            }

            [Serializable]
            public class UpdatePaletteTextureObjectPoolsOptions
            {
                [Serializable]
                public class UpdateAllPaletteTextureObjectPoolsOptions
                {
                    public bool useOnEnable;
                    public bool useOnStart;
                    public bool useOnDisable;
                    public bool useOnDestroy;
                    public int poolSize;
                }

                [Serializable]
                public class UpdatePaletteTextureObjectPoolsByCharacterNameOptions
                {
                    public bool useOnEnable;
                    public bool useOnStart;
                    public bool useOnDisable;
                    public bool useOnDestroy;
                    public int poolSize;
                    public string[] characterNames;
                }

                [Header("UPDATE ALL PALETTE TEXTURE OBJECT POOLS OPTIONS")]
                public bool useUpdateAllPaletteTextureObjectPoolsOptions;
                public UpdateAllPaletteTextureObjectPoolsOptions updateAllPaletteTexturePoolsOptions;

                [Header("UPDATE PALETTE TEXTURE OBJECT POOLS BY CHARACTER NAME OPTIONS")]
                public bool useUpdatePaletteTextureObjectPoolsByCharacterNameOptions;
                public UpdatePaletteTextureObjectPoolsByCharacterNameOptions updatePaletteTextureObjectPoolsByCharacterNameOptions;

                [Header("UPDATE PALETTE TEXTURE OBJECT POOLS BY UFE PRELOAD OPTIONS")]
                public bool useUpdatePaletteTextureObjectPoolsByUFEPreloadOptions;
                public UpdateAllPaletteTextureObjectPoolsOptions updatePaletteTextureObjectPoolsByUFEPreloadOptions;
            }
            //[Header("UPDATE PALETTE TEXTURE OBJECT POOLS OPTIONS")]
        }
        [Header("PALETTE TEXTURE OBJECT POOL OPTIONS")]
        public PaletteTextureObjectPoolOptions paletteTextureObjectPoolOptions;

        #endregion

        #region Ghost Object Pool Options

        [Serializable]
        public class GhostObjectPoolOptions
        {
            public int poolSize;
            public bool usePoolCanGrow;
            public UFE2FTEPaletteSwapSpriteController ghostPrefab;
            private List<UFE2FTEPaletteSwapSpriteController> pooledGhostGameObjects;
            public List<UFE2FTEPaletteSwapSpriteController> GetPooledGhostGameObjects()
            {
                if (pooledGhostGameObjects == null)
                {
                    pooledGhostGameObjects = new List<UFE2FTEPaletteSwapSpriteController>();
                }

                return pooledGhostGameObjects;
            }

            [Serializable]
            public class UpdateGhostObjectPoolsOptions
            {
                [Serializable]
                public class UpdateAllGhostObjectPoolsOptions
                {
                    public bool useOnEnable;
                    public bool useOnStart;
                    public bool useOnDisable;
                    public bool useOnDestroy;
                    public int poolSize;
                }
                public bool useUpdateAllGhostObjectPoolsOptions;
                public UpdateAllGhostObjectPoolsOptions updateAllGhostObjectPoolsOptions;
            }
            //[Header("UPDATE GHOST PREFAB POOL OPTIONS")]
        }
        [Header("GHOST OBJECT POOL OPTIONS")]
        public GhostObjectPoolOptions ghostObjectPoolOptions;

        #endregion

        #region Network Manager Options

        [Serializable]
        public class NetworkManagerOptions
        {
            public UFE2FTEPaletteSwapSpritePhoton2NetworkManager photon2NetworkManagerPrefab;
        }
        [Header("NETWORK MANAGER OPTIONS")]
        public NetworkManagerOptions networkManagerOptions;

        #endregion

        void Awake()
        {
            instance = this;

            SetSwapColorsSaveDataOptions();

            LoadSwapColorsSaveData();

            SaveSwapColorsSaveData();

            SetCharacterPrefabsParentTransform();

            SetCharacterPrefabsList();

            ResetMainMenuScreenOptions();
        }

        // Update is called once per frame
        void Update()
        {
            float deltaTime = (float)UFE.fixedDeltaTime;

            SetCharacterPrefabsList();

            SetScreenType(deltaTime);

            SetAllPaletteTextureObjectPools();

            SetAllGhostObjectPools();

            SetNetworkManager();
        }

        void OnDestroy()
        {      
            DestroyAllCharacterGameObjects();

            DestroyAllPooledPaletteTextureInfos();

            DestroyNetworkManager();
        }

        #region Swap Colors Save Data Methods

        private void SetSwapColorsSaveDataOptions()
        {
            int defaultSwapColorsLength = 0;
            int defaultSwapColorsHighestLength = 0;
            int length = swapColorsSaveData.swapColorsData.Length;
            for (int i = 0; i < length; i++)
            {
                defaultSwapColorsLength = swapColorsSaveData.swapColorsData[i].defaultSwapColors.Length;

                if (defaultSwapColorsLength > defaultSwapColorsHighestLength)
                {
                    defaultSwapColorsHighestLength = defaultSwapColorsLength;
                }
            }

            swapColorsSaveDataOptions.defaultSwapColorNames = new string[defaultSwapColorsHighestLength];
            int defaultStartNumber = 1;
            int length1 = swapColorsSaveDataOptions.defaultSwapColorNames.Length;
            for (int i = 0; i < length1; i++)
            {
                swapColorsSaveDataOptions.defaultSwapColorNames[i] = swapColorsSaveDataOptions.defaultSwapColorName + defaultStartNumber;
                defaultStartNumber++;
            }

            swapColorsSaveDataOptions.customSwapColorNames = new string[swapColorsSaveDataOptions.customSwapColorsSlots];
            int customStartNumber = 1;
            int length2 = swapColorsSaveDataOptions.customSwapColorNames.Length;
            for (int i = 0; i < length2; i++)
            {
                swapColorsSaveDataOptions.customSwapColorNames[i] = swapColorsSaveDataOptions.customSwapColorName + customStartNumber;
                customStartNumber++;
            }
        }

        public void OpenCharacterCustomSwapColorsFilesFolder(string characterName)
        {
            Application.OpenURL(Application.persistentDataPath + "\\" + "Swap Colors Save Data/" + characterName);
        }

        public void SaveSwapColorsSaveData()
        {
            try
            {
                ES3.Save("swapColorsSaveData", swapColorsSaveData, "Swap Colors Save Data/Swap Colors Save Data/Swap Colors Save Data.es3");

#if UNITY_EDITOR
                Debug.Log("File saved: Swap Colors Save Data.");
#endif
            }
            catch (Exception exception)
            {
#if UNITY_EDITOR
                Debug.Log("File saved error: " + exception);
#endif
            }

            LoadSwapColorsSaveData();
        }

        public void SaveCharacterCustomSwapColors(string characterName, int customSwapColorsIndex)
        {
            int length = swapColorsSaveData.swapColorsData.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                int lengthA = swapColorsSaveData.swapColorsData[i].customSwapColors.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (customSwapColorsIndex >= swapColorsSaveData.swapColorsData[i].customSwapColors.Length) continue;

                    int customSwapColorsIndexName = customSwapColorsIndex + 1;

                    try
                    {
                        ES3.Save(characterName + "CustomSwapColors", swapColorsSaveData.swapColorsData[i].customSwapColors[customSwapColorsIndex].swapColors, "Swap Colors Save Data/" + characterName + "/" + characterName + " Custom Swap Colors " + customSwapColorsIndexName + ".es3");

#if UNITY_EDITOR
                        Debug.Log("File saved: " + "Swap Colors Save Data/" + characterName + "/" + characterName + " Custom Swap Colors " + customSwapColorsIndexName + ".es3");
#endif
                    }
                    catch (Exception exception)
                    {
#if UNITY_EDITOR
                        Debug.Log("File saved error: " + "Swap Colors Save Data/" + characterName + "/" + characterName + " Custom Swap Colors " + customSwapColorsIndexName + ".es3" + " " + exception);
#endif
                    }

                    return;
                }
            }    
        }
       
        public void LoadSwapColorsSaveData()
        {
            int length = swapColorsSaveData.swapColorsData.Length;
            for (int i = 0; i < length; i++)
            {
                swapColorsSaveData.swapColorsData[i].player1UsingSwapColors = SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default;
                swapColorsSaveData.swapColorsData[i].player2UsingSwapColors = SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default;

                swapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex = 0;
                swapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex = 0;

                swapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex = 0;
                swapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex = 0;

                int validSwapColorsLength = 0;

                try
                {
                    // We set the first defaultSwapColors[0].swapColors.Length as our validSwapColorsLength.
                    validSwapColorsLength = swapColorsSaveData.swapColorsData[i].defaultSwapColors[0].swapColors.Length;
                }
                catch (Exception exception)
                {
#if UNITY_EDITOR
                    Debug.Log(exception);
                    Debug.Log(swapColorsSaveData.swapColorsData[i].characterName + " Default Swap Colors " + i + " is invaild.");
                    Debug.Log("Palette Swap Sprite Manager has been destroyed to avoid errors.");                  
#endif

                    Destroy(gameObject);

                    return;
                }

                // Ensure defaultSwapColors arrays are valid.
                int lengthA = swapColorsSaveData.swapColorsData[i].defaultSwapColors.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    int lengthB = swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColors.Length;
                    for (int b = 0; b < lengthB; b++)
                    {
                        swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColors[b] = new Color32(swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColors[b].r, swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColors[b].g, swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColors[b].b, 255);

                        if (swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColors.Length == validSwapColorsLength) continue;

                        swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColors = (Color32[])swapColorsSaveData.swapColorsData[i].defaultSwapColors[0].swapColors.Clone();

#if UNITY_EDITOR
                        Debug.Log(swapColorsSaveData.swapColorsData[i].characterName + " Default Swap Colors " + a + " is invaild.");
#endif
                    }
                }

                // Setup customSwapColors arrays.
                swapColorsSaveData.swapColorsData[i].customSwapColors = new SwapColorsSaveData.SwapColorsData.SwapColors[swapColorsSaveDataOptions.customSwapColorsSlots];

                int length1A = swapColorsSaveData.swapColorsData[i].customSwapColors.Length;
                for (int a = 0; a < length1A; a++)
                {
                    SwapColorsSaveData.SwapColorsData.SwapColors newSwapColors = new SwapColorsSaveData.SwapColorsData.SwapColors();
                    newSwapColors.swapColors = new Color32[validSwapColorsLength];

                    for (int b = 0; b < validSwapColorsLength; b++)
                    {
                        newSwapColors.swapColors[b] = new Color32((byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), 255);
                    }

                    swapColorsSaveData.swapColorsData[i].customSwapColors[a] = newSwapColors;
                }
            }

            if (ES3.DirectoryExists("Swap Colors Save Data/Swap Colors Save Data/") == true)
            {
                foreach (var fileName in ES3.GetFiles("Swap Colors Save Data/Swap Colors Save Data/"))
                {
                    if (fileName != "Swap Colors Save Data.es3") continue;

                    SwapColorsSaveData newSwapColorsSaveData = new SwapColorsSaveData();

                    try
                    {
                        newSwapColorsSaveData = ES3.Load<SwapColorsSaveData>("swapColorsSaveData", "Swap Colors Save Data/Swap Colors Save Data/Swap Colors Save Data.es3");

#if UNITY_EDITOR
                        Debug.Log("File loaded: Swap Colors Save Data.");
#endif
                    }
                    catch (Exception exception)
                    {
#if UNITY_EDITOR
                        Debug.Log("File loaded error: Swap Colors Save Data. " + exception);
#endif

                        DeleteSwapColorsSaveDataFile();

                        SaveSwapColorsSaveData();

                        return;
                    }

                    for (int i = 0; i < length; i++)
                    {                    
                        if (i >= newSwapColorsSaveData.swapColorsData.Length
                            || newSwapColorsSaveData.swapColorsData[i].characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                        // Failsafe incase of save file tampering.
                        if (newSwapColorsSaveData.swapColorsData[i].player1UsingSwapColors != SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default
                            && newSwapColorsSaveData.swapColorsData[i].player1UsingSwapColors != SwapColorsSaveData.SwapColorsData.UsingSwapColors.Custom)
                        {
                            swapColorsSaveData.swapColorsData[i].player1UsingSwapColors = SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default;
                        }
                        else
                        {
                            swapColorsSaveData.swapColorsData[i].player1UsingSwapColors = newSwapColorsSaveData.swapColorsData[i].player1UsingSwapColors;
                        }

                        // Failsafe incase of save file tampering.
                        if (newSwapColorsSaveData.swapColorsData[i].player2UsingSwapColors != SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default
                            && newSwapColorsSaveData.swapColorsData[i].player2UsingSwapColors != SwapColorsSaveData.SwapColorsData.UsingSwapColors.Custom)
                        {
                            swapColorsSaveData.swapColorsData[i].player2UsingSwapColors = SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default;
                        }
                        else
                        {
                            swapColorsSaveData.swapColorsData[i].player2UsingSwapColors = newSwapColorsSaveData.swapColorsData[i].player2UsingSwapColors;
                        }

                        // Failsafe incase of save file tampering.
                        if (newSwapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex < 0
                            || newSwapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex >= swapColorsSaveData.swapColorsData[i].defaultSwapColors.Length)
                        {
                            swapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex = 0;
                        }
                        else
                        {
                            swapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex = newSwapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex;
                        }

                        // Failsafe incase of save file tampering.
                        if (newSwapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex < 0
                            || newSwapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex >= swapColorsSaveData.swapColorsData[i].defaultSwapColors.Length)
                        {
                            swapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex = 0;
                        }
                        else
                        {
                            swapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex = newSwapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex;
                        }

                        // Failsafe incase of save file tampering.
                        if (newSwapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex < 0
                            || newSwapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex >= swapColorsSaveData.swapColorsData[i].customSwapColors.Length)
                        {
                            swapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex = 0;
                        }
                        else
                        {
                            swapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex = newSwapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex;
                        }

                        // Failsafe incase of save file tampering.
                        if (newSwapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex < 0
                            || newSwapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex >= swapColorsSaveData.swapColorsData[i].customSwapColors.Length)
                        {
                            swapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex = 0;
                        }
                        else
                        {
                            swapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex = newSwapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex;
                        }

                        int lengthA = swapColorsSaveData.swapColorsData[i].customSwapColors.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            int lengthB = swapColorsSaveData.swapColorsData[i].customSwapColors[a].swapColors.Length;
                            for (int b = 0; b < lengthB; b++)
                            {
                                // Bytes have a built in behaviour to deal with tampering.
                                // If we are outside 0-255 it will auto correct to somewhere inside 0-255.
                                Color32 color = new Color32((byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), 255);

                                try
                                {
                                    color = new Color32(newSwapColorsSaveData.swapColorsData[i].customSwapColors[a].swapColors[b].r, newSwapColorsSaveData.swapColorsData[i].customSwapColors[a].swapColors[b].g, newSwapColorsSaveData.swapColorsData[i].customSwapColors[a].swapColors[b].b, 255);
                                }
                                catch (Exception exception)
                                {
#if UNITY_EDITOR
                                    Debug.Log("Error while setting" + swapColorsSaveData.swapColorsData[i].characterName + "customSwapColors from newSwapColorsSaveData. " + exception);
#endif

                                    color = new Color32((byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), 255);
                                }

                                swapColorsSaveData.swapColorsData[i].customSwapColors[a].swapColors[b] = color;
                            }
                        }
                    }

                    break;
                }
            }

            SetAllSwapColorsRGBColorBytes();
        }

        private void SetAllSwapColorsRGBColorBytes()
        {
            int length = swapColorsSaveData.swapColorsData.Length;
            for (int i = 0; i < length; i++)
            {
                int lengthA = swapColorsSaveData.swapColorsData[i].defaultSwapColors.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    int lengthB = swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColors.Length;
                    for (int b = 0; b < lengthB; b++)
                    {
                        if (swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColorsRGBColorBytes == null
                            || swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColorsRGBColorBytes.Length != lengthB)
                        {
                            Vector3[] newSwapColorsRGBColorBytes = new Vector3[lengthB];
                            swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColorsRGBColorBytes = newSwapColorsRGBColorBytes;
                        }

                        for (int c = 0; c < lengthB; c++)
                        {
                            swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColorsRGBColorBytes[c].x = swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColors[c].r;
                            swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColorsRGBColorBytes[c].y = swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColors[c].g;
                            swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColorsRGBColorBytes[c].z = swapColorsSaveData.swapColorsData[i].defaultSwapColors[a].swapColors[c].b;
                        }
                    }              
                }

                int length1A = swapColorsSaveData.swapColorsData[i].customSwapColors.Length;
                for (int a = 0; a < length1A; a++)
                {
                    int length1B = swapColorsSaveData.swapColorsData[i].customSwapColors[a].swapColors.Length;
                    for (int b = 0; b < length1B; b++)
                    {
                        if (swapColorsSaveData.swapColorsData[i].customSwapColors[a].swapColorsRGBColorBytes == null
                            || swapColorsSaveData.swapColorsData[i].customSwapColors[a].swapColorsRGBColorBytes.Length != length1B)
                        {
                            Vector3[] newSwapColorsRGBColorBytes = new Vector3[length1B];
                            swapColorsSaveData.swapColorsData[i].customSwapColors[a].swapColorsRGBColorBytes = newSwapColorsRGBColorBytes;
                        }

                        for (int c = 0; c < length1B; c++)
                        {
                            swapColorsSaveData.swapColorsData[i].customSwapColors[a].swapColorsRGBColorBytes[c].x = swapColorsSaveData.swapColorsData[i].customSwapColors[a].swapColors[c].r;
                            swapColorsSaveData.swapColorsData[i].customSwapColors[a].swapColorsRGBColorBytes[c].y = swapColorsSaveData.swapColorsData[i].customSwapColors[a].swapColors[c].g;
                            swapColorsSaveData.swapColorsData[i].customSwapColors[a].swapColorsRGBColorBytes[c].z = swapColorsSaveData.swapColorsData[i].customSwapColors[a].swapColors[c].b;
                        }
                    }
                }
            }
        }

        public void DeleteSwapColorsSaveDataFile()
        {
            if (ES3.DirectoryExists("Swap Colors Save Data/Swap Colors Save Data/") == false) return;

            foreach (var fileName in ES3.GetFiles("Swap Colors Save Data/Swap Colors Save Data/"))
            {
                if (fileName != "Swap Colors Save Data.es3") continue;

                try
                {
                    ES3.DeleteFile("Swap Colors Save Data/Swap Colors Save Data/Swap Colors Save Data.es3");

#if UNITY_EDITOR
                    Debug.Log("File deleted: Swap Colors Save Data.");
#endif
                }
                catch (Exception exception)
                {
#if UNITY_EDITOR
                    Debug.Log("File deleted error: Swap Colors Save Data. " + exception);
#endif
                }
            }
        }

        #endregion

        #region Loaded Swap Colors Methods

        public void ResetLoadedSwapColorsVariables()
        {
            loadedSwapColorsOptions.GetLoadedSwapColors().Clear();
            loadedSwapColorsOptions.loadedSwapColorsIndex = -1;
        }

        public void LoadLoadedSwapColors(string characterName)
        {
            bool abort = true;
            int validLength = 0;

            int length = swapColorsSaveData.swapColorsData.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                abort = false;

                validLength = swapColorsSaveData.swapColorsData[i].defaultSwapColors[0].swapColors.Length;

                break;
            }

            if (abort == true) return;
        
            if (ES3.DirectoryExists("Swap Colors Save Data/" + characterName + "/") == true)
            {
                List<LoadedSwapColorsOptions.LoadedSwapColors> newLoadedSwapColorsList = new List<LoadedSwapColorsOptions.LoadedSwapColors>();

                foreach (var fileName in ES3.GetFiles("Swap Colors Save Data/" + characterName + "/"))
                {
                    LoadedSwapColorsOptions.LoadedSwapColors newLoadedSwapColors = new LoadedSwapColorsOptions.LoadedSwapColors();

                    try
                    {
                        newLoadedSwapColors.swapColors = ES3.Load<Color32[]>(characterName + "CustomSwapColors", "Swap Colors Save Data/" + characterName + "/" + fileName);

#if UNITY_EDITOR
                        Debug.Log("File loaded: " + "Swap Colors Save Data/" + characterName + "/" + fileName);
#endif
                    }
                    catch (Exception exception)
                    {                      
#if UNITY_EDITOR
                        Debug.Log("File loaded error: " + "Swap Colors Save Data/" + characterName + "/" + fileName + " " + exception);
#endif

                        // Don't add invalid files to list.
                        continue;
                    }    
                                 
                    newLoadedSwapColorsList.Add(newLoadedSwapColors);
                }

                // Clean up list to ensure we have valid sized entries to use.           
                int count = newLoadedSwapColorsList.Count - 1;
                for (int i = count; i >= 0; i--)
                {
                    if (newLoadedSwapColorsList[i].swapColors.Length == validLength) continue;

                    newLoadedSwapColorsList.RemoveAt(i);
                }

                // Clean up list to ensure we have valid color entries to use.
                int count1 = newLoadedSwapColorsList.Count;
                for (int i = 0; i < count1; i++)
                {
                    int lengthA = newLoadedSwapColorsList[i].swapColors.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        // Bytes have a built in behaviour to deal with tampering.
                        // If we are outside 0-255 it will auto correct to somewhere inside 0-255.
                        newLoadedSwapColorsList[i].swapColors[a] = new Color32(newLoadedSwapColorsList[i].swapColors[a].r, newLoadedSwapColorsList[i].swapColors[a].g, newLoadedSwapColorsList[i].swapColors[a].b, 255);
                    }
                }

                loadedSwapColorsOptions.loadedSwapColors = newLoadedSwapColorsList;
            }
        }

        public Color32[] GetLoadedSwapColors()
        {
            if (loadedSwapColorsOptions.GetLoadedSwapColors().Count == 0) return null;

            return loadedSwapColorsOptions.GetLoadedSwapColors()[loadedSwapColorsOptions.loadedSwapColorsIndex].swapColors;
        }

        public string GetLoadedSwapColorsName()
        {
            if (loadedSwapColorsOptions.loadedSwapColorsIndex < 0)
            {
                return loadedSwapColorsOptions.noLoadedSwapColorsName;
            }

            int loadedSwapColorsIndexName = loadedSwapColorsOptions.loadedSwapColorsIndex + 1;

            return loadedSwapColorsOptions.loadedSwapColorsName + loadedSwapColorsIndexName.ToString();
        }

        public void NextLoadedSwapColors()
        {
            if (loadedSwapColorsOptions.loadedSwapColors.Count == 0) return;

            loadedSwapColorsOptions.loadedSwapColorsIndex++;

            if (loadedSwapColorsOptions.loadedSwapColorsIndex > loadedSwapColorsOptions.loadedSwapColors.Count - 1)
            {
                loadedSwapColorsOptions.loadedSwapColorsIndex = 0;
            }
        }

        public void PreviousLoadedSwapColors()
        {
            if (loadedSwapColorsOptions.loadedSwapColors.Count == 0) return;

            loadedSwapColorsOptions.loadedSwapColorsIndex--;

            if (loadedSwapColorsOptions.loadedSwapColorsIndex < 0)
            {
                loadedSwapColorsOptions.loadedSwapColorsIndex = loadedSwapColorsOptions.loadedSwapColors.Count - 1;
            }
        }

        #endregion

        #region Character Prefabs And GameObject Methods

        private void SetCharacterPrefabsParentTransform()
        {
            if (characterPrefabScreenOptions.useMainCameraAsCharacterPrefabsParentTransform == true)
            {
                characterPrefabScreenOptions.characterPrefabsParentTransform = Camera.main.transform;
            }
        }

        private void SetCharacterPrefabsList()
        {
            int count = characterPrefabScreenOptions.characterPrefabs.Count - 1;      
            for (int i = count; i >= 0; i--)
            {
                if (characterPrefabScreenOptions.characterPrefabs[i] != null) continue;

                characterPrefabScreenOptions.characterPrefabs.RemoveAt(i);
            }
        }

        public void CreateAndDestroyCharacterGameObjectByCharacterName(string characterName, int playerNumber)
        {        
            if (playerNumber == 1)
            {
                int count = characterPrefabScreenOptions.characterPrefabs.Count;
                for (int i = 0; i < count; i++)
                {
                    if (characterPrefabScreenOptions.characterPrefabs[i] == null) continue;

                    if (characterPrefabScreenOptions.player1CharacterGameObject == null
                        && characterName == characterPrefabScreenOptions.characterPrefabs[i].characterName)
                    {
                        SpawnPlayerCharacterGameObject(1, characterPrefabScreenOptions.characterPrefabs[i]);

                        break;
                    }
                    else if (characterPrefabScreenOptions.player1CharacterGameObject != null
                        && characterName != characterPrefabScreenOptions.player1CharacterGameObject.characterName)
                    {
                        DestroyPlayerCharacterGameObject(1);

                        if (characterName == characterPrefabScreenOptions.characterPrefabs[i].characterName)
                        {
                            SpawnPlayerCharacterGameObject(1, characterPrefabScreenOptions.characterPrefabs[i]);

                            break;
                        }
                    }
                }
            }
            else if (playerNumber == 2)
            {
                int count = characterPrefabScreenOptions.characterPrefabs.Count;
                for (int i = 0; i < count; i++)
                {
                    if (characterPrefabScreenOptions.characterPrefabs[i] == null) continue;

                    if (characterPrefabScreenOptions.player2CharacterGameObject == null
                        && characterName == characterPrefabScreenOptions.characterPrefabs[i].characterName)
                    {
                        SpawnPlayerCharacterGameObject(2, characterPrefabScreenOptions.characterPrefabs[i]);

                        break;
                    }
                    else if (characterPrefabScreenOptions.player2CharacterGameObject != null
                        && characterName != characterPrefabScreenOptions.player2CharacterGameObject.characterName)
                    {
                        DestroyPlayerCharacterGameObject(2);

                        if (characterName == characterPrefabScreenOptions.characterPrefabs[i].characterName)
                        {
                            SpawnPlayerCharacterGameObject(2, characterPrefabScreenOptions.characterPrefabs[i]);

                            break;
                        }
                    }
                }
            }

            SetAllCharacterGameObjectsSpriteColorsWithSelectedSwapColors();
        }

        private void SpawnPlayerCharacterGameObject(int playerNumber, UFE2FTEPaletteSwapSpriteController characterPrefab)
        {
            if (characterPrefab == null) return;

            if (playerNumber == 1)
            {
                characterPrefabScreenOptions.player1CharacterGameObject = Instantiate(characterPrefab, characterPrefabScreenOptions.characterPrefabsParentTransform);

                if (characterPrefabScreenOptions.characterPrefabsParentTransform == null)
                {
                    characterPrefabScreenOptions.player1CharacterGameObject.transform.position = characterPrefabScreenOptions.player1CharacterPrefabSpawnPosition;
                }
                else
                {
                    characterPrefabScreenOptions.player1CharacterGameObject.transform.localPosition = characterPrefabScreenOptions.player1CharacterPrefabSpawnPosition;
                }

                characterPrefabScreenOptions.player1CharacterGameObject.transform.localEulerAngles = new Vector3(0, 0, 0);

                characterPrefabScreenOptions.player1CharacterGameObject.transform.localScale = characterPrefab.transform.localScale;

                characterPrefabScreenOptions.player1CharacterGameObject.playerNumber = 1;

                SetNetworkManagerSwapColorsData(characterPrefabScreenOptions.player1CharacterGameObject.characterName, characterPrefabScreenOptions.player1CharacterGameObject.playerNumber, GetLocalSwapColorsName(characterPrefabScreenOptions.player1CharacterGameObject.characterName, characterPrefabScreenOptions.player1CharacterGameObject.playerNumber), GetSwapColorsRGBColorBytes(characterPrefabScreenOptions.player1CharacterGameObject.characterName, characterPrefabScreenOptions.player1CharacterGameObject.playerNumber));

                int lengthA = characterPrefabScreenOptions.player1CharacterGameObject.linkedCharacterNames.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    SetLinkedCharacterNameSwapColorsData(characterPrefabScreenOptions.player1CharacterGameObject.characterName, characterPrefabScreenOptions.player1CharacterGameObject.linkedCharacterNames[a], characterPrefabScreenOptions.player1CharacterGameObject.playerNumber);
                }
            }
            else if (playerNumber == 2)
            {
                characterPrefabScreenOptions.player2CharacterGameObject = Instantiate(characterPrefab, characterPrefabScreenOptions.characterPrefabsParentTransform);

                if (characterPrefabScreenOptions.characterPrefabsParentTransform == null)
                {
                    characterPrefabScreenOptions.player2CharacterGameObject.transform.position = characterPrefabScreenOptions.player2CharacterPrefabSpawnPosition;
                }
                else
                {
                    characterPrefabScreenOptions.player2CharacterGameObject.transform.localPosition = characterPrefabScreenOptions.player2CharacterPrefabSpawnPosition;
                }

                characterPrefabScreenOptions.player2CharacterGameObject.transform.localEulerAngles = new Vector3(0, 180, 0);

                characterPrefabScreenOptions.player2CharacterGameObject.transform.localScale = characterPrefab.transform.localScale;

                characterPrefabScreenOptions.player2CharacterGameObject.playerNumber = 2;

                SetNetworkManagerSwapColorsData(characterPrefabScreenOptions.player2CharacterGameObject.characterName, characterPrefabScreenOptions.player2CharacterGameObject.playerNumber, GetLocalSwapColorsName(characterPrefabScreenOptions.player2CharacterGameObject.characterName, characterPrefabScreenOptions.player2CharacterGameObject.playerNumber), GetSwapColorsRGBColorBytes(characterPrefabScreenOptions.player2CharacterGameObject.characterName, characterPrefabScreenOptions.player2CharacterGameObject.playerNumber));

                int lengthA = characterPrefabScreenOptions.player2CharacterGameObject.linkedCharacterNames.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    SetLinkedCharacterNameSwapColorsData(characterPrefabScreenOptions.player2CharacterGameObject.characterName, characterPrefabScreenOptions.player2CharacterGameObject.linkedCharacterNames[a], characterPrefabScreenOptions.player2CharacterGameObject.playerNumber);
                }
            }
        }

        private void DestroyPlayerCharacterGameObject(int playerNumber)
        {
            if (playerNumber == 1
                && characterPrefabScreenOptions.player1CharacterGameObject != null)
            {
                Destroy(characterPrefabScreenOptions.player1CharacterGameObject.gameObject);           
            }
            else if (playerNumber == 2
                && characterPrefabScreenOptions.player2CharacterGameObject != null)
            {
                Destroy(characterPrefabScreenOptions.player2CharacterGameObject.gameObject);              
            } 
        }

        private void DestroyAllCharacterGameObjects()
        {
            if (characterPrefabScreenOptions.player1CharacterGameObject != null)
            {
                Destroy(characterPrefabScreenOptions.player1CharacterGameObject.gameObject);
            }

            if (characterPrefabScreenOptions.player2CharacterGameObject != null)
            {
                Destroy(characterPrefabScreenOptions.player2CharacterGameObject.gameObject);
            }
        }

        private void SetAllCharacterGameObjectsSpriteColorsWithDefaultSwapColors()
        {
            int length = swapColorsSaveData.swapColorsData.Length;

            for (int a = 0; a < length; a++)
            {
                if (characterPrefabScreenOptions.player1CharacterGameObject == null
                    || characterPrefabScreenOptions.player1CharacterGameObject.characterName != swapColorsSaveData.swapColorsData[a].characterName) continue;

                characterPrefabScreenOptions.player1CharacterGameObject.SwapAllSpriteColorsWithSwapColorsArray(swapColorsSaveData.swapColorsData[a].defaultSwapColors[0].swapColors);

                break;
            }

            for (int a = 0; a < length; a++)
            {
                if (characterPrefabScreenOptions.player2CharacterGameObject == null
                    || characterPrefabScreenOptions.player2CharacterGameObject.characterName != swapColorsSaveData.swapColorsData[a].characterName) continue;

                characterPrefabScreenOptions.player2CharacterGameObject.SwapAllSpriteColorsWithSwapColorsArray(swapColorsSaveData.swapColorsData[a].defaultSwapColors[0].swapColors);

                break;
            }
        }

        private void SetAllCharacterGameObjectsSpriteColorsWithSelectedSwapColors()
        {
            int count = characterPrefabScreenOptions.characterPrefabs.Count;
            int length = swapColorsSaveData.swapColorsData.Length;

            for (int i = 0; i < count; i++)
            {
                for (int a = 0; a < length; a++)
                {
                    if (characterPrefabScreenOptions.player1CharacterGameObject == null
                        || characterPrefabScreenOptions.player1CharacterGameObject.characterName != swapColorsSaveData.swapColorsData[a].characterName) continue;

                    characterPrefabScreenOptions.player1CharacterGameObject.SwapAllSpriteColorsWithSwapColorsArray(GetSwapColors(characterPrefabScreenOptions.player1CharacterGameObject.characterName, characterPrefabScreenOptions.player1CharacterGameObject.playerNumber));

                    break;
                }
            }

            for (int i = 0; i < count; i++)
            {
                for (int a = 0; a < length; a++)
                {
                    if (characterPrefabScreenOptions.player2CharacterGameObject == null
                        || characterPrefabScreenOptions.player2CharacterGameObject.characterName != swapColorsSaveData.swapColorsData[a].characterName) continue;

                    characterPrefabScreenOptions.player2CharacterGameObject.SwapAllSpriteColorsWithSwapColorsArray(GetSwapColors(characterPrefabScreenOptions.player2CharacterGameObject.characterName, characterPrefabScreenOptions.player2CharacterGameObject.playerNumber));

                    break;
                }
            }
        }

        #endregion

        #region Screen Type Methods

        private void SetScreenType(float deltaTime)
        {
            switch (screenType)
            {
                case ScreenType.None:
                    SetScreenTypeNone();

                    ResetMainMenuScreenOptions();  
                    break;

                case ScreenType.MainMenuScreen:
                    SetScreenTypeMainMenuScreen(deltaTime);
                    break;

                case ScreenType.CharacterSelectionScreen:
                    SetScreenTypeCharacterSelectionScreen();

                    ResetMainMenuScreenOptions();
                    break;

                case ScreenType.LoadingBattleScreen:
                    SetScreenTypeLoadingBattleScreen();

                    ResetMainMenuScreenOptions();

                    RemoveAllPaletteSwapSpriteControllersFromPooledPaletteTextureInfos();
                    break;
            }
        }

        private void SetScreenTypeNone()
        {
            DestroyAllCharacterGameObjects();      
        }

        private void ResetMainMenuScreenOptions()
        {
            mainMenuScreenOptions.characterSwitchTimer = mainMenuScreenOptions.characterSwitchDelay;
        }

        private void SetScreenTypeMainMenuScreen(float deltaTime)
        {
            mainMenuScreenOptions.characterSwitchTimer += deltaTime;

            if (mainMenuScreenOptions.characterSwitchTimer >= mainMenuScreenOptions.characterSwitchDelay)
            {
                mainMenuScreenOptions.characterSwitchTimer = 0;

                var DefaultScreenAnimation = Animator.StringToHash("Default Screen Animation");

                int count = characterPrefabScreenOptions.characterPrefabs.Count;

                int player1RandomIndex = UnityEngine.Random.Range(0, count);
                int player2RandomIndex = UnityEngine.Random.Range(0, count);

                CreateAndDestroyCharacterGameObjectByCharacterName(characterPrefabScreenOptions.characterPrefabs[player1RandomIndex].characterName, 1);
                CreateAndDestroyCharacterGameObjectByCharacterName(characterPrefabScreenOptions.characterPrefabs[player2RandomIndex].characterName, 2);

                for (int i = 0; i < count; i++)
                {
                    if (characterPrefabScreenOptions.player1CharacterGameObject != null
                        && characterPrefabScreenOptions.player1CharacterGameObject.myAnimator != null
                        && characterPrefabScreenOptions.player1CharacterGameObject.myAnimator.HasState(0, DefaultScreenAnimation) == true
                        && characterPrefabScreenOptions.player1CharacterGameObject.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default Screen Animation") == false)
                    {
                        characterPrefabScreenOptions.player1CharacterGameObject.myAnimator.Play("Default Screen Animation");
                    }

                    if (characterPrefabScreenOptions.player2CharacterGameObject != null
                        && characterPrefabScreenOptions.player2CharacterGameObject.myAnimator != null
                        && characterPrefabScreenOptions.player2CharacterGameObject.myAnimator.HasState(0, DefaultScreenAnimation) == true
                        && characterPrefabScreenOptions.player2CharacterGameObject.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default Screen Animation") == false)
                    {
                        characterPrefabScreenOptions.player2CharacterGameObject.myAnimator.Play("Default Screen Animation");
                    }
                }

                SetAllCharacterGameObjectsSpriteColorsWithDefaultSwapColors();
            }
        }

        private void SetScreenTypeCharacterSelectionScreen()
        {
            var DefaultScreenAnimation = Animator.StringToHash("Default Screen Animation");
            var CSSScreenAnimation = Animator.StringToHash("CSS Screen Animation");

            int count = characterPrefabScreenOptions.characterPrefabs.Count;
            int length = swapColorsSaveData.swapColorsData.Length;

            if (UFE.config.player1Character == null)
            {
                for (int i = 0; i < count; i++)
                {
                    if (characterPrefabScreenOptions.player1CharacterGameObject != null
                        && characterPrefabScreenOptions.player1CharacterGameObject.myAnimator != null
                        && characterPrefabScreenOptions.player1CharacterGameObject.myAnimator.HasState(0, DefaultScreenAnimation) == true
                        && characterPrefabScreenOptions.player1CharacterGameObject.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default Screen Animation") == false)
                    {
                        characterPrefabScreenOptions.player1CharacterGameObject.myAnimator.Play("Default Screen Animation");

                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    if (characterPrefabScreenOptions.player1CharacterGameObject != null
                        && characterPrefabScreenOptions.player1CharacterGameObject.myAnimator != null
                        && characterPrefabScreenOptions.player1CharacterGameObject.myAnimator.HasState(0, DefaultScreenAnimation) == true
                        && characterPrefabScreenOptions.player1CharacterGameObject.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default Screen Animation") == true
                        && characterPrefabScreenOptions.player1CharacterGameObject.myAnimator.HasState(0, CSSScreenAnimation) == true)
                    {
                        characterPrefabScreenOptions.player1CharacterGameObject.myAnimator.Play("CSS Screen Animation");

                        break;
                    }
                }
            }

            if (UFE.config.player2Character == null)
            {
                for (int i = 0; i < count; i++)
                {
                    if (characterPrefabScreenOptions.player2CharacterGameObject != null
                        && characterPrefabScreenOptions.player2CharacterGameObject.myAnimator != null
                        && characterPrefabScreenOptions.player2CharacterGameObject.myAnimator.HasState(0, DefaultScreenAnimation) == true
                        && characterPrefabScreenOptions.player2CharacterGameObject.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default Screen Animation") == false)
                    {
                        characterPrefabScreenOptions.player2CharacterGameObject.myAnimator.Play("Default Screen Animation");

                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    if (characterPrefabScreenOptions.player2CharacterGameObject != null
                        && characterPrefabScreenOptions.player2CharacterGameObject.myAnimator != null
                        && characterPrefabScreenOptions.player2CharacterGameObject.myAnimator.HasState(0, DefaultScreenAnimation) == true
                        && characterPrefabScreenOptions.player2CharacterGameObject.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default Screen Animation") == true
                        && characterPrefabScreenOptions.player2CharacterGameObject.myAnimator.HasState(0, CSSScreenAnimation) == true)
                    {
                        characterPrefabScreenOptions.player2CharacterGameObject.myAnimator.Play("CSS Screen Animation");

                        break;
                    }
                }
            }

            SetAllCharacterGameObjectsSpriteColorsWithSelectedSwapColors();
        }

        private void SetScreenTypeLoadingBattleScreen()
        {
            var DefaultScreenAnimation = Animator.StringToHash("Default Screen Animation");

            int count = characterPrefabScreenOptions.characterPrefabs.Count;
            for (int i = 0; i < count; i++)
            {
                if (characterPrefabScreenOptions.player1CharacterGameObject != null
                    && characterPrefabScreenOptions.player1CharacterGameObject.myAnimator != null
                    && characterPrefabScreenOptions.player1CharacterGameObject.myAnimator.HasState(0, DefaultScreenAnimation) == true
                    && characterPrefabScreenOptions.player1CharacterGameObject.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default Screen Animation") == false)
                {
                    characterPrefabScreenOptions.player1CharacterGameObject.myAnimator.Play("Default Screen Animation");
                }

                if (characterPrefabScreenOptions.player2CharacterGameObject != null
                    && characterPrefabScreenOptions.player2CharacterGameObject.myAnimator != null
                    && characterPrefabScreenOptions.player2CharacterGameObject.myAnimator.HasState(0, DefaultScreenAnimation) == true
                    && characterPrefabScreenOptions.player2CharacterGameObject.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default Screen Animation") == false)
                {
                    characterPrefabScreenOptions.player2CharacterGameObject.myAnimator.Play("Default Screen Animation");
                }
            }

            SetAllCharacterGameObjectsSpriteColorsWithSelectedSwapColors();
        }

        #endregion

        #region Swap Colors Index Methods

        public void NextSwapColorsIndex(int playerNumber)
        {
            if (playerNumber == 1)
            {
                int length = swapColorsSaveData.swapColorsData.Length;
                for (int i = 0; i < length; i++)
                {
                    if (characterPrefabScreenOptions.player1CharacterGameObject == null
                        || characterPrefabScreenOptions.player1CharacterGameObject.characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                    if (swapColorsSaveData.swapColorsData[i].player1UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default)
                    {
                        swapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex++;

                        if (swapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex > swapColorsSaveData.swapColorsData[i].defaultSwapColors.Length - 1)
                        {
                            swapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex = 0;
                            swapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex = 0;
                            swapColorsSaveData.swapColorsData[i].player1UsingSwapColors = SwapColorsSaveData.SwapColorsData.UsingSwapColors.Custom;
                        }
                    }
                    else if (swapColorsSaveData.swapColorsData[i].player1UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Custom)
                    {
                        swapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex++;

                        if (swapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex > swapColorsSaveData.swapColorsData[i].customSwapColors.Length - 1)
                        {
                            swapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex = 0;
                            swapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex = 0;
                            swapColorsSaveData.swapColorsData[i].player1UsingSwapColors = SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default;
                        }
                    }

                    SetNetworkManagerSwapColorsData(characterPrefabScreenOptions.player1CharacterGameObject.characterName, playerNumber, GetLocalSwapColorsName(characterPrefabScreenOptions.player1CharacterGameObject.characterName, playerNumber), GetSwapColorsRGBColorBytes(characterPrefabScreenOptions.player1CharacterGameObject.characterName, playerNumber));

                    int lengthA = characterPrefabScreenOptions.player1CharacterGameObject.linkedCharacterNames.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        SetLinkedCharacterNameSwapColorsData(characterPrefabScreenOptions.player1CharacterGameObject.characterName, characterPrefabScreenOptions.player1CharacterGameObject.linkedCharacterNames[a], playerNumber);
                    }

                    break;
                }
            }
            else if (playerNumber == 2)
            {
                int length = swapColorsSaveData.swapColorsData.Length;
                for (int i = 0; i < length; i++)
                {
                    if (characterPrefabScreenOptions.player2CharacterGameObject == null
                        || characterPrefabScreenOptions.player2CharacterGameObject.characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                    if (swapColorsSaveData.swapColorsData[i].player2UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default)
                    {
                        swapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex++;

                        if (swapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex > swapColorsSaveData.swapColorsData[i].defaultSwapColors.Length - 1)
                        {
                            swapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex = 0;
                            swapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex = 0;
                            swapColorsSaveData.swapColorsData[i].player2UsingSwapColors = SwapColorsSaveData.SwapColorsData.UsingSwapColors.Custom;
                        }
                    }
                    else if (swapColorsSaveData.swapColorsData[i].player2UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Custom)
                    {
                        swapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex++;

                        if (swapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex > swapColorsSaveData.swapColorsData[i].customSwapColors.Length - 1)
                        {
                            swapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex = 0;
                            swapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex = 0;
                            swapColorsSaveData.swapColorsData[i].player2UsingSwapColors = SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default;
                        }
                    }

                    SetNetworkManagerSwapColorsData(characterPrefabScreenOptions.player2CharacterGameObject.characterName, playerNumber, GetLocalSwapColorsName(characterPrefabScreenOptions.player2CharacterGameObject.characterName, playerNumber), GetSwapColorsRGBColorBytes(characterPrefabScreenOptions.player2CharacterGameObject.characterName, playerNumber));

                    int lengthA = characterPrefabScreenOptions.player2CharacterGameObject.linkedCharacterNames.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        SetLinkedCharacterNameSwapColorsData(characterPrefabScreenOptions.player2CharacterGameObject.characterName, characterPrefabScreenOptions.player2CharacterGameObject.linkedCharacterNames[a], playerNumber);
                    }

                    break;
                }
            }
        }

        public void PreviousSwapColorsIndex(int playerNumber)
        {        
            if (playerNumber == 1)
            {
                int length = swapColorsSaveData.swapColorsData.Length;
                for (int i = 0; i < length; i++)
                {
                    if (characterPrefabScreenOptions.player1CharacterGameObject == null
                        || characterPrefabScreenOptions.player1CharacterGameObject.characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                    if (swapColorsSaveData.swapColorsData[i].player1UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default)
                    {
                        swapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex--;

                        if (swapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex < 0)
                        {
                            swapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex = 0;
                            swapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex = swapColorsSaveData.swapColorsData[i].customSwapColors.Length - 1;
                            swapColorsSaveData.swapColorsData[i].player1UsingSwapColors = SwapColorsSaveData.SwapColorsData.UsingSwapColors.Custom;
                        }
                    }
                    else if (swapColorsSaveData.swapColorsData[i].player1UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Custom)
                    {
                        swapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex--;

                        if (swapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex < 0)
                        {
                            swapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex = swapColorsSaveData.swapColorsData[i].defaultSwapColors.Length - 1;
                            swapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex = 0;
                            swapColorsSaveData.swapColorsData[i].player1UsingSwapColors = SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default;
                        }
                    }

                    SetNetworkManagerSwapColorsData(characterPrefabScreenOptions.player1CharacterGameObject.characterName, playerNumber, GetLocalSwapColorsName(characterPrefabScreenOptions.player1CharacterGameObject.characterName, playerNumber), GetSwapColorsRGBColorBytes(characterPrefabScreenOptions.player1CharacterGameObject.characterName, playerNumber));

                    int lengthA = characterPrefabScreenOptions.player1CharacterGameObject.linkedCharacterNames.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        SetLinkedCharacterNameSwapColorsData(characterPrefabScreenOptions.player1CharacterGameObject.characterName, characterPrefabScreenOptions.player1CharacterGameObject.linkedCharacterNames[a], playerNumber);
                    }

                    break;
                }
            }
            else if (playerNumber == 2)
            {
                int length = swapColorsSaveData.swapColorsData.Length;
                for (int i = 0; i < length; i++)
                {
                    if (characterPrefabScreenOptions.player2CharacterGameObject == null
                        || characterPrefabScreenOptions.player2CharacterGameObject.characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                    if (swapColorsSaveData.swapColorsData[i].player2UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default)
                    {
                        swapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex--;

                        if (swapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex < 0)
                        {
                            swapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex = 0;
                            swapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex = swapColorsSaveData.swapColorsData[i].customSwapColors.Length - 1;
                            swapColorsSaveData.swapColorsData[i].player2UsingSwapColors = SwapColorsSaveData.SwapColorsData.UsingSwapColors.Custom;
                        }
                    }
                    else if (swapColorsSaveData.swapColorsData[i].player2UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Custom)
                    {
                        swapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex--;

                        if (swapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex < 0)
                        {
                            swapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex = swapColorsSaveData.swapColorsData[i].defaultSwapColors.Length - 1;
                            swapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex = 0;
                            swapColorsSaveData.swapColorsData[i].player2UsingSwapColors = SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default;
                        }
                    }

                    SetNetworkManagerSwapColorsData(characterPrefabScreenOptions.player2CharacterGameObject.characterName, playerNumber, GetLocalSwapColorsName(characterPrefabScreenOptions.player2CharacterGameObject.characterName, playerNumber), GetSwapColorsRGBColorBytes(characterPrefabScreenOptions.player2CharacterGameObject.characterName, playerNumber));

                    int lengthA = characterPrefabScreenOptions.player2CharacterGameObject.linkedCharacterNames.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        SetLinkedCharacterNameSwapColorsData(characterPrefabScreenOptions.player2CharacterGameObject.characterName, characterPrefabScreenOptions.player2CharacterGameObject.linkedCharacterNames[a], playerNumber);
                    }

                    break;
                }
            }
        }

        #endregion

        #region Set Linked Character Name Swap Colors Data Methods

        private void SetLinkedCharacterNameSwapColorsData(string characterNameToCopy, string characterNameToUpdate, int playerNumber)
        {
            SwapColorsSaveData.SwapColorsData.UsingSwapColors usingSwapColors = SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default;
            int defaultSwapColorsIndex = 0;
            int customSwapColorsIndex = 0;

            if (playerNumber == 1)
            {
                int length = swapColorsSaveData.swapColorsData.Length;
                for (int i = 0; i < length; i++)
                {
                    if (characterNameToCopy != swapColorsSaveData.swapColorsData[i].characterName) continue;

                    usingSwapColors = swapColorsSaveData.swapColorsData[i].player1UsingSwapColors;
                    defaultSwapColorsIndex = swapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex;
                    customSwapColorsIndex = swapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex;

                    break;
                }

                for (int i = 0; i < length; i++)
                {
                    if (characterNameToUpdate != swapColorsSaveData.swapColorsData[i].characterName) continue;

                    swapColorsSaveData.swapColorsData[i].player1UsingSwapColors = usingSwapColors;
                    swapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex = defaultSwapColorsIndex;
                    swapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex = customSwapColorsIndex;

                    SetNetworkManagerSwapColorsData(characterNameToUpdate, playerNumber, GetLocalSwapColorsName(characterNameToUpdate, playerNumber), GetSwapColorsRGBColorBytes(characterNameToUpdate, playerNumber));

                    break;
                }
            }
            else if (playerNumber == 2)
            {
                int length = swapColorsSaveData.swapColorsData.Length;
                for (int i = 0; i < length; i++)
                {
                    if (characterNameToCopy != swapColorsSaveData.swapColorsData[i].characterName) continue;

                    usingSwapColors = swapColorsSaveData.swapColorsData[i].player2UsingSwapColors;
                    defaultSwapColorsIndex = swapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex;
                    customSwapColorsIndex = swapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex;

                    break;
                }

                for (int i = 0; i < length; i++)
                {
                    if (characterNameToUpdate == swapColorsSaveData.swapColorsData[i].characterName) continue;

                    swapColorsSaveData.swapColorsData[i].player2UsingSwapColors = usingSwapColors;
                    swapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex = defaultSwapColorsIndex;
                    swapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex = customSwapColorsIndex;

                    SetNetworkManagerSwapColorsData(characterNameToUpdate, playerNumber, GetLocalSwapColorsName(characterNameToUpdate, playerNumber), GetSwapColorsRGBColorBytes(characterNameToUpdate, playerNumber));

                    break;
                }
            }
        }

        #endregion

        #region Get Swap Colors Methods

        public Color32[] GetSwapColors(string characterName, int playerNumber)
        {
            if (UFE.gameMode != GameMode.NetworkGame)
            {
                return GetLocalSwapColors(characterName, playerNumber);              
            }
            else
            {
                return GetNetworkSwapColors(characterName, playerNumber);
            }
        }

        private Color32[] GetLocalSwapColors(string characterName, int playerNumber)
        {
            if (playerNumber == 1)
            {
                int length = swapColorsSaveData.swapColorsData.Length;
                for (int i = 0; i < length; i++)
                {
                    if (characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                    if (swapColorsSaveData.swapColorsData[i].player1UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default)
                    {
                        return swapColorsSaveData.swapColorsData[i].defaultSwapColors[swapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex].swapColors;
                    }
                    else if (swapColorsSaveData.swapColorsData[i].player1UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Custom)
                    {
                        return swapColorsSaveData.swapColorsData[i].customSwapColors[swapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex].swapColors;
                    }
                }
            }
            else if (playerNumber == 2)
            {
                int length = swapColorsSaveData.swapColorsData.Length;
                for (int i = 0; i < length; i++)
                {
                    if (characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                    if (swapColorsSaveData.swapColorsData[i].player2UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default)
                    {
                        return swapColorsSaveData.swapColorsData[i].defaultSwapColors[swapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex].swapColors;
                    }
                    else if (swapColorsSaveData.swapColorsData[i].player2UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Custom)
                    {
                        return swapColorsSaveData.swapColorsData[i].customSwapColors[swapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex].swapColors;
                    }
                }
            }

            return null;
        }

        private Color32[] GetNetworkSwapColors(string characterName, int playerNumber)
        {
            if (playerNumber == 1)
            {
                if (GetNetworkSwapColorsFromNetworkManager(characterName, playerNumber) != null)
                {
                    return GetNetworkSwapColorsFromNetworkManager(characterName, playerNumber);
                }
                else
                {
                    return GetLocalSwapColors(characterName, playerNumber);
                }
            }
            else if (playerNumber == 2)
            {
                if (GetNetworkSwapColorsFromNetworkManager(characterName, playerNumber) != null)
                {
                    return GetNetworkSwapColorsFromNetworkManager(characterName, playerNumber);
                }
                else
                {
                    return GetLocalSwapColors(characterName, playerNumber);
                }
            }

            return null;
        }

        #endregion

        #region Get Swap Colors Name Methods

        public string GetSwapColorsName(string characterName, int playerNumber)
        {
            if (UFE.gameMode != GameMode.NetworkGame)
            {
                return GetLocalSwapColorsName(characterName, playerNumber);
            }
            else
            {
                return GetNetworkSwapColorsName(characterName, playerNumber);              
            }
        }

        private string GetLocalSwapColorsName(string characterName, int playerNumber)
        {
            if (playerNumber == 1)
            {
                int length = swapColorsSaveData.swapColorsData.Length;
                for (int i = 0; i < length; i++)
                {
                    if (characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                    if (swapColorsSaveData.swapColorsData[i].player1UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default)
                    {
                        return swapColorsSaveDataOptions.defaultSwapColorNames[swapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex];
                    }
                    else if (swapColorsSaveData.swapColorsData[i].player1UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Custom)
                    {
                        return swapColorsSaveDataOptions.customSwapColorNames[swapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex];
                    }
                }
            }
            else if (playerNumber == 2)
            {
                int length = swapColorsSaveData.swapColorsData.Length;
                for (int i = 0; i < length; i++)
                {
                    if (characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                    if (swapColorsSaveData.swapColorsData[i].player2UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default)
                    {
                        return swapColorsSaveDataOptions.defaultSwapColorNames[swapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex];
                    }
                    else if (swapColorsSaveData.swapColorsData[i].player2UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Custom)
                    {
                        return swapColorsSaveDataOptions.customSwapColorNames[swapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex];
                    }
                }
            }

            return "";
        }

        private string GetNetworkSwapColorsName(string characterName, int playerNumber)
        {
            if (playerNumber == 1)
            {
                if (GetNetworkSwapColorsNameFromNetworkManager(characterName, playerNumber) != "")
                {
                    return GetNetworkSwapColorsNameFromNetworkManager(characterName, playerNumber);
                }
                else
                {
                    return GetLocalSwapColorsName(characterName, playerNumber);
                }
            }
            else if (playerNumber == 2)
            {
                if (GetNetworkSwapColorsNameFromNetworkManager(characterName, playerNumber) != "")
                {
                    return GetNetworkSwapColorsNameFromNetworkManager(characterName, playerNumber);
                }
                else
                {
                    return GetLocalSwapColorsName(characterName, playerNumber);
                }
            }

            return "";
        }

        #endregion

        #region Get Swap Colors RGB Color Bytes Methods

        private Vector3[] GetSwapColorsRGBColorBytes(string characterName, int playerNumber)
        {     
            if (playerNumber == 1)
            {
                int length = swapColorsSaveData.swapColorsData.Length;
                for (int i = 0; i < length; i++)
                {
                    if (characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                    if (swapColorsSaveData.swapColorsData[i].player1UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default)
                    {
                        return swapColorsSaveData.swapColorsData[i].defaultSwapColors[swapColorsSaveData.swapColorsData[i].player1DefaultSwapColorsIndex].swapColorsRGBColorBytes;
                    }
                    else if (swapColorsSaveData.swapColorsData[i].player1UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Custom)
                    {
                        return swapColorsSaveData.swapColorsData[i].customSwapColors[swapColorsSaveData.swapColorsData[i].player1CustomSwapColorsIndex].swapColorsRGBColorBytes;
                    }
                }
            }
            else if (playerNumber == 2)
            {
                int length = swapColorsSaveData.swapColorsData.Length;
                for (int i = 0; i < length; i++)
                {
                    if (characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                    if (swapColorsSaveData.swapColorsData[i].player2UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Default)
                    {
                        return swapColorsSaveData.swapColorsData[i].defaultSwapColors[swapColorsSaveData.swapColorsData[i].player2DefaultSwapColorsIndex].swapColorsRGBColorBytes;
                    }
                    else if (swapColorsSaveData.swapColorsData[i].player2UsingSwapColors == SwapColorsSaveData.SwapColorsData.UsingSwapColors.Custom)
                    {
                        return swapColorsSaveData.swapColorsData[i].customSwapColors[swapColorsSaveData.swapColorsData[i].player2CustomSwapColorsIndex].swapColorsRGBColorBytes;
                    }
                }
            }

            return null;
        }

        #endregion

        #region Palette Texture Object Pools Methods

        public void SetAllPaletteTextureObjectPoolsSize(int poolSize)
        {
            int count = paletteTextureObjectPoolOptions.paletteTextureInfos.Count;
            for (int i = 0; i < count; i++)
            {
                paletteTextureObjectPoolOptions.paletteTextureInfos[i].poolSize = poolSize;
            }

            SetAllPaletteTextureObjectPools();
        }

        public void SetPaletteTextureObjectPoolsSizeByCharacterName(int poolSize, string characterName = null, string[] characterNamesArray = null, List<string> characterNamesList = null)
        {
            if (characterName != null)
            {
                int count = paletteTextureObjectPoolOptions.paletteTextureInfos.Count;
                for (int i = 0; i < count; i++)
                {
                    if (characterName != paletteTextureObjectPoolOptions.paletteTextureInfos[i].characterName) continue;

                    paletteTextureObjectPoolOptions.paletteTextureInfos[i].poolSize = poolSize;

                    SetAllPaletteTextureObjectPools();

                    break;
                }
            }

            if (characterNamesArray != null)
            {
                int count = paletteTextureObjectPoolOptions.paletteTextureInfos.Count;
                for (int i = 0; i < count; i++)
                {
                    int lengthA = characterNamesArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (characterNamesArray[a] != paletteTextureObjectPoolOptions.paletteTextureInfos[i].characterName) continue;

                        paletteTextureObjectPoolOptions.paletteTextureInfos[i].poolSize = poolSize;

                        SetAllPaletteTextureObjectPools();

                        break;
                    }
                }
            }

            if (characterNamesList != null)
            {
                int count = paletteTextureObjectPoolOptions.paletteTextureInfos.Count;
                for (int i = 0; i < count; i++)
                {
                    int countA = characterNamesList.Count;
                    for (int a = 0; a < countA; a++)
                    {
                        if (characterNamesList[a] != paletteTextureObjectPoolOptions.paletteTextureInfos[i].characterName) continue;

                        paletteTextureObjectPoolOptions.paletteTextureInfos[i].poolSize = poolSize;

                        SetAllPaletteTextureObjectPools();

                        break;
                    }
                }
            }
        }

        public void SetPaletteTextureObjectPoolsByUFEPreload(int poolSize)
        {
            if (UFE.config.player1Character == null
                || UFE.config.player2Character == null) return;

            if (UFE.config.player1Character.characterPrefabStorage == StorageMode.Prefab
                && UFE.config.player1Character.characterPrefab != null)
            {
                UFE2FTEPaletteSwapSpriteController player1PaletteSwapSpriteController = UFE.config.player1Character.characterPrefab.GetComponent<UFE2FTEPaletteSwapSpriteController>();

                if (player1PaletteSwapSpriteController != null)
                {
                    SetPaletteTextureObjectPoolsSizeByCharacterName(poolSize, player1PaletteSwapSpriteController.characterName, player1PaletteSwapSpriteController.linkedCharacterNames);
                }
            }
            else
            {
                UFE2FTEPaletteSwapSpriteController player1PaletteSwapSpriteController = Resources.Load<GameObject>(UFE.config.player1Character.prefabResourcePath).GetComponent<UFE2FTEPaletteSwapSpriteController>();

                if (player1PaletteSwapSpriteController != null)
                {
                    SetPaletteTextureObjectPoolsSizeByCharacterName(poolSize, player1PaletteSwapSpriteController.characterName, player1PaletteSwapSpriteController.linkedCharacterNames);
                }
            }

            if (UFE.config.player2Character.characterPrefabStorage == StorageMode.Prefab
                && UFE.config.player2Character.characterPrefab != null)
            {
                UFE2FTEPaletteSwapSpriteController player2PaletteSwapSpriteController = UFE.config.player2Character.characterPrefab.GetComponent<UFE2FTEPaletteSwapSpriteController>();

                if (player2PaletteSwapSpriteController != null)
                {
                    SetPaletteTextureObjectPoolsSizeByCharacterName(poolSize, player2PaletteSwapSpriteController.characterName, player2PaletteSwapSpriteController.linkedCharacterNames);
                }
            }
            else
            {
                UFE2FTEPaletteSwapSpriteController player2PaletteSwapSpriteController = Resources.Load<GameObject>(UFE.config.player2Character.prefabResourcePath).GetComponent<UFE2FTEPaletteSwapSpriteController>();

                if (player2PaletteSwapSpriteController != null)
                {
                    SetPaletteTextureObjectPoolsSizeByCharacterName(poolSize, player2PaletteSwapSpriteController.characterName, player2PaletteSwapSpriteController.linkedCharacterNames);
                }
            }

            List<MoveSetData> loadedMoveSets = new List<MoveSetData>();
            foreach (MoveSetData moveSetData in UFE.config.player1Character.moves)
            {
                loadedMoveSets.Add(moveSetData);
            }
            foreach (string path in UFE.config.player1Character.stanceResourcePath)
            {
                loadedMoveSets.Add(Resources.Load<StanceInfo>(path).ConvertData());
            }

            foreach (MoveSetData moveSetData in UFE.config.player2Character.moves)
            {
                loadedMoveSets.Add(moveSetData);
            }
            foreach (string path in UFE.config.player2Character.stanceResourcePath)
            {
                loadedMoveSets.Add(Resources.Load<StanceInfo>(path).ConvertData());
            }

            foreach (MoveSetData moveSet in loadedMoveSets)
            {
                foreach (MoveInfo move in moveSet.attackMoves)
                {
                    foreach (CharacterAssist assist in move.characterAssist)
                    {
                        if (assist.characterInfo == null) continue;

                        SetPaletteTextureObjectPoolsSizeByCharacterName(poolSize, assist.characterInfo.characterName);
                    }
                }
            }

            loadedMoveSets.Clear();
        }

        private void SetAllPaletteTextureObjectPools()
        {
            int count = paletteTextureObjectPoolOptions.paletteTextureInfos.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                int countA = paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos().Count - 1;
                for (int a = countA; a >= 0; a--)
                {
                    if (paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos()[a].paletteTexture != null) continue;

                    paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos().RemoveAt(a);
                }

                if (paletteTextureObjectPoolOptions.paletteTextureInfos[i].paletteTexture == null)
                {
                    paletteTextureObjectPoolOptions.paletteTextureInfos.RemoveAt(i);
                }
            }

            int count1 = paletteTextureObjectPoolOptions.paletteTextureInfos.Count;
            for (int i = 0; i < count1; i++)
            {
                if (paletteTextureObjectPoolOptions.paletteTextureInfos[i].poolSize <= 0)
                {
                    int countA = paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos().Count - 1;
                    for (int a = countA; a >= 0; a--)
                    {
                        if (paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos()[a].paletteSwapSpriteController != null) continue;

                        if (paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos()[a].paletteTexture != null)
                        {
                            Destroy(paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos()[a].paletteTexture);
                        }

                        paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos().RemoveAt(a);
                    }
                }

                for (int a = 0; a < paletteTextureObjectPoolOptions.paletteTextureInfos[i].poolSize; a++)
                {
                    if (paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos().Count < paletteTextureObjectPoolOptions.paletteTextureInfos[i].poolSize)
                    {
                        PaletteTextureObjectPoolOptions.PooledPaletteTextureInfo newPooledPaletteTextureInfo = new PaletteTextureObjectPoolOptions.PooledPaletteTextureInfo();

                        newPooledPaletteTextureInfo.characterName = paletteTextureObjectPoolOptions.paletteTextureInfos[i].characterName;

                        int paletteTextureWidth = paletteTextureObjectPoolOptions.paletteTextureInfos[i].paletteTexture.width;

                        newPooledPaletteTextureInfo.swapColors = new Color32[paletteTextureWidth];
                        for (int b = 0; b < paletteTextureWidth; b++)
                        {
                            newPooledPaletteTextureInfo.swapColors[b] = new Color32(0, 0, 0, 255);
                        }

                        newPooledPaletteTextureInfo.paletteTexture = new Texture2D(paletteTextureWidth, 1, TextureFormat.RGBA32, false, false);
                        newPooledPaletteTextureInfo.paletteTexture.filterMode = FilterMode.Point;
                        newPooledPaletteTextureInfo.paletteTexture.wrapMode = TextureWrapMode.Clamp;
                        for (int b = 0; b < paletteTextureWidth; b++)
                        {
                            newPooledPaletteTextureInfo.paletteTexture.SetPixel(a, 0, new Color32(0, 0, 0, 255));
                        }
                        newPooledPaletteTextureInfo.paletteTexture.Apply();

                        newPooledPaletteTextureInfo.paletteSwapSpriteController = null;

                        paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos().Add(newPooledPaletteTextureInfo);
                    }
                    else if (paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos().Count > paletteTextureObjectPoolOptions.paletteTextureInfos[i].poolSize)
                    {
                        int countB = paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos().Count - 1;
                        for (int b = countB; b >= 0; b--)
                        {
                            if (paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos()[b].paletteSwapSpriteController != null) continue;

                            if (paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos()[b].paletteTexture != null)
                            {
                                Destroy(paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos()[b].paletteTexture);
                            }

                            paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos().RemoveAt(b);

                            if (paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos().Count <= paletteTextureObjectPoolOptions.paletteTextureInfos[i].poolSize) break;
                        }
                    }
                }
            }
        }

        public PaletteTextureObjectPoolOptions.PooledPaletteTextureInfo GetPooledPaletteTextureInfoFromPooledPaletteTextureInfos(UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController)
        {
            int count = paletteTextureObjectPoolOptions.paletteTextureInfos.Count;
            for (int i = 0; i < count; i++)
            {
                if (paletteSwapSpriteController.characterName != paletteTextureObjectPoolOptions.paletteTextureInfos[i].characterName) continue;

                int countA = paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos().Count;
                for (int a = 0; a < countA; a++)
                {
                    if (paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos()[a].paletteSwapSpriteController == null)
                    {
                        paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos()[a].paletteSwapSpriteController = paletteSwapSpriteController;

                        return paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos()[a];
                    }

                    if (paletteSwapSpriteController == paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos()[a].paletteSwapSpriteController)
                    {
                        paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos()[a].paletteSwapSpriteController = paletteSwapSpriteController;

                        return paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos()[a];
                    }
                }

                if (paletteTextureObjectPoolOptions.paletteTextureInfos[i].usePoolCanGrow == false) return null;

                SetPaletteTextureObjectPoolsSizeByCharacterName(paletteTextureObjectPoolOptions.paletteTextureInfos[i].poolSize + 1, paletteSwapSpriteController.characterName, paletteSwapSpriteController.linkedCharacterNames);

                return GetPooledPaletteTextureInfoFromPooledPaletteTextureInfos(paletteSwapSpriteController);
            }

            return null;
        }

        public void DestroyAllPooledPaletteTextureInfos()
        {
            int count = paletteTextureObjectPoolOptions.paletteTextureInfos.Count;
            for (int i = 0; i < count; i++)
            {
                int countA = paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos().Count;
                for (int a = 0; a < countA; a++)
                {
                    Destroy(paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos()[a].paletteTexture);
                }

                paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos().Clear();
            }
        }

        public bool CheckIfPaletteTextureIsPooledPaletteTexture(Texture2D paletteTexture)
        {
            int count = paletteTextureObjectPoolOptions.paletteTextureInfos.Count;
            for (int i = 0; i < count; i++)
            {
                int countA = paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos().Count;
                for (int a = 0; a < countA; a++)
                {
                    if (paletteTexture != paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos()[a].paletteTexture) continue;

                    return true;
                }
            }

            return false;
        }

        private void RemoveAllPaletteSwapSpriteControllersFromPooledPaletteTextureInfos()
        {
            int count = paletteTextureObjectPoolOptions.paletteTextureInfos.Count;
            for (int i = 0; i < count; i++)
            {
                int countA = paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos().Count;
                for (int a = 0; a < countA; a++)
                {
                    paletteTextureObjectPoolOptions.paletteTextureInfos[i].GetPooledPaletteTextureInfos()[a].paletteSwapSpriteController = null;
                }
            }
        }

        #endregion

        #region Ghost Object Pools Methods

        public void SetAllGhostObjectPoolsSize(int poolSize)
        {
            if (ghostObjectPoolOptions.ghostPrefab == null) return;

            ghostObjectPoolOptions.poolSize = poolSize;

            SetAllGhostObjectPools();
        }

        private void SetAllGhostObjectPools()
        {
            if (ghostObjectPoolOptions.ghostPrefab == null) return;

            int count = ghostObjectPoolOptions.GetPooledGhostGameObjects().Count - 1;

            if (ghostObjectPoolOptions.poolSize <= 0)
            {
                for (int i = count; i >= 0; i--)
                {
                    if (ghostObjectPoolOptions.GetPooledGhostGameObjects()[i] != null)
                    {
                        Destroy(ghostObjectPoolOptions.GetPooledGhostGameObjects()[i].gameObject);
                    }

                    ghostObjectPoolOptions.GetPooledGhostGameObjects().RemoveAt(i);
                }

                return;
            }

            for (int i = count; i >= 0; i--)
            {
                if (ghostObjectPoolOptions.GetPooledGhostGameObjects()[i] != null) continue;

                ghostObjectPoolOptions.GetPooledGhostGameObjects().RemoveAt(i);
            }

            for (int i = 0; i < ghostObjectPoolOptions.poolSize; i++)
            {
                if (ghostObjectPoolOptions.GetPooledGhostGameObjects().Count < ghostObjectPoolOptions.poolSize)
                {
                    UFE2FTEPaletteSwapSpriteController pooledGhostGameObject = Instantiate(ghostObjectPoolOptions.ghostPrefab);

                    pooledGhostGameObject.gameObject.SetActive(false);

                    ghostObjectPoolOptions.GetPooledGhostGameObjects().Add(pooledGhostGameObject);
                }
                else if (ghostObjectPoolOptions.GetPooledGhostGameObjects().Count > ghostObjectPoolOptions.poolSize)
                {
                    for (int a = count; a >= 0; a--)
                    {
                        if (ghostObjectPoolOptions.GetPooledGhostGameObjects()[a] != null)
                        {
                            Destroy(ghostObjectPoolOptions.GetPooledGhostGameObjects()[a].gameObject);
                        }

                        ghostObjectPoolOptions.GetPooledGhostGameObjects().RemoveAt(a);

                        if (ghostObjectPoolOptions.GetPooledGhostGameObjects().Count <= ghostObjectPoolOptions.poolSize) break;
                    }
                }
            }
        }

        public UFE2FTEPaletteSwapSpriteController GetPooledGhostGameObjectFromPooledGhostGameObjectsList()
        {
            if (ghostObjectPoolOptions.ghostPrefab == null) return null;

            int count = ghostObjectPoolOptions.GetPooledGhostGameObjects().Count;
            for (int i = 0; i < count; i++)
            {
                if (ghostObjectPoolOptions.GetPooledGhostGameObjects()[i] == null
                    || ghostObjectPoolOptions.GetPooledGhostGameObjects()[i].gameObject.activeInHierarchy == true) continue;

                return ghostObjectPoolOptions.GetPooledGhostGameObjects()[i];
            }

            if (ghostObjectPoolOptions.usePoolCanGrow == false) return null;

            SetAllGhostObjectPoolsSize(ghostObjectPoolOptions.poolSize + 1);

            return GetPooledGhostGameObjectFromPooledGhostGameObjectsList();
        }

        #endregion

        #region Palette Editor Sprite Methods

        public Color32[] PaletteEditorSpriteGetDefaultSwapColors(string characterName)
        {
            int length = swapColorsSaveData.swapColorsData.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                return swapColorsSaveData.swapColorsData[i].defaultSwapColors[0].swapColors;              
            }

            return null;
        }

        public Color32[] PaletteEditorSpriteGetCustomSwapColors(string characterName, int customSwapColorsIndex)
        {
            int length = swapColorsSaveData.swapColorsData.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                return swapColorsSaveData.swapColorsData[i].customSwapColors[customSwapColorsIndex].swapColors;               
            }

            return null;
        }

        public string PaletteEditorSpriteGetPalettePartName(string characterName, int palettePartNameIndex)
        {
            int length = swapColorsSaveData.swapColorsData.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                return swapColorsSaveData.swapColorsData[i].partNames[palettePartNameIndex];
            }

            return "";
        }

        public int PaletteEditorSpriteGetNextPalettePartIndex(string characterName, int palettePartNameIndex)
        {
            int length = swapColorsSaveData.swapColorsData.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                palettePartNameIndex += 1;

                if (palettePartNameIndex > swapColorsSaveData.swapColorsData[i].partNames.Length - 1)
                {
                    palettePartNameIndex = 0;
                }

                break;
            }

            return palettePartNameIndex;
        }

        public int PaletteEditorSpriteGetPreviousPalettePartIndex(string characterName, int palettePartNameIndex)
        {
            int length = swapColorsSaveData.swapColorsData.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                palettePartNameIndex -= 1;

                if (palettePartNameIndex < 0)
                {
                    palettePartNameIndex = swapColorsSaveData.swapColorsData[i].partNames.Length - 1;
                }

                break;
            }

            return palettePartNameIndex;
        }

        public void PaletteEditorSpriteSetCustomSwapColors(string characterName, int customSwapColorsIndex, Color32[] colors)
        {
            int length = swapColorsSaveData.swapColorsData.Length;
            int lengthA = colors.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterName != swapColorsSaveData.swapColorsData[i].characterName) continue;

                if (lengthA != swapColorsSaveData.swapColorsData[i].defaultSwapColors[0].swapColors.Length) return;

                for (int a = 0; a < lengthA; a++)
                {
                    swapColorsSaveData.swapColorsData[i].customSwapColors[customSwapColorsIndex].swapColors[a] = new Color32(colors[a].r, colors[a].g, colors[a].b, 255);
                }

                return;
            }
        }

        #endregion

        #region Network Manager Methods

        private void CreateNetworkManager()
        {
            if (networkManagerOptions.photon2NetworkManagerPrefab != null
                && UFE2FTEPaletteSwapSpritePhoton2NetworkManager.instance == null)
            {
                Instantiate(networkManagerOptions.photon2NetworkManagerPrefab, transform);
            }
        }

        private void DestroyNetworkManager()
        {
            if (UFE2FTEPaletteSwapSpritePhoton2NetworkManager.instance != null)
            {
                Destroy(UFE2FTEPaletteSwapSpritePhoton2NetworkManager.instance.gameObject);
            }
        }

        private void SetNetworkManager()
        {
            if (UFE.isConnected == true)
            {
                CreateNetworkManager();
            }
            else
            {
                DestroyNetworkManager();
            }
        }

        private void SetNetworkManagerSwapColorsData(string characterName, int playerNumber, string swapColorsName, Vector3[] swapColorsRGBColorBytes)
        {
            if (UFE.gameMode != GameMode.NetworkGame
                || UFE.isConnected == false) return;

            if (UFE2FTEPaletteSwapSpritePhoton2NetworkManager.instance != null)
            {
                if (playerNumber == 1
                    //&& UFE.GetLocalPlayer() == 1)
                    && UFE.fluxCapacitor.PlayerManager.player1.isLocalPlayer == true)
                {
                    UFE2FTEPaletteSwapSpritePhoton2NetworkManager.instance.photonView.RPC("SetSwapColorsDataRPC", RpcTarget.All, characterName, playerNumber, swapColorsName, swapColorsRGBColorBytes);
                }
                else if (playerNumber == 2
                    //&& UFE.GetLocalPlayer() == 2)
                    && UFE.fluxCapacitor.PlayerManager.player2.isLocalPlayer == true)
                {
                    UFE2FTEPaletteSwapSpritePhoton2NetworkManager.instance.photonView.RPC("SetSwapColorsDataRPC", RpcTarget.All, characterName, playerNumber, swapColorsName, swapColorsRGBColorBytes);
                }
            }
        }

        private Color32[] GetNetworkSwapColorsFromNetworkManager(string characterName, int playerNumber)
        {
            if (UFE.gameMode != GameMode.NetworkGame
                || UFE.isConnected == false) return null;

            if (UFE2FTEPaletteSwapSpritePhoton2NetworkManager.instance != null)
            {
                return UFE2FTEPaletteSwapSpritePhoton2NetworkManager.instance.GetSwapColors(characterName, playerNumber);
            }

            return null;
        }

        private string GetNetworkSwapColorsNameFromNetworkManager(string characterName, int playerNumber)
        {
            if (UFE.gameMode != GameMode.NetworkGame
                || UFE.isConnected == false) return "";

            if (UFE2FTEPaletteSwapSpritePhoton2NetworkManager.instance != null)
            {
                return UFE2FTEPaletteSwapSpritePhoton2NetworkManager.instance.GetSwapColorsName(characterName, playerNumber);
            }

            return "";
        }

        #endregion    
    }
}

#region Old Code

/*public void CreateSwapColorsMaterialPoolsByUFEPreload()
{
    if (UFE.config.player1Character == null
        || UFE.config.player2Character == null) return;

    if (UFE.config.hitOptions.weakHit.hitParticle != null)
    {
        UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = UFE.config.hitOptions.weakHit.hitParticle.GetComponent<UFE2FTEPaletteSwapSpriteController>();

        if (paletteSwapSpriteController != null)
        {
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
        }
    }

    if (UFE.config.hitOptions.mediumHit.hitParticle != null)
    {
        UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = UFE.config.hitOptions.mediumHit.hitParticle.GetComponent<UFE2FTEPaletteSwapSpriteController>();

        if (paletteSwapSpriteController != null)
        {
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
        }
    }

    if (UFE.config.hitOptions.heavyHit.hitParticle != null)
    {
        UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = UFE.config.hitOptions.heavyHit.hitParticle.GetComponent<UFE2FTEPaletteSwapSpriteController>();

        if (paletteSwapSpriteController != null)
        {
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
        }
    }

    if (UFE.config.hitOptions.crumpleHit.hitParticle != null)
    {
        UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = UFE.config.hitOptions.crumpleHit.hitParticle.GetComponent<UFE2FTEPaletteSwapSpriteController>();

        if (paletteSwapSpriteController != null)
        {
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
        }
    }

    if (UFE.config.hitOptions.customHit1.hitParticle != null)
    {
        UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = UFE.config.hitOptions.customHit1.hitParticle.GetComponent<UFE2FTEPaletteSwapSpriteController>();

        if (paletteSwapSpriteController != null)
        {
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
        }
    }

    if (UFE.config.hitOptions.customHit2.hitParticle != null)
    {
        UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = UFE.config.hitOptions.customHit2.hitParticle.GetComponent<UFE2FTEPaletteSwapSpriteController>();

        if (paletteSwapSpriteController != null)
        {
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
        }
    }

    if (UFE.config.hitOptions.customHit3.hitParticle != null)
    {
        UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = UFE.config.hitOptions.customHit3.hitParticle.GetComponent<UFE2FTEPaletteSwapSpriteController>();

        if (paletteSwapSpriteController != null)
        {
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
        }
    }

    if (UFE.config.hitOptions.customHit4.hitParticle != null)
    {
        UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = UFE.config.hitOptions.customHit4.hitParticle.GetComponent<UFE2FTEPaletteSwapSpriteController>();

        if (paletteSwapSpriteController != null)
        {
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
        }
    }

    if (UFE.config.hitOptions.customHit5.hitParticle != null)
    {
        UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = UFE.config.hitOptions.customHit5.hitParticle.GetComponent<UFE2FTEPaletteSwapSpriteController>();

        if (paletteSwapSpriteController != null)
        {
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
        }
    }

    if (UFE.config.hitOptions.customHit6.hitParticle != null)
    {
        UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = UFE.config.hitOptions.customHit6.hitParticle.GetComponent<UFE2FTEPaletteSwapSpriteController>();

        if (paletteSwapSpriteController != null)
        {
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
        }
    }

    if (UFE.config.blockOptions.blockHitEffects.hitParticle != null)
    {
        UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = UFE.config.blockOptions.blockHitEffects.hitParticle.GetComponent<UFE2FTEPaletteSwapSpriteController>();

        if (paletteSwapSpriteController != null)
        {
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
        }
    }

    if (UFE.config.blockOptions.parryHitEffects.hitParticle != null)
    {
        UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = UFE.config.blockOptions.parryHitEffects.hitParticle.GetComponent<UFE2FTEPaletteSwapSpriteController>();

        if (paletteSwapSpriteController != null)
        {
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
        }
    }

    if (UFE.config.groundBounceOptions.bouncePrefab != null)
    {
        UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = UFE.config.groundBounceOptions.bouncePrefab.GetComponent<UFE2FTEPaletteSwapSpriteController>();

        if (paletteSwapSpriteController != null)
        {
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
        }
    }

    if (UFE.config.wallBounceOptions.bouncePrefab != null)
    {
        UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = UFE.config.wallBounceOptions.bouncePrefab.GetComponent<UFE2FTEPaletteSwapSpriteController>();

        if (paletteSwapSpriteController != null)
        {
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
        }
    }

    CreatePooledSwapColorsMaterialInfo(UFE.config.player1Character.characterName, 1);
    CreatePooledSwapColorsMaterialInfo(UFE.config.player1Character.characterName, 2);

    CreatePooledSwapColorsMaterialInfo(UFE.config.player2Character.characterName, 1);
    CreatePooledSwapColorsMaterialInfo(UFE.config.player2Character.characterName, 2);

    List<MoveSetData> loadedMoveSets = new List<MoveSetData>();

    foreach (MoveSetData moveSetData in UFE.config.player1Character.moves)
    {
        loadedMoveSets.Add(moveSetData);
    }
    foreach (string path in UFE.config.player1Character.stanceResourcePath)
    {
        loadedMoveSets.Add(Resources.Load<StanceInfo>(path).ConvertData());
    }

    foreach (MoveSetData moveSetData in UFE.config.player2Character.moves)
    {
        loadedMoveSets.Add(moveSetData);
    }
    foreach (string path in UFE.config.player2Character.stanceResourcePath)
    {
        loadedMoveSets.Add(Resources.Load<StanceInfo>(path).ConvertData());
    }

    foreach (MoveSetData moveSet in loadedMoveSets)
    {
        foreach (MoveInfo move in moveSet.attackMoves)
        {
            foreach (CharacterAssist assist in move.characterAssist)
            {
                if (assist.characterInfo != null)
                {
                    CreatePooledSwapColorsMaterialInfo(assist.characterInfo.characterName, 1);
                    CreatePooledSwapColorsMaterialInfo(assist.characterInfo.characterName, 2);
                }
            }

            foreach (Hit hitInfo in move.hits)
            {
                if (hitInfo.hitEffects.hitParticle != null)
                {
                    UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = hitInfo.hitEffects.hitParticle.GetComponent<UFE2FTEPaletteSwapSpriteController>();

                    if (paletteSwapSpriteController != null)
                    {
                        CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
                        CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
                    }
                }

                if (hitInfo.hitEffectsBlock.hitParticle != null)
                {
                    UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = hitInfo.hitEffectsBlock.hitParticle.GetComponent<UFE2FTEPaletteSwapSpriteController>();

                    if (paletteSwapSpriteController != null)
                    {
                        CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
                        CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
                    }
                }
            }

            foreach (MoveParticleEffect particle in move.particleEffects)
            {
                if (particle.particleEffect.prefab != null)
                {
                    UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = particle.particleEffect.prefab.GetComponent<UFE2FTEPaletteSwapSpriteController>();

                    if (paletteSwapSpriteController != null)
                    {
                        CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
                        CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
                    }
                }
            }

            foreach (Projectile projectile in move.projectiles)
            {
                if (projectile.projectilePrefab != null)
                {
                    UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = projectile.projectilePrefab.GetComponent<UFE2FTEPaletteSwapSpriteController>();

                    if (paletteSwapSpriteController != null)
                    {
                        CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
                        CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
                    }
                }

                if (projectile.hitEffects.hitParticle != null)
                {
                    UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = projectile.hitEffects.hitParticle.GetComponent<UFE2FTEPaletteSwapSpriteController>();

                    if (paletteSwapSpriteController != null)
                    {
                        CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
                        CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
                    }
                }

                if (projectile.impactPrefab != null)
                {
                    UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = projectile.impactPrefab.GetComponent<UFE2FTEPaletteSwapSpriteController>();

                    if (paletteSwapSpriteController != null)
                    {
                        CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
                        CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
                    }
                }
            }

            if (move.armorOptions.hitEffects.hitParticle != null)
            {
                UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = move.armorOptions.hitEffects.hitParticle.GetComponent<UFE2FTEPaletteSwapSpriteController>();

                if (paletteSwapSpriteController != null)
                {
                    CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
                    CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
                }
            }
        }
    }

    if (UFE.config.selectedStage.prefab != null)
    {
        UFE2FTEPaletteSwapSpriteController paletteSwapSpriteController = UFE.config.selectedStage.prefab.GetComponent<UFE2FTEPaletteSwapSpriteController>();

        if (paletteSwapSpriteController != null)
        {
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 1);
            CreatePooledSwapColorsMaterialInfo(paletteSwapSpriteController.characterName, 2);
        }
    }
}*/

#endregion
