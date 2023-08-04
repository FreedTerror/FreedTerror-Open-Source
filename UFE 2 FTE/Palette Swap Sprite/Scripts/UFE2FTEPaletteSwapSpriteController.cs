using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Collections;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTEPaletteSwapSpriteController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTEPaletteSwapSpriteManager.PaletteTextureObjectPoolOptions.UpdatePaletteTextureObjectPoolsOptions updatePaletteTextureObjectPoolsOptions;

        [SerializeField]
        private UFE2FTEPaletteSwapSpriteManager.GhostObjectPoolOptions.UpdateGhostObjectPoolsOptions updateGhostObjectPoolsOptions;

        #region Palette Swap Sprite UI Options

        [Serializable]
        private class PaletteSwapSpriteUIOptions
        {
            public UFE2FTEPaletteSwapSpriteManager.ScreenType screenTypeOnUpdate;
            public UFE2FTEPaletteSwapSpriteManager.ScreenType screenTypeOnDisable;
            public UFE2FTEPaletteSwapSpriteManager.ScreenType screenTypeOnDestroy;

            public Text player1CharacterNameText;
            public Text player2CharacterNameText;

            public Text player1ColorNameText;
            public Text player2ColorNameText;
        }
        [SerializeField]
        private PaletteSwapSpriteUIOptions paletteSwapSpriteUIOptions;

        #endregion

        #region Main Options

        private Transform myTransform;
        private ControlsScript myControlsScript;
        private ProjectileMoveScript myProjectileMoveScript;

        public Animator myAnimator;
        public SpriteRenderer mySpriteRenderer;

        public string characterName;
        public string[] linkedCharacterNames;

        [HideInInspector]
        public int playerNumber;

        [HideInInspector]
        public Color32[] mySwapColors;
        [HideInInspector]
        public Texture2D mySwapTexture;

        #endregion

        #region Raw Image Options

        [Serializable]
        private class RawImageOptions
        {
            public RawImage rawImage;
            [Range(1, 2)]
            public int playerNumber = 1;
        }
        [SerializeField]
        private RawImageOptions rawImageOptions;

        #endregion

        #region Swap Colors Move Options

        [Serializable]
        private class SwapColorsMoveOptions
        {
            public string[] moveNames;

            public enum SwapColorsMethod
            {
                None,
                //SwapSinglePart,
                SwapAllParts,
                //TintSinglePart,
                TintAllParts
            }
            public SwapColorsMethod swapColorsMethod;
            [Range(0, 1)]
            public float tintCombineAmount;

            public enum ChangeColorsMethod
            {
                Same,
                Multiple
            }
            public ChangeColorsMethod changeColorsMethod;

            public Color32 sameSwapColor;
            public Color32[] multipleSwapColors;
            public float changeColorDelay;
            [HideInInspector]
            public float changeColorTimer;
            [HideInInspector]
            public int changeColorIndex;
        }
        [SerializeField]
        private SwapColorsMoveOptions[] swapColorsMoveOptions;

        #endregion

        #region Ghost Move Options

        [Serializable]
        private class GhostMoveOptions
        {
            public string[] moveNames;

            public bool useGhostSpawnDelay;
            public float ghostSpawnDelay;
            [HideInInspector]
            public float ghostSpawnTimer;

            public int[] ghostSpawnFrames;
            [HideInInspector]
            public int ignoredGhostSpawnFrame;

            public bool disableGhostsWhenMoveEnds;
            private List<GameObject> enabledGhostGameObjects;
            public List<GameObject> GetEnabledGhostGameObjects()
            {
                if (enabledGhostGameObjects == null)
                {
                    enabledGhostGameObjects = new List<GameObject>();
                }

                return enabledGhostGameObjects;
            }

            public float ghostDisableDelay;

            public Vector3 ghostStartPositionOffset;
            public AnimationCurve ghostPositionAnimationCurve;
            public bool useUpdateGhostXPosition;
            public bool useUpdateGhostYPosition;
            public bool useUpdateGhostZPosition;

            public bool useUpdateGhostSpriteRendererSprite;
            public bool useUpdateGhostSpriteRendererFlipX;
            public bool useUpdateGhostSpriteRendererFlipY;
            public bool useGhostFadeOut;
            [Range(0, 1)]
            public float ghostAlphaValue;
            public enum OrderInLayer
            {
                Infront,
                Behind
            }
            public OrderInLayer ghostOrderInLayer;

            public enum SwapColorsMethod
            {
                None,
                //SwapSinglePart,
                SwapAllParts,
                //TintSinglePart,
                TintAllParts
            }
            public bool useGhostCopySwapColors;
            public SwapColorsMethod ghostSwapColorsMethod;
            [Range(0, 1)]
            public float ghostTintCombineAmount;

            public enum ChangeColorsMethod
            {
                Same,
                Multiple
            }
            public ChangeColorsMethod ghostChangeColorsMethod;

            public Color32 ghostSameSwapColor;
            public Color32[] ghostMultipleSwapColors;
            public float ghostChangeColorDelay;
        }
        [SerializeField]
        private GhostMoveOptions[] ghostMoveOptions;

        #endregion

        #region Ghost Prefab Options

        [Serializable]
        private class GhostPrefabOptions
        {
            public UFE2FTEPaletteSwapSpriteController linkedPaletteSwapSpriteController;

            public float ghostDisableDelay;
            [HideInInspector]
            public float ghostDisableTimer;

            public Vector3 ghostStartPositionOffset;
            public Vector3 ghostStartPosition;
            public AnimationCurve ghostPositionAnimationCurve;
            public bool useUpdateGhostXPosition;
            public bool useUpdateGhostYPosition;
            public bool useUpdateGhostZPosition;

            public bool useUpdateGhostSpriteRendererSprite;
            public bool useUpdateGhostSpriteRendererFlipX;
            public bool useUpdateGhostSpriteRendererFlipY;
            public bool useGhostFadeOut;
            [Range(0, 1)]
            public float ghostAlphaValue;
            public GhostMoveOptions.OrderInLayer ghostOrderInLayer;

            public bool useGhostCopySwapColors;
            public GhostMoveOptions.SwapColorsMethod ghostSwapColorsMethod;
            [Range(0, 1)]
            public float ghostTintCombineAmount;

            public GhostMoveOptions.ChangeColorsMethod ghostChangeColorsMethod;

            public Color32 ghostSameSwapColor;
            public Color32[] ghostMultipleSwapColors;
            public float ghostChangeColorDelay;
            [HideInInspector]
            public float ghostChangeColorTimer;
            [HideInInspector]
            public int ghostChangeColorIndex;
        }
        [SerializeField]
        private GhostPrefabOptions ghostPrefabOptions;

        #endregion

        void Awake()
        {
            myTransform = transform;   
        }

        void OnEnable()
        {
            float deltaTime = (float)UFE.fixedDeltaTime;

            SetSwapTextureInfo();

            SetPaletteTextureObjectPoolsOptions(true);

            SetGhostObjectPoolsOptions(true);

            SetGhostPrefabOptions(deltaTime, true);
        }

        // Start is called before the first frame update
        void Start()
        {
            SetPaletteTextureObjectPoolsOptions(false, true);

            SetGhostObjectPoolsOptions(false, true);

            SetControlsScript();

            SetProjectileMoveScript();

            SetRawImageOptions();
        }

        // Update is called once per frame
        void Update()
        {
            float deltaTime = (float)UFE.fixedDeltaTime;

            SetScreenType(paletteSwapSpriteUIOptions.screenTypeOnUpdate);

            SetCharacterNameText();

            SetColorNameText();

            SetSwapColorsMoveOptions(deltaTime);

            SetGhostMoveOptions(deltaTime);

            SetGhostPrefabOptions(deltaTime, false, true);
        }

        void OnDisable()
        {
            SetScreenType(paletteSwapSpriteUIOptions.screenTypeOnDisable);

            SetPaletteTextureObjectPoolsOptions(false, false, true);

            SetGhostObjectPoolsOptions(false, false, true);
        }

        void OnDestroy()
        {
            SetScreenType(paletteSwapSpriteUIOptions.screenTypeOnDestroy);

            SetPaletteTextureObjectPoolsOptions(false, false, false, true);

            SetGhostObjectPoolsOptions(false, false, false, true);

            DestroySwapTexture();
        }

        #region Palette Texture Object Pools Methods

        private void SetPaletteTextureObjectPoolsOptions(bool useOnEnable = false, bool useOnStart = false, bool useOnDisable = false, bool useOnDestroy = false)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null) return;

            if (updatePaletteTextureObjectPoolsOptions.useUpdateAllPaletteTextureObjectPoolsOptions == true)
            {
                if (updatePaletteTextureObjectPoolsOptions.updateAllPaletteTexturePoolsOptions.useOnEnable == true
                    && useOnEnable == true)
                {
                    UFE2FTEPaletteSwapSpriteManager.instance.SetAllPaletteTextureObjectPoolsSize(updatePaletteTextureObjectPoolsOptions.updateAllPaletteTexturePoolsOptions.poolSize);
                }

                if (updatePaletteTextureObjectPoolsOptions.updateAllPaletteTexturePoolsOptions.useOnStart == true
                    && useOnStart == true)
                {
                    UFE2FTEPaletteSwapSpriteManager.instance.SetAllPaletteTextureObjectPoolsSize(updatePaletteTextureObjectPoolsOptions.updateAllPaletteTexturePoolsOptions.poolSize);
                }

                if (updatePaletteTextureObjectPoolsOptions.updateAllPaletteTexturePoolsOptions.useOnDisable == true
                    && useOnDisable == true)
                {
                    UFE2FTEPaletteSwapSpriteManager.instance.SetAllPaletteTextureObjectPoolsSize(updatePaletteTextureObjectPoolsOptions.updateAllPaletteTexturePoolsOptions.poolSize);
                }

                if (updatePaletteTextureObjectPoolsOptions.updateAllPaletteTexturePoolsOptions.useOnDestroy == true
                    && useOnDestroy == true)
                {
                    UFE2FTEPaletteSwapSpriteManager.instance.SetAllPaletteTextureObjectPoolsSize(updatePaletteTextureObjectPoolsOptions.updateAllPaletteTexturePoolsOptions.poolSize);
                }
            }

            if (updatePaletteTextureObjectPoolsOptions.useUpdatePaletteTextureObjectPoolsByCharacterNameOptions == true)
            {
                if (updatePaletteTextureObjectPoolsOptions.updatePaletteTextureObjectPoolsByCharacterNameOptions.useOnEnable == true
                    && useOnEnable == true)
                {
                    UFE2FTEPaletteSwapSpriteManager.instance.SetPaletteTextureObjectPoolsSizeByCharacterName(updatePaletteTextureObjectPoolsOptions.updateAllPaletteTexturePoolsOptions.poolSize, null, updatePaletteTextureObjectPoolsOptions.updatePaletteTextureObjectPoolsByCharacterNameOptions.characterNames);
                }

                if (updatePaletteTextureObjectPoolsOptions.updatePaletteTextureObjectPoolsByCharacterNameOptions.useOnStart == true
                    && useOnStart == true)
                {
                    UFE2FTEPaletteSwapSpriteManager.instance.SetPaletteTextureObjectPoolsSizeByCharacterName(updatePaletteTextureObjectPoolsOptions.updateAllPaletteTexturePoolsOptions.poolSize, null, updatePaletteTextureObjectPoolsOptions.updatePaletteTextureObjectPoolsByCharacterNameOptions.characterNames);
                }

                if (updatePaletteTextureObjectPoolsOptions.updatePaletteTextureObjectPoolsByCharacterNameOptions.useOnDisable == true
                    && useOnDisable == true)
                {
                    UFE2FTEPaletteSwapSpriteManager.instance.SetPaletteTextureObjectPoolsSizeByCharacterName(updatePaletteTextureObjectPoolsOptions.updateAllPaletteTexturePoolsOptions.poolSize, null, updatePaletteTextureObjectPoolsOptions.updatePaletteTextureObjectPoolsByCharacterNameOptions.characterNames);
                }

                if (updatePaletteTextureObjectPoolsOptions.updatePaletteTextureObjectPoolsByCharacterNameOptions.useOnDestroy == true
                    && useOnDestroy == true)
                {
                    UFE2FTEPaletteSwapSpriteManager.instance.SetPaletteTextureObjectPoolsSizeByCharacterName(updatePaletteTextureObjectPoolsOptions.updateAllPaletteTexturePoolsOptions.poolSize, null, updatePaletteTextureObjectPoolsOptions.updatePaletteTextureObjectPoolsByCharacterNameOptions.characterNames);
                }
            }

            if (updatePaletteTextureObjectPoolsOptions.useUpdatePaletteTextureObjectPoolsByUFEPreloadOptions == true)
            {
                if (updatePaletteTextureObjectPoolsOptions.updatePaletteTextureObjectPoolsByUFEPreloadOptions.useOnEnable == true
                    && useOnEnable == true)
                {
                    UFE2FTEPaletteSwapSpriteManager.instance.SetPaletteTextureObjectPoolsByUFEPreload(updatePaletteTextureObjectPoolsOptions.updatePaletteTextureObjectPoolsByUFEPreloadOptions.poolSize);
                }

                if (updatePaletteTextureObjectPoolsOptions.updatePaletteTextureObjectPoolsByUFEPreloadOptions.useOnStart == true
                    && useOnStart == true)
                {
                    UFE2FTEPaletteSwapSpriteManager.instance.SetPaletteTextureObjectPoolsByUFEPreload(updatePaletteTextureObjectPoolsOptions.updatePaletteTextureObjectPoolsByUFEPreloadOptions.poolSize);
                }

                if (updatePaletteTextureObjectPoolsOptions.updatePaletteTextureObjectPoolsByUFEPreloadOptions.useOnDisable == true
                    && useOnDisable == true)
                {
                    UFE2FTEPaletteSwapSpriteManager.instance.SetPaletteTextureObjectPoolsByUFEPreload(updatePaletteTextureObjectPoolsOptions.updatePaletteTextureObjectPoolsByUFEPreloadOptions.poolSize);
                }

                if (updatePaletteTextureObjectPoolsOptions.updatePaletteTextureObjectPoolsByUFEPreloadOptions.useOnDestroy == true
                    && useOnDestroy == true)
                {
                    UFE2FTEPaletteSwapSpriteManager.instance.SetPaletteTextureObjectPoolsByUFEPreload(updatePaletteTextureObjectPoolsOptions.updatePaletteTextureObjectPoolsByUFEPreloadOptions.poolSize);
                }
            }
        }

        #endregion

        #region Ghost Object Pools Methods

        private void SetGhostObjectPoolsOptions(bool useOnEnable = false, bool useOnStart = false, bool useOnDisable = false, bool useOnDestroy = false)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null) return;

            if (updateGhostObjectPoolsOptions.useUpdateAllGhostObjectPoolsOptions == true)
            {
                if (updateGhostObjectPoolsOptions.updateAllGhostObjectPoolsOptions.useOnEnable == true
                    && useOnEnable == true)
                {
                    UFE2FTEPaletteSwapSpriteManager.instance.SetAllGhostObjectPoolsSize(updateGhostObjectPoolsOptions.updateAllGhostObjectPoolsOptions.poolSize);
                }

                if (updateGhostObjectPoolsOptions.updateAllGhostObjectPoolsOptions.useOnStart == true
                    && useOnStart == true)
                {
                    UFE2FTEPaletteSwapSpriteManager.instance.SetAllGhostObjectPoolsSize(updateGhostObjectPoolsOptions.updateAllGhostObjectPoolsOptions.poolSize);
                }

                if (updateGhostObjectPoolsOptions.updateAllGhostObjectPoolsOptions.useOnDisable == true
                    && useOnDisable == true)
                {
                    UFE2FTEPaletteSwapSpriteManager.instance.SetAllGhostObjectPoolsSize(updateGhostObjectPoolsOptions.updateAllGhostObjectPoolsOptions.poolSize);
                }

                if (updateGhostObjectPoolsOptions.updateAllGhostObjectPoolsOptions.useOnDestroy == true
                    && useOnDestroy == true)
                {
                    UFE2FTEPaletteSwapSpriteManager.instance.SetAllGhostObjectPoolsSize(updateGhostObjectPoolsOptions.updateAllGhostObjectPoolsOptions.poolSize);
                }
            }
        }

        #endregion

        #region Palette Swap Sprite UI Methods

        private void SetScreenType(UFE2FTEPaletteSwapSpriteManager.ScreenType screenType)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null) return;

            UFE2FTEPaletteSwapSpriteManager.instance.screenType = screenType;
        }

        private void SetCharacterNameText()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || UFE2FTEPaletteSwapSpriteManager.instance.screenType == UFE2FTEPaletteSwapSpriteManager.ScreenType.None) return;

            if (paletteSwapSpriteUIOptions.player1CharacterNameText != null)
            {
                UFE2FTEPaletteSwapSpriteManager.instance.CreateAndDestroyCharacterGameObjectByCharacterName(paletteSwapSpriteUIOptions.player1CharacterNameText.text, 1);
            }

            if (paletteSwapSpriteUIOptions.player2CharacterNameText != null)
            {
                UFE2FTEPaletteSwapSpriteManager.instance.CreateAndDestroyCharacterGameObjectByCharacterName(paletteSwapSpriteUIOptions.player2CharacterNameText.text, 2);
            }
        }

        private void SetColorNameText()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || UFE2FTEPaletteSwapSpriteManager.instance.screenType == UFE2FTEPaletteSwapSpriteManager.ScreenType.None) return;

            if (paletteSwapSpriteUIOptions.player1ColorNameText != null
                && paletteSwapSpriteUIOptions.player1CharacterNameText != null)
            {
                paletteSwapSpriteUIOptions.player1ColorNameText.text = UFE2FTEPaletteSwapSpriteManager.instance.GetSwapColorsName(paletteSwapSpriteUIOptions.player1CharacterNameText.text, 1);
            }

            if (paletteSwapSpriteUIOptions.player2ColorNameText != null
                && paletteSwapSpriteUIOptions.player2CharacterNameText != null)
            {
                paletteSwapSpriteUIOptions.player2ColorNameText.text = UFE2FTEPaletteSwapSpriteManager.instance.GetSwapColorsName(paletteSwapSpriteUIOptions.player2CharacterNameText.text, 2);
            }
        }

        #endregion

        #region Main Options Methods

        private void SetSwapTextureInfo()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || mySpriteRenderer == null
                || mySpriteRenderer.sharedMaterial == null
                || mySpriteRenderer.sharedMaterial.HasProperty("_PaletteTex") == false
                || mySpriteRenderer.sharedMaterial.HasProperty("_SwapTex") == false) return;

            var pooledPaletteTextureInfo = UFE2FTEPaletteSwapSpriteManager.instance.GetPooledPaletteTextureInfoFromPooledPaletteTextureInfos(this);

            if (pooledPaletteTextureInfo != null)
            {
                // 0 GC.
                mySwapColors = pooledPaletteTextureInfo.swapColors;

                mySpriteRenderer.GetPropertyBlock(UFE2FTEPaletteSwapSpriteManager.instance.GetMaterialPropertyBlock());
                UFE2FTEPaletteSwapSpriteManager.instance.GetMaterialPropertyBlock().SetTexture("_SwapTex", pooledPaletteTextureInfo.paletteTexture);
                mySpriteRenderer.SetPropertyBlock(UFE2FTEPaletteSwapSpriteManager.instance.GetMaterialPropertyBlock());

                mySwapTexture = pooledPaletteTextureInfo.paletteTexture;
            }
            else
            {
                if (mySpriteRenderer.sharedMaterial.GetTexture("_PaletteTex") == null) return;

                int paletteTextureWidth = mySpriteRenderer.sharedMaterial.GetTexture("_PaletteTex").width;

                mySwapColors = new Color32[paletteTextureWidth];
                for (int i = 0; i < paletteTextureWidth; i++)
                {
                    Color32 color = new Color32(0, 0, 0, 255);
                    mySwapColors[i] = color;
                }

                mySwapTexture = new Texture2D(paletteTextureWidth, 1, TextureFormat.RGBA32, false, false);
                mySwapTexture.filterMode = FilterMode.Point;
                mySwapTexture.wrapMode = TextureWrapMode.Clamp;
                for (int i = 0; i < paletteTextureWidth; i++)
                {
                    mySwapTexture.SetPixel(i, 0, new Color32(0, 0, 0, 255));
                }
                mySwapTexture.Apply();
                mySpriteRenderer.GetPropertyBlock(UFE2FTEPaletteSwapSpriteManager.instance.GetMaterialPropertyBlock());
                UFE2FTEPaletteSwapSpriteManager.instance.GetMaterialPropertyBlock().SetTexture("_SwapTex", mySwapTexture);
                mySpriteRenderer.SetPropertyBlock(UFE2FTEPaletteSwapSpriteManager.instance.GetMaterialPropertyBlock());
            }
        }

        private void DestroySwapTexture()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || mySwapTexture == null
                || UFE2FTEPaletteSwapSpriteManager.instance.CheckIfPaletteTextureIsPooledPaletteTexture(mySwapTexture) == true) return;

            Destroy(mySwapTexture);
        }

        private void SetControlsScript()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || myControlsScript != null) return;

            myControlsScript = GetComponentInParent<ControlsScript>();

            if (myControlsScript != null)
            {
                playerNumber = myControlsScript.playerNum;

                SwapAllSpriteColorsWithSwapColorsArray(UFE2FTEPaletteSwapSpriteManager.instance.GetSwapColors(characterName, playerNumber), true);
            }
        }

        private void SetProjectileMoveScript()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || myProjectileMoveScript != null) return;

            myProjectileMoveScript = GetComponent<ProjectileMoveScript>();

            if (myProjectileMoveScript != null)
            {
                playerNumber = myProjectileMoveScript.myControlsScript.playerNum;

                SwapAllSpriteColorsWithSwapColorsArray(UFE2FTEPaletteSwapSpriteManager.instance.GetSwapColors(characterName, playerNumber), true);
            }
        }

        #endregion

        #region Raw Image Methods

        private void SetRawImageOptions()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || rawImageOptions.rawImage == null
                || UFE.p1ControlsScript == null
                || UFE.p2ControlsScript == null) return;

            if (rawImageOptions.playerNumber == 1)
            {
                UFE2FTEPaletteSwapSpriteController player1PaletteSwapSpriteController = UFE.p1ControlsScript.GetComponentInChildren<UFE2FTEPaletteSwapSpriteController>();

                if (player1PaletteSwapSpriteController == null
                    || player1PaletteSwapSpriteController.mySpriteRenderer == null
                    || player1PaletteSwapSpriteController.mySpriteRenderer.sharedMaterial == null
                    || player1PaletteSwapSpriteController.mySpriteRenderer.sharedMaterial.HasProperty("_PaletteTex") == false
                    || player1PaletteSwapSpriteController.mySpriteRenderer.sharedMaterial.HasProperty("_SwapTex") == false) return;

                // 0 GC.
                rawImageOptions.rawImage.texture = UFE.p1ControlsScript.myInfo.profilePictureSmall;
                rawImageOptions.rawImage.material = player1PaletteSwapSpriteController.mySpriteRenderer.sharedMaterial;
                player1PaletteSwapSpriteController.mySpriteRenderer.GetPropertyBlock(UFE2FTEPaletteSwapSpriteManager.instance.GetMaterialPropertyBlock());
                rawImageOptions.rawImage.material.SetTexture("_SwapTex", UFE2FTEPaletteSwapSpriteManager.instance.GetMaterialPropertyBlock().GetTexture("_SwapTex"));
            }
            else if (rawImageOptions.playerNumber == 2)
            {
                UFE2FTEPaletteSwapSpriteController player2PaletteSwapSpriteController = UFE.p2ControlsScript.GetComponentInChildren<UFE2FTEPaletteSwapSpriteController>();

                if (player2PaletteSwapSpriteController == null
                    || player2PaletteSwapSpriteController.mySpriteRenderer == null
                    || player2PaletteSwapSpriteController.mySpriteRenderer.sharedMaterial == null
                    || player2PaletteSwapSpriteController.mySpriteRenderer.sharedMaterial.HasProperty("_PaletteTex") == false
                    || player2PaletteSwapSpriteController.mySpriteRenderer.sharedMaterial.HasProperty("_SwapTex") == false) return;

                // 0 GC.
                rawImageOptions.rawImage.texture = UFE.p2ControlsScript.myInfo.profilePictureSmall;
                rawImageOptions.rawImage.material = player2PaletteSwapSpriteController.mySpriteRenderer.sharedMaterial;
                player2PaletteSwapSpriteController.mySpriteRenderer.GetPropertyBlock(UFE2FTEPaletteSwapSpriteManager.instance.GetMaterialPropertyBlock());
                rawImageOptions.rawImage.material.SetTexture("_SwapTex", UFE2FTEPaletteSwapSpriteManager.instance.GetMaterialPropertyBlock().GetTexture("_SwapTex"));
            }
        }

        #endregion

        #region Swap Colors Move Methods

        private void SetSwapColorsMoveOptions(float deltaTime)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || myControlsScript == null) return;

            if (myControlsScript.currentMove != null)
            {
                int length = swapColorsMoveOptions.Length;
                for (int i = 0; i < length; i++)
                {
                    int lengthA = swapColorsMoveOptions[i].moveNames.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (myControlsScript.currentMove.moveName == swapColorsMoveOptions[i].moveNames[a])
                        {
                            if (swapColorsMoveOptions[i].swapColorsMethod == SwapColorsMoveOptions.SwapColorsMethod.SwapAllParts)
                            {
                                if (swapColorsMoveOptions[i].changeColorsMethod == SwapColorsMoveOptions.ChangeColorsMethod.Same)
                                {
                                    SwapAllSpriteColorsWithSwapColor(new Color32(swapColorsMoveOptions[i].sameSwapColor.r, swapColorsMoveOptions[i].sameSwapColor.g, swapColorsMoveOptions[i].sameSwapColor.b, 255));
                                }
                                else if (swapColorsMoveOptions[i].changeColorsMethod == SwapColorsMoveOptions.ChangeColorsMethod.Multiple)
                                {
                                    if (myControlsScript.Physics.freeze == false)
                                    {
                                        swapColorsMoveOptions[i].changeColorTimer += deltaTime;
                                    }

                                    if (swapColorsMoveOptions[i].changeColorTimer >= swapColorsMoveOptions[i].changeColorDelay)
                                    {
                                        swapColorsMoveOptions[i].changeColorTimer = 0;

                                        SwapAllSpriteColorsWithSwapColor(new Color32(swapColorsMoveOptions[i].multipleSwapColors[swapColorsMoveOptions[i].changeColorIndex].r, swapColorsMoveOptions[i].multipleSwapColors[swapColorsMoveOptions[i].changeColorIndex].g, swapColorsMoveOptions[i].multipleSwapColors[swapColorsMoveOptions[i].changeColorIndex].b, 255));

                                        swapColorsMoveOptions[i].changeColorIndex++;

                                        if (swapColorsMoveOptions[i].changeColorIndex >= swapColorsMoveOptions[i].multipleSwapColors.Length)
                                        {
                                            swapColorsMoveOptions[i].changeColorIndex = 0;
                                        }
                                    }
                                }                            
                            }
                            else if (swapColorsMoveOptions[i].swapColorsMethod == SwapColorsMoveOptions.SwapColorsMethod.TintAllParts)
                            {
                                if (swapColorsMoveOptions[i].changeColorsMethod == SwapColorsMoveOptions.ChangeColorsMethod.Same)
                                {                       
                                    ResetAllSpriteColors();

                                    SwapAllSpriteColorsWithTintColor(new Color32(swapColorsMoveOptions[i].sameSwapColor.r, swapColorsMoveOptions[i].sameSwapColor.g, swapColorsMoveOptions[i].sameSwapColor.b, 255), swapColorsMoveOptions[i].tintCombineAmount);
                                }
                                else if (swapColorsMoveOptions[i].changeColorsMethod == SwapColorsMoveOptions.ChangeColorsMethod.Multiple)
                                {
                                    if (myControlsScript.Physics.freeze == false)
                                    {
                                        swapColorsMoveOptions[i].changeColorTimer += deltaTime;
                                    }

                                    if (swapColorsMoveOptions[i].changeColorTimer >= swapColorsMoveOptions[i].changeColorDelay)
                                    {
                                        swapColorsMoveOptions[i].changeColorTimer = 0;

                                        ResetAllSpriteColors();

                                        SwapAllSpriteColorsWithTintColor(new Color32(swapColorsMoveOptions[i].multipleSwapColors[swapColorsMoveOptions[i].changeColorIndex].r, swapColorsMoveOptions[i].multipleSwapColors[swapColorsMoveOptions[i].changeColorIndex].g, swapColorsMoveOptions[i].multipleSwapColors[swapColorsMoveOptions[i].changeColorIndex].b, 255), swapColorsMoveOptions[i].tintCombineAmount);

                                        swapColorsMoveOptions[i].changeColorIndex++;

                                        if (swapColorsMoveOptions[i].changeColorIndex >= swapColorsMoveOptions[i].multipleSwapColors.Length)
                                        {
                                            swapColorsMoveOptions[i].changeColorIndex = 0;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            ResetAllSpriteColors();

                            swapColorsMoveOptions[i].changeColorTimer = 0;

                            swapColorsMoveOptions[i].changeColorIndex = 0;
                        }
                    }
                }
            }
            else
            {
                ResetAllSpriteColors();

                int length = swapColorsMoveOptions.Length;
                for (int i = 0; i < length; i++)
                {
                    swapColorsMoveOptions[i].changeColorTimer = 0;

                    swapColorsMoveOptions[i].changeColorIndex = 0;
                }
            }
        }

        #endregion

        #region Ghost Move Methods

        private void SetGhostMoveOptions(float deltaTime)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || myControlsScript == null) return;

            if (myControlsScript.currentMove != null)
            {
                int length = ghostMoveOptions.Length;
                for (int i = 0; i < length; i++)
                {
                    int lengthA = ghostMoveOptions[i].moveNames.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (myControlsScript.currentMove.moveName == ghostMoveOptions[i].moveNames[a])
                        {
                            if (ghostMoveOptions[i].useGhostSpawnDelay == true)
                            {
                                if (myControlsScript.Physics.freeze == false)
                                {
                                    ghostMoveOptions[i].ghostSpawnTimer += deltaTime;
                                }

                                if (ghostMoveOptions[i].ghostSpawnTimer >= ghostMoveOptions[i].ghostSpawnDelay)
                                {
                                    ghostMoveOptions[i].ghostSpawnTimer = 0;

                                    SpawnGhost(ghostMoveOptions[i]);
                                }
                            }

                            int lengthB = ghostMoveOptions[i].ghostSpawnFrames.Length;
                            for (int b = 0; b < lengthB; b++)
                            {
                                if (myControlsScript.currentMove.currentFrame == ghostMoveOptions[i].ignoredGhostSpawnFrame)
                                {
                                    break;
                                }
                                else
                                {
                                    ghostMoveOptions[i].ignoredGhostSpawnFrame = -1;
                                }

                                if (myControlsScript.currentMove.currentFrame != ghostMoveOptions[i].ghostSpawnFrames[b]) continue;

                                ghostMoveOptions[i].ignoredGhostSpawnFrame = ghostMoveOptions[i].ghostSpawnFrames[b];

                                SpawnGhost(ghostMoveOptions[i]);
                            }
                        }
                        else
                        {
                            ghostMoveOptions[i].ghostSpawnTimer = 0;

                            ghostMoveOptions[i].ignoredGhostSpawnFrame = -1;

                            int count = ghostMoveOptions[i].GetEnabledGhostGameObjects().Count - 1;
                            for (int b = count; b >= 0; b--)
                            {
                                if (ghostMoveOptions[i].GetEnabledGhostGameObjects()[a] == null)
                                {
                                    ghostMoveOptions[i].GetEnabledGhostGameObjects().RemoveAt(a);
                                }
                                else if (ghostMoveOptions[i].GetEnabledGhostGameObjects()[a].activeInHierarchy == false)
                                {
                                    ghostMoveOptions[i].GetEnabledGhostGameObjects().RemoveAt(a);
                                }
                                else if (ghostMoveOptions[i].disableGhostsWhenMoveEnds == true)
                                {
                                    ghostMoveOptions[i].GetEnabledGhostGameObjects()[a].SetActive(false);

                                    ghostMoveOptions[i].GetEnabledGhostGameObjects().RemoveAt(a);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                int length = ghostMoveOptions.Length;
                for (int i = 0; i < length; i++)
                {
                    ghostMoveOptions[i].ghostSpawnTimer = 0;

                    ghostMoveOptions[i].ignoredGhostSpawnFrame = -1;

                    int count = ghostMoveOptions[i].GetEnabledGhostGameObjects().Count - 1;
                    for (int a = count; a >= 0; a--)
                    {
                        if (ghostMoveOptions[i].GetEnabledGhostGameObjects()[a] == null)
                        {
                            ghostMoveOptions[i].GetEnabledGhostGameObjects().RemoveAt(a);
                        }
                        else if (ghostMoveOptions[i].GetEnabledGhostGameObjects()[a].activeInHierarchy == false)
                        {
                            ghostMoveOptions[i].GetEnabledGhostGameObjects().RemoveAt(a);
                        }
                        else if (ghostMoveOptions[i].disableGhostsWhenMoveEnds == true)
                        {
                            ghostMoveOptions[i].GetEnabledGhostGameObjects()[a].SetActive(false);

                            ghostMoveOptions[i].GetEnabledGhostGameObjects().RemoveAt(a);
                        }
                    }
                }
            }
        }

        private void SpawnGhost(GhostMoveOptions ghostMoveOptions)
        {
            UFE2FTEPaletteSwapSpriteController pooledGhostGameObject = UFE2FTEPaletteSwapSpriteManager.instance.GetPooledGhostGameObjectFromPooledGhostGameObjectsList();

            if (pooledGhostGameObject == null) return;

            ghostMoveOptions.GetEnabledGhostGameObjects().Add(pooledGhostGameObject.gameObject);

            pooledGhostGameObject.characterName = characterName;

            pooledGhostGameObject.linkedCharacterNames = linkedCharacterNames;

            //pooledGhostGameObject.useGhostPrefabOptions = true;

            pooledGhostGameObject.ghostPrefabOptions.linkedPaletteSwapSpriteController = this;

            pooledGhostGameObject.ghostPrefabOptions.ghostDisableDelay = ghostMoveOptions.ghostDisableDelay;

            pooledGhostGameObject.ghostPrefabOptions.ghostStartPositionOffset = ghostMoveOptions.ghostStartPositionOffset;

            pooledGhostGameObject.ghostPrefabOptions.ghostPositionAnimationCurve = ghostMoveOptions.ghostPositionAnimationCurve;

            pooledGhostGameObject.ghostPrefabOptions.useUpdateGhostXPosition = ghostMoveOptions.useUpdateGhostXPosition;

            pooledGhostGameObject.ghostPrefabOptions.useUpdateGhostYPosition = ghostMoveOptions.useUpdateGhostYPosition;

            pooledGhostGameObject.ghostPrefabOptions.useUpdateGhostZPosition = ghostMoveOptions.useUpdateGhostZPosition;

            pooledGhostGameObject.ghostPrefabOptions.useUpdateGhostSpriteRendererSprite = ghostMoveOptions.useUpdateGhostSpriteRendererSprite;

            pooledGhostGameObject.ghostPrefabOptions.useUpdateGhostSpriteRendererFlipX = ghostMoveOptions.useUpdateGhostSpriteRendererFlipX;

            pooledGhostGameObject.ghostPrefabOptions.useUpdateGhostSpriteRendererFlipY = ghostMoveOptions.useUpdateGhostSpriteRendererFlipY;

            pooledGhostGameObject.ghostPrefabOptions.useGhostFadeOut = ghostMoveOptions.useGhostFadeOut;

            pooledGhostGameObject.ghostPrefabOptions.ghostAlphaValue = ghostMoveOptions.ghostAlphaValue;

            pooledGhostGameObject.ghostPrefabOptions.ghostOrderInLayer = ghostMoveOptions.ghostOrderInLayer;

            pooledGhostGameObject.ghostPrefabOptions.useGhostCopySwapColors = ghostMoveOptions.useGhostCopySwapColors;

            pooledGhostGameObject.ghostPrefabOptions.ghostSwapColorsMethod = ghostMoveOptions.ghostSwapColorsMethod;

            pooledGhostGameObject.ghostPrefabOptions.ghostTintCombineAmount = ghostMoveOptions.ghostTintCombineAmount;

            pooledGhostGameObject.ghostPrefabOptions.ghostChangeColorsMethod = ghostMoveOptions.ghostChangeColorsMethod;

            pooledGhostGameObject.ghostPrefabOptions.ghostSameSwapColor = ghostMoveOptions.ghostSameSwapColor;

            pooledGhostGameObject.ghostPrefabOptions.ghostMultipleSwapColors = ghostMoveOptions.ghostMultipleSwapColors;

            pooledGhostGameObject.ghostPrefabOptions.ghostChangeColorDelay = ghostMoveOptions.ghostChangeColorDelay;

            pooledGhostGameObject.gameObject.SetActive(true);
        }

        #endregion

        #region Ghost Prefab Methods

        private void SetGhostPrefabOptions(float deltaTime, bool useOnEnable = false, bool useOnUpdate = false)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || ghostPrefabOptions.linkedPaletteSwapSpriteController == null
                || myTransform == null) return;

            if (useOnEnable == true)
            {
                ghostPrefabOptions.ghostDisableTimer = 0;

                ghostPrefabOptions.ghostChangeColorTimer = 0;

                ghostPrefabOptions.ghostChangeColorIndex = 0;

                Vector3 newPosition = ghostPrefabOptions.linkedPaletteSwapSpriteController.myTransform.position;

                if (ghostPrefabOptions.linkedPaletteSwapSpriteController.mySpriteRenderer.flipX == false)
                {
                    newPosition.x = ghostPrefabOptions.linkedPaletteSwapSpriteController.myTransform.position.x + ghostPrefabOptions.ghostStartPositionOffset.x;
                }
                else
                {
                    newPosition.x = ghostPrefabOptions.linkedPaletteSwapSpriteController.myTransform.position.x - ghostPrefabOptions.ghostStartPositionOffset.x;
                }

                newPosition.y = ghostPrefabOptions.linkedPaletteSwapSpriteController.myTransform.position.y + ghostPrefabOptions.ghostStartPositionOffset.y;

                newPosition.z = ghostPrefabOptions.linkedPaletteSwapSpriteController.myTransform.position.z + ghostPrefabOptions.ghostStartPositionOffset.z;

                myTransform.position = newPosition;

                ghostPrefabOptions.ghostStartPosition = newPosition;

                mySpriteRenderer.sprite = ghostPrefabOptions.linkedPaletteSwapSpriteController.mySpriteRenderer.sprite;

                mySpriteRenderer.flipX = ghostPrefabOptions.linkedPaletteSwapSpriteController.mySpriteRenderer.flipX;

                mySpriteRenderer.flipY = ghostPrefabOptions.linkedPaletteSwapSpriteController.mySpriteRenderer.flipY;

                mySpriteRenderer.color = new Color(mySpriteRenderer.color.r, mySpriteRenderer.color.g, mySpriteRenderer.color.b, ghostPrefabOptions.ghostAlphaValue);

                if (ghostPrefabOptions.ghostOrderInLayer == GhostMoveOptions.OrderInLayer.Infront)
                {
                    mySpriteRenderer.sortingOrder = ghostPrefabOptions.linkedPaletteSwapSpriteController.mySpriteRenderer.sortingOrder + 1;
                }
                else if (ghostPrefabOptions.ghostOrderInLayer == GhostMoveOptions.OrderInLayer.Behind)
                {
                    mySpriteRenderer.sortingOrder = ghostPrefabOptions.linkedPaletteSwapSpriteController.mySpriteRenderer.sortingOrder - 1;
                }

                mySpriteRenderer.GetPropertyBlock(UFE2FTEPaletteSwapSpriteManager.instance.GetMaterialPropertyBlock());
                UFE2FTEPaletteSwapSpriteManager.instance.GetMaterialPropertyBlock().SetTexture("_PaletteTex", ghostPrefabOptions.linkedPaletteSwapSpriteController.mySpriteRenderer.sharedMaterial.GetTexture("_PaletteTex"));
                mySpriteRenderer.SetPropertyBlock(UFE2FTEPaletteSwapSpriteManager.instance.GetMaterialPropertyBlock());

                if (ghostPrefabOptions.linkedPaletteSwapSpriteController.mySwapTexture != null)
                {
                    NativeArray<Color32> nativeColorsArray = ghostPrefabOptions.linkedPaletteSwapSpriteController.mySwapTexture.GetRawTextureData<Color32>();

                    if (nativeColorsArray.Length == mySwapColors.Length)
                    {
                        int length = nativeColorsArray.Length;
                        for (int i = 0; i < length; i++)
                        {
                            mySwapColors[i] = new Color32(nativeColorsArray[i].r, nativeColorsArray[i].g, nativeColorsArray[i].b, 255);
                        }

                        SwapAllSpriteColorsWithSwapColorsArray(mySwapColors);
                    }
                }

                if (ghostPrefabOptions.ghostSwapColorsMethod == GhostMoveOptions.SwapColorsMethod.SwapAllParts)
                {
                    if (ghostPrefabOptions.ghostChangeColorsMethod == GhostMoveOptions.ChangeColorsMethod.Same)
                    {
                        SwapAllSpriteColorsWithSwapColor(new Color32(ghostPrefabOptions.ghostSameSwapColor.r, ghostPrefabOptions.ghostSameSwapColor.g, ghostPrefabOptions.ghostSameSwapColor.b, 255));
                    }
                    else if (ghostPrefabOptions.ghostChangeColorsMethod == GhostMoveOptions.ChangeColorsMethod.Multiple)
                    {
                        ghostPrefabOptions.ghostChangeColorTimer += deltaTime;

                        if (ghostPrefabOptions.ghostChangeColorTimer >= ghostPrefabOptions.ghostChangeColorDelay)
                        {
                            ghostPrefabOptions.ghostChangeColorTimer = 0;

                            SwapAllSpriteColorsWithSwapColor(new Color32(ghostPrefabOptions.ghostMultipleSwapColors[ghostPrefabOptions.ghostChangeColorIndex].r, ghostPrefabOptions.ghostMultipleSwapColors[ghostPrefabOptions.ghostChangeColorIndex].g, ghostPrefabOptions.ghostMultipleSwapColors[ghostPrefabOptions.ghostChangeColorIndex].b, 255));

                            ghostPrefabOptions.ghostChangeColorIndex++;

                            if (ghostPrefabOptions.ghostChangeColorIndex >= ghostPrefabOptions.ghostMultipleSwapColors.Length)
                            {
                                ghostPrefabOptions.ghostChangeColorIndex = 0;
                            }
                        }
                    }
                }
                else if (ghostPrefabOptions.ghostSwapColorsMethod == GhostMoveOptions.SwapColorsMethod.TintAllParts)
                {
                    if (ghostPrefabOptions.ghostChangeColorsMethod == GhostMoveOptions.ChangeColorsMethod.Same)
                    {
                        ResetAllSpriteColors();

                        SwapAllSpriteColorsWithTintColor(new Color32(ghostPrefabOptions.ghostSameSwapColor.r, ghostPrefabOptions.ghostSameSwapColor.g, ghostPrefabOptions.ghostSameSwapColor.b, 255), ghostPrefabOptions.ghostTintCombineAmount);
                    }
                    else if (ghostPrefabOptions.ghostChangeColorsMethod == GhostMoveOptions.ChangeColorsMethod.Multiple)
                    {
                        ghostPrefabOptions.ghostChangeColorTimer += deltaTime;

                        if (ghostPrefabOptions.ghostChangeColorTimer >= ghostPrefabOptions.ghostChangeColorDelay)
                        {
                            ghostPrefabOptions.ghostChangeColorTimer = 0;

                            ResetAllSpriteColors();

                            SwapAllSpriteColorsWithTintColor(new Color32(ghostPrefabOptions.ghostMultipleSwapColors[ghostPrefabOptions.ghostChangeColorIndex].r, ghostPrefabOptions.ghostMultipleSwapColors[ghostPrefabOptions.ghostChangeColorIndex].g, ghostPrefabOptions.ghostMultipleSwapColors[ghostPrefabOptions.ghostChangeColorIndex].b, 255), ghostPrefabOptions.ghostTintCombineAmount);

                            ghostPrefabOptions.ghostChangeColorIndex++;

                            if (ghostPrefabOptions.ghostChangeColorIndex >= ghostPrefabOptions.ghostMultipleSwapColors.Length)
                            {
                                ghostPrefabOptions.ghostChangeColorIndex = 0;
                            }
                        }
                    }
                }
            }

            if (useOnUpdate == true)
            {
                ghostPrefabOptions.ghostDisableTimer += deltaTime;

                if (ghostPrefabOptions.ghostDisableTimer >= ghostPrefabOptions.ghostDisableDelay)
                {
                    gameObject.SetActive(false);
                }

                if (ghostPrefabOptions.useUpdateGhostXPosition == true
                    || ghostPrefabOptions.useUpdateGhostYPosition == true
                    || ghostPrefabOptions.useUpdateGhostZPosition == true)
                {
                    float percentageCompleted = ghostPrefabOptions.ghostDisableTimer / ghostPrefabOptions.ghostDisableDelay;

                    Vector3 newPosition = Vector3.Lerp(ghostPrefabOptions.ghostStartPosition, ghostPrefabOptions.linkedPaletteSwapSpriteController.myTransform.position, ghostPrefabOptions.ghostPositionAnimationCurve.Evaluate(percentageCompleted));

                    if (ghostPrefabOptions.useUpdateGhostXPosition == true)
                    {
                        myTransform.position = new Vector3(newPosition.x, myTransform.position.y, myTransform.position.z);
                    }

                    if (ghostPrefabOptions.useUpdateGhostYPosition == true)
                    {
                        myTransform.position = new Vector3(myTransform.position.x, newPosition.y, myTransform.position.z);
                    }

                    if (ghostPrefabOptions.useUpdateGhostZPosition == true)
                    {
                        myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y, newPosition.z);
                    }
                }

                if (ghostPrefabOptions.useUpdateGhostSpriteRendererSprite == true)
                {
                    mySpriteRenderer.sprite = ghostPrefabOptions.linkedPaletteSwapSpriteController.mySpriteRenderer.sprite;
                }

                if (ghostPrefabOptions.useUpdateGhostSpriteRendererFlipX == true)
                {
                    mySpriteRenderer.flipX = ghostPrefabOptions.linkedPaletteSwapSpriteController.mySpriteRenderer.flipX;
                }

                if (ghostPrefabOptions.useUpdateGhostSpriteRendererFlipY == true)
                {
                    mySpriteRenderer.flipY = ghostPrefabOptions.linkedPaletteSwapSpriteController.mySpriteRenderer.flipY;
                }

                if (ghostPrefabOptions.useGhostFadeOut == true)
                {
                    mySpriteRenderer.color = new Color(mySpriteRenderer.color.r, mySpriteRenderer.color.g, mySpriteRenderer.color.b, Mathf.MoveTowards(mySpriteRenderer.color.a, 0, (ghostPrefabOptions.ghostAlphaValue / ghostPrefabOptions.ghostDisableDelay) * deltaTime));
                }

                if (ghostPrefabOptions.ghostOrderInLayer == GhostMoveOptions.OrderInLayer.Infront)
                {
                    mySpriteRenderer.sortingOrder = ghostPrefabOptions.linkedPaletteSwapSpriteController.mySpriteRenderer.sortingOrder + 1;
                }
                else if (ghostPrefabOptions.ghostOrderInLayer == GhostMoveOptions.OrderInLayer.Behind)
                {
                    mySpriteRenderer.sortingOrder = ghostPrefabOptions.linkedPaletteSwapSpriteController.mySpriteRenderer.sortingOrder - 1;
                }

                if (ghostPrefabOptions.linkedPaletteSwapSpriteController.mySwapTexture != null
                    && ghostPrefabOptions.useGhostCopySwapColors == true)
                {
                    NativeArray<Color32> colorData = ghostPrefabOptions.linkedPaletteSwapSpriteController.mySwapTexture.GetRawTextureData<Color32>();

                    if (colorData.Length == mySwapColors.Length)
                    {
                        int length = colorData.Length;
                        for (int i = 0; i < length; i++)
                        {
                            mySwapColors[i] = new Color32(colorData[i].r, colorData[i].g, colorData[i].b, 255);
                        }

                        SwapAllSpriteColorsWithSwapColorsArray(mySwapColors);
                    }
                }

                if (ghostPrefabOptions.ghostSwapColorsMethod == GhostMoveOptions.SwapColorsMethod.SwapAllParts)
                {
                    if (ghostPrefabOptions.ghostChangeColorsMethod == GhostMoveOptions.ChangeColorsMethod.Same)
                    {
                        SwapAllSpriteColorsWithSwapColor(new Color32(ghostPrefabOptions.ghostSameSwapColor.r, ghostPrefabOptions.ghostSameSwapColor.g, ghostPrefabOptions.ghostSameSwapColor.b, 255));
                    }
                    else if (ghostPrefabOptions.ghostChangeColorsMethod == GhostMoveOptions.ChangeColorsMethod.Multiple)
                    {
                        ghostPrefabOptions.ghostChangeColorTimer += deltaTime;

                        if (ghostPrefabOptions.ghostChangeColorTimer >= ghostPrefabOptions.ghostChangeColorDelay)
                        {
                            ghostPrefabOptions.ghostChangeColorTimer = 0;

                            SwapAllSpriteColorsWithSwapColor(new Color32(ghostPrefabOptions.ghostMultipleSwapColors[ghostPrefabOptions.ghostChangeColorIndex].r, ghostPrefabOptions.ghostMultipleSwapColors[ghostPrefabOptions.ghostChangeColorIndex].g, ghostPrefabOptions.ghostMultipleSwapColors[ghostPrefabOptions.ghostChangeColorIndex].b, 255));

                            ghostPrefabOptions.ghostChangeColorIndex++;

                            if (ghostPrefabOptions.ghostChangeColorIndex >= ghostPrefabOptions.ghostMultipleSwapColors.Length)
                            {
                                ghostPrefabOptions.ghostChangeColorIndex = 0;
                            }
                        }
                    }
                }
                else if (ghostPrefabOptions.ghostSwapColorsMethod == GhostMoveOptions.SwapColorsMethod.TintAllParts)
                {
                    if (ghostPrefabOptions.ghostChangeColorsMethod == GhostMoveOptions.ChangeColorsMethod.Same)
                    {
                        ResetAllSpriteColors();

                        SwapAllSpriteColorsWithTintColor(new Color32(ghostPrefabOptions.ghostSameSwapColor.r, ghostPrefabOptions.ghostSameSwapColor.g, ghostPrefabOptions.ghostSameSwapColor.b, 255), ghostPrefabOptions.ghostTintCombineAmount);
                    }
                    else if (ghostPrefabOptions.ghostChangeColorsMethod == GhostMoveOptions.ChangeColorsMethod.Multiple)
                    {
                        ghostPrefabOptions.ghostChangeColorTimer += deltaTime;

                        if (ghostPrefabOptions.ghostChangeColorTimer >= ghostPrefabOptions.ghostChangeColorDelay)
                        {
                            ghostPrefabOptions.ghostChangeColorTimer = 0;

                            ResetAllSpriteColors();

                            SwapAllSpriteColorsWithTintColor(new Color32(ghostPrefabOptions.ghostMultipleSwapColors[ghostPrefabOptions.ghostChangeColorIndex].r, ghostPrefabOptions.ghostMultipleSwapColors[ghostPrefabOptions.ghostChangeColorIndex].g, ghostPrefabOptions.ghostMultipleSwapColors[ghostPrefabOptions.ghostChangeColorIndex].b, 255), ghostPrefabOptions.ghostTintCombineAmount);

                            ghostPrefabOptions.ghostChangeColorIndex++;

                            if (ghostPrefabOptions.ghostChangeColorIndex >= ghostPrefabOptions.ghostMultipleSwapColors.Length)
                            {
                                ghostPrefabOptions.ghostChangeColorIndex = 0;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Swap Sprite Colors Methods

        public void SwapSingleSpriteColorWithSwapColor(int palettePartIndex, Color32 color, bool storeColor = false)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || mySwapTexture == null) return;

            // 0 GC.
            NativeArray<Color32> colorData = mySwapTexture.GetRawTextureData<Color32>();

            if (palettePartIndex < 0
                || palettePartIndex >= colorData.Length) return;

            colorData[palettePartIndex] = color;

            mySwapTexture.SetPixelData(colorData, 0);

            mySwapTexture.Apply();

            if (storeColor == true)
            {
                mySwapColors[palettePartIndex] = color;
            }
        }

        public void SwapAllSpriteColorsWithSwapColor(Color32 color)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || mySwapTexture == null) return;

            // 0 GC.
            NativeArray<Color32> colorData = mySwapTexture.GetRawTextureData<Color32>();

            int length = colorData.Length;
            for (int i = 0; i < length; i++)
            {
                colorData[i] = color;
            }

            mySwapTexture.SetPixelData(colorData, 0);

            mySwapTexture.Apply();
        }

        public void SwapAllSpriteColorsWithSwapColorsArray(Color32[] colors, bool storeColors = false)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || mySwapTexture == null) return;

            // 0 GC.
            NativeArray<Color32> colorData = mySwapTexture.GetRawTextureData<Color32>();

            if (colors.Length != colorData.Length) return;

            // 0 GC.
            mySwapTexture.SetPixels32(colors);

            mySwapTexture.Apply();

            if (storeColors == true)
            {
                int length = mySwapColors.Length;
                for (int i = 0; i < length; i++)
                {
                    mySwapColors[i] = colors[i];
                }
            }
        }

        public void SwapSingleSpriteColorWithTintColor(int palettePartIndex, Color32 tintColor, float tintCombineAmount)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || mySwapTexture == null) return;

            // 0 GC.
            NativeArray<Color32> colorData = mySwapTexture.GetRawTextureData<Color32>();

            if (palettePartIndex < 0
                || palettePartIndex >= colorData.Length) return;

            colorData[palettePartIndex] = Color32.Lerp(colorData[palettePartIndex], tintColor, tintCombineAmount);

            mySwapTexture.SetPixelData(colorData, 0);

            mySwapTexture.Apply();
        }

        public void SwapAllSpriteColorsWithTintColor(Color32 tintColor, float tintCombineAmount)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || mySwapTexture == null) return;

            // 0 GC.
            NativeArray<Color32> colorData = mySwapTexture.GetRawTextureData<Color32>();

            int length = colorData.Length;
            for (int i = 0; i < length; i++)
            {
                colorData[i] = Color32.Lerp(colorData[i], tintColor, tintCombineAmount);
            }

            mySwapTexture.SetPixelData(colorData, 0);

            mySwapTexture.Apply();
        }

        public void ResetAllSpriteColors()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || mySwapTexture == null) return;

            // 0 GC.
            mySwapTexture.SetPixels32(mySwapColors);

            mySwapTexture.Apply();
        }

        #endregion
    }
}
