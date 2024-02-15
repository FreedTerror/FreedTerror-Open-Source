using System.Collections.Generic;
using UFE3D;
using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class ScrollRectUIController : MonoBehaviour
    {  
        [SerializeField]
        private ScrollRect scrollRect;
        private float currentHorizontalScrollSpeed;
        private float currentVerticalScrollSpeed;
        private float previousHorizontalNormalizedPosition;
        private float previousVerticalNormalizedPosition;
        [SerializeField]
        private ScrollDataScriptableObject scrollDataScriptableObject;
       
        private string HorizontalNormalizedPositionKey = "HorizontalNormalizedPosition";
        private string VerticalNormalizedPositionKey = "VerticalNormalizedPosition";

        private void OnEnable()
        {
            UFE2Manager.DoFixedUpdateEvent += DoFixedUpdateEvent;
        }

        private void Start()
        {
            SetKeys();

            LoadAndSetNormalizedPositions();
        }

        private void LateUpdate()
        {
            if (scrollRect != null)
            {
                float normalizedPosition = scrollRect.horizontalNormalizedPosition;
                if (previousHorizontalNormalizedPosition != normalizedPosition)
                {
                    previousHorizontalNormalizedPosition = normalizedPosition;

                    PlayerPrefs.SetFloat(HorizontalNormalizedPositionKey, normalizedPosition);
                }

                normalizedPosition = scrollRect.verticalNormalizedPosition;
                if (previousVerticalNormalizedPosition != normalizedPosition)
                {
                    previousVerticalNormalizedPosition = normalizedPosition;

                    PlayerPrefs.SetFloat(VerticalNormalizedPositionKey, normalizedPosition);
                }
            }
        }

        private void OnDisable()
        {
            UFE2Manager.DoFixedUpdateEvent -= DoFixedUpdateEvent;
        }

        private void DoFixedUpdateEvent(IDictionary<InputReferences, InputEvents> player1PreviousInputs, IDictionary<InputReferences, InputEvents> player1CurrentInputs, IDictionary<InputReferences, InputEvents> player2PreviousInputs, IDictionary<InputReferences, InputEvents> player2CurrentInputs)
        {
            if (UFE2Manager.instance.pausedPlayer == UFE2Manager.Player.Player1)
            {
                CheckInputs(player1CurrentInputs);
            }
            else if (UFE2Manager.instance.pausedPlayer == UFE2Manager.Player.Player2)
            {
                CheckInputs(player2CurrentInputs);
            }
        }

        private void CheckInputs(IDictionary<InputReferences, InputEvents> inputDictionary)
        {
            if (inputDictionary == null)
            {
                return;
            }

            float deltaTime = (float)UFE.fixedDeltaTime;

            foreach (KeyValuePair<InputReferences, InputEvents> pair in inputDictionary)
            {
                switch (pair.Key.inputType)
                {
                    case InputType.HorizontalAxis:
                        if ((int)pair.Value.axisRaw >= 1)
                        {
                            ScrollRectHorizontally(scrollRect, currentHorizontalScrollSpeed, deltaTime);
                        }
                        else if ((int)pair.Value.axisRaw <= -1)
                        {
                            ScrollRectHorizontally(scrollRect, -currentHorizontalScrollSpeed, deltaTime);
                        }
                        else
                        {
                            ResetHorizontalScrollSpeed();
                        }
                        break;

                    case InputType.VerticalAxis:
                        if ((int)pair.Value.axisRaw >= 1)
                        {
                            ScrollRectVertically(scrollRect, currentVerticalScrollSpeed, deltaTime);
                        }
                        else if ((int)pair.Value.axisRaw <= -1)
                        {
                            ScrollRectVertically(scrollRect, -currentVerticalScrollSpeed, deltaTime);
                        }
                        else
                        {
                            ResetVerticalScrollSpeed();
                        }
                        break;
                }
            }
        }

        private void ScrollRectHorizontally(ScrollRect scrollRect, float scrollSpeed, float deltaTime)
        {
            if (scrollRect == null 
                || scrollRect.horizontal == false
                || scrollRect.content == null
                || scrollDataScriptableObject == null)
            {
                return;
            }

            float position = scrollRect.horizontalNormalizedPosition + (scrollSpeed / scrollRect.content.rect.width);
            if (position < 0)
            {
                position = 0;
            }
            else if (position > 1)
            {
                position = 1;
            }
            scrollRect.horizontalNormalizedPosition = position;

            currentHorizontalScrollSpeed += scrollDataScriptableObject.horizontalScrollAcceleration * deltaTime;
            if (currentHorizontalScrollSpeed > scrollDataScriptableObject.maxHorizontalScrollSpeed)
            {
                currentHorizontalScrollSpeed = scrollDataScriptableObject.maxHorizontalScrollSpeed;
            }
        }

        private void ScrollRectVertically(ScrollRect scrollRect, float scrollSpeed, float deltaTime)
        {
            if (scrollRect == null
                || scrollRect.vertical == false
                || scrollRect.content == null
                || scrollDataScriptableObject == null)
            {
                return;
            }

            float position = scrollRect.verticalNormalizedPosition + (scrollSpeed / scrollRect.content.rect.height);
            if (position < 0)
            {
                position = 0;
            }
            else if (position > 1)
            {
                position = 1;
            }
            scrollRect.verticalNormalizedPosition = position;

            currentVerticalScrollSpeed += scrollDataScriptableObject.verticalScrollAcceleration * deltaTime;
            if (currentVerticalScrollSpeed > scrollDataScriptableObject.maxVerticalScrollSpeed)
            {
                currentVerticalScrollSpeed = scrollDataScriptableObject.maxVerticalScrollSpeed;
            }
        }

        private void ResetHorizontalScrollSpeed()
        {
            if (scrollDataScriptableObject == null)
            {
                return;
            }
            
            currentHorizontalScrollSpeed = scrollDataScriptableObject.minHorizontalScrollSpeed;
        }

        private void ResetVerticalScrollSpeed()
        {
            if (scrollDataScriptableObject == null)
            {
                return;
            }
            
            currentVerticalScrollSpeed = scrollDataScriptableObject.minVerticalScrollSpeed;
        }

        private void SetKeys()
        {
            if (gameObject.transform.parent == null)
            {
                return;
            }

            string nameKey = gameObject.transform.parent.name;

            HorizontalNormalizedPositionKey = HorizontalNormalizedPositionKey + nameKey;

            VerticalNormalizedPositionKey = VerticalNormalizedPositionKey + nameKey;
        }

        private void LoadAndSetNormalizedPositions()
        {
            if (PlayerPrefs.HasKey(HorizontalNormalizedPositionKey) == true)
            {
                float position = PlayerPrefs.GetFloat(HorizontalNormalizedPositionKey);
                if (position < 0)
                {
                    position = 0;
                }
                else if (position > 1)
                {
                    position = 1;
                }
                scrollRect.horizontalNormalizedPosition = position;
            }
            else
            {
                scrollRect.horizontalNormalizedPosition = 0;
            }

            if (PlayerPrefs.HasKey(VerticalNormalizedPositionKey) == true)
            {
                float position = PlayerPrefs.GetFloat(VerticalNormalizedPositionKey);
                if (position < 0)
                {
                    position = 0;
                }
                else if (position > 1)
                {
                    position = 1;
                }
                scrollRect.verticalNormalizedPosition = position;
            }
            else
            {
                scrollRect.verticalNormalizedPosition = 1;
            }       
        }
    }
}