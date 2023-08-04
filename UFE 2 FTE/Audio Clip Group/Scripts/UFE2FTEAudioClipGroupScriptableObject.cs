using System;
using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Audio Clip Group", menuName = "U.F.E. 2 F.T.E./Audio/Audio Clip Group")]
    public class UFE2FTEAudioClipGroupScriptableObject : ScriptableObject
    {
        private enum AudioClipGroupPlayMode
        {
            PlayAudioClipGroupAscending,
            PlayAudioClipGroupDescending,
            PlayAudioClipGroupRandom,
            PlayAudioClipGroupRandomWithExclusion,       
            PlayAllAudioClipGroups
        }
        [SerializeField]
        private AudioClipGroupPlayMode audioClipGroupPlayMode = AudioClipGroupPlayMode.PlayAudioClipGroupDescending;
        private int audioClipGroupOptionsArrayIndex;

        [Serializable]
        public class AudioClipOptions
        {
            public AudioClip audioClip;
            [Tooltip("Plays the sound even if the game is paused.")]
            public bool ignoreListenerPause;
            [Range(0, 1)]
            public float minVolume = 1;
            [Range(0, 1)]
            public float maxVolume = 1;
            [Range(0, 2)]
            public float minPitch = 1;
            [Range(0, 2)]
            public float maxPitch = 1;
        }

        [Serializable]
        public class AudioClipGroupOptions
        {
            public AudioClipOptions[] audioClipOptionsArray;
        }
        public AudioClipGroupOptions[] audioClipGroupOptionsArray;

        public static void PlayAudioClipGroup(UFE2FTEAudioClipGroupScriptableObject audioClipGroupScriptableObject)
        {
            if (audioClipGroupScriptableObject == null)
            {
                return;
            }

            switch (audioClipGroupScriptableObject.audioClipGroupPlayMode)
            {
                case AudioClipGroupPlayMode.PlayAudioClipGroupAscending:
                    if (audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex < 0)
                    {
                        audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex = audioClipGroupScriptableObject.audioClipGroupOptionsArray.Length - 1;
                    }

                    if (audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex > audioClipGroupScriptableObject.audioClipGroupOptionsArray.Length - 1)
                    {
                        audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex = 0;
                    }

                    int length = audioClipGroupScriptableObject.audioClipGroupOptionsArray[audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex].audioClipOptionsArray.Length;
                    for (int i = 0; i < length; i++)
                    {
                        UFE2FTEAudioEventsManager.CallOnAudioClip(
                            audioClipGroupScriptableObject.audioClipGroupOptionsArray[audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].audioClip,
                            audioClipGroupScriptableObject.audioClipGroupOptionsArray[audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].ignoreListenerPause,
                            GetRandomVolumeUFE(audioClipGroupScriptableObject.audioClipGroupOptionsArray[audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].minVolume,
                            audioClipGroupScriptableObject.audioClipGroupOptionsArray[audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].maxVolume),
                            GetRandomPitch(audioClipGroupScriptableObject.audioClipGroupOptionsArray[audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].minPitch,
                            audioClipGroupScriptableObject.audioClipGroupOptionsArray[audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].maxPitch));
                    }

                    audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex--;
                    break;

                case AudioClipGroupPlayMode.PlayAudioClipGroupDescending:
                    if (audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex < 0)
                    {
                        audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex = audioClipGroupScriptableObject.audioClipGroupOptionsArray.Length - 1;
                    }

                    if (audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex > audioClipGroupScriptableObject.audioClipGroupOptionsArray.Length - 1)
                    {
                        audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex = 0;
                    }

                    length = audioClipGroupScriptableObject.audioClipGroupOptionsArray[audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex].audioClipOptionsArray.Length;
                    for (int i = 0; i < length; i++)
                    {
                        UFE2FTEAudioEventsManager.CallOnAudioClip(
                            audioClipGroupScriptableObject.audioClipGroupOptionsArray[audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].audioClip,
                            audioClipGroupScriptableObject.audioClipGroupOptionsArray[audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].ignoreListenerPause,
                            GetRandomVolumeUFE(audioClipGroupScriptableObject.audioClipGroupOptionsArray[audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].minVolume,
                            audioClipGroupScriptableObject.audioClipGroupOptionsArray[audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].maxVolume),
                            GetRandomPitch(audioClipGroupScriptableObject.audioClipGroupOptionsArray[audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].minPitch,
                            audioClipGroupScriptableObject.audioClipGroupOptionsArray[audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].maxPitch));
                    }

                    audioClipGroupScriptableObject.audioClipGroupOptionsArrayIndex++;
                    break;

                case AudioClipGroupPlayMode.PlayAudioClipGroupRandom:
                    int randomAudioClipGroupOptionsArrayIndex = UnityEngine.Random.Range(0, audioClipGroupScriptableObject.audioClipGroupOptionsArray.Length);

                    length = audioClipGroupScriptableObject.audioClipGroupOptionsArray[randomAudioClipGroupOptionsArrayIndex].audioClipOptionsArray.Length;
                    for (int i = 0; i < length; i++)
                    {
                        UFE2FTEAudioEventsManager.CallOnAudioClip(
                            audioClipGroupScriptableObject.audioClipGroupOptionsArray[randomAudioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].audioClip,
                            audioClipGroupScriptableObject.audioClipGroupOptionsArray[randomAudioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].ignoreListenerPause,
                            GetRandomVolumeUFE(audioClipGroupScriptableObject.audioClipGroupOptionsArray[randomAudioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].minVolume,
                            audioClipGroupScriptableObject.audioClipGroupOptionsArray[randomAudioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].maxVolume),
                            GetRandomPitch(audioClipGroupScriptableObject.audioClipGroupOptionsArray[randomAudioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].minPitch,
                            audioClipGroupScriptableObject.audioClipGroupOptionsArray[randomAudioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].maxPitch));
                    }
                    break;

                case AudioClipGroupPlayMode.PlayAudioClipGroupRandomWithExclusion:
                    randomAudioClipGroupOptionsArrayIndex = audioClipGroupScriptableObject.RandomWithExclusion(0, audioClipGroupScriptableObject.audioClipGroupOptionsArray.Length);

                    length = audioClipGroupScriptableObject.audioClipGroupOptionsArray[randomAudioClipGroupOptionsArrayIndex].audioClipOptionsArray.Length;
                    for (int i = 0; i < length; i++)
                    {
                        UFE2FTEAudioEventsManager.CallOnAudioClip(
                            audioClipGroupScriptableObject.audioClipGroupOptionsArray[randomAudioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].audioClip,
                            audioClipGroupScriptableObject.audioClipGroupOptionsArray[randomAudioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].ignoreListenerPause,
                            GetRandomVolumeUFE(audioClipGroupScriptableObject.audioClipGroupOptionsArray[randomAudioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].minVolume,
                            audioClipGroupScriptableObject.audioClipGroupOptionsArray[randomAudioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].maxVolume),
                            GetRandomPitch(audioClipGroupScriptableObject.audioClipGroupOptionsArray[randomAudioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].minPitch,
                            audioClipGroupScriptableObject.audioClipGroupOptionsArray[randomAudioClipGroupOptionsArrayIndex].audioClipOptionsArray[i].maxPitch));
                    }
                    break;

                case AudioClipGroupPlayMode.PlayAllAudioClipGroups:
                    length = audioClipGroupScriptableObject.audioClipGroupOptionsArray.Length;
                    for (int i = 0; i < length; i++)
                    {
                        int lengthA = audioClipGroupScriptableObject.audioClipGroupOptionsArray[i].audioClipOptionsArray.Length;
                        for (int a = 0; a < lengthA; a++)
                        {
                            UFE2FTEAudioEventsManager.CallOnAudioClip(
                                audioClipGroupScriptableObject.audioClipGroupOptionsArray[i].audioClipOptionsArray[a].audioClip,
                                audioClipGroupScriptableObject.audioClipGroupOptionsArray[i].audioClipOptionsArray[a].ignoreListenerPause,
                                GetRandomVolumeUFE(audioClipGroupScriptableObject.audioClipGroupOptionsArray[i].audioClipOptionsArray[a].minVolume,
                                audioClipGroupScriptableObject.audioClipGroupOptionsArray[i].audioClipOptionsArray[a].maxVolume),
                                GetRandomPitch(audioClipGroupScriptableObject.audioClipGroupOptionsArray[i].audioClipOptionsArray[a].minPitch,
                                audioClipGroupScriptableObject.audioClipGroupOptionsArray[i].audioClipOptionsArray[a].maxPitch));
                        }
                    }
                    break;
            }
        }

        public static void PlayAudioClipGroup(UFE2FTEAudioClipGroupScriptableObject[] audioClipGroupScriptableObjectArray)
        {
            if (audioClipGroupScriptableObjectArray == null)
            {
                return;
            }

            int length = audioClipGroupScriptableObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                PlayAudioClipGroup(audioClipGroupScriptableObjectArray[i]);
            }
        }

        private static float GetRandomVolumeUFE(float minVolume, float maxVolume)
        {
            if (maxVolume > UFE.GetSoundFXVolume())
            {
                float volumeDifference = minVolume - maxVolume;

                minVolume = UFE.GetSoundFXVolume() + volumeDifference;

                maxVolume = UFE.GetSoundFXVolume();
            }

            return UnityEngine.Random.Range(minVolume, maxVolume);
        }

        private static float GetRandomPitch(float minPitch, float maxPitch)
        {
            return UnityEngine.Random.Range(minPitch, maxPitch);
        }

        int excludeLastRandNum;
        bool firstRun = true;
        int RandomWithExclusion(int min, int max)
        {
            int result;
            //Don't exclude if this is first run.
            if (firstRun)
            {
                //Generate normal random number
                result = UnityEngine.Random.Range(min, max);
                excludeLastRandNum = result;
                firstRun = false;
                return result;
            }

            //Not first run, exclude last random number with -1 on the max
            result = UnityEngine.Random.Range(min, max - 1);
            //Apply +1 to the result to cancel out that -1 depending on the if statement
            result = (result < excludeLastRandNum) ? result : result + 1;
            excludeLastRandNum = result;
            return result;
        }
    }
}