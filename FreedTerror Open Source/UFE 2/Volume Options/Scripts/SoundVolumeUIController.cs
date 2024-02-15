using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class SoundVolumeUIController : MonoBehaviour
    {
        private float previousSoundVolume;
        [SerializeField]
        private Text soundVolumeText;

        private void OnEnable()
        {
            previousSoundVolume = UFE.GetSoundFXVolume();

            if (soundVolumeText != null)
            {
                soundVolumeText.text = UFE.GetSoundFXVolume().ToString();
            }
        }

        private void Update()
        {
            if (previousSoundVolume != UFE.GetSoundFXVolume())
            {
                previousSoundVolume = UFE.GetSoundFXVolume();

                if (soundVolumeText != null)
                {
                    soundVolumeText.text = UFE.GetSoundFXVolume().ToString();
                }
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