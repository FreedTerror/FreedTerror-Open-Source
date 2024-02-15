using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class MusicVolumeUIController : MonoBehaviour
    {
        private float previousMusicVolume;
        [SerializeField]
        private Text musicVolumeText;

        private void OnEnable()
        {
            previousMusicVolume = UFE.GetMusicVolume();

            if (musicVolumeText != null)
            {
                musicVolumeText.text = UFE.GetMusicVolume().ToString();
            }
        }

        private void Update()
        {
            if (previousMusicVolume != UFE.GetMusicVolume())
            {
                previousMusicVolume = UFE.GetMusicVolume();

                if (musicVolumeText != null)
                {
                    musicVolumeText.text = UFE.GetMusicVolume().ToString();
                }
            }
        }

        public void NextMusicVolume()
        {
            float volume = UFE.GetMusicVolume() + 0.05f;

            if (volume > 1)
            {
                volume = 0;
            }

            volume = Mathf.Round(volume * 100) / 100;

            UFE.SetMusicVolume(volume);
        }

        public void PreviousMusicVolume()
        {
            float volume = UFE.GetMusicVolume() - 0.05f;

            if (volume < 0)
            {
                volume = 1;
            }

            volume = Mathf.Round(volume * 100) / 100;

            UFE.SetMusicVolume(volume);
        }
    }
}