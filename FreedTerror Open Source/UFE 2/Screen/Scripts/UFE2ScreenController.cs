using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UFE3D;
using FPLibrary;

namespace FreedTerror.UFE2
{
    public class UFE2ScreenController : MonoBehaviour
    {
        private string myGameObjectName;
        private UFEScreen myUFEScreen = null;

        private GameObject previousSelectedGameObject = null;

        private readonly string savedScreenKey = "SavedScreenName";
        private string savedSelectableKey;
        private Selectable savedSelectable;
        private bool savedSelectableSelected = false;

        [SerializeField]
        private bool enableSavedScreen;

        private void Awake()
        {
            myGameObjectName = gameObject.name;

            myUFEScreen = GetComponent<UFEScreen>();

            savedSelectableKey = myGameObjectName + "SavedSelectable";
        }

        private void Start()
        {
            EnableSavedScreen();

            SetSavedSelectable();       
        }

        private void Update()
        {
            SaveScreen(myGameObjectName);

            SelectSavedSelectable();

            SaveCurrentSelectable();       
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
                    (Fix64)UFE.config.gameGUI.screenFadeDuration / (Fix64)2);
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
                        var item = screenArray[i];

                        if (item == null
                            || path != item.gameObject.name)
                        {
                            continue;
                        }

                        DisableUFEScreen(myUFEScreen);

                        EnableUFEScreen(item);

                        break;
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
                || PlayerPrefs.HasKey(savedScreenKey) == false)
            {
                return;
            }

            string loadedKey = PlayerPrefs.GetString(savedScreenKey);

            UFEScreen[] screenArray = FindObjectsOfType<UFEScreen>(true);
            if (screenArray != null)
            {
                int length = screenArray.Length;
                for (int i = 0; i < length; i++)
                {
                    var item = screenArray[i];

                    if (item == null
                        || loadedKey != item.gameObject.name)
                    {
                        continue;
                    }

                    DisableUFEScreen(myUFEScreen);

                    EnableUFEScreen(item);

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
            PlayerPrefs.SetString(savedScreenKey, value);
        }

        private void DeleteSavedScreen()
        {
            PlayerPrefs.DeleteKey(savedScreenKey);
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
  
        private void SaveCurrentSelectable()
        {
            if (EventSystem.current == null
                || EventSystem.current.currentSelectedGameObject == null
                || EventSystem.current.currentSelectedGameObject == previousSelectedGameObject)
            {
                return;
            }

            previousSelectedGameObject = EventSystem.current.currentSelectedGameObject;

            PlayerPrefs.SetString(savedSelectableKey, previousSelectedGameObject.name);
        }

        private void SetSavedSelectable()
        {
            //Selectable[] selectableArray = FindObjectsOfType<Selectable>(false);
            Selectable[] selectableArray = GetComponentsInChildren<Selectable>();
            if (selectableArray == null)
            {
                return;
            }

            string selectableName = PlayerPrefs.GetString(savedSelectableKey);
            int length = selectableArray.Length;
            for (int i = 0; i < length; i++)
            {
                var item = selectableArray[i];

                if (item == null
                    || item.name != selectableName)
                {
                    continue;
                }

                savedSelectable = item;

                break;
            }
        }

        private void SelectSavedSelectable()
        {
            if (savedSelectableSelected == true
                || savedSelectable == null)
            {
                return;
            }

            savedSelectableSelected = true;

            savedSelectable.Select();
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