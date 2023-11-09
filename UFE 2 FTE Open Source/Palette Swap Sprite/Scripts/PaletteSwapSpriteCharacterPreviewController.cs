using System.Collections.Generic;
using UFE3D;
using UnityEngine;

namespace UFE2FTE
{
    public class PaletteSwapSpriteCharacterPreviewController : MonoBehaviour
    {
        [SerializeField]
        private CharacterPreviewController characterPreviewController;
        private PaletteSwapSpriteController paletteSwapSpriteController;
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private ButtonPress nextSwapColorsButtonPress;
        private bool nextSwapColorsButtonPressed;
        [SerializeField]
        private ButtonPress previousSwapColorsButtonPress;
        private bool previousSwapColorsButtonPressed;

        private void OnEnable()
        {
            UFE2FTE.DoFixedUpdateEvent += OnDoFixedUpdate;
        }

        private void LateUpdate()
        {
            if (characterPreviewController != null
                && characterPreviewController.characterGameObject != null
                && paletteSwapSpriteController == null)
            {
                paletteSwapSpriteController = characterPreviewController.characterGameObject.GetComponent<PaletteSwapSpriteController>();
                if (paletteSwapSpriteController != null)
                {
                    paletteSwapSpriteController.player = player;
                    paletteSwapSpriteController.LoadSwapColorsVariables();
                    paletteSwapSpriteController.ApplyCurrentSwapColors();
                }
            }
        }

        private void OnDisable()
        {
            UFE2FTE.DoFixedUpdateEvent -= OnDoFixedUpdate;
        }

        private void OnDoFixedUpdate(IDictionary<InputReferences, InputEvents> player1PreviousInputs, IDictionary<InputReferences, InputEvents> player1CurrentInputs, IDictionary<InputReferences, InputEvents> player2PreviousInputs, IDictionary<InputReferences, InputEvents> player2CurrentInputs)
        {
            if (player == UFE2FTE.Player.Player1)
            {
                foreach (KeyValuePair<InputReferences, InputEvents> pair in player1CurrentInputs)
                {
                    if (pair.Key.inputType == InputType.Button 
                        && pair.Key.engineRelatedButton == nextSwapColorsButtonPress
                        && pair.Value.button == true
                        && nextSwapColorsButtonPressed == false)
                    {
                        NextSwapColors();
                        nextSwapColorsButtonPressed = true;
                    }
                    else if (pair.Key.inputType == InputType.Button
                        && pair.Key.engineRelatedButton == nextSwapColorsButtonPress
                        && pair.Value.button == false)
                    {
                        nextSwapColorsButtonPressed = false;
                    }

                    if (pair.Key.inputType == InputType.Button
                        && pair.Key.engineRelatedButton == previousSwapColorsButtonPress
                        && pair.Value.button == true
                        && previousSwapColorsButtonPressed == false)
                    {
                        PreviousSwapColors();
                        previousSwapColorsButtonPressed = true;
                    }
                    else if (pair.Key.inputType == InputType.Button
                        && pair.Key.engineRelatedButton == previousSwapColorsButtonPress
                        && pair.Value.button == false)
                    {
                        previousSwapColorsButtonPressed = false;
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<InputReferences, InputEvents> pair in player2CurrentInputs)
                {
                    if (pair.Key.inputType == InputType.Button
                        && pair.Key.engineRelatedButton == nextSwapColorsButtonPress
                        && pair.Value.button == true
                        && nextSwapColorsButtonPressed == false)
                    {
                        NextSwapColors();
                        nextSwapColorsButtonPressed = true;
                    }
                    else if (pair.Key.inputType == InputType.Button
                        && pair.Key.engineRelatedButton == nextSwapColorsButtonPress
                        && pair.Value.button == false)
                    {
                        nextSwapColorsButtonPressed = false;
                    }

                    if (pair.Key.inputType == InputType.Button
                        && pair.Key.engineRelatedButton == previousSwapColorsButtonPress
                        && pair.Value.button == true
                        && previousSwapColorsButtonPressed == false)
                    {
                        PreviousSwapColors();
                        previousSwapColorsButtonPressed = true;
                    }
                    else if (pair.Key.inputType == InputType.Button
                        && pair.Key.engineRelatedButton == previousSwapColorsButtonPress
                        && pair.Value.button == false)
                    {
                        previousSwapColorsButtonPressed = false;
                    }
                }
            }
        }

        public void NextSwapColors()
        {
            if (paletteSwapSpriteController == null)
            {
                return;
            }

            paletteSwapSpriteController.NextSwapColors();
        }

        public void PreviousSwapColors()
        {
            if (paletteSwapSpriteController == null)
            {
                return;
            }

            paletteSwapSpriteController.PreviousSwapColors();
        }
    }
}