using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class PaletteSwapSpriteEditor : MonoBehaviour
    {
        private readonly string saveSlotMessage = "Save Slot ";
        private readonly string partMessage = "Part ";

        private Camera myCamera;

        [SerializeField]
        private RectTransform objectRectTransform;
        private Vector3 objectPositionOffset;
        [SerializeField]
        private float objectMoveAmount;
        [SerializeField]
        private float objectZoomAmount;
        [SerializeField]
        private Text objectNameText;
        [SerializeField]
        private Text objectPartText;
        [SerializeField]
        private Text objectSaveSlotText;

        private int currentObjectArrayIndex;
        private int previousObjectArrayIndex;
        private int currentObjectPartArrayIndex;
        private int previousObjectPartArrayIndex;
        private int currentObjectSpriteArrayIndex;
        private int currentObjectSaveSlotIndex;
        private int previousObjectSaveSlotIndex;

        [SerializeField]
        private PaletteSwapSpriteController[] paletteSwapSpriteControllerArray;
        private PaletteSwapSpriteController currentPaletteSwapSpriteController;

        private List<PaletteSwapSpriteController.SwapColors> loadedSwapColorsList = new List<PaletteSwapSpriteController.SwapColors>();
        private int loadedSwapColorsListIndex;

        private void Start()
        {
            myCamera = Camera.main;

            currentObjectArrayIndex = 0;
            previousObjectArrayIndex = -1;
            currentObjectPartArrayIndex = 0;
            previousObjectPartArrayIndex = -1;
            currentObjectSaveSlotIndex = 0;
            previousObjectSaveSlotIndex = -1;
        }

        private void Update()
        {
            if (previousObjectArrayIndex != currentObjectArrayIndex)
            {
                if (currentPaletteSwapSpriteController != null)
                {
                    Destroy(currentPaletteSwapSpriteController.gameObject);
                }
                if (paletteSwapSpriteControllerArray[currentObjectArrayIndex] != null
                    && currentObjectArrayIndex < paletteSwapSpriteControllerArray.Length)
                {
                    currentPaletteSwapSpriteController = Instantiate(paletteSwapSpriteControllerArray[currentObjectArrayIndex]);
                }
                objectPositionOffset = new Vector3(0, 0, 0);
                previousObjectArrayIndex = currentObjectArrayIndex;
                currentObjectPartArrayIndex = 0;
                previousObjectPartArrayIndex = -1;
                currentObjectSpriteArrayIndex = 0;
                currentObjectSaveSlotIndex = 0;
                ApplySavedColorAllParts();
            }

            if (currentPaletteSwapSpriteController != null)
            {
                currentPaletteSwapSpriteController.transform.position = Utility.GetWorldPositionOfCanvasElement(objectRectTransform, myCamera) + objectPositionOffset;

                if (currentPaletteSwapSpriteController.mySpriteRenderer != null)
                {
                    if (currentObjectSpriteArrayIndex < currentPaletteSwapSpriteController.paletteSwapSpriteEditorOptions.objectSpriteArray.Length)
                    {
                        currentPaletteSwapSpriteController.mySpriteRenderer.sprite = currentPaletteSwapSpriteController.paletteSwapSpriteEditorOptions.objectSpriteArray[currentObjectSpriteArrayIndex];
                    }
                }

                if (objectNameText != null)
                {
                    objectNameText.text = currentPaletteSwapSpriteController.paletteSwapSpriteEditorOptions.objectName;
                }

                if (objectPartText != null
                    && previousObjectPartArrayIndex != currentObjectPartArrayIndex)
                {
                    previousObjectPartArrayIndex = currentObjectPartArrayIndex;   
                    
                    if (currentObjectPartArrayIndex >= currentPaletteSwapSpriteController.paletteSwapSpriteEditorOptions.objectPartNameArray.Length)
                    {
                        int partIndex = currentObjectPartArrayIndex + 1;
                        objectPartText.text = partMessage + partIndex;
                    }
                    else
                    {
                        objectPartText.text = currentPaletteSwapSpriteController.paletteSwapSpriteEditorOptions.objectPartNameArray[currentObjectPartArrayIndex];
                    }
                }

                if (objectSaveSlotText != null
                    && previousObjectSaveSlotIndex != currentObjectSaveSlotIndex)
                {
                    previousObjectSaveSlotIndex = currentObjectSaveSlotIndex;
                    int saveSlotIndex = currentObjectSaveSlotIndex + 1;
                    objectSaveSlotText.text = saveSlotMessage + saveSlotIndex;
                }
            }
        }

        private void OnDestroy()
        {
            if (currentPaletteSwapSpriteController != null)
            {
                Destroy(currentPaletteSwapSpriteController.gameObject);
            }
        }

        #region Object Methods

        public void NextObject()
        {
            currentObjectArrayIndex += 1;

            if (currentObjectArrayIndex >= paletteSwapSpriteControllerArray.Length)
            {
                currentObjectArrayIndex = 0;
            }
        }

        public void PreviousObject()
        {
            currentObjectArrayIndex -= 1;

            if (currentObjectArrayIndex < 0)
            {
                currentObjectArrayIndex = paletteSwapSpriteControllerArray.Length - 1;
            }
        }

        #endregion

        #region Object Part Methods

        public void NextObjectPart()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            currentObjectPartArrayIndex += 1;

            if (currentObjectPartArrayIndex >= currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray.Length)
            {
                currentObjectPartArrayIndex = 0;
            }
        }

        public void PreviousObjectPart()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            currentObjectPartArrayIndex -= 1;

            if (currentObjectPartArrayIndex < 0)
            {
                currentObjectPartArrayIndex = currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray.Length - 1;
            }
        }

        #endregion

        #region Object Sprite Methods

        public void NextObjectSprite()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            currentObjectSpriteArrayIndex += 1;

            if (currentObjectSpriteArrayIndex >= currentPaletteSwapSpriteController.paletteSwapSpriteEditorOptions.objectSpriteArray.Length)
            {
                currentObjectSpriteArrayIndex = 0;
            }
        }

        public void PreviousObjectSprite()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            currentObjectSpriteArrayIndex -= 1;

            if (currentObjectSpriteArrayIndex < 0)
            {
                currentObjectSpriteArrayIndex = currentPaletteSwapSpriteController.paletteSwapSpriteEditorOptions.objectSpriteArray.Length - 1;
            }
        }

        #endregion

        #region Object Save Slot Methods

        public void NextObjectSaveSlot()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            currentObjectSaveSlotIndex += 1;

            if (currentObjectSaveSlotIndex >= currentPaletteSwapSpriteController.customSwapColorsArray.Length)
            {
                currentObjectSaveSlotIndex = 0;
            }

            ApplySavedColorAllParts();
        }

        public void PreviousObjectSaveSlot()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            currentObjectSaveSlotIndex -= 1;

            if (currentObjectSaveSlotIndex < 0)
            {
                currentObjectSaveSlotIndex = currentPaletteSwapSpriteController.customSwapColorsArray.Length - 1;
            }

            ApplySavedColorAllParts();
        }

        #endregion

        #region Object Position Methods

        public void MoveObjectPositionUp()
        {
            objectPositionOffset.y += objectMoveAmount;
        }

        public void MoveObjectPositionDown()
        {
            objectPositionOffset.y -= objectMoveAmount;
        }

        public void MoveObjectPositionLeft()
        {
            objectPositionOffset.x -= objectMoveAmount;
        }

        public void MoveObjectPositionRight()
        {
            objectPositionOffset.x += objectMoveAmount;
        }

        public void ZoomObjectPositionIn()
        {
            objectPositionOffset.z -= objectZoomAmount;
        }

        public void ZoomObjectPositionOut()
        {
            objectPositionOffset.z += objectZoomAmount;
        }

        public void ResetObjectPosition()
        {
            objectPositionOffset = new Vector3(0, 0, 0);
        }

        #endregion

        #region Save And Load Methods

        public void Save()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            currentPaletteSwapSpriteController.SaveSwapColors();
        }

        public void Load()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            loadedSwapColorsList = currentPaletteSwapSpriteController.GetSavedSwapColors();
        }

        public void Unload()
        {
            loadedSwapColorsList.Clear();
            loadedSwapColorsListIndex = -1;
        }

        public void NextLoadedSwapColors()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            loadedSwapColorsListIndex += 1;

            if (loadedSwapColorsListIndex >= loadedSwapColorsList.Count)
            {
                loadedSwapColorsListIndex = 0;
            }

            currentPaletteSwapSpriteController.CopyAndPaste(loadedSwapColorsList[loadedSwapColorsListIndex].swapColorsArray, currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray);
            currentPaletteSwapSpriteController.SwapAllSpriteColors(currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray);
        }

        public void PreviousLoadedSwapColors()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            loadedSwapColorsListIndex -= 1;

            if (loadedSwapColorsListIndex < 0)
            {
                loadedSwapColorsListIndex = loadedSwapColorsList.Count - 1;
            }

            currentPaletteSwapSpriteController.CopyAndPaste(loadedSwapColorsList[loadedSwapColorsListIndex].swapColorsArray, currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray);
            currentPaletteSwapSpriteController.SwapAllSpriteColors(currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray);
        }

        public void OpenSaveDataLocation()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            currentPaletteSwapSpriteController.OpenSaveDataLocation();
        }

        #endregion

        #region Apply Color Methods

        public void ApplyDefaultColorAllParts()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            currentPaletteSwapSpriteController.CopyAndPaste(currentPaletteSwapSpriteController.defaultSwapColorsArray[0].swapColorsArray, currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray);
            currentPaletteSwapSpriteController.SwapAllSpriteColors(currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray);
        }

        public void ApplySavedColorAllParts()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            currentPaletteSwapSpriteController.LoadSwapColors();
            currentPaletteSwapSpriteController.SwapAllSpriteColors(currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray);
        }

        public void ApplyColorCurrentPart(Color32 color)
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            int length = currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (i != currentObjectPartArrayIndex)
                {
                    continue;
                }

                currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray[i] = color;
            }

            currentPaletteSwapSpriteController.SwapAllSpriteColors(currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray);
        }

        public void ApplyRandomColorCurrentPart()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            int length = currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (i != currentObjectPartArrayIndex)
                {
                    continue;
                }

                currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray[i] = new Color32((byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), 255);
            }

            currentPaletteSwapSpriteController.SwapAllSpriteColors(currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray);
        }

        public void ApplyRandomColorOtherParts()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            int length = currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (i == currentObjectPartArrayIndex)
                {
                    continue;
                }

                currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray[i] = new Color32((byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), 255);
            }

            currentPaletteSwapSpriteController.SwapAllSpriteColors(currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray);
        }

        public void ApplyRandomColorAllParts()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            int length = currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray.Length;
            for (int i = 0; i < length; i++)
            {
                currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray[i] = new Color32((byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), 255);
            }

            currentPaletteSwapSpriteController.SwapAllSpriteColors(currentPaletteSwapSpriteController.customSwapColorsArray[currentObjectSaveSlotIndex].swapColorsArray);
        }

        #endregion

        #region Helper Methods

        public void DisableGameObject(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }

            gameObject.SetActive(false);
        }

        public void EnableGameObject(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }

            gameObject.SetActive(true);
        }

        public void SelectSelectable(Selectable selectable)
        {
            if (selectable == null)
            {
                return;
            }

            selectable.Select();
        }

        #endregion
    }
}