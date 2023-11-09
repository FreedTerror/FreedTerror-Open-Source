using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UFE3D;
using FPLibrary;

namespace UFE2FTE
{
    public class UFE2FTE : MonoBehaviour
    {
        #region Enum Variables

        public enum AIMode
        {
            Human,
            Stand,
            Crouch,
            MoveForward,
            MoveBackward,
            BlockAll,
            StandBlock,
            CrouchBlock,
            ParryAll,
            StandParry,
            CrouchParry,
            JumpParry,
            NeutralJump,
            ForwardJump,
            BackwardJump
        }

        public enum HitBoxDisplayMode
        {
            Off,
            SpriteRenderer2DInfront,
            SpriteRenderer2DBehind,
            MeshRenderer3D,
            PopcronGizmos2D,
            PopcronGizmos3D
        }

        public enum Player
        {
            Player1,
            Player2,
        }

        public enum Toggle
        {
            Off,
            On
        }

        #endregion

        private static UFE2FTE instance = null;
        public static UFE2FTE Instance
        {
            get
            {
                return instance;
            }
        }

        public static UFEScreen currentScreen = null;
        private AIController player2AIController;

        public FrameDelayScriptableObject defaultFrameDelayScriptableObject;

        public GameSettingsScriptableObject defaultGameSettingsScriptableObject;
        public ParryType[] allowedParryTypeArray;

        public InputDisplayScriptableObject inputDisplayScriptableObject;

        public AIMode aiMode = AIMode.Human;
        public Toggle aiThrowTechMode = Toggle.Off;

        public bool useCameraShake;
        public bool useCharacterPortraitShake;

        public bool displayBattleGUI;
        public bool displayFrameAdvantage;
        public bool displayFrameDelay;
        public bool displayHitData;
        public bool displayInputs;
        public bool displayMoveData;
        public bool displayPing;

        public HitBoxDisplayMode hitBoxDisplayMode = HitBoxDisplayMode.Off;
        [Range(0, 255)]
        public int hitBoxDisplayAlphaValue = 128;

        public Player pausedPlayer = Player.Player1;

        public Player trainingModeCornerTarget = Player.Player2;
        public Fix64 trainingModeCornerPositionXOffset = 3;

        [SerializeField]
        private LanguageOptions _languageOptions;
        public static LanguageOptions languageOptions { get { return Instance._languageOptions; } }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }

        private void Start()
        {         
            defaultFrameDelayScriptableObject.UpdateFrameDelaySettings();
            defaultGameSettingsScriptableObject.UpdateGameSettings();
            languageOptions.Initialize();
        }

        private void Update()
        {
            switch (UFE.gameMode)
            {
                case GameMode.NetworkGame:
                    defaultFrameDelayScriptableObject.UpdateFrameDelaySettings();
                    defaultGameSettingsScriptableObject.UpdateGameSettings();
                    break;
            }

            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                aiMode = AIMode.Human;
                aiThrowTechMode = Toggle.Off;
            }

            if (player2AIController == null
                && UFE.GetPlayer2ControlsScript() != null)
            {
                player2AIController = AddComponentToControlsScriptCharacterGameObject<AIController>(UFE.GetPlayer2ControlsScript());
            }
        }

        #region Bool Methods

        public static bool IsInMinMaxRange(float comparing, float min, float max)
        {
            return comparing >= min && comparing <= max;
        }

        /// <summary>
        /// 1 returns true. Any other number returns false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool GetBoolFromInt(int value)
        {
            if (value == 1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// True returns 1. False returns 0.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetIntFromBool(bool value)
        {
            if (value == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// True returns On. False returns Off.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringFromBool(bool value)
        {
            if (value == true)
            {
                return languageOptions.selectedLanguage.On;
            }
            else
            {
                return languageOptions.selectedLanguage.Off;
            }
        }

        /// <summary>
        /// True returns false. False returns true.
        /// </summary>
        /// <param name="value"></param>
        public static bool ToggleBool(bool value)
        {
            if (value == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void SaveBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, GetIntFromBool(value));
        }

        public static void LoadBool(ref bool value, string key)
        {
            value = GetBoolFromInt(PlayerPrefs.GetInt(key));
        }

        #endregion

        #region Enum Methods

        public static AIMode GetNextEnum(AIMode value)
        {
            int index = (int)value;
            index++;
            if (System.Enum.IsDefined(typeof(AIMode), (AIMode)index) == false)
            {
                index = 0;
            }
            return (AIMode)index;
        }

        public static AIMode GetPreviousEnum(AIMode value)
        {
            int index = (int)value;
            index--;
            if (System.Enum.IsDefined(typeof(AIMode), (AIMode)index) == false)
            {
                index = System.Enum.GetValues(typeof(AIMode)).Length - 1;
            }
            return (AIMode)index;
        }

        public string GetStringFromEnum(AIMode aiMode)
        {
            switch (aiMode)
            {
                case AIMode.Human:
                    return languageOptions.selectedLanguage.Human;

                case AIMode.Stand:
                    return languageOptions.selectedLanguage.Stand;

                case AIMode.Crouch:
                    return languageOptions.selectedLanguage.Crouch;

                case AIMode.MoveForward:
                    return languageOptions.selectedLanguage.MoveForward;

                case AIMode.MoveBackward:
                    return languageOptions.selectedLanguage.MoveBackward;

                case AIMode.BlockAll:
                    return languageOptions.selectedLanguage.BlockAll;

                case AIMode.StandBlock:
                    return languageOptions.selectedLanguage.BlockStand;

                case AIMode.CrouchBlock:
                    return languageOptions.selectedLanguage.BlockCrouch;

                case AIMode.ParryAll:
                    return languageOptions.selectedLanguage.ParryAll;

                case AIMode.StandParry:
                    return languageOptions.selectedLanguage.ParryStand;

                case AIMode.CrouchParry:
                    return languageOptions.selectedLanguage.ParryCrouch;

                case AIMode.JumpParry:
                    return languageOptions.selectedLanguage.ParryJump;

                case AIMode.NeutralJump:
                    return languageOptions.selectedLanguage.JumpNeutral;

                case AIMode.ForwardJump:
                    return languageOptions.selectedLanguage.JumpForward;

                case AIMode.BackwardJump:
                    return languageOptions.selectedLanguage.JumpBackward;

                default:
                    return languageOptions.selectedLanguage.Human;
            }
        }

        public static Player GetNextEnum(Player value)
        {
            int index = (int)value;
            index++;
            if (System.Enum.IsDefined(typeof(Player), (Player)index) == false)
            {
                index = 0;
            }
            return (Player)index;
        }

        public static Player GetPreviousEnum(Player value)
        {
            int index = (int)value;
            index--;
            if (System.Enum.IsDefined(typeof(Player), (Player)index) == false)
            {
                index = System.Enum.GetValues(typeof(Player)).Length - 1;
            }
            return (Player)index;
        }

        public static string GetStringFromEnum(Player value)
        {
            switch (value)
            {
                case Player.Player1:
                    return languageOptions.selectedLanguage.Player1;

                case Player.Player2:
                    return languageOptions.selectedLanguage.Player2;

                default:
                    return "";
            }
        }

        public static Toggle GetNextEnum(Toggle value)
        {
            int index = (int)value;
            index++;
            if (System.Enum.IsDefined(typeof(Toggle), (Toggle)index) == false)
            {
                index = 0;
            }
            return (Toggle)index;
        }

        public static Toggle GetPreviousEnum(Toggle value)
        {
            int index = (int)value;
            index--;
            if (System.Enum.IsDefined(typeof(Toggle), (Toggle)index) == false)
            {
                index = System.Enum.GetValues(typeof(Toggle)).Length - 1;
            }
            return (Toggle)index;
        }

        public static string GetStringFromEnum(Toggle value)
        {
            if (value == Toggle.On)
            {
                return languageOptions.selectedLanguage.On;
            }
            else
            {
                return languageOptions.selectedLanguage.Off;
            }
        }

        public static BlockType GetNextEnum(BlockType value, BlockType[] allowedValue)
        {
            int index = 0;
            if (allowedValue == null)
            {
                return (BlockType)index;
            }
            int length = allowedValue.Length;
            for (int i = 0; i < length; i++)
            {
                if (value != allowedValue[i])
                {
                    continue;
                }

                index = i;
                index++;
                if (index >= length)
                {
                    index = 0;
                }
            }

            return allowedValue[index];
        }

        public static BlockType GetPreviousEnum(BlockType value, BlockType[] allowedValue)
        {
            int index = 0;
            if (allowedValue == null)
            {
                return (BlockType)index;
            }
            int length = allowedValue.Length;
            for (int i = 0; i < length; i++)
            {
                if (value != allowedValue[i])
                {
                    continue;
                }

                index = i;
                index--;
                if (index < 0)
                {
                    index = length - 1;
                }
            }

            return allowedValue[index];
        }

        public static ParryType GetNextEnum(ParryType value, ParryType[] allowedValue)
        {
            int index = 0;
            if (allowedValue == null)
            {
                return (ParryType)index;
            }
            int length = allowedValue.Length;
            for (int i = 0; i < length; i++)
            {
                if (value != allowedValue[i])
                {
                    continue;
                }

                index = i;
                index++;
                if (index >= length)
                {
                    index = 0;
                }
            }

            return allowedValue[index];
        }

        public static ParryType GetPreviousEnum(ParryType value, ParryType[] allowedValue)
        {
            int index = 0;
            if (allowedValue == null)
            {
                return (ParryType)index;
            }
            int length = allowedValue.Length;
            for (int i = 0; i < length; i++)
            {
                if (value != allowedValue[i])
                {
                    continue;
                }

                index = i;
                index--;
                if (index < 0)
                {
                    index = length - 1;
                }
            }

            return allowedValue[index];
        }

        public HitBoxDisplayMode GetNextEnum(HitBoxDisplayMode value)
        {
            int index = (int)value;
            index++;
            if (System.Enum.IsDefined(typeof(HitBoxDisplayMode), (HitBoxDisplayMode)index) == false)
            {
                index = 0;
            }
            return (HitBoxDisplayMode)index;
        }

        public HitBoxDisplayMode GetPreviousEnum(HitBoxDisplayMode value)
        {
            int index = (int)value;
            index--;
            if (System.Enum.IsDefined(typeof(HitBoxDisplayMode), (HitBoxDisplayMode)index) == false)
            {
                index = System.Enum.GetValues(typeof(HitBoxDisplayMode)).Length - 1;
            }
            return (HitBoxDisplayMode)index;
        }

        public string GetStringFromEnum(HitBoxDisplayMode hitBoxDisplayMode)
        {
            switch (hitBoxDisplayMode)
            {
                case HitBoxDisplayMode.Off:
                    return GetStringFromBool(false);

                case HitBoxDisplayMode.SpriteRenderer2DInfront:
                    return languageOptions.selectedLanguage.HitBoxDisplaySpriteRenderer2DInfront;

                case HitBoxDisplayMode.SpriteRenderer2DBehind:
                    return languageOptions.selectedLanguage.HitBoxDisplaySpriteRenderer2DBehind;

                case HitBoxDisplayMode.MeshRenderer3D:
                    return languageOptions.selectedLanguage.HitBoxDisplayMeshRenderer3D;

                case HitBoxDisplayMode.PopcronGizmos2D:
                    return languageOptions.selectedLanguage.HitBoxDisplayPopcronGizmos2D;

                case HitBoxDisplayMode.PopcronGizmos3D:
                    return languageOptions.selectedLanguage.HitBoxDisplayPopcronGizmos3D;

                default:
                    return "";
            }
        }

        #endregion

        #region Int Methods

        public static void SetInt(ref int value, int setValue, bool saveInt = true)
        {
            value = setValue;

            if (saveInt == true)
            {
                SaveInt(nameof(value), setValue);
            }
        }

        public static void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public static void LoadInt(ref int value, string key)
        {
            value = PlayerPrefs.GetInt(key);
        }

        #endregion

        #region Audio Methods

        public static float GetRandomVolumeUFE(float minVolume, float maxVolume)
        {
            if (maxVolume > UFE.GetSoundFXVolume())
            {
                float volumeDifference = minVolume - maxVolume;

                minVolume = UFE.GetSoundFXVolume() + volumeDifference;

                maxVolume = UFE.GetSoundFXVolume();
            }

            return UnityEngine.Random.Range(minVolume, maxVolume);
        }

        public static float GetRandomPitch(float minPitch, float maxPitch)
        {
            return UnityEngine.Random.Range(minPitch, maxPitch);
        }

        public static IEnumerator AudioSourceFadeCoroutine(AudioSource audioSource, float duration, float targetVolume)
        {
            //StartCoroutine(UFE2FTE.AudioSourceFadeCoroutine(AudioSource audioSource, float duration, float targetVolume));

            if (audioSource != null)
            {
                float currentTime = 0;

                float start = audioSource.volume;

                while (currentTime < duration)
                {
                    currentTime += (float)UFE.fixedDeltaTime;

                    if (audioSource != null)
                    {
                        audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                    }

                    yield return null;
                }

                yield break;
            }
        }


        public static IEnumerator AudioMixerFadeCoroutine(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
        {
            //StartCoroutine(UFE2FTE.AudioMixerFadeCoroutine(AudioMixer audioMixer, string exposedParameter, float duration, float targetVolume));

            if (audioMixer != null)
            {
                float currentTime = 0;

                float currentVol;

                audioMixer.GetFloat(exposedParam, out currentVol);

                currentVol = Mathf.Pow(10, currentVol / 20);

                float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

                while (currentTime < duration)
                {
                    currentTime += (float)UFE.fixedDeltaTime;

                    float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);

                    if (audioMixer != null)
                    {
                        audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
                    }

                    yield return null;
                }

                yield break;
            }
        }

        #endregion

        #region Button Press Methods

        public static ButtonPress GetButtonPress(BlockType blockType)
        {
            switch (blockType)
            {
                case BlockType.None:
                    return ButtonPress.Back;

                case BlockType.HoldBack:
                    return ButtonPress.Back;

                case BlockType.HoldButton1:
                    return ButtonPress.Button1;

                case BlockType.HoldButton2:
                    return ButtonPress.Button2;

                case BlockType.HoldButton3:
                    return ButtonPress.Button3;

                case BlockType.HoldButton4:
                    return ButtonPress.Button4;

                case BlockType.HoldButton5:
                    return ButtonPress.Button5;

                case BlockType.HoldButton6:
                    return ButtonPress.Button6;

                case BlockType.HoldButton7:
                    return ButtonPress.Button7;

                case BlockType.HoldButton8:
                    return ButtonPress.Button8;

                case BlockType.HoldButton9:
                    return ButtonPress.Button9;

                case BlockType.HoldButton10:
                    return ButtonPress.Button10;

                case BlockType.HoldButton11:
                    return ButtonPress.Button11;

                case BlockType.HoldButton12:
                    return ButtonPress.Button12;

                default:
                    return ButtonPress.Back;
            }
        }

        #endregion

        #region Character Info Methods

        public static UFE3D.CharacterInfo GetCharacterInfo(Player player)
        {
            switch (player)
            {
                case Player.Player1:
                    return UFE.GetPlayer1();

                case Player.Player2:
                    return UFE.GetPlayer2();

                default:
                    return null;
            }
        }

        public static UFE3D.CharacterInfo GetCharacterInfo(string characterName)
        {
            if (UFE.config == null)
            {
                return null;
            }

            int length = UFE.config.characters.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterName != UFE.config.characters[i].characterName)
                {
                    continue;
                }

                return UFE.config.characters[i];
            }

            return null;
        }

        #endregion

        #region Component Methods

        public static T GetComponentFromEventSystemCurrentSelectedGameObject<T>() where T : Component
        {
            if (EventSystem.current == null
                || EventSystem.current.currentSelectedGameObject == null)
            {
                return null;
            }

            return EventSystem.current.currentSelectedGameObject.GetComponent<T>();
        }

        public static T AddComponentToGameEngineGameObject<T>() where T : Component
        {
            if (UFE.gameEngine == null)
            {
                return null;
            }

            T component = UFE.gameEngine.GetComponent<T>();
            if (component == null)
            {
                return UFE.gameEngine.AddComponent<T>();
            }

            return component;
        }

        public static T GetComponentFromGameEngineGameObject<T>() where T : Component
        {
            if (UFE.gameEngine == null)
            {
                return null;
            }

            return UFE.gameEngine.GetComponent<T>();
        }

        public static T AddComponentToControlsScriptCharacterGameObject<T>(ControlsScript player) where T : Component
        {
            if (player == null)
            {
                return null;
            }

            T component = player.character.GetComponent<T>();
            if (component == null)
            {
                return player.character.AddComponent<T>();
            }

            return component;
        }

        public static void AddComponentToAllControlsScriptsCharacterGameObject<T>() where T : Component
        {
            List<ControlsScript> controlsScriptList = UFE.GetAllControlsScripts();
            int count = controlsScriptList.Count;
            for (int i = 0; i < count; i++)
            {
                AddComponentToControlsScriptCharacterGameObject<T>(controlsScriptList[i]);
            }
        }

        #endregion

        #region Controls Script Methods

        public static ControlsScript GetControlsScript(Player player)
        {
            switch (player)
            {
                case Player.Player1:
                    return UFE.GetPlayer1ControlsScript();

                case Player.Player2:
                    return UFE.GetPlayer2ControlsScript();

                default:
                    return null;
            }
        }

        public static ControlsScript GetControlsScript(HitBox hitBox)
        {
            if (hitBox == null)
            {
                return null;
            }

            return hitBox.position.GetComponentInParent<ControlsScript>();
        }

        public static void ResetControlsScriptData(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            player.ResetData(true);
            KillAllMoves(player);
        }

        public static void ResetAllControlsScriptsData()
        {
            ResetControlsScriptData(UFE.GetPlayer1ControlsScript());
            ResetControlsScriptData(UFE.GetPlayer2ControlsScript());
        }

        #endregion

        #region DoFixedUpdate Methods

        public delegate void DoFixedUpdateEventHandler(IDictionary<InputReferences, InputEvents> player1PreviousInputs, IDictionary<InputReferences, InputEvents> player1CurrentInputs, IDictionary<InputReferences, InputEvents> player2PreviousInputs, IDictionary<InputReferences, InputEvents> player2CurrentInputs);
        public static event DoFixedUpdateEventHandler DoFixedUpdateEvent;

        public static void CallDoFixedUpdateEvent(IDictionary<InputReferences, InputEvents> player1PreviousInputs, IDictionary<InputReferences, InputEvents> player1CurrentInputs, IDictionary<InputReferences, InputEvents> player2PreviousInputs, IDictionary<InputReferences, InputEvents> player2CurrentInputs)
        {
            //UFE2FTE.UFE2FTE.CallDoFixedUpdateEvent(player1PreviousInputs, player1CurrentInputs, player2PreviousInputs, player2CurrentInputs);
            //player1PreviousInputs, player1CurrentInputs, player2PreviousInputs, player2CurrentInputs

            if (DoFixedUpdateEvent == null)
            {
                return;
            }

            DoFixedUpdateEvent(player1PreviousInputs, player1CurrentInputs, player2PreviousInputs, player2CurrentInputs);
        }

        /// <summary>
        /// The ComparingScreen parameter is needed to prevent crashes.
        /// </summary>
        /// <param name="comparingScreen"></param>
        /// <param name="player1PreviousInputs"></param>
        /// <param name="player1CurrentInputs"></param>
        /// <param name="player2PreviousInputs"></param>
        /// <param name="player2CurrentInputs"></param>
        public static void DoFixedUpdate(UFEScreen comparingScreen, IDictionary<InputReferences, InputEvents> player1PreviousInputs, IDictionary<InputReferences, InputEvents> player1CurrentInputs, IDictionary<InputReferences, InputEvents> player2PreviousInputs, IDictionary<InputReferences, InputEvents> player2CurrentInputs)
        {
            if (UFE.currentScreen == null
                && comparingScreen != null
                && currentScreen != null
                && comparingScreen != currentScreen)
            {
                currentScreen.DoFixedUpdate(player1PreviousInputs, player1CurrentInputs, player2PreviousInputs, player2CurrentInputs);

                CallDoFixedUpdateEvent(player1PreviousInputs, player1CurrentInputs, player2PreviousInputs, player2CurrentInputs);
            }
            else if (UFE.currentScreen != null)
            {
                CallDoFixedUpdateEvent(player1PreviousInputs, player1CurrentInputs, player2PreviousInputs, player2CurrentInputs);
            }
        }

        #endregion

        #region Frame Delay Methods

        public static int GetFrameDelay()
        {
            if (UFE.gameMode == GameMode.NetworkGame)
            {
                return GetOnlineFrameDelay();
            }
            else
            {
                return GetOfflineFrameDelay();
            }
        }

        private static int GetOfflineFrameDelay()
        {
            if (UFE.config == null)
            {
                return 0;
            }
         
            if (UFE.config.networkOptions.frameDelayType == NetworkFrameDelay.Fixed)
            {
                return UFE.config.networkOptions.defaultFrameDelay;
            }
            else if (UFE.config.networkOptions.frameDelayType == NetworkFrameDelay.Auto)
            {
                return UFE.config.networkOptions.minFrameDelay;
            }

            return 0;
        }

        private static int GetOnlineFrameDelay()
        {
            if (UFE.fluxCapacitor == null)
            {
                return 0;
            }

            return UFE.fluxCapacitor.GetOptimalFrameDelay();
        }

        public static void AddOrSubtractFrameDelay(int frameDelay)
        {
            if (UFE.config == null)
            {
                return;
            }

            if (UFE.config.networkOptions.frameDelayType == NetworkFrameDelay.Fixed)
            {
                UFE.config.networkOptions.defaultFrameDelay += frameDelay;

                if (UFE.config.networkOptions.defaultFrameDelay < 0)
                {
                    UFE.config.networkOptions.defaultFrameDelay = UFE.config.networkOptions.maxFrameDelay;
                }
                else if (UFE.config.networkOptions.defaultFrameDelay > UFE.config.networkOptions.maxFrameDelay)
                {
                    UFE.config.networkOptions.defaultFrameDelay = UFE.config.networkOptions.minFrameDelay;
                }
            }
            else if (UFE.config.networkOptions.frameDelayType == NetworkFrameDelay.Auto)
            {
                UFE.config.networkOptions.minFrameDelay += frameDelay;

                if (UFE.config.networkOptions.minFrameDelay < 0)
                {
                    UFE.config.networkOptions.minFrameDelay = UFE.config.networkOptions.maxFrameDelay;
                }
                else if (UFE.config.networkOptions.minFrameDelay > UFE.config.networkOptions.maxFrameDelay)
                {
                    UFE.config.networkOptions.minFrameDelay = 0;
                }
            }
        }

        #endregion

        #region Freezing Time Methods

        public static Fix64 GetFreezingTimeFromHitStrength(HitStrengh hitStrength)
        {
            switch (hitStrength)
            {
                case HitStrengh.Weak:
                    return UFE.config.hitOptions.weakHit._freezingTime;

                case HitStrengh.Medium:
                    return UFE.config.hitOptions.mediumHit._freezingTime;

                case HitStrengh.Heavy:
                    return UFE.config.hitOptions.heavyHit._freezingTime;

                case HitStrengh.Custom1:
                    return UFE.config.hitOptions.customHit1._freezingTime;

                case HitStrengh.Custom2:
                    return UFE.config.hitOptions.customHit2._freezingTime;

                case HitStrengh.Custom3:
                    return UFE.config.hitOptions.customHit3._freezingTime;

                case HitStrengh.Custom4:
                    return UFE.config.hitOptions.customHit4._freezingTime;

                case HitStrengh.Custom5:
                    return UFE.config.hitOptions.customHit5._freezingTime;

                case HitStrengh.Custom6:
                    return UFE.config.hitOptions.customHit6._freezingTime;

                default:
                    return 0;
            }
        }

        public static Fix64 GetFreezingTimeFromHitOnHit(Hit hit)
        {
            if (hit == null)
            {
                return 0;
            }

            if (hit.overrideHitEffects == true)
            {
                return hit.hitEffects._freezingTime;
            }

            return GetFreezingTimeFromHitStrength(hit.hitStrength);
        }

        public static Fix64 GetFreezingTimeFromHitOnBlock(Hit hit)
        {
            if (hit == null)
            {
                return 0;
            }

            if (hit.overrideHitEffectsBlock == true)
            {
                return hit.hitEffects._freezingTime;
            }

            return GetFreezingTimeFromHitStrength(hit.hitStrength);
        }

        #endregion

        #region GameObject Methods

        public static void SetGameObjectActive(GameObject gameObject, bool active)
        {
            if (gameObject == null)
            {
                return;
            }

            gameObject.SetActive(active);
        }

        public static void SetGameObjectActive(GameObject[] gameObject, bool active)
        {
            if (gameObject == null)
            {
                return;
            }

            int length = gameObject.Length;
            for (int i = 0; i < length; i++)
            {
                SetGameObjectActive(gameObject[i], active);
            }
        }

        public static void SetGameObjectActive(List<GameObject> gameObject, bool active)
        {
            if (gameObject == null)
            {
                return;
            }

            int count = gameObject.Count;
            for (int i = 0; i < count; i++)
            {
                SetGameObjectActive(gameObject[i], active);
            }
        }

        public static void SetActiveAllChildrenGameObjects(Transform transform, bool active)
        {
            if (transform == null)
            {
                return;
            }

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(active);
            }
        }

        public static void DestroyAllChildrenGameObjects(Transform transform)
        {
            if (transform == null)
            {
                return;
            }

            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }


        public static void SpawnNetworkGameObject(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }

            UFE.SpawnGameObject(gameObject, new Vector3(0, 0, 0), Quaternion.identity, true, 0);
        }

        public static void SpawnNetworkGameObject(GameObject[] gameObjectArray)
        {
            if (gameObjectArray == null)
            {
                return;
            }

            int length = gameObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                SpawnNetworkGameObject(gameObjectArray[i]);
            }
        }

        #endregion

        #region Gauge Methods

        public static void SetAllGaugePointsValue(ControlsScript player, Fix64 value)
        {
            if (player == null)
            {
                return;
            }

            int length = player.currentGaugesPoints.Length;
            for (int i = 0; i < length; i++)
            {
                player.currentGaugesPoints[i] = value;

                if (player.currentGaugesPoints[i] > player.myInfo.maxGaugePoints)
                {
                    player.currentGaugesPoints[i] = player.myInfo.maxGaugePoints;
                }
                else if (player.currentGaugesPoints[i] < 0)
                {
                    player.currentGaugesPoints[i] = 0;
                }
            }
        }

        public static void SetGaugePointsValue(ControlsScript player, GaugeId gaugeId, Fix64 value)
        {
            if (player == null)
            {
                return;
            }

            player.currentGaugesPoints[(int)gaugeId] = value;

            if (player.currentGaugesPoints[(int)gaugeId] > player.myInfo.maxGaugePoints)
            {
                player.currentGaugesPoints[(int)gaugeId] = player.myInfo.maxGaugePoints;
            }
            else if (player.currentGaugesPoints[(int)gaugeId] < 0)
            {
                player.currentGaugesPoints[(int)gaugeId] = 0;
            }
        }

        public static void SetGaugePointsValue(ControlsScript player, GaugeId[] gaugeId, Fix64 value)
        {
            if (player == null
                || gaugeId == null)
            {
                return;
            }

            int length = gaugeId.Length;
            for (int i = 0; i < length; i++)
            {
                SetGaugePointsValue(player, gaugeId[i], value);
            }
        }

        public static void SetAllGaugePointsPercent(ControlsScript player, Fix64 percent)
        {
            if (player == null)
            {
                return;
            }

            int length = player.currentGaugesPoints.Length;
            for (int i = 0; i < length; i++)
            {
                player.currentGaugesPoints[i] = player.myInfo.maxGaugePoints * (percent / 100);

                if (player.currentGaugesPoints[i] > player.myInfo.maxGaugePoints)
                {
                    player.currentGaugesPoints[i] = player.myInfo.maxGaugePoints;
                }
                else if (player.currentGaugesPoints[i] < 0)
                {
                    player.currentGaugesPoints[i] = 0;
                }
            }
        }

        public static void SetGaugePointsPercent(ControlsScript player, GaugeId gaugeId, Fix64 percent)
        {
            if (player == null)
            {
                return;
            }

            player.currentGaugesPoints[(int)gaugeId] = player.myInfo.maxGaugePoints * (percent / 100);

            if (player.currentGaugesPoints[(int)gaugeId] > player.myInfo.maxGaugePoints)
            {
                player.currentGaugesPoints[(int)gaugeId] = player.myInfo.maxGaugePoints;
            }
            else if (player.currentGaugesPoints[(int)gaugeId] < 0)
            {
                player.currentGaugesPoints[(int)gaugeId] = 0;
            }
        }

        public static void SetGaugePointsPercent(ControlsScript player, GaugeId[] gaugeId, Fix64 percent)
        {
            if (player == null
                || gaugeId == null)
            {
                return;
            }

            int length = gaugeId.Length;
            for (int i = 0; i < length; i++)
            {
                SetGaugePointsPercent(player, gaugeId[i], percent);
            }
        }

        public static void AddOrSubtractGaugePointsPercent(ControlsScript player, GaugeId gaugeId, Fix64 percent)
        {
            if (player == null)
            {
                return;
            }

            player.currentGaugesPoints[(int)gaugeId] += player.myInfo.maxGaugePoints * (percent / 100);

            if (player.currentGaugesPoints[(int)gaugeId] > player.myInfo.maxGaugePoints)
            {
                player.currentGaugesPoints[(int)gaugeId] = player.myInfo.maxGaugePoints;
            }
            else if (player.currentGaugesPoints[(int)gaugeId] < 0)
            {
                player.currentGaugesPoints[(int)gaugeId] = 0;
            }
        }

        #endregion

        #region Hit Pause Methods

        public static void HitPause(ControlsScript player, Fix64 delayActionTime)
        {
            if (player == null)
            {
                return;
            }

            player.HitPause(0);

            HitUnpause(player, delayActionTime);
        }

        public static void HitUnpause(ControlsScript player, Fix64 delayActionTime)
        {
            if (player == null)
            {
                return;
            }

            UFE.DelaySynchronizedAction(player.HitUnpause, delayActionTime);
        }

        #endregion

        #region Input Display Methods

        public static void SetInputDisplayRotation(Transform transform, ButtonPress buttonPress)
        {
            if (transform == null)
            {
                return;
            }

            Transform cachedTransform = transform;
            Vector3 rotation = cachedTransform.eulerAngles;
            switch (buttonPress)
            {
                case ButtonPress.Forward:
                    rotation.z = 0;
                    break;

                case ButtonPress.Back:
                    rotation.z = 180;
                    break;

                case ButtonPress.Up:
                    rotation.z = 90;
                    break;

                case ButtonPress.UpForward:
                    rotation.z = 45;
                    break;

                case ButtonPress.UpBack:
                    rotation.z = 135;
                    break;

                case ButtonPress.Down:
                    rotation.z = -90;
                    break;

                case ButtonPress.DownForward:
                    rotation.z = -45;
                    break;

                case ButtonPress.DownBack:
                    rotation.z = -135;
                    break;

                case ButtonPress.Button1:
                case ButtonPress.Button2:
                case ButtonPress.Button3:
                case ButtonPress.Button4:
                case ButtonPress.Button5:
                case ButtonPress.Button6:
                case ButtonPress.Button7:
                case ButtonPress.Button8:
                case ButtonPress.Button9:
                case ButtonPress.Button10:
                case ButtonPress.Button11:
                case ButtonPress.Button12:
                    rotation.z = 0;
                    break;

                default:
                    rotation.z = 0;
                    break;
            }
            cachedTransform.eulerAngles = rotation;
        }

        public static void SetInputDisplayRotation(Transform transform, ButtonPress buttonPress, ControlsScript player)
        {
            if (transform == null
                || player == null)
            {
                return;
            }

            Transform cachedTransform = transform;
            Vector3 rotation = cachedTransform.eulerAngles;
            switch (buttonPress)
            {
                case ButtonPress.Forward:
                    if (player.mirror == -1)
                    {
                        rotation.z = 0;
                    }
                    else
                    {
                        rotation.z = 180;
                    }
                    break;

                case ButtonPress.Back:
                    if (player.mirror == -1)
                    {
                        rotation.z = 180;
                    }
                    else
                    {
                        rotation.z = 0;
                    }
                    break;

                case ButtonPress.Up:
                    rotation.z = 90;
                    break;

                case ButtonPress.UpForward:
                    if (player.mirror == -1)
                    {
                        rotation.z = 45;
                    }
                    else
                    {
                        rotation.z = 135;
                    }
                    break;

                case ButtonPress.UpBack:
                    if (player.mirror == -1)
                    {
                        rotation.z = 135;
                    }
                    else
                    {
                        rotation.z = 45;
                    }
                    break;

                case ButtonPress.Down:
                    rotation.z = -90;
                    break;

                case ButtonPress.DownForward:
                    if (player.mirror == -1)
                    {
                        rotation.z = -45;
                    }
                    else
                    {
                        rotation.z = -135;
                    }
                    break;

                case ButtonPress.DownBack:
                    if (player.mirror == -1)
                    {
                        rotation.z = -135;
                    }
                    else
                    {
                        rotation.z = -45;
                    }
                    break;

                case ButtonPress.Button1:
                case ButtonPress.Button2:
                case ButtonPress.Button3:
                case ButtonPress.Button4:
                case ButtonPress.Button5:
                case ButtonPress.Button6:
                case ButtonPress.Button7:
                case ButtonPress.Button8:
                case ButtonPress.Button9:
                case ButtonPress.Button10:
                case ButtonPress.Button11:
                case ButtonPress.Button12:
                    rotation.z = 0;
                    break;

                default:
                    rotation.z = 0;
                    break;
            }
            cachedTransform.eulerAngles = rotation;
        }

        #endregion

        #region Input References Methods

        public static InputReferences[] GetInputReferences(Player player)
        {
            if (UFE.config == null)
            {
                return null;
            }

            switch (player)
            {
                case Player.Player1:
                    return UFE.config.player1_Inputs;

                case Player.Player2:
                    return UFE.config.player2_Inputs;

                default:
                    return null;
            }
        }

        #endregion

        #region Life Points Methods

        public static void SetLifePointsValue(ControlsScript player, Fix64 value)
        {
            if (player == null)
            {
                return;
            }

            player.currentLifePoints = value;

            if (player.currentLifePoints > player.myInfo.lifePoints)
            {
                player.currentLifePoints = player.myInfo.lifePoints;
            }
            else if (player.currentLifePoints < 0)
            {
                player.currentLifePoints = 0;
            }
        }

        public static void SetLifePointsPercent(ControlsScript player, Fix64 percent)
        {
            if (player == null)
            {
                return;
            }

            player.currentLifePoints = player.myInfo.lifePoints * (percent / 100);

            if (player.currentLifePoints > player.myInfo.lifePoints)
            {
                player.currentLifePoints = player.myInfo.lifePoints;
            }
            else if (player.currentLifePoints < 0)
            {
                player.currentLifePoints = 0;
            }
        }

        /// <summary>
        /// A positive percent value will add. A negative percent value will subtract.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="percent"></param>
        public static void AddOrSubtractLifePointsPercent(ControlsScript player, Fix64 percent)
        {
            if (player == null)
            {
                return;
            }

            player.currentLifePoints += player.myInfo.lifePoints * (percent / 100);

            if (player.currentLifePoints > player.myInfo.lifePoints)
            {
                player.currentLifePoints = player.myInfo.lifePoints;
            }
            else if (player.currentLifePoints < 0)
            {
                player.currentLifePoints = 0;
            }
        }

        #endregion

        #region Matching Methods

        public static bool IsIntMatch(int comparing, int matching)
        {
            if (comparing == matching)
            {
                return true;
            }

            return false;
        }

        public static bool IsIntMatch(int comparing, int[] matching)
        {
            if (matching == null)
            {
                return false;
            }

            int length = matching.Length;
            for (int i = 0; i < length; i++)
            {
                if (IsIntMatch(comparing, matching[i]) == false)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public static bool IsStringMatch(string comparing, string matching)
        {
            if (comparing == matching)
            {
                return true;
            }

            return false;
        }

        public static bool IsStringMatch(string comparing, string[] matching)
        {
            if (matching == null)
            {
                return false;
            }

            int length = matching.Length;
            for (int i = 0; i < length; i++)
            {
                if (IsStringMatch(comparing, matching[i]) == false)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public static bool IsBasicMoveReferenceMatch(BasicMoveReference comparing, BasicMoveReference matching)
        {
            if (comparing == matching)
            {
                return true;
            }

            return false;
        }

        public static bool IsBasicMoveReferenceMatch(BasicMoveReference comparing, BasicMoveReference[] matching)
        {
            if (matching == null)
            {
                return false;
            }

            int length = matching.Length;
            for (int i = 0; i < length; i++)
            {
                if (IsBasicMoveReferenceMatch(comparing, matching[i]) == false)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public static bool IsButtonPressMatch(ButtonPress comparing, ButtonPress matching)
        {
            if (comparing == matching)
            {
                return true;
            }

            return false;
        }

        public static bool IsButtonPressMatch(ButtonPress comparing, ButtonPress[] matching)
        {
            if (matching == null)
            {
                return false;
            }

            int length = matching.Length;
            for (int i = 0; i < length; i++)
            {
                if (IsButtonPressMatch(comparing, matching[i]) == false)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public static bool IsCombatStancesMatch(CombatStances comparing, CombatStances matching)
        {
            if (comparing == matching)
            {
                return true;
            }

            return false;
        }

        public static bool IsCombatStancesMatch(CombatStances comparing, CombatStances[] matching)
        {
            if (matching == null)
            {
                return false;
            }

            int length = matching.Length;
            for (int i = 0; i < length; i++)
            {
                if (IsCombatStancesMatch(comparing, matching[i]) == false)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public static bool IsGameModeMatch(GameMode comparing, GameMode matching)
        {
            if (comparing == matching)
            {
                return true;
            }

            return false;
        }

        public static bool IsGameModeMatch(GameMode comparing, GameMode[] matching)
        {
            if (matching == null)
            {
                return false;
            }

            int length = matching.Length;
            for (int i = 0; i < length; i++)
            {
                if (IsGameModeMatch(comparing, matching[i]) == false)
                {
                    continue;
                }

                return true;
            }

            return false;
        }


        #endregion

        #region Move Info Methods

        public static void CastMoveByMoveName(ControlsScript player, string moveName, bool overrideCurrentMove = true, bool forceGrounded = false, bool castWarning = false)
        {
            if (player == null
                || player.MoveSet == null)
            {
                return;
            }

            player.CastMove(GetMoveInfoFromControlsScriptByMoveName(player, moveName), overrideCurrentMove, forceGrounded, castWarning);
        }

        public static void TryCastMoveOnLandingOverrideAllControlsScripts(string[] includedMoveNameArray)
        {
            if (UFE.GetPlayer1ControlsScript() != null)
            {
                CastMoveOnLandingOverride(UFE.GetPlayer1ControlsScript(), includedMoveNameArray);

                int count = UFE.GetPlayer1ControlsScript().assists.Count;
                for (int i = 0; i < count; i++)
                {
                    CastMoveOnLandingOverride(UFE.GetPlayer1ControlsScript().assists[i], includedMoveNameArray);
                }
            }

            if (UFE.GetPlayer2ControlsScript() != null)
            {
                CastMoveOnLandingOverride(UFE.GetPlayer2ControlsScript(), includedMoveNameArray);

                int count = UFE.GetPlayer2ControlsScript().assists.Count;
                for (int i = 0; i < count; i++)
                {
                    CastMoveOnLandingOverride(UFE.GetPlayer2ControlsScript().assists[i], includedMoveNameArray);
                }
            }
        }

        public static void CastMoveOnLandingOverride(ControlsScript player, string[] includedMoveNameArray)
        {
            if (player == null
                || player.currentMove == null
                || player.currentMove.cancelMoveWhenLanding == false
                || player.currentMove.landingMoveLink == null
                || player.Physics.IsGrounded() == false
                || IsStringMatch(player.currentMove.moveName, includedMoveNameArray) == false)
            {
                return;
            }

            player.CastMove(player.currentMove.landingMoveLink, true);
        }

        public static MoveInfo GetMoveInfoFromControlsScriptByMoveName(ControlsScript player, string moveName)
        {
            if (player == null
                || player.MoveSet == null)
            {
                return null;
            }

            if (player.MoveSet.attackMoves != null)
            {
                int length = player.MoveSet.attackMoves.Length;
                for (int i = 0; i < length; i++)
                {
                    if (moveName != player.MoveSet.attackMoves[i].moveName)
                    {
                        continue;
                    }

                    return player.MoveSet.attackMoves[i];
                }
            }

            return null;
        }

        public static void KillAllMoves(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            player.storedMove = null;

            player.KillCurrentMove();
        }

        public static void KillAllMovesPlayer1()
        {
            if (UFE.GetPlayer1ControlsScript() == null)
            {
                return;
            }

            UFE.GetPlayer1ControlsScript().storedMove = null;

            UFE.GetPlayer1ControlsScript().KillCurrentMove();
        }

        public static void KillAllMovesPlayer2()
        {
            if (UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            UFE.GetPlayer2ControlsScript().storedMove = null;

            UFE.GetPlayer2ControlsScript().KillCurrentMove();
        }

        public static void RemoveMoveFromMoveSet(ControlsScript player, string moveName)
        {
            if (player == null
                || player.MoveSet == null)
            {
                return;
            }

            List<MoveInfo> moveInfoList = player.MoveSet.moves.ToList();
            if (moveInfoList != null)
            {
                int count = moveInfoList.Count - 1;
                for (int i = count; i >= 0; i--)
                {
                    if (IsStringMatch(moveName, moveInfoList[i].moveName) == false)
                    {
                        continue;
                    }

                    moveInfoList.RemoveAt(i);
                }
                player.MoveSet.moves = moveInfoList.ToArray();
            }

            moveInfoList = player.MoveSet.attackMoves.ToList();
            if (moveInfoList != null)
            {
                int count = moveInfoList.Count - 1;
                for (int i = count; i >= 0; i--)
                {
                    if (IsStringMatch(moveName, moveInfoList[i].moveName) == false)
                    {
                        continue;
                    }

                    moveInfoList.RemoveAt(i);
                }
                player.MoveSet.attackMoves = moveInfoList.ToArray();
            }
        }

        #endregion

        #region Move Set Script Methods

        public static void ChangeMoveStances(ControlsScript player, CombatStances newStance)
        {
            if (player == null
                || player.MoveSet == null)
            {
                return;
            }

            player.MoveSet.ChangeMoveStances(newStance);
        }

        #endregion

        #region Physics Methods

        public static void ForceGrounded(ControlsScript player, int timesToExecute = 1)
        {
            if (player == null
                || player.Physics == null)
            {
                return;
            }

            for (int i = 0; i < timesToExecute; i++)
            {
                player.Physics.ForceGrounded();
            }
        }

        public static void AddForce(ControlsScript player, FPVector forces)
        {
            if (player == null
                || player.Physics == null)
            {
                return;
            }

            player.Physics.AddForce(forces, player.GetDirection(), true);
        }

        public static void ResetForces(ControlsScript player, bool resetXForce, bool resetYForce, bool resetZForce)
        {
            if (player == null
                || player.Physics == null)
            {
                return;
            }

            player.Physics.ResetForces(resetXForce, resetYForce, resetZForce);
        }

        #endregion

        #region Ping Methods

        public static int GetOnlinePing()
        {
            if (UFE.multiplayerAPI == null)
            {
                return 0;
            }

            return UFE.multiplayerAPI.GetLastPing();
        }

        #endregion

        #region Position Methods

        public static void SetPlayerPosition(ControlsScript player, FPVector position)
        {
            if (player == null)
            {
                return;
            }

            player.worldTransform.position = position;
        }

        public static void SetPlayerRotation(ControlsScript player, FPQuaternion rotation)
        {
            if (player == null)
            {
                return;
            }

            player.worldTransform.rotation = rotation;
        }

        public static void ShakeCharacterPosition(ControlsScript player, FPVector shakeOffset)
        {
            if (player == null)
            {
                return;
            }

            FPVector offset = new FPVector(
                offset.x = RandomWithTwoPossibleOutcomes(-1, 1) * shakeOffset.x,
                offset.y = RandomWithTwoPossibleOutcomes(-1, 1) * shakeOffset.y,
                offset.z = RandomWithTwoPossibleOutcomes(-1, 1) * shakeOffset.z);

            player.localTransform.position = new FPVector(
                player.localTransform.position.x + offset.x,
                player.localTransform.position.y + offset.y,
                player.localTransform.position.z + offset.z);
        }

        public static void SetAllPlayersLeftCornerPosition(ControlsScript player, Fix64 cornerPositionXOffset)
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            switch (UFE.config.gameplayType)
            {
                case GameplayType._2DFighter:
                    if (player == UFE.GetPlayer1ControlsScript())
                    {
                        SetPlayerPosition(
                            UFE.GetPlayer1ControlsScript(),
                            new FPVector(
                                UFE.GetStage()._leftBoundary,
                                UFE.GetStage().position.y,
                                UFE.GetPlayer1ControlsScript().worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.GetPlayer2ControlsScript(),
                            new FPVector(
                                UFE.GetStage()._leftBoundary + cornerPositionXOffset,
                                UFE.GetStage().position.y,
                                UFE.GetPlayer2ControlsScript().worldTransform.position.z));
                    }
                    else if (player == UFE.GetPlayer2ControlsScript())
                    {
                        SetPlayerPosition(
                            UFE.GetPlayer1ControlsScript(),
                            new FPVector(
                                UFE.GetStage()._leftBoundary + cornerPositionXOffset,
                                UFE.GetStage().position.y,
                                UFE.GetPlayer1ControlsScript().worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.GetPlayer2ControlsScript(),
                            new FPVector(
                                UFE.GetStage()._leftBoundary,
                                UFE.GetStage().position.y,
                                UFE.GetPlayer2ControlsScript().worldTransform.position.z));
                    }
                    break;

                case GameplayType._3DFighter:
                    break;

                case GameplayType._3DArena:
                    break;
            }
        }

        public static void SetAllPlayersRightCornerPosition(ControlsScript player, Fix64 cornerPositionXOffset)
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            KillAllMoves(UFE.GetPlayer1ControlsScript());
            UFE.GetPlayer1ControlsScript().ResetData(true);

            KillAllMoves(UFE.GetPlayer2ControlsScript());
            UFE.GetPlayer2ControlsScript().ResetData(true);

            switch (UFE.config.gameplayType)
            {
                case GameplayType._2DFighter:
                    if (player == UFE.GetPlayer1ControlsScript())
                    {
                        SetPlayerPosition(
                            UFE.GetPlayer1ControlsScript(),
                            new FPVector(
                                UFE.GetStage()._rightBoundary,
                                UFE.GetStage().position.y,
                                UFE.GetPlayer1ControlsScript().worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.GetPlayer2ControlsScript(),
                            new FPVector(
                                UFE.GetStage()._rightBoundary - cornerPositionXOffset,
                                UFE.GetStage().position.y,
                                UFE.GetPlayer2ControlsScript().worldTransform.position.z));
                    }
                    else if (player == UFE.GetPlayer2ControlsScript())
                    {
                        SetPlayerPosition(
                            UFE.GetPlayer1ControlsScript(),
                            new FPVector(
                                UFE.GetStage()._rightBoundary - cornerPositionXOffset,
                                UFE.GetStage().position.y,
                                UFE.GetPlayer1ControlsScript().worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.GetPlayer2ControlsScript(),
                            new FPVector(
                                UFE.GetStage()._rightBoundary,
                                UFE.GetStage().position.y,
                                UFE.GetPlayer2ControlsScript().worldTransform.position.z));
                    }
                    break;

                case GameplayType._3DFighter:
                    break;

                case GameplayType._3DArena:
                    break;
            }
        }

        public static void SetAllPlayersLeftCornerPosition(Player player, Fix64 cornerPositionXOffset)
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            KillAllMoves(UFE.GetPlayer1ControlsScript());
            UFE.GetPlayer1ControlsScript().ResetData(true);

            KillAllMoves(UFE.GetPlayer2ControlsScript());
            UFE.GetPlayer2ControlsScript().ResetData(true);

            switch (UFE.config.gameplayType)
            {
                case GameplayType._2DFighter:
                    if (player == Player.Player1)
                    {
                        SetPlayerPosition(
                            UFE.GetPlayer1ControlsScript(),
                            new FPVector(
                                UFE.GetStage()._leftBoundary,
                                UFE.GetStage().position.y,
                                UFE.GetPlayer1ControlsScript().worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.GetPlayer2ControlsScript(),
                            new FPVector(
                                UFE.GetStage()._leftBoundary + cornerPositionXOffset,
                                UFE.GetStage().position.y,
                                UFE.GetPlayer2ControlsScript().worldTransform.position.z));
                    }
                    else if (player == Player.Player2)
                    {
                        SetPlayerPosition(
                            UFE.GetPlayer1ControlsScript(),
                            new FPVector(
                                UFE.GetStage()._leftBoundary + cornerPositionXOffset,
                                UFE.GetStage().position.y,
                                UFE.GetPlayer1ControlsScript().worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.GetPlayer2ControlsScript(),
                            new FPVector(
                                UFE.GetStage()._leftBoundary,
                                UFE.GetStage().position.y,
                                UFE.GetPlayer2ControlsScript().worldTransform.position.z));
                    }
                    break;

                case GameplayType._3DFighter:
                    break;

                case GameplayType._3DArena:
                    break;
            }
        }

        public static void SetAllPlayersRightCornerPosition(Player player, Fix64 cornerPositionXOffset)
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            KillAllMoves(UFE.GetPlayer1ControlsScript());
            UFE.GetPlayer1ControlsScript().ResetData(true);

            KillAllMoves(UFE.GetPlayer2ControlsScript());
            UFE.GetPlayer2ControlsScript().ResetData(true);

            switch (UFE.config.gameplayType)
            {
                case GameplayType._2DFighter:
                    if (player == Player.Player1)
                    {
                        SetPlayerPosition(
                            UFE.GetPlayer1ControlsScript(),
                            new FPVector(
                                UFE.GetStage()._rightBoundary,
                                UFE.GetStage().position.y,
                                UFE.GetPlayer1ControlsScript().worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.GetPlayer2ControlsScript(),
                            new FPVector(
                                UFE.GetStage()._rightBoundary - cornerPositionXOffset,
                                UFE.GetStage().position.y,
                                UFE.GetPlayer2ControlsScript().worldTransform.position.z));
                    }
                    else if (player == Player.Player2)
                    {
                        SetPlayerPosition(
                            UFE.GetPlayer1ControlsScript(),
                            new FPVector(
                                UFE.GetStage()._rightBoundary - cornerPositionXOffset,
                                UFE.GetStage().position.y,
                                UFE.GetPlayer1ControlsScript().worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.GetPlayer2ControlsScript(),
                            new FPVector(
                                UFE.GetStage()._rightBoundary,
                                UFE.GetStage().position.y,
                                UFE.GetPlayer2ControlsScript().worldTransform.position.z));
                    }
                    break;

                case GameplayType._3DFighter:
                    break;

                case GameplayType._3DArena:
                    break;
            }
        }

        public static void ResetAllPlayersPosition()
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            KillAllMoves(UFE.GetPlayer1ControlsScript());
            UFE.GetPlayer1ControlsScript().ResetData(true);
            SetPlayerPosition(UFE.GetPlayer1ControlsScript(), UFE.config.roundOptions._p1XPosition);

            KillAllMoves(UFE.GetPlayer2ControlsScript());
            UFE.GetPlayer2ControlsScript().ResetData(true);
            SetPlayerPosition(UFE.GetPlayer2ControlsScript(), UFE.config.roundOptions._p2XPosition);

            switch (UFE.config.gameplayType)
            {
                case GameplayType._2DFighter:
                    break;

                case GameplayType._3DFighter:
                    UFE.GetPlayer1ControlsScript().LookAtTarget();

                    UFE.GetPlayer2ControlsScript().LookAtTarget();
                    break;

                case GameplayType._3DArena:
                    SetPlayerRotation(UFE.GetPlayer1ControlsScript(), FPQuaternion.Euler(UFE.config.roundOptions._p1XRotation));

                    SetPlayerRotation(UFE.GetPlayer2ControlsScript(), FPQuaternion.Euler(UFE.config.roundOptions._p2XRotation));
                    break;
            }
        }

        public static Vector3 GetCenterPositionFromHitBox(HitBox hitBox)
        {
            if (hitBox == null)
            {
                return new Vector3(0, 0, 0);
            }

            if (hitBox.shape == HitBoxShape.circle)
            {
                return new FPVector(hitBox.mappedPosition.x, hitBox.mappedPosition.y, hitBox.mappedPosition.z).ToVector();
            }
            else if (hitBox.shape == HitBoxShape.rectangle)
            {
                return new FPVector(hitBox.rect.center.x + hitBox.mappedPosition.x, hitBox.rect.center.y + hitBox.mappedPosition.y, hitBox.mappedPosition.z).ToVector();
            }

            return new Vector3(0, 0, 0);
        }

        #endregion

        #region Random Methods

        public static int RandomWithTwoPossibleOutcomes(int outcome1, int outcome2)
        {
            int randomOutcomeNumber = FPRandom.Range(0, 2);

            if (randomOutcomeNumber == 0)
            {
                return outcome1;
            }
            else
            {
                return outcome2;
            }
        }

        #endregion

        #region Save And Load State Methods

        public static void SaveState()
        {
            if (UFE.replayMode == null)
            {
                return;
            }

            UFE.replayMode.SaveState();
        }

        public static void LoadState()
        {
            if (UFE.replayMode == null
                || UFE.fluxCapacitor.savedState == null)
            {
                return;
            }

            UFE.replayMode.LoadState();
        }

        #endregion

        #region Stage Options Methods

        public static StageOptions GetStageOptions(string stageName)
        {
            if (UFE.config == null)
            {
                return null;
            }

            int length = UFE.config.stages.Length;
            for (int i = 0; i < length; i++)
            {
                if (stageName != UFE.config.stages[i].stageName)
                {
                    continue;
                }

                return UFE.config.stages[i];
            }

            return null;
        }

        #endregion

        #region Stun Time Methods

        public static Fix64 GetStunTime(int stunFrames)
        {
            return (Fix64)stunFrames / UFE.config.fps;
        }

        public static Fix64 GetStunTime(Fix64 stunFrames)
        {
            return stunFrames / UFE.config.fps;
        }

        #endregion

        #region Texture Methods

        public static Texture GetInputViewerIcon(InputReferences[] inputReferences, ButtonPress buttonPress)
        {
            if (inputReferences == null)
            {
                return null;
            }

            int length = inputReferences.Length;
            for (int i = 0; i < length; i++)
            {
                switch (buttonPress)
                {
                    case ButtonPress.Forward:
                        if (inputReferences[i].inputType == InputType.HorizontalAxis)
                        {
                            return inputReferences[i].inputViewerIcon1;
                        }
                        break;

                    case ButtonPress.Back:
                        if (inputReferences[i].inputType == InputType.HorizontalAxis)
                        {
                            return inputReferences[i].inputViewerIcon2;
                        }
                        break;

                    case ButtonPress.Up:
                        if (inputReferences[i].inputType == InputType.VerticalAxis)
                        {
                            return inputReferences[i].inputViewerIcon1;
                        }
                        break;

                    case ButtonPress.UpForward:
                        if (inputReferences[i].inputType == InputType.HorizontalAxis)
                        {
                            return inputReferences[i].inputViewerIcon3;
                        }
                        break;

                    case ButtonPress.UpBack:
                        if (inputReferences[i].inputType == InputType.HorizontalAxis)
                        {
                            return inputReferences[i].inputViewerIcon4;
                        }
                        break;

                    case ButtonPress.Down:
                        if (inputReferences[i].inputType == InputType.VerticalAxis)
                        {
                            return inputReferences[i].inputViewerIcon2;
                        }
                        break;

                    case ButtonPress.DownForward:
                        if (inputReferences[i].inputType == InputType.HorizontalAxis)
                        {
                            return inputReferences[i].inputViewerIcon5;
                        }
                        break;

                    case ButtonPress.DownBack:
                        if (inputReferences[i].inputType == InputType.HorizontalAxis)
                        {
                            return inputReferences[i].inputViewerIcon6;
                        }
                        break;

                    case ButtonPress.Button1:
                    case ButtonPress.Button2:
                    case ButtonPress.Button3:
                    case ButtonPress.Button4:
                    case ButtonPress.Button5:
                    case ButtonPress.Button6:
                    case ButtonPress.Button7:
                    case ButtonPress.Button8:
                    case ButtonPress.Button9:
                    case ButtonPress.Button10:
                    case ButtonPress.Button11:
                    case ButtonPress.Button12:
                        if (buttonPress == inputReferences[i].engineRelatedButton)
                        {
                            return inputReferences[i].inputViewerIcon1;
                        }
                        break;

                    default:
                        return null;
                }
            }

            return null;
        }

        #endregion

        #region Training Mode Methods

        public static void NextTrainingModeGaugeMode()
        {
            if (UFE.config == null)
            {
                return;
            }

            SetNextEnum(ref UFE.config.trainingModeOptions.p1Gauge);
            UFE.config.trainingModeOptions.p2Gauge = UFE.config.trainingModeOptions.p1Gauge;
        }

        public static void PreviousTrainingModeGaugeMode()
        {
            if (UFE.config == null)
            {
                return;
            }

            SetPreviousEnum(ref UFE.config.trainingModeOptions.p1Gauge);
            UFE.config.trainingModeOptions.p2Gauge = UFE.config.trainingModeOptions.p1Gauge;
        }

        public static void SetAllPlayersTrainingModeLifeMode(LifeBarTrainingMode lifeBarTrainingMode)
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.trainingModeOptions.p1Life = lifeBarTrainingMode;
            UFE.config.trainingModeOptions.p2Life = lifeBarTrainingMode;
        }

        public static void SetAllPlayersTrainingModeGaugeMode(LifeBarTrainingMode lifeBarTrainingMode)
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.trainingModeOptions.p1Gauge = lifeBarTrainingMode;
            UFE.config.trainingModeOptions.p2Gauge = lifeBarTrainingMode;
        }


        public static void SetNextEnum(ref LifeBarTrainingMode value)
        {
            int index = (int)value;
            index++;
            if (System.Enum.IsDefined(typeof(LifeBarTrainingMode), (LifeBarTrainingMode)index) == false)
            {
                index = 0;
            }
            value = (LifeBarTrainingMode)index;
        }

        public static void SetPreviousEnum(ref LifeBarTrainingMode value)
        {
            int index = (int)value;
            index--;
            if (System.Enum.IsDefined(typeof(LifeBarTrainingMode), (LifeBarTrainingMode)index) == false)
            {
                index = System.Enum.GetValues(typeof(LifeBarTrainingMode)).Length - 1;
            }
            value = (LifeBarTrainingMode)index;
        }

        public static string GetStringFromEnum(LifeBarTrainingMode value)
        {
            switch (value)
            {
                case LifeBarTrainingMode.Normal:
                    return languageOptions.selectedLanguage.Normal;

                case LifeBarTrainingMode.Refill:
                    return languageOptions.selectedLanguage.Refill;

                case LifeBarTrainingMode.Infinite:
                    return languageOptions.selectedLanguage.Infinite;

                default:
                    return "";
            }
        }

        #endregion

        #region UFE Controller Methods

        public static void PressAxis(UFEController controller, InputType inputType = InputType.HorizontalAxis, int axisValue = 0)
        {
            if (controller == null)
            {
                return;
            }

            foreach (InputReferences inputReference in controller.inputReferences)
            {
                if (controller.isCPU == false
                    && inputReference.inputType == inputType
                    && inputType != InputType.Button)
                {
                    controller.inputs[inputReference] = new InputEvents(axisValue);
                }
            }
        }

        public static void PressButton(UFEController controller, ButtonPress button)
        {
            if (controller == null)
            {
                return;
            }

            foreach (InputReferences inputReference in controller.inputReferences)
            {
                if (controller.isCPU == false
                    && inputReference.inputType == InputType.Button
                    && inputReference.engineRelatedButton == button)
                {
                    controller.inputs[inputReference] = new InputEvents(true);
                }
            }
        }

        public static void PressButton(UFEController controller, ButtonPress[] button)
        {
            if (controller == null
                || button == null)
            {
                return;
            }

            int length = button.Length;
            for (int i = 0; i < length; i++)
            {
                PressButton(controller, button[i]);
            }
        }

        public static void PressButton(ControlsScript player, UFEController controller, ButtonPress buttonPress)
        {
            if (player == null
                || controller == null)
            {
                return;
            }

            switch (buttonPress)
            {
                case ButtonPress.Forward:
                    PressAxis(controller, InputType.HorizontalAxis, 1 * -player.mirror);
                    break;

                case ButtonPress.Back:
                    PressAxis(controller, InputType.HorizontalAxis, -1 * -player.mirror);
                    break;

                case ButtonPress.Up:
                    PressAxis(controller, InputType.VerticalAxis, 1);
                    break;

                case ButtonPress.UpForward:
                    PressAxis(controller, InputType.HorizontalAxis, 1 * -player.mirror);
                    PressAxis(controller, InputType.VerticalAxis, 1);
                    break;

                case ButtonPress.UpBack:
                    PressAxis(controller, InputType.HorizontalAxis, -1 * -player.mirror);
                    PressAxis(controller, InputType.VerticalAxis, 1);
                    break;

                case ButtonPress.Down:
                    PressAxis(controller, InputType.VerticalAxis, -1);
                    break;

                case ButtonPress.DownForward:
                    PressAxis(controller, InputType.HorizontalAxis, 1 * -player.mirror);
                    PressAxis(controller, InputType.VerticalAxis, -1);
                    break;

                case ButtonPress.DownBack:
                    PressAxis(controller, InputType.HorizontalAxis, -1 * -player.mirror);
                    PressAxis(controller, InputType.VerticalAxis, -1);
                    break;

                default:
                    PressButton(controller, buttonPress);
                    break;
            }
        }

        public static void PressButton(ControlsScript player, UFEController controller, ButtonPress[] buttonPress)
        {
            if (player == null
                || controller == null
                || buttonPress == null)
            {
                return;
            }

            int length = buttonPress.Length;
            for (int i = 0; i < length; i++)
            {
                PressButton(player, controller, buttonPress[i]);
            }
        }

        public static void PressMoveInfoDefaultInputs(ControlsScript player, UFEController controller, MoveInfo moveInfo)
        {
            if (player == null
                || controller == null
                || moveInfo == null)
            {
                return;
            }

            PressMoveInputs(player, controller, moveInfo.defaultInputs);
        }

        public static void PressMoveInfoAlternativeInputs(ControlsScript player, UFEController controller, MoveInfo moveInfo)
        {
            if (player == null
                || controller == null
                || moveInfo == null)
            {
                return;
            }

            PressMoveInputs(player, controller, moveInfo.altInputs);
        }

        public static void PressMoveInputs(ControlsScript player, UFEController controller, MoveInputs moveInputs)
        {
            if (player == null
                || controller == null
                || moveInputs == null)
            {
                return;
            }

            PressButton(controller, moveInputs.buttonSequence);

            PressButton(controller, moveInputs.buttonExecution);
        }

        public static UFEController GetUFEController(Player player)
        {
            return UFE.GetController((int)player + 1);
        }

        #endregion

        #region UI Methods

        public static void SelectSelectable(Selectable selectable)
        {
            if (selectable == null)
            {
                return;
            }

            selectable.Select();
        }

        public static void SetSelectableInteractable(Selectable selectable, bool interactable)
        {
            if (selectable == null)
            {
                return;
            }

            selectable.interactable = interactable;      
        }

        public static void SetSelectableInteractable(Selectable[] selectableArray, bool interactable)
        {
            if (selectableArray == null)
            {
                return;
            }

            int length = selectableArray.Length;
            for (int i = 0; i < length; i++)
            {
                SetSelectableInteractable(selectableArray[i], interactable);
            }
        }

        public static string GetTextMessage(Text text)
        {
            if (text == null)
            {
                return "";
            }

            return text.text;
        }

        public static void SetTextMessage(Text text, string message)
        {
            if (text == null)
            {
                return;
            }

            text.text = message;
        }

        public static void SetTextMessage(Text[] textArray, string message)
        {
            if (textArray == null)
            {
                return;
            }

            int length = textArray.Length;
            for (int i = 0; i < length; i++)
            {
                SetTextMessage(textArray[i], message);
            }
        }

        private static PointerEventData pointerEventDataCurrentPosition = null;
        private static List<RaycastResult> raycastResultList = null;
        public static bool IsOverUI()
        {
            pointerEventDataCurrentPosition = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            raycastResultList = new List<RaycastResult>();

            EventSystem.current.RaycastAll(pointerEventDataCurrentPosition, raycastResultList);

            return raycastResultList.Count > 0;
        }

        public static Vector3 GetWorldPositionOfCanvasElement(RectTransform rectTransform, Camera camera)
        {
            if (rectTransform == null
                || camera == null)
            {
                return new Vector3(0, 0, 0);
            }

            RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, rectTransform.position, camera, out var result);

            return result;
        }

        public static void KeepChildInScrollViewPort(ScrollRect scrollRect, RectTransform child, Vector2 margin)
        {
            if (scrollRect == null
                || scrollRect.content == null
                || scrollRect.viewport == null
                || child == null
                || child.rect == null)
            {
                return;
            }

            Canvas.ForceUpdateCanvases();

            // Get min and max of the viewport and child in local space to the viewport so we can compare them.
            // NOTE: use viewport instead of the scrollRect as viewport doesn't include the scrollbars in it.
            Vector2 viewPosMin = scrollRect.viewport.rect.min;
            Vector2 viewPosMax = scrollRect.viewport.rect.max;

            Vector2 childPosMin = scrollRect.viewport.InverseTransformPoint(child.TransformPoint(child.rect.min));
            Vector2 childPosMax = scrollRect.viewport.InverseTransformPoint(child.TransformPoint(child.rect.max));

            childPosMin -= margin;
            childPosMax += margin;

            Vector2 move = Vector2.zero;

            // Check if one (or more) of the child bounding edges goes outside the viewport and
            // calculate move vector for the content rect so it can keep it visible.
            if (childPosMax.y > viewPosMax.y)
            {
                move.y = childPosMax.y - viewPosMax.y;
            }
            if (childPosMin.x < viewPosMin.x)
            {
                move.x = childPosMin.x - viewPosMin.x;
            }
            if (childPosMax.x > viewPosMax.x)
            {
                move.x = childPosMax.x - viewPosMax.x;
            }
            if (childPosMin.y < viewPosMin.y)
            {
                move.y = childPosMin.y - viewPosMin.y;
            }

            // Transform the move vector to world space, then to content local space (in case of scaling or rotation?) and apply it.
            Vector3 worldMove = scrollRect.viewport.TransformDirection(move);
            scrollRect.content.localPosition -= scrollRect.content.InverseTransformDirection(worldMove);
        }

        #endregion

        #region Unity Editor Methods

#if UNITY_EDITOR
        /// <summary>
        /// Gets the object the property represents.
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static object GetTargetObjectOfProperty(UnityEditor.SerializedProperty prop)
        {
            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }
            return obj;
        }

        private static object GetValue_Imp(object source, string name)
        {
            if (source == null)
                return null;
            var type = source.GetType();

            while (type != null)
            {
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                    return f.GetValue(source);

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                    return p.GetValue(source, null);

                type = type.BaseType;
            }
            return null;
        }

        private static object GetValue_Imp(object source, string name, int index)
        {
            var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
            if (enumerable == null) return null;
            var enm = enumerable.GetEnumerator();
            //while (index-- >= 0)
            //    enm.MoveNext();
            //return enm.Current;

            for (int i = 0; i <= index; i++)
            {
                if (!enm.MoveNext()) return null;
            }
            return enm.Current;
        }

        public static void SetTargetObjectOfProperty(UnityEditor.SerializedProperty prop, object value)
        {
            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements.Take(elements.Length - 1))
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }

            if (Object.ReferenceEquals(obj, null)) return;

            try
            {
                var element = elements.Last();

                if (element.Contains("["))
                {
                    var tp = obj.GetType();
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    var field = tp.GetField(elementName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var arr = field.GetValue(obj) as System.Collections.IList;
                    arr[index] = value;
                    //var elementName = element.Substring(0, element.IndexOf("["));
                    //var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    //var arr = DynamicUtil.GetValue(element, elementName) as System.Collections.IList;
                    //if (arr != null) arr[index] = value;
                }
                else
                {
                    var tp = obj.GetType();
                    var field = tp.GetField(element, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (field != null)
                    {
                        field.SetValue(obj, value);
                    }
                    //DynamicUtil.SetValue(obj, element, value);
                }
            }
            catch
            {
                return;
            }
        }
#endif
        
        #endregion

        #region Wait Methods

        private static Dictionary<float, WaitForSeconds> waitForSecondsDictionary = null;
        public static WaitForSeconds GetWaitForSeconds(float time)
        {
            if (waitForSecondsDictionary == null)
            {
                waitForSecondsDictionary = new Dictionary<float, WaitForSeconds>();
            }

            if (waitForSecondsDictionary.TryGetValue(time, out var wait) == true)
            {
                return wait;
            }

            waitForSecondsDictionary[time] = new WaitForSeconds(time);

            return waitForSecondsDictionary[time];
        }

        #endregion

        public static string AddSpacesBeforeCapitalLetters(string message)
        {
            return message = string.Concat(message.Select(x => System.Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }

        [NaughtyAttributes.Button()]
        private void StartMainAlert()
        {
            MainAlertController.CallOnMainAlertEvent(UnityEngine.Random.Range(0, int.MaxValue).ToString());
        }

        [NaughtyAttributes.Button()]
        private void StartCharacterAlert()
        {
            for (int i = 0; i < 10; i++)
            {
                UFE.FireAlert("UFE Alert " + UnityEngine.Random.Range(0, int.MaxValue).ToString(), UFE.GetPlayer1ControlsScript());
                UFE.FireAlert("UFE Alert " + UnityEngine.Random.Range(0, int.MaxValue).ToString(), UFE.GetPlayer2ControlsScript());
            }
        }
    }
}

/*
 * 
 *  // In that case, we can process pause menu events
     //UFE.PauseGame(!UFE.isPaused());

     if (player1CurrentStartButton && !player1PreviousStartButton)
     {
    UFE2FTE.UFE2FTEPlayerPausedManager.PlayerPaused = UFE2FTE.UFE2FTEPlayerPausedManager.Player.Player1Paused;
    UFE.PauseGame(!UFE.isPaused());
}
     else if (player2CurrentStartButton && !player2PreviousStartButton)
{
    UFE2FTE.UFE2FTEPlayerPausedManager.PlayerPaused = UFE2FTE.UFE2FTEPlayerPausedManager.Player.Player2Paused;
    UFE.PauseGame(!UFE.isPaused());
}
 * 
 *
 *        /// <summary>
        /// Discards the contents of this List and copies from another.
        /// Returns the new List count.
        /// </summary>
        /// <param name="copyFrom">The List to copy from.</param>
        public static int CopyFrom<T>(this List<T> list, List<T> copyFrom)
        {
            list.Clear();   // clear our own list
            list.AddRange(copyFrom);    // copy the entires
            return list.Count;
        }
 * 
 * 
 * 
 * 
 * 
 * private static string PrefsKey
{
    get
    {
        if (string.IsNullOrEmpty(_prefsKey))
        {
            _prefsKey = $"{SystemInfo.deviceUniqueIdentifier}.{Application.companyName}.{Application.productName}.{Constants.UniqueIdentifier}";
        }

        return _prefsKey;
    }
}
*/
