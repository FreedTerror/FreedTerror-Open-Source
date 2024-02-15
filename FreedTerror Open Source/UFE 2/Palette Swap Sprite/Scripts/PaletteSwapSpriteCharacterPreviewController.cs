using System.Collections.Generic;
using UFE3D;
using UnityEngine;

namespace FreedTerror.UFE2
{
    public class PaletteSwapSpriteCharacterPreviewController : MonoBehaviour
    {
        [SerializeField]
        private CharacterPreviewController characterPreviewController;
        private PaletteSwapSpriteController paletteSwapSpriteController;
        [SerializeField]
        private UFE2Manager.Player player;    
        [SerializeField]
        private ButtonPress[] nextSwapColorsButtonPressArray;
        private bool nextSwapColorsButtonPressed;
        [SerializeField]
        private ButtonPress[] previousSwapColorsButtonPressArray;
        private bool previousSwapColorsButtonPressed;

        private void OnEnable()
        {
            UFE2Manager.DoFixedUpdateEvent += OnDoFixedUpdate;
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
            UFE2Manager.DoFixedUpdateEvent -= OnDoFixedUpdate;
        }

        private void OnDoFixedUpdate(IDictionary<InputReferences, InputEvents> player1PreviousInputs, IDictionary<InputReferences, InputEvents> player1CurrentInputs, IDictionary<InputReferences, InputEvents> player2PreviousInputs, IDictionary<InputReferences, InputEvents> player2CurrentInputs)
        {
            if (player == UFE2Manager.Player.Player1)
            {
                bool matchingButtonPressed = false;

                foreach (KeyValuePair<InputReferences, InputEvents> pair in player1CurrentInputs)
                {
                    if (pair.Key.inputType == InputType.Button
                        && UFE2Manager.IsButtonPressMatch(pair.Key.engineRelatedButton, nextSwapColorsButtonPressArray) == true
                        && pair.Value.button == true)
                    {
                        matchingButtonPressed = true;

                        break;
                    }
                }

                if (matchingButtonPressed == true
                    && nextSwapColorsButtonPressed == false)
                {
                    nextSwapColorsButtonPressed = true;
                    NextSwapColors();
                }
                else if (matchingButtonPressed == false)
                {
                    nextSwapColorsButtonPressed = false;
                }

                matchingButtonPressed = false;

                foreach (KeyValuePair<InputReferences, InputEvents> pair in player1CurrentInputs)
                {
                    if (pair.Key.inputType == InputType.Button
                        && UFE2Manager.IsButtonPressMatch(pair.Key.engineRelatedButton, previousSwapColorsButtonPressArray) == true
                        && pair.Value.button == true)
                    {
                        matchingButtonPressed = true;

                        break;
                    }
                }

                if (matchingButtonPressed == true
                    && previousSwapColorsButtonPressed == false)
                {
                    previousSwapColorsButtonPressed = true;
                    PreviousSwapColors();
                }
                else if (matchingButtonPressed == false)
                {
                    previousSwapColorsButtonPressed = false;
                }
            }
            else
            {
                bool matchingButtonPressed = false;

                foreach (KeyValuePair<InputReferences, InputEvents> pair in player2CurrentInputs)
                {
                    if (pair.Key.inputType == InputType.Button
                        && UFE2Manager.IsButtonPressMatch(pair.Key.engineRelatedButton, nextSwapColorsButtonPressArray) == true
                        && pair.Value.button == true)
                    {
                        matchingButtonPressed = true;

                        break;
                    }
                }

                if (matchingButtonPressed == true
                    && nextSwapColorsButtonPressed == false)
                {
                    nextSwapColorsButtonPressed = true;
                    NextSwapColors();
                }
                else if (matchingButtonPressed == false)
                {
                    nextSwapColorsButtonPressed = false;
                }

                matchingButtonPressed = false;

                foreach (KeyValuePair<InputReferences, InputEvents> pair in player2CurrentInputs)
                {
                    if (pair.Key.inputType == InputType.Button
                        && UFE2Manager.IsButtonPressMatch(pair.Key.engineRelatedButton, previousSwapColorsButtonPressArray) == true
                        && pair.Value.button == true)
                    {
                        matchingButtonPressed = true;

                        break;
                    }
                }

                if (matchingButtonPressed == true
                    && previousSwapColorsButtonPressed == false)
                {
                    previousSwapColorsButtonPressed = true;
                    PreviousSwapColors();
                }
                else if (matchingButtonPressed == false)
                {
                    previousSwapColorsButtonPressed = false;
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