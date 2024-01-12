using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class PaletteSwapSpriteEditor : MonoBehaviour
    {
        private readonly string saveSlotMessage = "Save Slot ";

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

        private int objectArrayIndex;
        private int previousObjectArrayIndex;
        private int objectPartArrayIndex;
        private int objectSpriteArrayIndex;
        private int objectSaveSlotIndex;
        private int previousObjectSaveSlotIndex;

        [SerializeField]
        private PaletteSwapSpriteController[] paletteSwapSpriteControllerArray;
        private PaletteSwapSpriteController currentPaletteSwapSpriteController;

        private List<PaletteSwapSpriteController.SwapColors> loadedSwapColorsList = new List<PaletteSwapSpriteController.SwapColors>();
        private int loadedSwapColorsListIndex;

        private void Start()
        {
            myCamera = Camera.main;

            objectArrayIndex = 0;
            previousObjectArrayIndex = -1;
            objectSaveSlotIndex = 0;
            previousObjectSaveSlotIndex = -1;
        }

        private void Update()
        {
            if (previousObjectArrayIndex != objectArrayIndex)
            {
                if (currentPaletteSwapSpriteController != null)
                {
                    Destroy(currentPaletteSwapSpriteController.gameObject);
                }
                if (paletteSwapSpriteControllerArray[objectArrayIndex] != null
                    && objectArrayIndex < paletteSwapSpriteControllerArray.Length)
                {
                    currentPaletteSwapSpriteController = Instantiate(paletteSwapSpriteControllerArray[objectArrayIndex]);
                }
                objectPositionOffset = new Vector3(0, 0, 0);
                objectPartArrayIndex = 0;
                previousObjectArrayIndex = objectArrayIndex;
                objectSpriteArrayIndex = 0;
                objectSaveSlotIndex = 0;
                ApplySavedColorAllParts();
            }

            if (currentPaletteSwapSpriteController != null)
            {
                currentPaletteSwapSpriteController.transform.position = UFE2FTE.GetWorldPositionOfCanvasElement(objectRectTransform, myCamera) + objectPositionOffset;

                if (currentPaletteSwapSpriteController.mySpriteRenderer != null
                    && objectSpriteArrayIndex < currentPaletteSwapSpriteController.paletteSwapSpriteEditorOptions.objectSpriteArray.Length)
                {
                    currentPaletteSwapSpriteController.mySpriteRenderer.sprite = currentPaletteSwapSpriteController.paletteSwapSpriteEditorOptions.objectSpriteArray[objectSpriteArrayIndex];
                }

                if (objectNameText != null)
                {
                    objectNameText.text = currentPaletteSwapSpriteController.paletteSwapSpriteEditorOptions.objectName;
                }

                if (objectPartText != null)
                {
                    objectPartText.text = currentPaletteSwapSpriteController.paletteSwapSpriteEditorOptions.objectPartNameArray[objectPartArrayIndex];
                }
            }

            if (previousObjectSaveSlotIndex != objectSaveSlotIndex
                && objectSaveSlotText != null)
            {
                previousObjectSaveSlotIndex = objectSaveSlotIndex;
                int saveSlotIndex = objectSaveSlotIndex + 1;
                //objectSaveSlotText.text = saveSlotMessage + saveSlotIndex; //68 B GC
                objectSaveSlotText.text = saveSlotMessage + UFE2FTE.GetNormalStringNumber(saveSlotIndex); //44 B GC
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
            objectArrayIndex += 1;

            if (objectArrayIndex >= paletteSwapSpriteControllerArray.Length)
            {
                objectArrayIndex = 0;
            }
        }

        public void PreviousObject()
        {
            objectArrayIndex -= 1;

            if (objectArrayIndex < 0)
            {
                objectArrayIndex = paletteSwapSpriteControllerArray.Length - 1;
            }
        }

        #endregion

        #region Object Part Methods

        public void NextObjectPart()
        {
            objectPartArrayIndex += 1;

            if (objectPartArrayIndex >= currentPaletteSwapSpriteController.paletteSwapSpriteEditorOptions.objectPartNameArray.Length)
            {
                objectPartArrayIndex = 0;
            }
        }

        public void PreviousObjectPart()
        {
            objectPartArrayIndex -= 1;

            if (objectPartArrayIndex < 0)
            {
                objectPartArrayIndex = currentPaletteSwapSpriteController.paletteSwapSpriteEditorOptions.objectPartNameArray.Length - 1;
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

            objectSpriteArrayIndex += 1;

            if (objectSpriteArrayIndex >= currentPaletteSwapSpriteController.paletteSwapSpriteEditorOptions.objectSpriteArray.Length)
            {
                objectSpriteArrayIndex = 0;
            }
        }

        public void PreviousObjectSprite()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            objectSpriteArrayIndex -= 1;

            if (objectSpriteArrayIndex < 0)
            {
                objectSpriteArrayIndex = currentPaletteSwapSpriteController.paletteSwapSpriteEditorOptions.objectSpriteArray.Length - 1;
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

            objectSaveSlotIndex += 1;

            if (objectSaveSlotIndex >= currentPaletteSwapSpriteController.customSwapColorsArray.Length)
            {
                objectSaveSlotIndex = 0;
            }

            ApplySavedColorAllParts();
        }

        public void PreviousObjectSaveSlot()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            objectSaveSlotIndex -= 1;

            if (objectSaveSlotIndex < 0)
            {
                objectSaveSlotIndex = currentPaletteSwapSpriteController.customSwapColorsArray.Length - 1;
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

            currentPaletteSwapSpriteController.CopyAndPaste(loadedSwapColorsList[loadedSwapColorsListIndex].swapColorsArray, currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray);
            currentPaletteSwapSpriteController.SwapAllSpriteColors(currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray);
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

            currentPaletteSwapSpriteController.CopyAndPaste(loadedSwapColorsList[loadedSwapColorsListIndex].swapColorsArray, currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray);
            currentPaletteSwapSpriteController.SwapAllSpriteColors(currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray);
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

            currentPaletteSwapSpriteController.CopyAndPaste(currentPaletteSwapSpriteController.defaultSwapColorsArray[0].swapColorsArray, currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray);
            currentPaletteSwapSpriteController.SwapAllSpriteColors(currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray);
        }

        public void ApplySavedColorAllParts()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            currentPaletteSwapSpriteController.LoadSwapColors();
            currentPaletteSwapSpriteController.SwapAllSpriteColors(currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray);
        }

        public void ApplyColorCurrentPart(Color32 color)
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            int length = currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (i != objectPartArrayIndex)
                {
                    continue;
                }

                currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray[i] = color;
            }

            currentPaletteSwapSpriteController.SwapAllSpriteColors(currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray);
        }

        public void ApplyRandomColorCurrentPart()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            int length = currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (i != objectPartArrayIndex)
                {
                    continue;
                }

                currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray[i] = new Color32((byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), 255);
            }

            currentPaletteSwapSpriteController.SwapAllSpriteColors(currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray);
        }

        public void ApplyRandomColorOtherParts()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            int length = currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (i == objectPartArrayIndex)
                {
                    continue;
                }

                currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray[i] = new Color32((byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), 255);
            }

            currentPaletteSwapSpriteController.SwapAllSpriteColors(currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray);
        }

        public void ApplyRandomColorAllParts()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
            }

            int length = currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray.Length;
            for (int i = 0; i < length; i++)
            {
                currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray[i] = new Color32((byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), 255);
            }

            currentPaletteSwapSpriteController.SwapAllSpriteColors(currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray);
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