using FPLibrary;
using System.Collections.Generic;
using UFE3D;
using UnityEngine;

namespace FreedTerror.UFE2
{
    public class ChallengeModeController : MonoBehaviour
    {
        public static ChallengeModeController instance;

        public Fix64 nextChallengeDelay = 2;

        public Color32 challengeActionCurrentColor = new Color32(0, 0, 255, 255);
        public Color32 challengeActionIncompleteColor = new Color32(255, 0, 0, 255);
        public Color32 challengeActionCompleteColor = new Color32(0, 128, 0, 255);

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
        public List<ChallengeOptions> currentChallengeOptionsList = new List<ChallengeOptions>();

        private void Awake()
        {
            if (instance != null 
                && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
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
            UpdateChallengeData();

            UpdateCurrentChallengeActionOnButtonTimer(UFE.p1ControlsScript);

            UpdateCurrentChallengeActionOnBasicMoveTimer(UFE.p1ControlsScript);
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
            public UFE3D.CharacterInfo player1CharacterInfo;
            public UFE3D.CharacterInfo player2CharacterInfo;
            public string stageName;
      
            public enum OnChallengeSuccessAction
            {
                ChallengeModeAfterBattleScreen,
                AutoStartNextChallenge
            }
            public OnChallengeSuccessAction onChallengeSuccessAction;
            [Tooltip("Next Challenge Delay of 0 or less will use the global delay.")]
            public Fix64 nextChallengeDelay;

            public AIOptions aiOptions;
            public ResetOptions resetOptions;
            public GaugeOptions[] player1GaugeOptionsArray;
            public GaugeOptions[] player2GaugeOptionsArray;  
            public ChallengeActionOptions[] challengeActionOptionsArray;

            public static bool IsValidChallengeOptions(ChallengeOptions challengeOptions)
            {
                if (challengeOptions == null)
                {
                    return false;
                }

                if (challengeOptions.player1CharacterInfo == null
                    || challengeOptions.player2CharacterInfo == null
                    || UFE2Manager.GetStageOptions(challengeOptions.stageName) == null
                    || challengeOptions.challengeActionOptionsArray.Length <= 0)
                {
                    return false;
                }

                return true;
            }
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
            public OnButtonOptions onButtonOptions;
            public OnBasicMoveOptions onBasicMoveOptions;
            public MoveInfoOptions moveInfoOptions;
            public ResetOptions resetOptions;
            public GaugeOptions[] player1GaugeOptionsArray;
            public GaugeOptions[] player2GaugeOptionsArray;
            public ButtonPress[] inputDisplayButtonPressArray;
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
        public class AIOptions
        {
            public bool aiOpponent;
        }

        [System.Serializable]
        public class GaugeOptions
        {
            [Fix64Range(0, 100)]
            public Fix64 gaugePercent;
            public bool useAllGauges;
            public GaugeId[] gaugeIdArray;
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
        public class ResetOptions
        {
            public bool resetIfOpponentNotStunned;
            public bool resetOnHit;
            public bool resetOnBlock;
            public bool resetOnParry;
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
                public UFE2Manager.Player cornerPlayer;
                public Fix64 cornerPositionXOffset;
            }
            public ScriptedPositionOptions scriptedPositionOptions;

            [System.Serializable]
            public class ScriptedInputOptions
            {
                public Fix64 executeScriptedActionDelay;
                [HideInInspector]
                public bool executedScriptedAction;
                public UFE2Manager.Player inputPlayer;
                public Fix64 inputHoldTime;
                [HideInInspector]
                public Fix64 inputHoldTimer;
                public ButtonPress[] buttonPressArray;
            }
            public ScriptedInputOptions[] scriptedInputOptionsArray;
        }

        #endregion

        #region Events

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

            UFE.SetPlayer1(challengeOptions.player1CharacterInfo);
            UFE.GetPlayer1Controller().isCPU = false;
            UFE.SetPlayer2(challengeOptions.player2CharacterInfo);
            UFE.GetPlayer2Controller().isCPU = challengeOptions.aiOptions.aiOpponent;
            UFE.SetStage(challengeOptions.stageName);

            UFE.RestartMatch();
        }

        [NaughtyAttributes.Button()]
        public void StartNextChallenge()
        {
            currentChallengeNumber += 1;

            if (currentChallengeNumber > currentChallengeOptionsList.Count - 1)
            {
                currentChallengeNumber = currentChallengeOptionsList.Count - 1;

                return;
            }

            StartChallenge(GetCurrentChallenge());
        }

        [NaughtyAttributes.Button()]
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

        [NaughtyAttributes.Button()]
        public void CompleteCurrentChallenge()
        {
            if (currentChallengeCompleted == true)
            {
                return;
            }

            currentChallengeCompleted = true;

            UFE.config.lockMovements = true;
            UFE.config.lockInputs = true;

            CallOnChallengeCompleteEvent(GetCurrentChallenge());

            MainAlertController.CallOnMainAlertEvent("Success");

            UFE.DelayLocalAction(() =>
            {
                if (currentChallengeNumber >= currentChallengeOptionsList.Count - 1)
                {
                    currentChallengeNumber = currentChallengeOptionsList.Count - 1;

                    UFE.StartChallengeModeAfterBattleScreen();
                }
                else
                {
                    switch (GetCurrentChallenge().onChallengeSuccessAction)
                    {
                        case ChallengeOptions.OnChallengeSuccessAction.ChallengeModeAfterBattleScreen:
                            UFE.StartChallengeModeAfterBattleScreen();
                            break;

                        case ChallengeOptions.OnChallengeSuccessAction.AutoStartNextChallenge:
                            StartNextChallenge();
                            break;
                    }
                }
            },
            GetNextChallengeDelay());
        }

        [NaughtyAttributes.Button()]
        public void ResetCurrentChallenge()
        {
            currentChallengeActionNumber = 0;
            currentChallengeActionProgress = 0;
        }

        [NaughtyAttributes.Button()]
        public void RestartCurrentChallenge()
        {
            StartChallenge(GetCurrentChallenge());
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
            if (UFE2Manager.instance.characterInfoReferencesScriptableObject == null)
            {
                return;
            }

            ChallengeModeScriptableObject challengeModeScriptableObject = UFE2Manager.instance.characterInfoReferencesScriptableObject.GetChallengeModeScriptableObject(characterInfo);
            if (challengeModeScriptableObject == null)
            {
                return;
            }

            AddChallenges(challengeModeScriptableObject.challengeOptionsArray);
        }

        public void UnloadChallenges()
        {
            currentChallengeOptionsList.Clear();

            Resources.UnloadUnusedAssets();

#if UNITY_EDITOR
            Debug.Log(" Unloaded Challenges");
#endif
        }

        public void AddChallenges(ChallengeOptions[] challengeOptions)
        {
            currentChallengeOptionsList.Clear();

            int count = challengeOptions.Length;
            for (int i = 0; i < count; i++)
            {
                currentChallengeOptionsList.Add(challengeOptions[i]);
            }

            ValidateCurrentChallengesList();
        }

        public void ValidateCurrentChallengesList()
        {
            int count = currentChallengeOptionsList.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                if (ChallengeOptions.IsValidChallengeOptions(currentChallengeOptionsList[i]) == true)
                {
                    continue;
                }

                currentChallengeOptionsList.RemoveAt(i);
            }
        }

        public List<ChallengeOptions> GetCurrentChallenges()
        {
            return currentChallengeOptionsList;
        }

        public ChallengeOptions GetCurrentChallenge()
        {
            return currentChallengeOptionsList[currentChallengeNumber];
        }

        public int GetChallengeNumber(ChallengeOptions challengeOptions)
        {
            if (ChallengeOptions.IsValidChallengeOptions(challengeOptions) == false)
            {
                return 0;
            }

            int count = currentChallengeOptionsList.Count;
            for (int i = 0; i < count; i++)
            {
                if (challengeOptions != currentChallengeOptionsList[i])
                {
                    continue;
                }

                return i;
            }

            return 0;
        }

        public Fix64 GetNextChallengeDelay()
        {
            if (GetCurrentChallenge().nextChallengeDelay > 0)
            {
                return GetCurrentChallenge().nextChallengeDelay;
            }

            return nextChallengeDelay;
        }

        #endregion

        #region Challenge Action Methods

        [NaughtyAttributes.Button]
        public void NextChallengeAction()
        {
            CallOnChallengeActionCompleteEvent(GetCurrentChallengeAction());

            currentChallengeActionNumber += 1;

            if (currentChallengeActionNumber >= GetCurrentChallenge().challengeActionOptionsArray.Length)
            {
                currentChallengeActionNumber = GetCurrentChallenge().challengeActionOptionsArray.Length - 1;

                CompleteCurrentChallenge();

                return;
            }

            currentChallengeActionProgress = 0;
        }

        [NaughtyAttributes.Button]
        public void PreviousChallengeAction()
        {
            currentChallengeActionNumber -= 1;

            if (currentChallengeActionNumber <= 0)
            {
                currentChallengeActionNumber = 0;
            }

            currentChallengeActionProgress = 0;
        }

        [NaughtyAttributes.Button]
        public void ResetCurrentChallengeAction()
        {
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

        public ChallengeActionOptions[] GetCurrentChallengeActions()
        {
            return currentChallengeOptionsList[currentChallengeNumber].challengeActionOptionsArray;
        }

        public ChallengeActionOptions GetCurrentChallengeAction()
        {
            return currentChallengeOptionsList[currentChallengeNumber].challengeActionOptionsArray[currentChallengeActionNumber];
        }

        #endregion

        #region Challenge Data Methods

        private void UpdateChallengeData()
        {
            if (GetCurrentChallenges().Count <= 0)
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

            int length = GetCurrentChallenge().player1GaugeOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (GetCurrentChallenge().player1GaugeOptionsArray[i].useAllGauges == true)
                {
                    UFE2Manager.SetAllGaugePointsPercent(UFE.p1ControlsScript, GetCurrentChallenge().player1GaugeOptionsArray[i].gaugePercent);

                    continue;
                }

                UFE2Manager.SetGaugePointsPercent(UFE.p1ControlsScript, GetCurrentChallenge().player1GaugeOptionsArray[i].gaugeIdArray, GetCurrentChallenge().player1GaugeOptionsArray[i].gaugePercent);
            }

            length = GetCurrentChallenge().player2GaugeOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (GetCurrentChallenge().player2GaugeOptionsArray[i].useAllGauges == true)
                {
                    UFE2Manager.SetAllGaugePointsPercent(UFE.p2ControlsScript, GetCurrentChallenge().player2GaugeOptionsArray[i].gaugePercent);

                    continue;
                }

                UFE2Manager.SetGaugePointsPercent(UFE.p2ControlsScript, GetCurrentChallenge().player2GaugeOptionsArray[i].gaugeIdArray, GetCurrentChallenge().player2GaugeOptionsArray[i].gaugePercent);
            }

            length = GetCurrentChallengeAction().player1GaugeOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (GetCurrentChallengeAction().player1GaugeOptionsArray[i].useAllGauges == true)
                {
                    UFE2Manager.SetAllGaugePointsPercent(UFE.p1ControlsScript, GetCurrentChallengeAction().player1GaugeOptionsArray[i].gaugePercent);

                    continue;
                }

                UFE2Manager.SetGaugePointsPercent(UFE.p1ControlsScript, GetCurrentChallengeAction().player1GaugeOptionsArray[i].gaugeIdArray, GetCurrentChallengeAction().player1GaugeOptionsArray[i].gaugePercent);
            }

            length = GetCurrentChallengeAction().player2GaugeOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (GetCurrentChallengeAction().player2GaugeOptionsArray[i].useAllGauges == true)
                {
                    UFE2Manager.SetAllGaugePointsPercent(UFE.p1ControlsScript, GetCurrentChallengeAction().player2GaugeOptionsArray[i].gaugePercent);

                    continue;
                }

                UFE2Manager.SetGaugePointsPercent(UFE.p1ControlsScript, GetCurrentChallengeAction().player2GaugeOptionsArray[i].gaugeIdArray, GetCurrentChallengeAction().player2GaugeOptionsArray[i].gaugePercent);
            }

            length = GetCurrentChallengeAction().scriptedActionOptionsArray.Length;
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
                            UFE2Manager.ResetAllControlsScriptsData();
                            UFE2Manager.SetPlayerPosition(UFE.p1ControlsScript, GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedPositionOptions.player1ManualPosition);
                            UFE2Manager.SetPlayerPosition(UFE.p2ControlsScript, GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedPositionOptions.player2ManualPosition);
                            break;

                        case ScriptedActionOptions.ScriptedPositionOptions.PositionType.LeftCorner:
                            UFE2Manager.ResetAllControlsScriptsData();
                            UFE2Manager.SetAllPlayersLeftCornerPosition(GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedPositionOptions.cornerPlayer, GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedPositionOptions.cornerPositionXOffset);
                            break;

                        case ScriptedActionOptions.ScriptedPositionOptions.PositionType.RightCorner:
                            UFE2Manager.ResetAllControlsScriptsData();
                            UFE2Manager.SetAllPlayersRightCornerPosition(GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedPositionOptions.cornerPlayer, GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedPositionOptions.cornerPositionXOffset);
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

                    UFE2Manager.PressButton(UFE2Manager.GetControlsScript(GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].inputPlayer), UFE2Manager.GetUFEController(GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].inputPlayer), GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].buttonPressArray);

                    if (GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].inputHoldTimer >= GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].inputHoldTime)
                    {
                        GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].inputHoldTimer = 0;
                        GetCurrentChallengeAction().scriptedActionOptionsArray[i].scriptedInputOptionsArray[a].executedScriptedAction = true;
                    }
                }
            }

            //ResetChallengeData
            if (GetCurrentChallenge().resetOptions.resetIfOpponentNotStunned == true
                && UFE.p1ControlsScript != null
                && UFE.p1ControlsScript.opControlsScript != null
                && UFE.p1ControlsScript.opControlsScript.stunTime <= 0)
            {
                ResetCurrentChallenge();
            }

            if (GetCurrentChallengeAction().resetOptions.resetIfOpponentNotStunned == true
                && UFE.p1ControlsScript != null
                && UFE.p1ControlsScript.opControlsScript != null
                && UFE.p1ControlsScript.opControlsScript.stunTime <= 0)
            {
                ResetCurrentChallengeAction();
            }

            length = GetCurrentChallenges().Count;
            for (int i = 0; i < length; i++)
            {
                /*if (GetCurrentChallenges()[i] != GetCurrentChallenge())
                {

                }*/

                int lengthA = GetCurrentChallenges()[i].challengeActionOptionsArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (GetCurrentChallenges()[i].challengeActionOptionsArray[a] != GetCurrentChallengeAction())
                    {
                        GetCurrentChallenges()[i].challengeActionOptionsArray[a].scriptedActionOptionsArrayIndex = 0;
                        GetCurrentChallenges()[i].challengeActionOptionsArray[a].scriptedActionOptionsArrayIndexRandomWithExclusion = 0;
                        GetCurrentChallenges()[i].challengeActionOptionsArray[a].applyRandomWithExclusion = false;
                    }

                    int lengthB = GetCurrentChallenges()[i].challengeActionOptionsArray[a].scriptedActionOptionsArray.Length;

                    if (GetCurrentChallenges()[i].challengeActionOptionsArray[a].scriptedActionOptionsArrayIndex < 0
                        || GetCurrentChallenges()[i].challengeActionOptionsArray[a].scriptedActionOptionsArrayIndex >= lengthB)
                    {
                        GetCurrentChallenges()[i].challengeActionOptionsArray[a].scriptedActionOptionsArrayIndex = 0;
                    }

                    for (int b = 0; b < lengthB; b++)
                    {
                        if (b != GetCurrentChallenges()[i].challengeActionOptionsArray[a].scriptedActionOptionsArrayIndex)
                        {
                            GetCurrentChallenges()[i].challengeActionOptionsArray[a].scriptedActionOptionsArray[b].executeScriptedActionTimer = 0;

                            int lengthC = GetCurrentChallenges()[i].challengeActionOptionsArray[a].scriptedActionOptionsArray[b].scriptedInputOptionsArray.Length;
                            for (int c = 0; c < lengthC; c++)
                            {
                                GetCurrentChallenges()[i].challengeActionOptionsArray[a].scriptedActionOptionsArray[b].scriptedInputOptionsArray[c].inputHoldTimer = 0;
                                GetCurrentChallenges()[i].challengeActionOptionsArray[a].scriptedActionOptionsArray[b].scriptedInputOptionsArray[c].executedScriptedAction = false;
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
            TestCurrentChallengeActionOnButton(player, button);
        }

        private void TestCurrentChallengeActionOnButton(ControlsScript player, ButtonPress buttonPress)
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true
                || player == null
                || GetCurrentChallengeAction().eventType != ChallengeActionOptions.EventType.OnButton
                || GetCurrentChallengeAction().onButtonOptions.eventType != OnButtonOptions.EventType.Press
                || buttonPress != GetCurrentChallengeAction().onButtonOptions.buttonPress)
            {
                return;
            }

            AddCurrentChallengeActionProgress(1);
        }

        private void UpdateCurrentChallengeActionOnButtonTimer(ControlsScript player)
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true
                || player == null
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
            TestCurrentChallengeActionOnBasicMove(player, basicMove);
        }

        private void TestCurrentChallengeActionOnBasicMove(ControlsScript player, BasicMoveReference basicMove)
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true
                || player == null
                || GetCurrentChallengeAction().eventType != ChallengeActionOptions.EventType.OnBasicMove
                || GetCurrentChallengeAction().onBasicMoveOptions.eventType != OnBasicMoveOptions.EventType.TimesPlayed
                || basicMove != GetCurrentChallengeAction().onBasicMoveOptions.basicMove)
            {
                return;
            }

            AddCurrentChallengeActionProgress(1);
        }

        private void UpdateCurrentChallengeActionOnBasicMoveTimer(ControlsScript player)
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true
                || player == null
                || GetCurrentChallengeAction().eventType != ChallengeActionOptions.EventType.OnBasicMove
                || GetCurrentChallengeAction().onBasicMoveOptions.eventType != OnBasicMoveOptions.EventType.Timer)
            {
                return;
            }

            if (player.currentBasicMoveReference == currentChallengeOptionsList[currentChallengeNumber].challengeActionOptionsArray[currentChallengeActionNumber].onBasicMoveOptions.basicMove)
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
            TestCurrentChallengeActionOnMove(player, move);
        }

        private void TestCurrentChallengeActionOnMove(ControlsScript player, MoveInfo moveInfo)
        {
            if (GetCurrentChallenges().Count <= 0
                || currentChallengeCompleted == true
                || player == null
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
                && moveInfo != null
                && moveInfo.moveName == GetCurrentChallengeAction().moveInfoOptions.moveInfo.moveName)
            {
                AddCurrentChallengeActionProgress(1);
            }

            if (GetCurrentChallenge().resetOptions.resetOnHit == true)
            {
                ResetCurrentChallenge();
            }

            if (GetCurrentChallengeAction().resetOptions.resetOnHit == true)
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

            if (GetCurrentChallenge().resetOptions.resetOnBlock == true)
            {
                ResetCurrentChallenge();
            }

            if (GetCurrentChallengeAction().resetOptions.resetOnBlock == true)
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

            if (GetCurrentChallenge().resetOptions.resetOnParry == true)
            {
                ResetCurrentChallenge();
            }

            if (GetCurrentChallengeAction().resetOptions.resetOnParry == true)
            {
                ResetCurrentChallengeAction();
            }
        }

        #endregion
    }
}