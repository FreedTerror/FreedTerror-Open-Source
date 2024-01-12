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

        #region Screen Methods

        public void StartUFEScreen(string path)
        {
            UFEScreen screen = GetUFEScreen(path);
            if (screen == null)
            {
                return;
            }

            if (UFE.GameEngine == null)
            {
                if (UFE.currentScreen.hasFadeOut == true)
                {
                    UFE.eventSystem.enabled = false;

                    CameraFade.StartAlphaFade(UFE.config.gameGUI.screenFadeColor, false, (float)UFE.config.gameGUI.screenFadeDuration / 2f, 0f);

                    UFE.DelayLocalAction(() =>
                    {
                        UFE.eventSystem.enabled = true;
                        UFE.HideScreen(UFE.currentScreen);
                        UFE.ShowScreen(screen);
                        if (screen.hasFadeIn == true)
                        {
                            CameraFade.StartAlphaFade(UFE.config.gameGUI.screenFadeColor, true, (float)UFE.config.gameGUI.screenFadeDuration);
                        }
                        else
                        {
                            CameraFade.StartAlphaFade(UFE.config.gameGUI.screenFadeColor, true, 0);
                        }
                        DeleteSavedScreen();
                    },
                    (Fix64)UFE.config.gameGUI.screenFadeDuration / 2);
                }
                else
                {
                    UFE.HideScreen(UFE.currentScreen);
                    UFE.ShowScreen(screen);
                    if (screen.hasFadeIn == true)
                    {
                        CameraFade.StartAlphaFade(UFE.config.gameGUI.screenFadeColor, true, (float)UFE.config.gameGUI.screenFadeDuration);
                    }
                    else
                    {
                        CameraFade.StartAlphaFade(UFE.config.gameGUI.screenFadeColor, true, 0);
                    }
                    DeleteSavedScreen();
                }
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
                        if (path != screenArray[i].gameObject.name)
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
                    if (loadedKey != screenArray[i].gameObject.name)
                    {
                        continue;
                    }

                    DisableUFEScreen(myUFEScreen);

                    EnableUFEScreen(screenArray[i]);

                    break;
                }
            }
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
            ChallengeModeController.instance.StartNextChallenge();
        }

        public void StartPreviousChallenge()
        {
            ChallengeModeController.instance.StartPreviousChallenge();
        }

        public void RestartCurrentChallenge()
        {
            ChallengeModeController.instance.RestartCurrentChallenge();
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
                    if (loadedKey != selectableArray[i].gameObject.name)
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

        public void ResumeGame()
        {
            UFE.PauseGame(false);
        }

        public void Quit()
        {
            UFE.Quit();
        }
    }
}