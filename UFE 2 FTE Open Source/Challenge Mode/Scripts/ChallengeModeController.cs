using FPLibrary;
using System.Collections;
using System.Collections.Generic;
using UFE3D;
using UnityEngine;

namespace UFE2FTE
{
    public class ChallengeModeController : MonoBehaviour
    {
        public delegate void ChallengeOptionsEventHandler(ChallengeOptions challengeOptions);
        public event ChallengeOptionsEventHandler OnChallengeCompleteEvent;
        public void CallOnChallengeCompleteEvent(ChallengeOptions challengeOptions)
        {
            if (OnChallengeCompleteEvent == null)
            {
                return;
            }

            OnChallengeCompleteEvent(challengeOptions);
        }

        public delegate void EventHandler(ChallengeActionOptions challengeActionOptions);
        public event EventHandler OnChallengeActionCompleteEvent;
        public void CallOnChallengeActionCompleteEvent(ChallengeActionOptions challengeActionOptions)
        {
            if (OnChallengeActionCompleteEvent == null)
            {
                return;
            }

            OnChallengeActionCompleteEvent(challengeActionOptions);
        }

        public static ChallengeModeController Instance { get; private set; }

        public bool lockInputsOnChallengeComplete = true;
        public bool lockMovementsOnChallengeComplete = true;
        public Fix64 nextChallengeDelayOnChallengeComplete = 2;

        public Color32 challengeActionCurrentColor = new Color32(0, 0, 255, 255);
        public Color32 challengeActionIncompleteColor = new Color32(255, 0, 0, 255);
        public Color32 challengeActionCompleteColor = new Color32(0, 128, 0, 255);

        public CharacterInfoReferencesScriptableObject characterInfoReferencesScriptableObject;

        [HideInInspector]
        public ControlsScript player1ControlsScript;
        [HideInInspector]
        public int currentChallengeNumber;
        [HideInInspector]
        public bool currentChallengeCompleted = false;
        [HideInInspector]
        public int currentChallengeActionNumber;
        [HideInInspector]
        public int highestChallengeActionNumber = 0;
        [HideInInspector]
        public Fix64 currentChallengeActionProgress = 0;
        [HideInInspector]
        public List<ChallengeOptions> currentChallengesList = new List<ChallengeOptions>();

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            UFE.OnScreenChanged += OnScreenChanged;

            UFE.OnButton += OnButton;
            UFE.OnBasicMove += OnBasicMove;
            UFE.OnMove += OnMove;
            UFE.OnHit += OnHit;
            UFE.OnBlock += OnBlock;
            UFE.OnParry += OnParry;
        }

        private void FixedUpdate()
        {
            player1ControlsScript = UFE.GetPlayer1ControlsScript();

            //TODO figure out a better way to handle this validation? Currently it feels pretty solid.
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true)
            {
                return;
            }

            UpdateChallengeData();

            ResetChallengeData();

            TestCurrentChallengeActionOnButtonTimer(player1ControlsScript);

            TestCurrentChallengeActionOnBasicMoveTimer(player1ControlsScript);
        }

        private void OnDisable()
        {
            UFE.OnScreenChanged -= OnScreenChanged;

            UFE.OnButton -= OnButton;
            UFE.OnBasicMove -= OnBasicMove;
            UFE.OnMove -= OnMove;
            UFE.OnHit -= OnHit;
            UFE.OnBlock -= OnBlock;
            UFE.OnParry -= OnParry;
        }

        private void OnScreenChanged(UFEScreen previousScreen, UFEScreen newScreen)
        {
            if (newScreen == UFE.GetMainMenuScreen())
            {
                UnloadChallenges();
            }
        }

        #region Classes

        [System.Serializable]
        public class ChallengeOptions
        {
            public string challengeName;
            public string challengeDescription;
            public UFE3D.CharacterInfo player1Character;
            public UFE3D.CharacterInfo player2Character;
            public string stageName;
            public OnChallengeCompleteOptions onChallengeCompleteOptions;
            public ResetChallengeOptions resetChallengeOptions;
            public AIOptions aiOptions;
            public ChallengeActionOptions[] challengeActionsArray;

            public static bool IsValidChallengeOptions(ChallengeOptions challengeOptions)
            {
                if (challengeOptions == null)
                {
                    return false;
                }

                if (challengeOptions.player1Character == null
                    || challengeOptions.player2Character == null
                    || UFE2FTE.GetStageOptions(challengeOptions.stageName) == null
                    || challengeOptions.challengeActionsArray.Length <= 0)
                {
                    return false;
                }

                return true;
            }
        }

        [System.Serializable]
        public class OnChallengeCompleteOptions
        {
            [Header("Next Challenge Delay of 0 or less will use the global delay.")]
            public Fix64 nextChallengeDelay;
            public enum EventType
            {
                ChallengeModeAfterBattleScreen,
                AutoStartNextChallenge
            }
            public EventType eventType;
        }

        [System.Serializable]
        public class ResetChallengeOptions
        {
            public bool resetIfOpponentNotStunned;
            public bool resetOnHit;
            public bool resetOnBlock;
            public bool resetOnParry;
        }

        [System.Serializable]
        public class AIOptions
        {
            public bool aiOpponent;
        }

        [System.Serializable]
        public class ChallengeActionOptions
        {
            public string challengeActionName;
            public string challengeActionDescription;
            public Fix64 successValue;
            public enum EventType
            {
                OnButton,
                OnBasicMove,
                MoveInfo
            }
            public EventType eventType;
            public ResetChallengeOptions resetChallengeActionOptions;
            public OnButtonOptions onButtonOptions;
            public OnBasicMoveOptions onBasicMoveOptions;
            public MoveInfoOptions moveInfoOptions;
            public GaugeOptions gaugeOptions;
            public InputDisplayOptions inputDisplayOptions;
            public ScriptedActionOptions.SelectionType scriptedActionOptionsSelectionType;
            [HideInInspector]
            public int scriptedActionOptionsArrayIndex;
            [HideInInspector]
            public int scriptedActionOptionsArrayIndexRandomWithExclusion;
            [HideInInspector]
            public bool applyRandomWithExclusion = false;
            public ScriptedActionOptions[] scriptedActionOptionsArray;

            public int GetNewScriptedActionOptionsArrayIndex()
            {
                switch (scriptedActionOptionsSelectionType)
                {
                    case ScriptedActionOptions.SelectionType.Descending:
                        scriptedActionOptionsArrayIndex += 1;
                        if (scriptedActionOptionsArrayIndex > scriptedActionOptionsArray.Length - 1)
                        {
                            scriptedActionOptionsArrayIndex = 0;
                        }
                        return scriptedActionOptionsArrayIndex;

                    case ScriptedActionOptions.SelectionType.Random:
                        return UnityEngine.Random.Range(0, scriptedActionOptionsArray.Length);

                    case ScriptedActionOptions.SelectionType.RandomWithExclusion:
                        return RandomWithExclusion(0, scriptedActionOptionsArray.Length);

                    default:
                        return 0;
                }
            }

            private int RandomWithExclusion(int min, int max)
            {
                int result;
                //Don't exclude if this is first run.
                if (applyRandomWithExclusion == false)
                {
                    //Generate normal random number
                    result = UnityEngine.Random.Range(min, max);
                    scriptedActionOptionsArrayIndexRandomWithExclusion = result;
                    applyRandomWithExclusion = true;
                    return result;
                }

                //Not first run, exclude last random number with -1 on the max
                result = UnityEngine.Random.Range(min, max - 1);
                //Apply +1 to the result to cancel out that -1 depending on the if statement
                result = (result < scriptedActionOptionsArrayIndexRandomWithExclusion) ? result : result + 1;
                scriptedActionOptionsArrayIndexRandomWithExclusion = result;
                return result;
            }
        }

        [System.Serializable]
        public class OnButtonOptions
        {
            public enum EventType
            {
                Press,
                Hold
            }
            public EventType eventType;
            public ButtonPress buttonPress;
        }

        [System.Serializable]
        public class OnBasicMoveOptions
        {
            public enum EventType
            {
                TimesPlayed,
                Timer
            }
            public EventType eventType;
            public BasicMoveReference basicMove;
        }

        [System.Serializable]
        public class MoveInfoOptions
        {
            public enum EventType
            {
                OnMove,
                OnHit
            }
            public EventType eventType;
            public MoveInfo moveInfo;
            public bool displayMoveInfoMoveName;
            public bool displayMoveInfoDefaultInputs;
            public bool displayMoveInfoAlternativeInputs;
        }

        [System.Serializable]
        public class GaugeOptions
        {
            [Fix64Range(0, 100)]
            public Fix64 player1GaugePercent;
            public bool useAllGauges;
            public GaugeId[] gaugeIdArray;
        }

        [System.Serializable]
        public class InputDisplayOptions
        {
            public ButtonPress[] inputDisplayButtonPressArray;
        }

        [System.Serializable]
        public class ScriptedActionOptions
        {
            public enum SelectionType
            {
                Descending,
                Random,
                RandomWithExclusion
            }

            public Fix64 executeScriptedActionDelay;
            [HideInInspector]
            public Fix64 executeScriptedActionTimer;

            [System.Serializable]
            public class ScriptedPositionOptions
            {
                public enum PositionType
                {
                    None,
                    Manual,
                    LeftCorner,
                    RightCorner
                }
                public PositionType positionType;
                public FPVector player1ManualPosition;
                public FPVector player2ManualPosition;
                public UFE2FTE.Player cornerPlayer;
                public Fix64 cornerPositionXOffset;
            }
            public ScriptedPositionOptions scriptedPositionOptions;

            [System.Serializable]
            public class ScriptedInputOptions
            {
                public Fix64 executeScriptedActionDelay;
                [HideInInspector]
                public bool executedScriptedAction;
                public UFE2FTE.Player inputPlayer;
                public Fix64 inputHoldTime;
                [HideInInspector]
                public Fix64 inputHoldTimer;
                public ButtonPress[] buttonPressArray;
            }
            public ScriptedInputOptions[] scriptedInputOptionsArray;
        }

        [System.Serializable]
        public class ChallengeModeScriptableObjectOptions
        {
            public string challengeModeScriptableObjectPath;
            public UFE3D.CharacterInfo characterInfo;
        }

        #endregion

        #region Challenge Methods

        public void StartChallenge(ChallengeOptions challengeOptions)
        {
            if (ChallengeOptions.IsValidChallengeOptions(challengeOptions) == false)
            {
                return;
            }

            UFE.gameMode = GameMode.ChallengeMode;

            currentChallengeNumber = GetChallengeNumber(challengeOptions);
            currentChallengeCompleted = false;
            currentChallengeActionNumber = 0;
            highestChallengeActionNumber = 0;
            currentChallengeActionProgress = 0;

            UFE.SetPlayer1(challengeOptions.player1Character);
            UFE.GetPlayer1Controller().isCPU = false;
            UFE.SetPlayer2(challengeOptions.player2Character);
            UFE.GetPlayer2Controller().isCPU = challengeOptions.aiOptions.aiOpponent;
            UFE.SetStage(challengeOptions.stageName);

            UFE.RestartMatch();
        }

        public void StartNextChallenge()
        {
            currentChallengeNumber += 1;

            if (currentChallengeNumber > currentChallengesList.Count - 1)
            {
                currentChallengeNumber = currentChallengesList.Count - 1;

                return;
            }

            StartChallenge(GetCurrentChallenge());
        }

        public void StartPreviousChallenge()
        {
            currentChallengeNumber -= 1;

            if (currentChallengeNumber < 0)
            {
                currentChallengeNumber = 0;

                return;
            }

            StartChallenge(GetCurrentChallenge());
        }

        public void RestartCurrentChallenge()
        {
            StartChallenge(GetCurrentChallenge());
        }

        public void CompleteCurrentChallenge()
        {
            if (currentChallengeCompleted == true)
            {
                return;
            }

            currentChallengeCompleted = true;

            CallOnChallengeCompleteEvent(GetCurrentChallenge());

            MainAlertController.CallOnMainAlertEvent(UFE2FTE.languageOptions.selectedLanguage.Success);

            UFE.config.lockMovements = lockMovementsOnChallengeComplete;
            UFE.config.lockInputs = lockInputsOnChallengeComplete;

            if (GetCurrentChallenge().onChallengeCompleteOptions.nextChallengeDelay <= 0)
            {
                UFE.DelayLocalAction(CompleteCurrentChallengeDelayed, nextChallengeDelayOnChallengeComplete);
            }
            else
            {
                UFE.DelayLocalAction(CompleteCurrentChallengeDelayed, GetCurrentChallenge().onChallengeCompleteOptions.nextChallengeDelay);
            }
        }

        private void CompleteCurrentChallengeDelayed()
        {
            if (currentChallengeNumber >= currentChallengesList.Count - 1)
            {
                currentChallengeNumber = currentChallengesList.Count - 1;

                UFE.StartChallengeModeAfterBattleScreen();
            }
            else
            {
                switch (GetCurrentChallenge().onChallengeCompleteOptions.eventType)
                {
                    case OnChallengeCompleteOptions.EventType.ChallengeModeAfterBattleScreen:
                        UFE.StartChallengeModeAfterBattleScreen();
                        break;

                    case OnChallengeCompleteOptions.EventType.AutoStartNextChallenge:
                        StartNextChallenge();
                        break;
                }
            }
        }

        #endregion

        #region Challenge Action Methods

        public void NextChallengeAction()
        {
            CallOnChallengeActionCompleteEvent(GetCurrentChallengeAction());

            currentChallengeActionNumber += 1;

            if (currentChallengeActionNumber > GetCurrentChallenge().challengeActionsArray.Length - 1)
            {
                currentChallengeActionNumber = GetCurrentChallenge().challengeActionsArray.Length - 1;

                CompleteCurrentChallenge();

                return;
            }

            currentChallengeActionProgress = 0;
        }

        public void PreviousChallengeAction()
        {
            currentChallengeActionNumber -= 1;

            if (currentChallengeActionNumber <= 0)
            {
                currentChallengeActionNumber = 0;
            }

            currentChallengeActionProgress = 0;
        }

        public void SetCurrentChallengeActionProgress(Fix64 progressValue)
        {
            currentChallengeActionProgress = progressValue;

            if (currentChallengeActionProgress >= GetCurrentChallengeAction().successValue)
            {
                NextChallengeAction();
            }
        }

        public void AddCurrentChallengeActionProgress(Fix64 progressValue)
        {
            currentChallengeActionProgress += progressValue;

            if (currentChallengeActionProgress >= GetCurrentChallengeAction().successValue)
            {
                NextChallengeAction();
            }
        }

        #endregion

        #region Challenge Helper Methods

        public List<ChallengeOptions> GetCurrentChallenges()
        {
            return currentChallengesList;
        }

        public ChallengeOptions GetCurrentChallenge()
        {
            return currentChallengesList[currentChallengeNumber];
        }

        public ChallengeActionOptions[] GetCurrentChallengeActions()
        {
            return currentChallengesList[currentChallengeNumber].challengeActionsArray;
        }

        public ChallengeActionOptions GetCurrentChallengeAction()
        {
            return currentChallengesList[currentChallengeNumber].challengeActionsArray[currentChallengeActionNumber];
        }

        public int GetChallengeNumber(ChallengeOptions challengeOptions)
        {
            if (ChallengeOptions.IsValidChallengeOptions(challengeOptions) == false)
            {
                return 0;
            }

            int count = currentChallengesList.Count;
            for (int i = 0; i < count; i++)
            {
                if (challengeOptions != currentChallengesList[i])
                {
                    continue;
                }

                return i;
            }

            return 0;
        }

        public void LoadChallenges(string path)
        {
            ChallengeModeScriptableObject challengeModeScriptableObject = Resources.Load<ChallengeModeScriptableObject>(path);
            if (challengeModeScriptableObject == null)
            {
                return;
            }

            AddChallenges(challengeModeScriptableObject.challengeOptionsArray);
        }

        public void LoadChallenges(UFE3D.CharacterInfo characterInfo)
        {
            ChallengeModeScriptableObject challengeModeScriptableObject = characterInfoReferencesScriptableObject.GetChallengeModeScriptableObject(characterInfo);
            if (challengeModeScriptableObject == null)
            {
                return;
            }

            AddChallenges(challengeModeScriptableObject.challengeOptionsArray);
        }

        private void LoadRandomChallenges()
        {
            ChallengeModeScriptableObject[] challengeModeScriptableObjectArray = Resources.FindObjectsOfTypeAll<ChallengeModeScriptableObject>();
            if (challengeModeScriptableObjectArray != null)
            {
                AddChallenges(challengeModeScriptableObjectArray[Random.Range(0, challengeModeScriptableObjectArray.Length)].challengeOptionsArray);
            }
        }

        public void UnloadChallenges()
        {
            if (currentChallengesList == null)
            {
                return;
            }

            currentChallengesList.Clear();

            Resources.UnloadUnusedAssets();

#if UNITY_EDITOR
            //Debug.Log(nameof(UnloadChallenges));
#endif
        }

        public void ValidateCurrentChallengesList()
        {
            if (currentChallengesList == null)
            {
                currentChallengesList = new List<ChallengeOptions>();
            }

            int count = currentChallengesList.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                if (ChallengeOptions.IsValidChallengeOptions(currentChallengesList[i]) == true)
                {
                    continue;
                }

                currentChallengesList.RemoveAt(i);
            }
        }

        public void AddChallenges(ChallengeOptions[] challengeOptions)
        {
            if (currentChallengesList == null)
            {
                currentChallengesList = new List<ChallengeOptions>();
            }

            currentChallengesList.Clear();

            int count = challengeOptions.Length;
            for (int i = 0; i < count; i++)
            {
                currentChallengesList.Add(challengeOptions[i]);
            }

            ValidateCurrentChallengesList();
        }

        #endregion

        #region Reset Methods

        public void ResetCurrentChallenge()
        {
            currentChallengeActionNumber = 0;
            currentChallengeActionProgress = 0;
        }

        public void ResetCurrentChallengeAction()
        {
            currentChallengeActionProgress = 0;
        }

        #endregion

        private void ValidateCurrentChallengeNumber()
        {
            if (currentChallengeNumber < 0
                || currentChallengeNumber >= currentChallengesList.Count)
            {
                currentChallengeNumber = 0;
            }
        }
        private void ValidateCurrentChallengeActionNumber()
        {
            if (currentChallengeActionNumber < 0
                || currentChallengeActionNumber >= GetCurrentChallengeActions().Length)
            {
                currentChallengeNumber = 0;
            }
        }

        #region Challenge Data Methods

        private void UpdateChallengeData()
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true)
            {
                return;
            }

            if (currentChallengeCompleted == true)
            {
                currentChallengeActionProgress = GetCurrentChallengeAction().successValue;
            }

            if (currentChallengeActionNumber > 0)
            {
                highestChallengeActionNumber = currentChallengeActionNumber;
            }

            if (GetCurrentChallengeAction().gaugeOptions.useAllGauges == true)
            {
                UFE2FTE.SetAllGaugePointsPercent(player1ControlsScript, GetCurrentChallengeAction().gaugeOptions.player1GaugePercent);
            }

            UFE2FTE.SetGaugePointsPercent(player1ControlsScript, GetCurrentChallengeAction().gaugeOptions.gaugeIdArray, GetCurrentChallengeAction().gaugeOptions.player1GaugePercent);

            int length = GetCurrentChallengeAction().scriptedActionOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (i != GetCurrentChallengeAction().scriptedActionOptionsArrayIndex)
                {
                    continue;
                }

                GetCurrentChallengeAction().scriptedActionOptionsArray[i].executeScriptedActionTimer += UFE.fixedDeltaTime;

                if (GetCurrentChallengeAction().scriptedActionOptionsArray[i].executeScriptedActionTimer >= GetCurrentChallengeAction().scriptedActionOptionsArray[i].executeScriptedActionDelay)
                {
                    switch (GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedPositionOptions.positionType)
                    {
                        case ScriptedActionOptions.ScriptedPositionOptions.PositionType.Manual:
                            UFE2FTE.ResetAllControlsScriptsData();
                            UFE2FTE.SetPlayerPosition(player1ControlsScript, GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedPositionOptions.player1ManualPosition);
                            UFE2FTE.SetPlayerPosition(UFE.GetPlayer2ControlsScript(), GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedPositionOptions.player2ManualPosition);
                            break;

                        case ScriptedActionOptions.ScriptedPositionOptions.PositionType.LeftCorner:
                            UFE2FTE.ResetAllControlsScriptsData();
                            UFE2FTE.SetAllPlayersLeftCornerPosition(GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedPositionOptions.cornerPlayer, GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedPositionOptions.cornerPositionXOffset);
                            break;

                        case ScriptedActionOptions.ScriptedPositionOptions.PositionType.RightCorner:
                            UFE2FTE.ResetAllControlsScriptsData();
                            UFE2FTE.SetAllPlayersRightCornerPosition(GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedPositionOptions.cornerPlayer, GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedPositionOptions.cornerPositionXOffset);
                            break;
                    }

                    GetCurrentChallengeAction().scriptedActionOptionsArray[i].executeScriptedActionTimer = 0;
                    GetCurrentChallengeAction().scriptedActionOptionsArrayIndex = GetCurrentChallengeAction().GetNewScriptedActionOptionsArrayIndex();

                    int length1A = GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray.Length;
                    for (int a = 0; a < length1A; a++)
                    {
                        GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].inputHoldTimer = 0;
                        GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].executedScriptedAction = false;
                    }
                }

                int lengthA = GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].executeScriptedActionDelay > GetCurrentChallengeAction().scriptedActionOptionsArray[i].executeScriptedActionTimer
                        || GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].executedScriptedAction == true)
                    {
                        continue;
                    }

                    GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].inputHoldTimer += UFE.fixedDeltaTime;

                    UFE2FTE.PressButton(UFE2FTE.GetControlsScript(GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].inputPlayer), UFE2FTE.GetUFEController(GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].inputPlayer), GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].buttonPressArray);

                    if (GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].inputHoldTimer >= GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].inputHoldTime)
                    {
                        GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].inputHoldTimer = 0;
                        GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].executedScriptedAction = true;
                    }
                }
            }
        }

        private void ResetChallengeData()
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true)
            {
                return;
            }

            if (GetCurrentChallenge().resetChallengeOptions.resetIfOpponentNotStunned == true
                && player1ControlsScript != null
                && player1ControlsScript.opControlsScript != null
                && player1ControlsScript.opControlsScript.stunTime <= 0)
            {
                ResetCurrentChallenge();
            }

            if (GetCurrentChallengeAction().resetChallengeActionOptions.resetIfOpponentNotStunned == true
                && player1ControlsScript != null
                && player1ControlsScript.opControlsScript != null
                && player1ControlsScript.opControlsScript.stunTime <= 0)
            {
                ResetCurrentChallengeAction();
            }

            int count = GetCurrentChallenges().Count;
            for (int i = 0; i < count; i++)
            {
                /*if (GetCurrentChallenges()[i] != GetCurrentChallenge())
                {

                }*/

                int lengthA = GetCurrentChallenges()[i].challengeActionsArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (GetCurrentChallenges()[i].challengeActionsArray[a] != GetCurrentChallengeAction())
                    {
                        GetCurrentChallenges()[i].challengeActionsArray[a].scriptedActionOptionsArrayIndex = 0;
                        GetCurrentChallenges()[i].challengeActionsArray[a].scriptedActionOptionsArrayIndexRandomWithExclusion = 0;
                        GetCurrentChallenges()[i].challengeActionsArray[a].applyRandomWithExclusion = false;
                    }

                    int lengthB = GetCurrentChallenges()[i].challengeActionsArray[a].scriptedActionOptionsArray.Length;

                    if (GetCurrentChallenges()[i].challengeActionsArray[a].scriptedActionOptionsArrayIndex < 0
                        || GetCurrentChallenges()[i].challengeActionsArray[a].scriptedActionOptionsArrayIndex >= lengthB)
                    {
                        GetCurrentChallenges()[i].challengeActionsArray[a].scriptedActionOptionsArrayIndex = 0;
                    }

                    for (int b = 0; b < lengthB; b++)
                    {
                        if (b != GetCurrentChallenges()[i].challengeActionsArray[a].scriptedActionOptionsArrayIndex)
                        {
                            GetCurrentChallenges()[i].challengeActionsArray[a].scriptedActionOptionsArray[b].executeScriptedActionTimer = 0;

                            int lengthC = GetCurrentChallenges()[i].challengeActionsArray[a].scriptedActionOptionsArray[b].scriptedInputOptionsArray.Length;
                            for (int c = 0; c < lengthC; c++)
                            {
                                GetCurrentChallenges()[i].challengeActionsArray[a].scriptedActionOptionsArray[b].scriptedInputOptionsArray[c].inputHoldTimer = 0;
                                GetCurrentChallenges()[i].challengeActionsArray[a].scriptedActionOptionsArray[b].scriptedInputOptionsArray[c].executedScriptedAction = false;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region On Button Methods

        private void OnButton(ButtonPress button, ControlsScript player)
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true)
            {
                return;
            }

            TestCurrentChallengeActionOnButton(player, button);
        }

        private void TestCurrentChallengeActionOnButton(ControlsScript player, ButtonPress buttonPress)
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true
                || player == null
                || player != player1ControlsScript
                || GetCurrentChallengeAction().eventType != ChallengeActionOptions.EventType.OnButton
                || GetCurrentChallengeAction().onButtonOptions.eventType != OnButtonOptions.EventType.Press
                || buttonPress != GetCurrentChallengeAction().onButtonOptions.buttonPress)
            {
                return;
            }

            AddCurrentChallengeActionProgress(1);
        }

        private void TestCurrentChallengeActionOnButtonTimer(ControlsScript player)
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true
                || player == null
                || player != player1ControlsScript
                || GetCurrentChallengeAction().eventType != ChallengeActionOptions.EventType.OnButton
                || GetCurrentChallengeAction().onButtonOptions.eventType != OnButtonOptions.EventType.Hold)
            {
                return;
            }

            foreach (ButtonPress buttonPress in player.inputHeldDown.Keys)
            {
                if (buttonPress != GetCurrentChallengeAction().onButtonOptions.buttonPress)
                {
                    continue;
                }

                SetCurrentChallengeActionProgress(player.inputHeldDown[buttonPress]);
            }
        }

        #endregion

        #region On Basic Move Methods

        private void OnBasicMove(BasicMoveReference basicMove, ControlsScript player)
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true)
            {
                return;
            }

            TestCurrentChallengeActionOnBasicMove(player, basicMove);
        }

        private void TestCurrentChallengeActionOnBasicMove(ControlsScript player, BasicMoveReference basicMove)
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true
                || player == null
                || player != player1ControlsScript
                || GetCurrentChallengeAction().eventType != ChallengeActionOptions.EventType.OnBasicMove
                || GetCurrentChallengeAction().onBasicMoveOptions.eventType != OnBasicMoveOptions.EventType.TimesPlayed
                || basicMove != GetCurrentChallengeAction().onBasicMoveOptions.basicMove)
            {
                return;
            }

            AddCurrentChallengeActionProgress(1);
        }

        private void TestCurrentChallengeActionOnBasicMoveTimer(ControlsScript player)
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true
                || player == null
                || player != player1ControlsScript
                || GetCurrentChallengeAction().eventType != ChallengeActionOptions.EventType.OnBasicMove
                || GetCurrentChallengeAction().onBasicMoveOptions.eventType != OnBasicMoveOptions.EventType.Timer)
            {
                return;
            }

            if (player.currentBasicMove == currentChallengesList[currentChallengeNumber].challengeActionsArray[currentChallengeActionNumber].onBasicMoveOptions.basicMove)
            {
                AddCurrentChallengeActionProgress(UFE.fixedDeltaTime);
            }
            else
            {
                currentChallengeActionProgress = 0;
            }
        }

        #endregion

        #region On Move Methods

        private void OnMove(MoveInfo move, ControlsScript player)
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true)
            {
                return;
            }

            TestCurrentChallengeActionOnMove(player, move);
        }

        private void TestCurrentChallengeActionOnMove(ControlsScript player, MoveInfo moveInfo)
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true
                || player == null
                || player != player1ControlsScript
                || moveInfo == null
                || GetCurrentChallengeAction().eventType != ChallengeActionOptions.EventType.MoveInfo
                || GetCurrentChallengeAction().moveInfoOptions.eventType != MoveInfoOptions.EventType.OnMove
                || moveInfo.moveName != GetCurrentChallengeAction().moveInfoOptions.moveInfo.moveName)
            {
                return;
            }

            AddCurrentChallengeActionProgress(1);
        }

        #endregion

        #region On Hit Methods

        private void OnHit(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            TestCurrentChallengeOnHit(player, move);
        }

        private void TestCurrentChallengeOnHit(ControlsScript player, MoveInfo moveInfo)
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true)
            {
                return;
            }

            if (GetCurrentChallengeAction().eventType == ChallengeActionOptions.EventType.MoveInfo
                && GetCurrentChallengeAction().moveInfoOptions.eventType == MoveInfoOptions.EventType.OnHit
                && player != null
                && player == player1ControlsScript
                && moveInfo != null
                && moveInfo.moveName == GetCurrentChallengeAction().moveInfoOptions.moveInfo.moveName)
            {
                AddCurrentChallengeActionProgress(1);
            }

            if (GetCurrentChallenge().resetChallengeOptions.resetOnHit == true)
            {
                ResetCurrentChallenge();
            }

            if (GetCurrentChallengeAction().resetChallengeActionOptions.resetOnHit == true)
            {
                ResetCurrentChallengeAction();
            }
        }

        #endregion

        #region On Block Methods

        private void OnBlock(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            TestCurrentChallengeOnBlock();
        }

        private void TestCurrentChallengeOnBlock()
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true)
            {
                return;
            }

            if (GetCurrentChallenge().resetChallengeOptions.resetOnBlock == true)
            {
                ResetCurrentChallenge();
            }

            if (GetCurrentChallengeAction().resetChallengeActionOptions.resetOnBlock == true)
            {
                ResetCurrentChallengeAction();
            }
        }

        #endregion

        #region On Parry Methods

        private void OnParry(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            TestCurrentChallengeOnParry();
        }

        private void TestCurrentChallengeOnParry()
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true)
            {
                return;
            }

            if (GetCurrentChallenge().resetChallengeOptions.resetOnParry == true)
            {
                ResetCurrentChallenge();
            }

            if (GetCurrentChallengeAction().resetChallengeActionOptions.resetOnParry == true)
            {
                ResetCurrentChallengeAction();
            }
        }

        #endregion
    }
}