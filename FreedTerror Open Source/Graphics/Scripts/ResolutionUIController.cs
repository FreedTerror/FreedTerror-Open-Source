using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror
{
    public class ResolutionUIController : MonoBehaviour
    {
        //The Screen class seems to have a built in PlayerPrefs system.

        [SerializeField]
        private Text resolutionText;

        private Resolution[] resolutionArray;
        private List<Resolution> resolutionList = new List<Resolution>();
        private int currentResolutionIndex;
        private readonly string currentResolutionIndexKey = "CurrentResolutionIndex";
        private int previousScreenWidth;
        private int previousScreenHeight;
        
        private void Start()
        {
            resolutionArray = Screen.resolutions;
            resolutionList.Clear();

            int length = resolutionArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = resolutionArray[i];

                if (!resolutionList.Any(resolution => resolution.width == item.width && resolution.height == item.height))  //check if resolution already exists in list
                {
                    resolutionList.Add(item);  //add resolution to list if it doesn't exist yet
                }
            }

            currentResolutionIndex = PlayerPrefs.GetInt(currentResolutionIndexKey);

            previousScreenWidth = Screen.width;
            previousScreenHeight = Screen.height;

            if (resolutionText != null)
            {
                resolutionText.text =
                    Screen.width +
                    " X " +
                    Screen.height;
            }
        }

        private void Update()
        {
            if (previousScreenWidth != Screen.width
                || previousScreenHeight != Screen.height)
            {
                previousScreenWidth = Screen.width;
                previousScreenHeight = Screen.height;

                if (resolutionText != null)
                {
                    resolutionText.text = 
                        Screen.width +
                        " X " +
                        Screen.height;
                }
            }
        }

        public void DefaultResolution()
        {
            currentResolutionIndex = resolutionList.Count - 1;

            Screen.SetResolution(
                resolutionList[currentResolutionIndex].width,
                resolutionList[currentResolutionIndex].height,
                Screen.fullScreen);

            PlayerPrefs.SetInt(currentResolutionIndexKey, currentResolutionIndex);
        }

        public void NextResolution()
        {
            currentResolutionIndex += 1;

            if (currentResolutionIndex > resolutionList.Count - 1)
            {
                currentResolutionIndex = 0;
            }

            Screen.SetResolution(
                resolutionList[currentResolutionIndex].width, 
                resolutionList[currentResolutionIndex].height, 
                Screen.fullScreen);

            PlayerPrefs.SetInt(currentResolutionIndexKey, currentResolutionIndex);
        }

        public void PreviousResolution()
        {
            currentResolutionIndex -= 1;

            if (currentResolutionIndex < 0)
            {
                currentResolutionIndex = resolutionList.Count - 1;
            }

            Screen.SetResolution(
                resolutionList[currentResolutionIndex].width,
                resolutionList[currentResolutionIndex].height,
                Screen.fullScreen);

            PlayerPrefs.SetInt(currentResolutionIndexKey, currentResolutionIndex);
        }
    }
}