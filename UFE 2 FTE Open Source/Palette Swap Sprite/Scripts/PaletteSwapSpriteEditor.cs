using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class PaletteSwapSpriteEditor : MonoBehaviour
    {
        private Camera myCamera;
        private readonly string saveSlotName = "Save Slot ";

        [SerializeField]
        private PaletteSwapSpriteController[] paletteSwapSpriteControllerArray;
        private PaletteSwapSpriteController currentPaletteSwapSpriteController;

        private int objectArrayIndex;
        private int objectPartArrayIndex;
        private int objectSaveSlotIndex;

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

        private void Start()
        {
            myCamera = Camera.main;
        }

        private void Update()
        {
            if (objectNameText != null)
            {
                objectNameText.text = paletteSwapSpriteControllerArray[objectArrayIndex].paletteSwapSpriteEditorName;
       
                int length = paletteSwapSpriteControllerArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (paletteSwapSpriteControllerArray[i] == null
                        || paletteSwapSpriteControllerArray[i].paletteSwapSpriteEditorName != objectNameText.text)
                    {
                        continue;
                    }

                    if (currentPaletteSwapSpriteController == null)
                    {
                        currentPaletteSwapSpriteController = Instantiate(paletteSwapSpriteControllerArray[i]);
                        objectPositionOffset = new Vector3(0, 0, 0);
                        objectPartArrayIndex = 0;
                        objectSaveSlotIndex = 0;
                        ApplySavedColorAllParts();
                    }

                    if (currentPaletteSwapSpriteController != null
                        && currentPaletteSwapSpriteController.paletteSwapSpriteEditorName != objectNameText.text)
                    {
                        Destroy(currentPaletteSwapSpriteController.gameObject);
                        currentPaletteSwapSpriteController = Instantiate(paletteSwapSpriteControllerArray[i]);
                        objectPositionOffset = new Vector3(0, 0, 0);
                        objectPartArrayIndex = 0;
                        objectSaveSlotIndex = 0;
                        ApplySavedColorAllParts();
                    }

                    break;
                }
            }

            if (objectPartText != null)
            {
                objectPartText.text = paletteSwapSpriteControllerArray[objectArrayIndex].palettePartNameArray[objectPartArrayIndex];
            }

            if (objectSaveSlotText != null)
            {
                int saveSlotIndex = objectSaveSlotIndex + 1;
                objectSaveSlotText.text = saveSlotName + saveSlotIndex;
            }

            if (currentPaletteSwapSpriteController != null)
            {
                currentPaletteSwapSpriteController.transform.position = UFE2FTE.GetWorldPositionOfCanvasElement(objectRectTransform, myCamera) + objectPositionOffset;
            }
        }

        private void OnDestroy()
        {
            if (currentPaletteSwapSpriteController != null)
            {
                Destroy(currentPaletteSwapSpriteController.gameObject);
            }
        }

        public void NextSprite()
        {
            if (currentPaletteSwapSpriteController == null)
            {
                return;
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

        #region Object Part

        public void NextObjectPart()
        {
            objectPartArrayIndex += 1;

            if (objectPartArrayIndex >= currentPaletteSwapSpriteController.palettePartNameArray.Length)
            {
                objectPartArrayIndex = 0;
            }
        }

        public void PreviousObjectPart()
        {
            objectPartArrayIndex -= 1;

            if (objectPartArrayIndex < 0)
            {
                objectPartArrayIndex = currentPaletteSwapSpriteController.palettePartNameArray.Length - 1;
            }
        }

        #endregion

        #region Object Save Slot

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

            currentPaletteSwapSpriteController.LoadCustomSwapColorsArray();
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

                currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray[i] = currentPaletteSwapSpriteController.GetDefaultRandomColor32();
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

                currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray[i] = currentPaletteSwapSpriteController.GetDefaultRandomColor32();
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
                currentPaletteSwapSpriteController.customSwapColorsArray[objectSaveSlotIndex].swapColorsArray[i] = currentPaletteSwapSpriteController.GetDefaultRandomColor32();
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