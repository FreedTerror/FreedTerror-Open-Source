using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class ScrollRectUIController : MonoBehaviour
    {  
        [SerializeField]
        private ScrollRect scrollRect;
        [SerializeField]
        private ScrollRectScrollDataScriptableObject scrollRectScrollDataScriptableObject;
        //[SerializeField]
        //private bool matchCurrentScrollSpeed;
        private float currentHorizontalScrollSpeed;
        private float currentVerticalScrollSpeed;

        private float loadedHorizontalNormalizedPosition = 0;
        private float loadedVerticalNormalizedPosition = 0;
        private bool scrollRectAtLoadedNormalizedPosition = false;
        private string HorizontalNormalizedPositionKey { get; set; } = "HorizontalNormalizedPosition";
        private string VerticalNormalizedPositionKey { get; set; } = "VerticalNormalizedPosition";

        private void OnEnable()
        {
            UFE2FTE.DoFixedUpdateEvent += DoFixedUpdateEvent;
        }

        private void Start()
        {
            SetPlayerPrefsKeys();

            LoadFromPlayerPrefs();

            scrollRectAtLoadedNormalizedPosition = false;
        }

        private void LateUpdate()
        {
            SetScrollRectPositionToLoadedPosition();

            //MatchCurrentScrollSpeed();

            SaveToPlayerPrefs();
        }

        private void OnDisable()
        {
            UFE2FTE.DoFixedUpdateEvent -= DoFixedUpdateEvent;

            SaveToPlayerPrefs();
        }

        private void SetScrollRectPositionToLoadedPosition()
        {
            if (scrollRectAtLoadedNormalizedPosition == false)
            {
                SetScrollRectHorizontalNormalizedPosition(loadedHorizontalNormalizedPosition);

                SetScrollRectVerticalNormalizedPosition(loadedVerticalNormalizedPosition);

                scrollRectAtLoadedNormalizedPosition = true;
            }
        }

        /*private void MatchCurrentScrollSpeed()
        {
            if (matchCurrentScrollSpeed == false)
            {
                return;
            }

            if (currentHorizontalScrollSpeed > currentVerticalScrollSpeed)
            {
                currentVerticalScrollSpeed = currentHorizontalScrollSpeed;
            }

            if (currentVerticalScrollSpeed > currentHorizontalScrollSpeed)
            {
                currentHorizontalScrollSpeed = currentVerticalScrollSpeed;
            }
        }*/

        private void DoFixedUpdateEvent(IDictionary<InputReferences, InputEvents> player1PreviousInputs, IDictionary<InputReferences, InputEvents> player1CurrentInputs, IDictionary<InputReferences, InputEvents> player2PreviousInputs, IDictionary<InputReferences, InputEvents> player2CurrentInputs)
        {
            if (UFE2FTE.instance.pausedPlayer == UFE2FTE.Player.Player1)
            {
                CheckInputs(player1CurrentInputs);
            }
            else if (UFE2FTE.instance.pausedPlayer == UFE2FTE.Player.Player2)
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

            foreach (KeyValuePair<InputReferences, InputEvents> pair in inputDictionary)
            {
                switch (pair.Key.inputType)
                {
                    case InputType.HorizontalAxis:
                        if ((int)pair.Value.axisRaw >= 1)
                        {
                            ScrollRectHorizontally(scrollRect, currentHorizontalScrollSpeed, GetRectTransformFromScrollRectContent(scrollRect));

                            HorizontalSpeedUp(Time.deltaTime);
                        }
                        else if ((int)pair.Value.axisRaw <= -1)
                        {
                            ScrollRectHorizontally(scrollRect, -currentHorizontalScrollSpeed, GetRectTransformFromScrollRectContent(scrollRect));

                            HorizontalSpeedUp(Time.deltaTime);
                        }
                        else
                        {
                            ResetHorizontalSpeed();
                        }
                        break;

                    case InputType.VerticalAxis:
                        if ((int)pair.Value.axisRaw >= 1)
                        {
                            ScrollRectVertically(scrollRect, currentVerticalScrollSpeed, GetRectTransformFromScrollRectContent(scrollRect));

                            VerticalSpeedUp(Time.deltaTime);
                        }
                        else if ((int)pair.Value.axisRaw <= -1)
                        {
                            ScrollRectVertically(scrollRect, -currentVerticalScrollSpeed, GetRectTransformFromScrollRectContent(scrollRect));

                            VerticalSpeedUp(Time.deltaTime);
                        }
                        else
                        {
                            ResetVerticalSpeed();
                        }
                        break;
                }
            }
        }

        private static void ScrollRectHorizontally(ScrollRect scrollRect, float scrollSpeed, RectTransform rectWidth)
        {
            if (scrollRect == null
                || scrollRect.horizontal == false
                || rectWidth == null)
            {
                return;
            }

            float position = scrollRect.horizontalNormalizedPosition + (scrollSpeed / rectWidth.rect.width);

            if (position <= 0)
            {
                position = 0;
            }
            else if (position >= 1)
            {
                position = 1;
            }

            scrollRect.horizontalNormalizedPosition = position;
        }

        private static void ScrollRectVertically(ScrollRect scrollRect, float scrollSpeed, RectTransform rectHeight)
        {
            if (scrollRect == null
                || scrollRect.vertical == false
                || rectHeight == null)
            {
                return;
            }

            float position = scrollRect.verticalNormalizedPosition + (scrollSpeed / rectHeight.rect.height);

            if (position <= 0)
            {
                position = 0;
            }
            else if (position >= 1)
            {
                position = 1;
            }

            scrollRect.verticalNormalizedPosition = position;
        }

        private void HorizontalSpeedUp(float deltaTime)
        {
            float speed = currentHorizontalScrollSpeed + GetHorizontalAcceleration() * deltaTime;

            float maxSpeed = GetHorizontalMaxSpeed();
            if (speed >= maxSpeed)
            {
                speed = maxSpeed;
            }

            currentHorizontalScrollSpeed = speed;
        }

        private void VerticalSpeedUp(float deltaTime)
        {
            float speed = currentVerticalScrollSpeed + GetVerticalAcceleration() * deltaTime;

            float maxSpeed = GetVerticalMaxSpeed();
            if (speed >= maxSpeed)
            {
                speed = maxSpeed;
            }

            currentVerticalScrollSpeed = speed;
        }

        private void SetScrollRectHorizontalNormalizedPosition(float normalizedPosition)
        {
            if (scrollRect == null)
            {
                return;
            }

            if (normalizedPosition <= 0)
            {
                normalizedPosition = 0;
            }
            else if (normalizedPosition >= 1)
            {
                normalizedPosition = 1;
            }

            scrollRect.horizontalNormalizedPosition = normalizedPosition;
        }

        private void SetScrollRectVerticalNormalizedPosition(float normalizedPosition)
        {
            if (scrollRect == null)
            {
                return;
            }

            if (normalizedPosition <= 0)
            {
                normalizedPosition = 0;
            }
            else if (normalizedPosition >= 1)
            {
                normalizedPosition = 1;
            }

            scrollRect.verticalNormalizedPosition = normalizedPosition;
        }

        private RectTransform GetRectTransformFromScrollRectContent(ScrollRect scrollRect)
        {
            if (scrollRect == null)
            {
                return null;
            }

            return scrollRect.content;
        }

        private float GetHorizontalAcceleration()
        {
            if (scrollRectScrollDataScriptableObject == null)
            {
                return 0;
            }

            return scrollRectScrollDataScriptableObject.horizontalScrollAcceleration;
        }

        private float GetVerticalAcceleration()
        {
            if (scrollRectScrollDataScriptableObject == null)
            {
                return 0;
            }

            return scrollRectScrollDataScriptableObject.verticalScrollAcceleration;
        }

        private float GetHorizontalMaxSpeed()
        {
            if (scrollRectScrollDataScriptableObject == null)
            {
                return 0;
            }
            
            return scrollRectScrollDataScriptableObject.horizontalMaxScrollSpeed;
        }

        private float GetVerticalMaxSpeed()
        {
            if (scrollRectScrollDataScriptableObject == null)
            {
                return 0;
            }
            
            return scrollRectScrollDataScriptableObject.verticalMaxScrollSpeed;
        }

        private void ResetHorizontalSpeed()
        {
            if (scrollRectScrollDataScriptableObject == null)
            {
                return;
            }
            
            currentHorizontalScrollSpeed = scrollRectScrollDataScriptableObject.horizontalScrollSpeed;
        }

        private void ResetVerticalSpeed()
        {
            if (scrollRectScrollDataScriptableObject == null)
            {
                return;
            }
            
            currentVerticalScrollSpeed = scrollRectScrollDataScriptableObject.verticalScrollSpeed;
        }

        private void SetPlayerPrefsKeys()
        {
            if (gameObject.transform.parent == null)
            {
                return;
            }

            HorizontalNormalizedPositionKey = HorizontalNormalizedPositionKey + gameObject.transform.parent.name;

            VerticalNormalizedPositionKey = VerticalNormalizedPositionKey + gameObject.transform.parent.name;
        }

        private void SaveToPlayerPrefs()
        {
            if (scrollRect == null)
            {
                return;
            }

            PlayerPrefs.SetFloat(HorizontalNormalizedPositionKey, scrollRect.horizontalNormalizedPosition);

            PlayerPrefs.SetFloat(VerticalNormalizedPositionKey, scrollRect.verticalNormalizedPosition);
        }

        private void LoadFromPlayerPrefs()
        {
            float position = 0;

            if (PlayerPrefs.HasKey(HorizontalNormalizedPositionKey) == true)
            {
                position = PlayerPrefs.GetFloat(HorizontalNormalizedPositionKey);
            }
            else
            {
                position = 0;
            }

            loadedHorizontalNormalizedPosition = position;

            if (PlayerPrefs.HasKey(VerticalNormalizedPositionKey) == true)
            {
                position = PlayerPrefs.GetFloat(VerticalNormalizedPositionKey);
            }
            else
            {
                position = 1;
            }

            loadedVerticalNormalizedPosition = position;
        }
    }
}