using System;
using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTEAudioClipGroupController : MonoBehaviour
    {
        [Serializable]
        private class AudioClipGroupOptions
        {
            public UFE2FTEAudioClipGroupScriptableObject[] audioClipGroupScriptableObjectArray;
            public bool useOnEnable;
            public bool useOnStart;
            public bool useOnDisable;
            public bool useOnDestroy;
        }
        [SerializeField]
        private AudioClipGroupOptions[] audioClipGroupOptionsArray;

        private void OnEnable()
        {
            SetAudioEventOptions(true);
        }

        private void Start()
        {
            SetAudioEventOptions(false, true);
        }

        private void OnDisable()
        {
            SetAudioEventOptions(false, false, true);
        }

        private void OnDestroy()
        {
            SetAudioEventOptions(false, false, false, true);
        }

        private void SetAudioEventOptions(bool useOnEnable = false, bool useOnStart = false, bool useOnDisable = false, bool useOnDestroy = false)
        {
            int length = audioClipGroupOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (audioClipGroupOptionsArray[i].useOnEnable == true
                    && useOnEnable == true
                    || (audioClipGroupOptionsArray[i].useOnStart == true
                    && useOnStart == true)
                    || (audioClipGroupOptionsArray[i].useOnDisable == true
                    && useOnDisable == true)
                    || (audioClipGroupOptionsArray[i].useOnDestroy == true
                    && useOnDestroy == true))
                {
                    UFE2FTEAudioClipGroupScriptableObject.PlayAudioClipGroup(audioClipGroupOptionsArray[i].audioClipGroupScriptableObjectArray);
                }
            }
        }
    }
}