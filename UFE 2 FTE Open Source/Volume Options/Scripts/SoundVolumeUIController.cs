using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class SoundVolumeUIController : MonoBehaviour
    {
        private float previousSoundVolume;
        [SerializeField]
        private Text soundVolumeText;

        private void OnEnable()
        {
            previousSoundVolume = UFE.GetSoundFXVolume();
            UFE2FTE.SetTextMessage(soundVolumeText, UFE.GetSoundFXVolume().ToString());
        }

        private void Update()
        {
            if (previousSoundVolume != UFE.GetSoundFXVolume())
            {
                previousSoundVolume = UFE.GetSoundFXVolume();
                UFE2FTE.SetTextMessage(soundVolumeText, UFE.GetSoundFXVolume().ToString());
            }
        }

        public void NextSoundVolume()
        {
            float volume = UFE.GetSoundFXVolume() + 0.05f;

            if (volume > 1)
            {
                volume = 0;
            }

            volume = Mathf.Round(volume * 100) / 100;

            UFE.SetSoundFXVolume(volume);
        }

        public void PreviousSoundVolume()
        {
            float volume = UFE.GetSoundFXVolume() - 0.05f;

            if (volume < 0)
            {
                volume = 1;
            }

            volume = Mathf.Round(volume * 100) / 100;

            UFE.SetSoundFXVolume(volume);
        }
    }
}