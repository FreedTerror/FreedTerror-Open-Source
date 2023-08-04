#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UFE2FTE
{
    [ExecuteInEditMode]
    public class UFE2FTEAudioClipGroupScriptableObjectInspectorAudioSource : MonoBehaviour
    {
        private void OnEnable()
        {
            AudioListener.pause = false;

            EditorApplication.playModeStateChanged += PlayModeStateChanged;
        }

        private void Update()
        {
            SetAudioListenerPause();
        }

        private void OnDisable()
        {
            AudioListener.pause = false;

            EditorApplication.playModeStateChanged -= PlayModeStateChanged;
        }

        private void PlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            AudioListener.pause = false;

            DestroyImmediate(gameObject);
        }

        private void SetAudioListenerPause()
        {
            if (Application.isPlaying == true)
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
            else
            {
                AudioListener.pause = false;
            }
        }
    }
}
#endif
