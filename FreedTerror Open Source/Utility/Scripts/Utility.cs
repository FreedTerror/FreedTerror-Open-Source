using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FreedTerror
{
    public static class Utility
    {
        #region Audio Methods

        public static IEnumerator AudioSourceFadeCoroutine(AudioSource audioSource, float duration, float targetVolume, float deltaTime)
        {
            if (audioSource != null)
            {
                float currentTime = 0;

                float start = audioSource.volume;

                while (currentTime < duration)
                {
                    currentTime += deltaTime;

                    if (audioSource != null)
                    {
                        audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                    }

                    yield return null;
                }

                yield break;
            }
        }


        public static IEnumerator AudioMixerFadeCoroutine(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume, float deltaTime)
        {
            if (audioMixer != null)
            {
                float currentTime = 0;

                float currentVol;

                audioMixer.GetFloat(exposedParam, out currentVol);

                currentVol = Mathf.Pow(10, currentVol / 20);

                float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

                while (currentTime < duration)
                {
                    currentTime += deltaTime;

                    float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);

                    if (audioMixer != null)
                    {
                        audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
                    }

                    yield return null;
                }

                yield break;
            }
        }

        #endregion

        #region Bool Methods

        /// <summary>
        /// 1 returns true. Any other number returns false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool GetBoolFromInt(int value)
        {
            if (value == 1)
            {
                return true;
            }

            return false;
        }

        public static void SaveBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, GetIntFromBool(value));
        }

        public static bool LoadBool(string key)
        {
            return GetBoolFromInt(PlayerPrefs.GetInt(key));
        }

        private static PointerEventData pointerEventDataCurrentPosition = null;
        private static List<RaycastResult> raycastResultList = null;
        public static bool IsOverUI()
        {
            pointerEventDataCurrentPosition = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            raycastResultList = new List<RaycastResult>();

            EventSystem.current.RaycastAll(pointerEventDataCurrentPosition, raycastResultList);

            return raycastResultList.Count > 0;
        }

        #endregion

        #region Enum Methods

        public static T GetNextEnum<T>(this T _enum) where T : Enum
        {
            T[] Arr = (T[])Enum.GetValues(_enum.GetType());
            int j = Array.IndexOf<T>(Arr, _enum) + 1;
            return (j >= Arr.Length) ? Arr[0] : Arr[j];
        }

        public static T GetPreviousEnum<T>(this T _enum) where T : Enum
        {
            T[] Arr = (T[])Enum.GetValues(_enum.GetType());
            int j = Array.IndexOf<T>(Arr, _enum) - 1;
            return (j < 0) ? Arr[Arr.Length - 1] : Arr[j];
        }

        #endregion

        #region Int Methods

        /// <summary>
        /// True returns 1. False returns 0.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetIntFromBool(bool value)
        {
            if (value == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static bool IsInMinMaxRange(float comparing, float min, float max)
        {
            return comparing >= min && comparing <= max;
        }

        #endregion

        #region String Methods

        public static string AddSpacesBeforeCapitalLetters(string message)
        {
            return message = string.Concat(message.Select(x => System.Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }

        public static string AddSpacesBeforeNumbers(string message)
        {
            return message = string.Concat(message.Select(x => System.Char.IsNumber(x) ? " " + x : x.ToString())).TrimStart(' ');
        }

        /// <summary>
        /// True returns On. False returns Off.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringFromBool(bool value)
        {
            if (value == true)
            {
                return "On";
            }
            else
            {
                return "Off";
            }
        }

        #endregion

        #region Unity Editor Methods
        /*
#if UNITY_EDITOR
        /// <summary>
        /// Gets the object the property represents.
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static object GetTargetObjectOfProperty(UnityEditor.SerializedProperty prop)
        {
            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }
            return obj;
        }

        private static object GetValue_Imp(object source, string name)
        {
            if (source == null)
                return null;
            var type = source.GetType();

            while (type != null)
            {
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                    return f.GetValue(source);

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                    return p.GetValue(source, null);

                type = type.BaseType;
            }
            return null;
        }

        private static object GetValue_Imp(object source, string name, int index)
        {
            var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
            if (enumerable == null) return null;
            var enm = enumerable.GetEnumerator();
            //while (index-- >= 0)
            //    enm.MoveNext();
            //return enm.Current;

            for (int i = 0; i <= index; i++)
            {
                if (!enm.MoveNext()) return null;
            }
            return enm.Current;
        }

        public static void SetTargetObjectOfProperty(UnityEditor.SerializedProperty prop, object value)
        {
            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements.Take(elements.Length - 1))
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }

            if (Object.ReferenceEquals(obj, null)) return;

            try
            {
                var element = elements.Last();

                if (element.Contains("["))
                {
                    var tp = obj.GetType();
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    var field = tp.GetField(elementName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var arr = field.GetValue(obj) as System.Collections.IList;
                    arr[index] = value;
                    //var elementName = element.Substring(0, element.IndexOf("["));
                    //var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    //var arr = DynamicUtil.GetValue(element, elementName) as System.Collections.IList;
                    //if (arr != null) arr[index] = value;
                }
                else
                {
                    var tp = obj.GetType();
                    var field = tp.GetField(element, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (field != null)
                    {
                        field.SetValue(obj, value);
                    }
                    //DynamicUtil.SetValue(obj, element, value);
                }
            }
            catch
            {
                return;
            }
        }
#endif
        */
        #endregion

        #region Vector3 Methods

        public static Vector3 GetWorldPositionOfCanvasElement(RectTransform rectTransform, Camera camera)
        {
            if (rectTransform == null
                || camera == null)
            {
                return new Vector3(0, 0, 0);
            }

            RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, rectTransform.position, camera, out var result);

            return result;
        }

        #endregion

        #region Wait Methods

        private static Dictionary<float, WaitForSeconds> waitForSecondsDictionary = null;
        public static WaitForSeconds GetWaitForSeconds(float time)
        {
            if (waitForSecondsDictionary == null)
            {
                waitForSecondsDictionary = new Dictionary<float, WaitForSeconds>();
            }

            if (waitForSecondsDictionary.TryGetValue(time, out var wait) == true)
            {
                return wait;
            }

            waitForSecondsDictionary[time] = new WaitForSeconds(time);

            return waitForSecondsDictionary[time];
        }

        #endregion

        #region Scroll Rect Methods

        public static void KeepChildInScrollViewPort(ScrollRect scrollRect, RectTransform child, Vector2 margin)
        {
            if (scrollRect == null
                || scrollRect.content == null
                || scrollRect.viewport == null
                || child == null
                || child.rect == null)
            {
                return;
            }

            Canvas.ForceUpdateCanvases();

            // Get min and max of the viewport and child in local space to the viewport so we can compare them.
            // NOTE: use viewport instead of the scrollRect as viewport doesn't include the scrollbars in it.
            Vector2 viewPosMin = scrollRect.viewport.rect.min;
            Vector2 viewPosMax = scrollRect.viewport.rect.max;

            Vector2 childPosMin = scrollRect.viewport.InverseTransformPoint(child.TransformPoint(child.rect.min));
            Vector2 childPosMax = scrollRect.viewport.InverseTransformPoint(child.TransformPoint(child.rect.max));

            childPosMin -= margin;
            childPosMax += margin;

            Vector2 move = new Vector2(0, 0);

            // Check if one (or more) of the child bounding edges goes outside the viewport and
            // calculate move vector for the content rect so it can keep it visible.
            if (childPosMax.y > viewPosMax.y)
            {
                move.y = childPosMax.y - viewPosMax.y;
            }
            if (childPosMin.x < viewPosMin.x)
            {
                move.x = childPosMin.x - viewPosMin.x;
            }
            if (childPosMax.x > viewPosMax.x)
            {
                move.x = childPosMax.x - viewPosMax.x;
            }
            if (childPosMin.y < viewPosMin.y)
            {
                move.y = childPosMin.y - viewPosMin.y;
            }

            // Transform the move vector to world space, then to content local space (in case of scaling or rotation?) and apply it.
            Vector3 worldMove = scrollRect.viewport.TransformDirection(move);
            scrollRect.content.localPosition -= scrollRect.content.InverseTransformDirection(worldMove);
        }

        #endregion
    }
}
