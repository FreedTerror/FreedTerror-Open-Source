using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTEAudioController : MonoBehaviour
    {
        [SerializeField]
        private int soundEffectAudioSourceArrayPoolSize = 20;
        private static AudioSource[] soundEffectAudioSourceArray;
        private void Awake()
        {
            SetAllAudioSources();
        }
        private void OnEnable()
        {
            UFE2FTEAudioEventsManager.OnAudioClip += OnAudioClip;
        }

        private void Update()
        {
            SetAudioListenerPause();
        }

        private void OnDisable()
        {
            UFE2FTEAudioEventsManager.OnAudioClip -= OnAudioClip;
        }

        private void SetAudioListenerPause()
        {
            if (UFE.isPaused() == true)
            {
                AudioListener.pause = true;
            }
            else
            {
                AudioListener.pause = false;
            }
        }

        private void SetAllAudioSources()
        {
            soundEffectAudioSourceArray = new AudioSource[soundEffectAudioSourceArrayPoolSize];

            int length = soundEffectAudioSourceArray.Length;
            for (int i = 0; i < length; i++)
            {
                soundEffectAudioSourceArray[i] = gameObject.AddComponent<AudioSource>();
            }
        }

        private void OnAudioClip(AudioClip audioClip, bool ignoreListenerPause, float volume, float pitch)
        {
            PlaySoundEffect(audioClip, ignoreListenerPause, volume, pitch);
        }

        public void PlaySoundEffect(AudioClip audioClip, bool ignoreListenerPause, float volume, float pitch)
        {
            int length = soundEffectAudioSourceArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (soundEffectAudioSourceArray[i] == null
                    || soundEffectAudioSourceArray[i].isPlaying == true)
                {
                    continue;
                }

                soundEffectAudioSourceArray[i].clip = audioClip;

                soundEffectAudioSourceArray[i].ignoreListenerPause = ignoreListenerPause;

                soundEffectAudioSourceArray[i].volume = volume;

                soundEffectAudioSourceArray[i].pitch = pitch;

                soundEffectAudioSourceArray[i].Play();

                return;
            }
        }
    }
}
