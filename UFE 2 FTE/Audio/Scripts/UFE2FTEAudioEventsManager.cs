using UnityEngine;

namespace UFE2FTE
{
    public static class UFE2FTEAudioEventsManager
    {
        public delegate void AudioClipHandler(AudioClip audioClip, bool ignoreListenerPause, float volume, float pitch);
        public static event AudioClipHandler OnAudioClip;

        public static void CallOnAudioClip(AudioClip audioClip, bool ignoreListenerPause, float volume, float pitch)
        {
            if (OnAudioClip == null)
            {
                return;
            }

            OnAudioClip(audioClip, ignoreListenerPause, volume, pitch);
        }
    }
}