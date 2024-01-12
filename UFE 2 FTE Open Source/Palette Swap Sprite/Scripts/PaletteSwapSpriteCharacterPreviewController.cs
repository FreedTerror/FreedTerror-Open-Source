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
        private ButtonPress[] previousSwapColorsButtonPressArray;
        private bool nextSwapColorsButtonPressed;
        [SerializeField]
        private ButtonPress[] nextSwapColorsButtonPressArray;
        private bool previousSwapColorsButtonPressed;

        [SerializeField]
        private static List<ButtonPress> player1ButtonPressList = new List<ButtonPress>();
        [SerializeField]
        private static List<ButtonPress> player2ButtonPressList = new List<ButtonPress>();

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
            player1ButtonPressList.Clear();
            player2ButtonPressList.Clear();

            UFE2FTE.DoFixedUpdateEvent -= OnDoFixedUpdate;
        }

        private void OnDoFixedUpdate(IDictionary<InputReferences, InputEvents> player1PreviousInputs, IDictionary<InputReferences, InputEvents> player1CurrentInputs, IDictionary<InputReferences, InputEvents> player2PreviousInputs, IDictionary<InputReferences, InputEvents> player2CurrentInputs)
        {
            if (player == UFE2FTE.Player.Player1)
            {
                UFE2FTE.AddButtonPressToListIfButtonIsPressed(ref player1ButtonPressList, player1CurrentInputs);

                int count = player1ButtonPressList.Count;
                if (count <= 0)
                {
                    nextSwapColorsButtonPressed = false;
                    previousSwapColorsButtonPressed = false;
                }
                for (int i = 0; i < count; i++)
                {
                    if (UFE2FTE.IsButtonPressMatch(player1ButtonPressList[i], nextSwapColorsButtonPressArray) == true
                        && nextSwapColorsButtonPressed == false)
                    {
                        NextSwapColors();
                        nextSwapColorsButtonPressed = true;
                    }
                    else if (UFE2FTE.IsButtonPressMatch(player1ButtonPressList[i], nextSwapColorsButtonPressArray) == false)
                    {
                        nextSwapColorsButtonPressed = false;
                    }

                    if (UFE2FTE.IsButtonPressMatch(player1ButtonPressList[i], previousSwapColorsButtonPressArray) == true
                        && previousSwapColorsButtonPressed == false)
                    {
                        PreviousSwapColors();
                        previousSwapColorsButtonPressed = true;
                    }
                    else if (UFE2FTE.IsButtonPressMatch(player1ButtonPressList[i], previousSwapColorsButtonPressArray) == false)
                    {
                        previousSwapColorsButtonPressed = false;
                    }
                }
            }
            else
            {
                UFE2FTE.AddButtonPressToListIfButtonIsPressed(ref player2ButtonPressList, player2CurrentInputs);

                int count = player2ButtonPressList.Count;
                if (count <= 0)
                {
                    nextSwapColorsButtonPressed = false;
                    previousSwapColorsButtonPressed = false;
                }
                for (int i = 0; i < count; i++)
                {
                    if (UFE2FTE.IsButtonPressMatch(player2ButtonPressList[i], nextSwapColorsButtonPressArray) == true
                        && nextSwapColorsButtonPressed == false)
                    {
                        NextSwapColors();
                        nextSwapColorsButtonPressed = true;
                    }
                    else if (UFE2FTE.IsButtonPressMatch(player2ButtonPressList[i], nextSwapColorsButtonPressArray) == false)
                    {
                        nextSwapColorsButtonPressed = false;
                    }

                    if (UFE2FTE.IsButtonPressMatch(player2ButtonPressList[i], previousSwapColorsButtonPressArray) == true
                        && previousSwapColorsButtonPressed == false)
                    {
                        PreviousSwapColors();
                        previousSwapColorsButtonPressed = true;
                    }
                    else if (UFE2FTE.IsButtonPressMatch(player2ButtonPressList[i], previousSwapColorsButtonPressArray) == false)
                    {
                        previousSwapColorsButtonPressed = false;
                    }
                }
            }

            player1ButtonPressList.Clear();
            player2ButtonPressList.Clear();
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