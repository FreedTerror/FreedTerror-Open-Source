using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UFE3D;
using FPLibrary;

namespace UFE2FTE
{
    public class UFE2FTEScreenController : MonoBehaviour
    {
        private string myGameObjectName;
        private UFEScreen myUFEScreen = null;
        private Selectable savedSelectable = null;
        private bool savedSelectableSelected = false;    
        private readonly string SavedScreenNameKey = "SavedScreenName";
        [SerializeField]
        private bool enableSavedScreen;

        private void Awake()
        {
            myGameObjectName = gameObject.name;

            myUFEScreen = GetComponent<UFEScreen>();

            UFE2FTE.currentScreen = myUFEScreen;
        }

        private void OnEnable()
        {
            SetSavedSelectable(myGameObjectName);
        }

        private void Start()
        {
            EnableSavedScreen();
        }

        private void Update()
        {
            SelectSavedSelectable();

            SaveCurrentSelectable(myGameObjectName);

            SaveScreen(myGameObjectName);
        }

        private void OnDisable()
        {
            savedSelectableSelected = false;
        }

        #region Public UFE Screen Methods

        public void StartUFEScreen(string path)
        {
            UFEScreen screen = GetUFEScreen(path);
            if (screen == null)
            {
                return;
            }

            if (UFE.gameEngine == null)
            {
                StartUFEScreen(screen, (float)UFE.config.gameGUI.screenFadeDuration);
            }
            else
            {
                if (myUFEScreen == null)
                {
                    return;
                }

                UFEScreen[] screenArray = FindObjectsOfType<UFEScreen>(true);
                if (screenArray != null)
                {
                    int length = screenArray.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (UFE2FTE.IsStringMatch(path, screenArray[i].gameObject.name) == false)
                        {
                            continue;
                        }

                        DisableUFEScreen(myUFEScreen);

                        EnableUFEScreen(screenArray[i]);

                        return;
                    }
                }
            }
        }

        public void ReturnToUFEScreen(string path)
        {
            UFEScreen screen = GetUFEScreen(path);
            if (screen == null)
            {
                return;
            }

            UFE.EndGame();

            StartUFEScreen(path);

            UFE.PauseGame(false);
        }

        public void ResumeGame()
        {
            UFE.PauseGame(false);
        }

        public void Quit()
        {
            UFE.Quit();
        }

        #endregion

        #region Private UFE Screen Methods

        private void StartUFEScreen(UFEScreen screen, float fadeTime)
        {
            if (screen == null)
            {
                return;
            }

            if (UFE.currentScreen.hasFadeOut)
            {
                UFE.eventSystem.enabled = false;

                CameraFade.StartAlphaFade(UFE.config.gameGUI.screenFadeColor, false, fadeTime / 2f, 0f);

                UFE.DelayLocalAction(() => { UFE.eventSystem.enabled = true; StartUFEScreenDelayed(screen, fadeTime / 2f); }, (Fix64)fadeTime / 2);
            }
            else
            {
                StartUFEScreenDelayed(screen, fadeTime / 2f);
            }
        }

        private void StartUFEScreenDelayed(UFEScreen screen, float fadeTime)
        {
            if (screen == null)
            {
                return;
            }

            UFE.HideScreen(UFE.currentScreen);

            UFE.ShowScreen(screen);
            if (!screen.hasFadeIn)
            {
                fadeTime = 0;
            }
            CameraFade.StartAlphaFade(UFE.config.gameGUI.screenFadeColor, true, fadeTime);

            DeleteSavedScreen();
        }

        private void EnableSavedScreen()
        {
            if (enableSavedScreen == false
                || myUFEScreen == null
                || PlayerPrefs.HasKey(SavedScreenNameKey) == false)
            {
                return;
            }

            string loadedKey = PlayerPrefs.GetString(SavedScreenNameKey);

            UFEScreen[] screenArray = FindObjectsOfType<UFEScreen>(true);
            if (screenArray != null)
            {
                int length = screenArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (UFE2FTE.IsStringMatch(loadedKey, screenArray[i].gameObject.name) == false)
                    {
                        continue;
                    }

                    DisableUFEScreen(myUFEScreen);

                    EnableUFEScreen(screenArray[i]);

                    break;
                }
            }
        }

        private void DisableUFEScreen(UFEScreen screen)
        {
            if (screen == null)
            {
                return;
            }

            screen.gameObject.SetActive(false);

            screen.OnHide();

            DeleteSavedScreen();
        }

        private void EnableUFEScreen(UFEScreen screen)
        {
            if (screen == null)
            {
                return;
            }

            screen.gameObject.SetActive(true);

            screen.OnShow();

            SaveScreen(screen.gameObject.name);
        }

        public static UFEScreen GetUFEScreen(string path)
        {
            UFEScreen screen = Resources.Load<UFEScreen>(path);
            if (screen == null)
            {
#if UNITY_EDITOR
                Debug.Log("Screen not found in Resources folder! Make sure you have the correct path name. Path: " + path);
#endif
            }

            return screen;
        }

        private void SaveScreen(string value)
        {
            PlayerPrefs.SetString(SavedScreenNameKey, value);
        }

        private void DeleteSavedScreen()
        {
            PlayerPrefs.DeleteKey(SavedScreenNameKey);
        }

        #endregion

        #region Challenge Mode Methods

        public void StartChallengeModeScreen(string path)
        {
            UFE.gameMode = GameMode.ChallengeMode;
            StartUFEScreen(path);
        }

        public void StartNextChallenge()
        {
            ChallengeModeController.Instance.StartNextChallenge();
        }

        public void StartPreviousChallenge()
        {
            ChallengeModeController.Instance.StartPreviousChallenge();
        }

        public void RestartCurrentChallenge()
        {
            ChallengeModeController.Instance.RestartCurrentChallenge();
        }

        #endregion

        #region Training Mode Methods

        public void StartTrainingModeScreen(string path)
        {
            //UFE.StartTrainingMode();
            UFE.gameMode = GameMode.TrainingRoom;
            UFE.SetCPU(1, false);
            UFE.SetCPU(2, false);
            StartUFEScreen(path);
        }

        #endregion

        #region Versus Mode Methods

        public void StartVersusModeScreen(string path)
        {
            UFE.gameMode = GameMode.VersusMode;
            StartUFEScreen(path);
        }

        public void StartVersusModePlayerVersusPlayer(string path)
        {
            //UFE.StartPlayerVersusPlayer();
            UFE.gameMode = GameMode.VersusMode;
            UFE.SetCPU(1, false);
            UFE.SetCPU(2, false);
            StartUFEScreen(path);
        }

        public void StartVersusModePlayerVersusCpu(string path)
        {
            //UFE.StartPlayerVersusCpu();
            UFE.gameMode = GameMode.VersusMode;
            UFE.SetCPU(1, false);
            UFE.SetCPU(2, true);
            StartUFEScreen(path);
        }

        public void StartVersusModeCpuVersusCpu(string path)
        {
            //UFE.StartCpuVersusCpu();
            UFE.gameMode = GameMode.VersusMode;
            UFE.SetCPU(1, true);
            UFE.SetCPU(2, true);
            StartUFEScreen(path);
        }

        #endregion

        #region Selectable Methods

        private GameObject currentSelectedGameObject = null;
        private void SaveCurrentSelectable(string key)
        {
            if (EventSystem.current == null
                || EventSystem.current.currentSelectedGameObject == null
                || EventSystem.current.currentSelectedGameObject == currentSelectedGameObject)
            {
                return;
            }

            currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;

            PlayerPrefs.SetString(key, EventSystem.current.currentSelectedGameObject.name);
        }

        private void SetSavedSelectable(string key)
        {
            //Selectable[] selectableArray = FindObjectsOfType<Selectable>(false);
            Selectable[] selectableArray = GetComponentsInChildren<Selectable>();
            if (selectableArray != null)
            {
                string loadedKey = PlayerPrefs.GetString(key);

                int length = selectableArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (UFE2FTE.IsStringMatch(loadedKey, selectableArray[i].gameObject.name) == false)
                    {
                        continue;
                    }

                    savedSelectable = selectableArray[i];

                    break;
                }
            }
        }

        private void SelectSavedSelectable()
        {
            if (savedSelectableSelected == true)
            {
                return;
            }

            UFE2FTE.SelectSelectable(savedSelectable);

            savedSelectableSelected = true;
        }

        #endregion
    }
}