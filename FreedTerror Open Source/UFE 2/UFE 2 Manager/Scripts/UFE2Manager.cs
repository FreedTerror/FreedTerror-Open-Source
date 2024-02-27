using FPLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using UFE3D;
using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class UFE2Manager : MonoBehaviour
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

        public static UFE2Manager instance;

        private AIController player2AIController;

        [Header("Scriptable Object Variables")]
        public CharacterInfoReferencesScriptableObject characterInfoReferencesScriptableObject;
        public FrameDelayScriptableObject defaultFrameDelayScriptableObject;
        public GameSettingsScriptableObject defaultGameSettingsScriptableObject;
        public HitBoxDisplayScriptableObject hitBoxDisplayScriptableObject;
        public InputDisplayScriptableObject inputDisplayScriptableObject;

        [Header("AI Variables")]
        public AIMode aiMode = AIMode.Human;
        public Toggle aiThrowTechMode = Toggle.Off;

        [Header("Pause Variables")]
        public Player pausedPlayer = Player.Player1;

        [Header("Training Mode Variables")]
        public InputReplayControllerScriptableObject trainingModeInputReplayControllerScriptableObject;
        public Player trainingModeCornerPlayer = Player.Player2;
        public Fix64 trainingModeCornerPositionXOffset = 3;

        #region Bool Variables

        [Header("Bool Variables")]
        public bool displayBattleGUI;
        public bool displayFrameAdvantage;
        public bool displayFrameDelay;
        public bool displayHitData;
        public bool displayInputs;
        public bool displayMoveData;
        public bool displayPing;

        public bool useCameraShake;
        public bool useCharacterPortraitShake;

        #endregion

        #region String Variables

        [Header("String Variables")]
        public CachedStringData cachedStringData = new CachedStringData();

        #endregion

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

            cachedStringData.InitializePositiveStringNumberArray();
            cachedStringData.InitializeNegativeStringNumberArray();
            cachedStringData.InitializePositivePercentStringNumberArray();
        }

        private void Start()
        {         
            if (defaultFrameDelayScriptableObject != null)
            {
                defaultFrameDelayScriptableObject.UpdateFrameDelaySettings();
            }

            if (defaultGameSettingsScriptableObject != null)
            {
                defaultGameSettingsScriptableObject.UpdateGameSettings();
            }

            if (hitBoxDisplayScriptableObject != null)
            {
                hitBoxDisplayScriptableObject.hitBoxDisplayMode = HitBoxDisplayController.HitBoxDisplayMode.Off;
                hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue = 128;
            }
        }

        private void FixedUpdate()
        {
            if (trainingModeInputReplayControllerScriptableObject != null
                && trainingModeInputReplayControllerScriptableObject.GetCurrentInputReplayController() != null)
            {
                //trainingModeInputReplayDataScriptableObjectManager.GetCurrentInputReplayDataScriptableObject().DoFixedUpdate(UFE.p1ControlsScript, UFE.GetPlayer1Controller());
                trainingModeInputReplayControllerScriptableObject.GetCurrentInputReplayController().DoFixedUpdate(UFE.p2ControlsScript, UFE.GetPlayer2Controller());
            }
        }

        private void Update()
        {
            switch (UFE.gameMode)
            {
                case GameMode.NetworkGame:
                    if (defaultFrameDelayScriptableObject != null)
                    {
                        defaultFrameDelayScriptableObject.UpdateFrameDelaySettings();
                    }

                    if (defaultGameSettingsScriptableObject != null)
                    {
                        defaultGameSettingsScriptableObject.UpdateGameSettings();
                    }
                    break;
            }

            if (UFE.p1ControlsScript == null
                || UFE.p2ControlsScript == null)
            {
                aiMode = AIMode.Human;
                aiThrowTechMode = Toggle.Off;

                if (trainingModeInputReplayControllerScriptableObject != null)
                {
                    trainingModeInputReplayControllerScriptableObject.Clear();
                }
            }

            if (player2AIController == null
                && UFE.p2ControlsScript != null)
            {
                player2AIController = AddComponentToControlsScriptCharacterGameObject<AIController>(UFE.p2ControlsScript);
            }

            //PatchStuff();

            /*if (Input.GetKeyDown(KeyCode.N))
            {
                UFEController test1 = UFE.p1Controller;
                UFEController test2 = UFE.p2Controller;
                UFE.p1Controller = test2;
                UFE.p2Controller = test1;
            }*/
        }

        private void PatchStuff()
        {
            if (UFE.p1ControlsScript != null)
            {
                SetAnimationClipWrapMode(UFE.p1ControlsScript, UFE.p1ControlsScript.MoveSet.GetAnimationString(BasicMoveReference.Crouching, 1), WrapMode.ClampForever);
                SetAnimationClipWrapMode(UFE.p1ControlsScript, UFE.p1ControlsScript.MoveSet.GetAnimationString(BasicMoveReference.Crouching, 2), WrapMode.ClampForever);

                if (UFE.p1ControlsScript.currentMove != null)
                {
                    UFE.p1ControlsScript.currentSubState = SubStates.Resting;
                }
            }

            if (UFE.p2ControlsScript != null)
            {
                SetAnimationClipWrapMode(UFE.p2ControlsScript, UFE.p2ControlsScript.MoveSet.GetAnimationString(BasicMoveReference.Crouching, 1), WrapMode.ClampForever);
                SetAnimationClipWrapMode(UFE.p2ControlsScript, UFE.p2ControlsScript.MoveSet.GetAnimationString(BasicMoveReference.Crouching, 2), WrapMode.ClampForever);

                if (UFE.p2ControlsScript.currentMove != null)
                {
                    UFE.p2ControlsScript.currentSubState = SubStates.Resting;
                }
            }
        }

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
                var item = UFE.config.characters[i];

                if (item == null
                    || characterName != item.characterName)
                {
                    continue;
                }

                return item;
            }

            return null;
        }

        #endregion

        #region Component Methods

        public static T AddComponentToGameEngineGameObject<T>() where T : Component
        {
            if (UFE.GameEngine == null)
            {
                return null;
            }

            T component = UFE.GameEngine.GetComponent<T>();
            if (component == null)
            {
                return UFE.GameEngine.AddComponent<T>();
            }

            return component;
        }

        public static T GetComponentFromGameEngineGameObject<T>() where T : Component
        {
            if (UFE.GameEngine == null)
            {
                return null;
            }

            return UFE.GameEngine.GetComponent<T>();
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
                    return UFE.p1ControlsScript;

                case Player.Player2:
                    return UFE.p2ControlsScript;

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
            ResetControlsScriptData(UFE.p1ControlsScript);
            ResetControlsScriptData(UFE.p2ControlsScript);
        }

        #endregion

        #region Event Methods

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
                return UFE.config.networkInputDelay;
            }
            else if (UFE.config.networkOptions.frameDelayType == NetworkFrameDelay.Auto)
            {
                return UFE.config.networkOptions.minFrameDelay;
            }

            return 0;
        }

        private static int GetOnlineFrameDelay()
        {
            if (UFE.FluxCapacitor == null)
            {
                return 0;
            }

            return UFE.FluxCapacitor.NetworkFrameDelay;
        }

        public static void AddOrSubtractFrameDelay(int frameDelay)
        {
            if (UFE.config == null)
            {
                return;
            }

            if (UFE.config.networkOptions.frameDelayType == NetworkFrameDelay.Fixed)
            {
                UFE.config.networkInputDelay += frameDelay;

                if (UFE.config.networkInputDelay < 0)
                {
                    UFE.config.networkInputDelay = UFE.config.networkOptions.maxFrameDelay;
                }
                else if (UFE.config.networkInputDelay > UFE.config.networkOptions.maxFrameDelay)
                {
                    UFE.config.networkInputDelay = 0;
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

        public static void SpawnUFENetworkGameObject(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }

            UFE.SpawnGameObject(gameObject, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1), true, 0);
        }

        public static void SpawnUFENetworkGameObject(GameObject[] gameObject)
        {
            if (gameObject == null)
            {
                return;
            }

            int length = gameObject.Length;
            for (int i = 0; i < length; i++)
            {
                var item = gameObject[i];

                if (item == null)
                {
                    continue;
                }

                UFE.SpawnGameObject(item, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1), true, 0);
            }
        }

        #endregion

        #region Gauge Methods

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

        public static void AddOrSubtractGaugePointsPercent(ControlsScript player, GaugeId[] gaugeId, Fix64 percent)
        {
            if (player == null
                || gaugeId == null)
            {
                return;
            }

            int length = gaugeId.Length;
            for (int i = 0; i < length; i++)
            {
                AddOrSubtractGaugePointsPercent(player, gaugeId[i], percent);
            }
        }

        public static void AddOrSubtractAllGaugePointsPercent(ControlsScript player, Fix64 percent)
        {
            if (player == null)
            {
                return;
            }

            int length = player.currentGaugesPoints.Length;
            for (int i = 0; i < length; i++)
            {
                player.currentGaugesPoints[i] += player.myInfo.maxGaugePoints * (percent / 100);

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

        public static bool IsIntMatch(int comparing, int[] matching)
        {
            if (matching == null)
            {
                return false;
            }

            int length = matching.Length;
            for (int i = 0; i < length; i++)
            {
                if (comparing != matching[i])
                {
                    continue;
                }

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
                if (comparing != matching[i])
                {
                    continue;
                }

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
                if (comparing != matching[i])
                {
                    continue;
                }

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
                if (comparing != matching[i])
                {
                    continue;
                }

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
                if (comparing != matching[i])
                {
                    continue;
                }

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
                if (comparing != matching[i])
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
            if (player == null)
            {
                return;
            }

            player.CastMove(GetMoveInfoByMoveName(moveName, player.MoveSet.attackMoves), overrideCurrentMove, forceGrounded, castWarning);
        }

        public static MoveInfo GetMoveInfoByMoveName(string moveName, MoveInfo[] moveInfo)
        {
            if (moveInfo == null)
            {
                return null;
            }

            int length = moveInfo.Length;
            for (int i = 0; i < length; i++)
            {
                var item = moveInfo[i];
                if (item == null
                    || moveName != item.moveName)
                {
                    continue;
                }

                return item;
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
            if (UFE.p1ControlsScript == null)
            {
                return;
            }

            UFE.p1ControlsScript.storedMove = null;

            UFE.p1ControlsScript.KillCurrentMove();
        }

        public static void KillAllMovesPlayer2()
        {
            if (UFE.p2ControlsScript == null)
            {
                return;
            }

            UFE.p2ControlsScript.storedMove = null;

            UFE.p2ControlsScript.KillCurrentMove();
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
                    if (moveName != moveInfoList[i].moveName)
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
                    if (moveName != moveInfoList[i].moveName)
                    {
                        continue;
                    }

                    moveInfoList.RemoveAt(i);
                }

                player.MoveSet.attackMoves = moveInfoList.ToArray();
            }
        }

        #endregion

        #region Ping Methods

        public static int GetPing()
        {
            if (UFE.MultiplayerAPI == null)
            {
                return 0;
            }

            return UFE.MultiplayerAPI.GetLastPing();
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
            if (UFE.p1ControlsScript == null
                || UFE.p2ControlsScript == null)
            {
                return;
            }

            switch (UFE.config.gameplayType)
            {
                case GameplayType._2DFighter:
                    if (player == UFE.p1ControlsScript)
                    {
                        SetPlayerPosition(
                            UFE.p1ControlsScript,
                            new FPVector(
                                UFE.GetStage()._leftBoundary,
                                UFE.GetStage().position.y,
                                UFE.p1ControlsScript.worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.p2ControlsScript,
                            new FPVector(
                                UFE.GetStage()._leftBoundary + cornerPositionXOffset,
                                UFE.GetStage().position.y,
                                UFE.p2ControlsScript.worldTransform.position.z));
                    }
                    else if (player == UFE.p2ControlsScript)
                    {
                        SetPlayerPosition(
                            UFE.p1ControlsScript,
                            new FPVector(
                                UFE.GetStage()._leftBoundary + cornerPositionXOffset,
                                UFE.GetStage().position.y,
                                UFE.p1ControlsScript.worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.p2ControlsScript,
                            new FPVector(
                                UFE.GetStage()._leftBoundary,
                                UFE.GetStage().position.y,
                                UFE.p2ControlsScript.worldTransform.position.z));
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
            if (UFE.p1ControlsScript == null
                || UFE.p2ControlsScript == null)
            {
                return;
            }

            KillAllMoves(UFE.p1ControlsScript);
            UFE.p1ControlsScript.ResetData(true);

            KillAllMoves(UFE.p2ControlsScript);
            UFE.p2ControlsScript.ResetData(true);

            switch (UFE.config.gameplayType)
            {
                case GameplayType._2DFighter:
                    if (player == UFE.p1ControlsScript)
                    {
                        SetPlayerPosition(
                            UFE.p1ControlsScript,
                            new FPVector(
                                UFE.GetStage()._rightBoundary,
                                UFE.GetStage().position.y,
                                UFE.p1ControlsScript.worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.p2ControlsScript,
                            new FPVector(
                                UFE.GetStage()._rightBoundary - cornerPositionXOffset,
                                UFE.GetStage().position.y,
                                UFE.p2ControlsScript.worldTransform.position.z));
                    }
                    else if (player == UFE.p2ControlsScript)
                    {
                        SetPlayerPosition(
                            UFE.p1ControlsScript,
                            new FPVector(
                                UFE.GetStage()._rightBoundary - cornerPositionXOffset,
                                UFE.GetStage().position.y,
                                UFE.p1ControlsScript.worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.p2ControlsScript,
                            new FPVector(
                                UFE.GetStage()._rightBoundary,
                                UFE.GetStage().position.y,
                                UFE.p2ControlsScript.worldTransform.position.z));
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
            if (UFE.p1ControlsScript == null
                || UFE.p2ControlsScript == null)
            {
                return;
            }

            KillAllMoves(UFE.p1ControlsScript);
            UFE.p1ControlsScript.ResetData(true);

            KillAllMoves(UFE.p2ControlsScript);
            UFE.p2ControlsScript.ResetData(true);

            switch (UFE.config.gameplayType)
            {
                case GameplayType._2DFighter:
                    if (player == Player.Player1)
                    {
                        SetPlayerPosition(
                            UFE.p1ControlsScript,
                            new FPVector(
                                UFE.GetStage()._leftBoundary,
                                UFE.GetStage().position.y,
                                UFE.p1ControlsScript.worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.p2ControlsScript,
                            new FPVector(
                                UFE.GetStage()._leftBoundary + cornerPositionXOffset,
                                UFE.GetStage().position.y,
                                UFE.p2ControlsScript.worldTransform.position.z));
                    }
                    else if (player == Player.Player2)
                    {
                        SetPlayerPosition(
                            UFE.p1ControlsScript,
                            new FPVector(
                                UFE.GetStage()._leftBoundary + cornerPositionXOffset,
                                UFE.GetStage().position.y,
                                UFE.p1ControlsScript.worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.p2ControlsScript,
                            new FPVector(
                                UFE.GetStage()._leftBoundary,
                                UFE.GetStage().position.y,
                                UFE.p2ControlsScript.worldTransform.position.z));
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
            if (UFE.p1ControlsScript == null
                || UFE.p2ControlsScript == null)
            {
                return;
            }

            KillAllMoves(UFE.p1ControlsScript);
            UFE.p1ControlsScript.ResetData(true);

            KillAllMoves(UFE.p2ControlsScript);
            UFE.p2ControlsScript.ResetData(true);

            switch (UFE.config.gameplayType)
            {
                case GameplayType._2DFighter:
                    if (player == Player.Player1)
                    {
                        SetPlayerPosition(
                            UFE.p1ControlsScript,
                            new FPVector(
                                UFE.GetStage()._rightBoundary,
                                UFE.GetStage().position.y,
                                UFE.p1ControlsScript.worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.p2ControlsScript,
                            new FPVector(
                                UFE.GetStage()._rightBoundary - cornerPositionXOffset,
                                UFE.GetStage().position.y,
                                UFE.p2ControlsScript.worldTransform.position.z));
                    }
                    else if (player == Player.Player2)
                    {
                        SetPlayerPosition(
                            UFE.p1ControlsScript,
                            new FPVector(
                                UFE.GetStage()._rightBoundary - cornerPositionXOffset,
                                UFE.GetStage().position.y,
                                UFE.p1ControlsScript.worldTransform.position.z));

                        SetPlayerPosition(
                            UFE.p2ControlsScript,
                            new FPVector(
                                UFE.GetStage()._rightBoundary,
                                UFE.GetStage().position.y,
                                UFE.p2ControlsScript.worldTransform.position.z));
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
            if (UFE.p1ControlsScript == null
                || UFE.p2ControlsScript == null)
            {
                return;
            }

            KillAllMoves(UFE.p1ControlsScript);
            UFE.p1ControlsScript.ResetData(true);
            SetPlayerPosition(UFE.p1ControlsScript, UFE.config.roundOptions._p1XPosition);

            KillAllMoves(UFE.p2ControlsScript);
            UFE.p2ControlsScript.ResetData(true);
            SetPlayerPosition(UFE.p2ControlsScript, UFE.config.roundOptions._p2XPosition);

            switch (UFE.config.gameplayType)
            {
                case GameplayType._2DFighter:
                    break;

                case GameplayType._3DFighter:
                    UFE.p1ControlsScript.LookAtTarget();

                    UFE.p2ControlsScript.LookAtTarget();
                    break;

                case GameplayType._3DArena:
                    SetPlayerRotation(UFE.p1ControlsScript, FPQuaternion.Euler(UFE.config.roundOptions._p1XRotation));

                    SetPlayerRotation(UFE.p2ControlsScript, FPQuaternion.Euler(UFE.config.roundOptions._p2XRotation));
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
            if (UFE.ReplayMode == null)
            {
                return;
            }

            UFE.ReplayMode.SaveState();
        }

        public static void LoadState()
        {
            if (UFE.ReplayMode == null
                || UFE.FluxCapacitor.savedState == null)
            {
                return;
            }

            UFE.ReplayMode.LoadState();
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
                var item = UFE.config.stages[i];

                if (stageName != item.stageName)
                {
                    continue;
                }

                return item;
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

        #region UFE Controller Methods

        public static void PressAxis(UFEController controller, InputType inputType, Fix64 axisRaw)
        {
            if (controller == null
                || controller.isCPU == true
                || inputType == InputType.Button
                || axisRaw == (Fix64)0)
            {
                return;
            }

            foreach (InputReferences inputReference in controller.inputReferences)
            {
                if (inputReference.inputType != inputType)
                {
                    continue;
                }

                controller.inputs[inputReference] = new InputEvents(axisRaw);

                break;
            }
        }

        public static void PressButton(UFEController controller, ButtonPress buttonPress)
        {
            if (controller == null
                || controller.isCPU == true)
            {
                return;
            }

            foreach (InputReferences inputReference in controller.inputReferences)
            {
                if (inputReference.inputType != InputType.Button
                    || inputReference.engineRelatedButton != buttonPress)
                {
                    continue;
                }

                controller.inputs[inputReference] = new InputEvents(true);

                break;
            }
        }

        public static void PressButton(UFEController controller, ButtonPress[] buttonPress)
        {
            if (controller == null
                || buttonPress == null)
            {
                return;
            }

            int length = buttonPress.Length;
            for (int i = 0; i < length; i++)
            {
                PressButton(controller, buttonPress[i]);
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

        public static int GetNumberOfPossibleRoundWins()
        {
            if (UFE.config == null)
            {
                return 0;
            }

            return (UFE.config.roundOptions.totalRounds + 1) / 2;
        }

        public static void SetAnimationClipWrapMode(ControlsScript player, string clipName, WrapMode wrapMode)
        {
            if (player == null)
            {
                return;
            }

            if (player.myInfo.animationType == AnimationType.Legacy)
            {
                int length = player.MoveSet.LegacyControl.animations.Length;
                for (int i = 0; i < length; i++)
                {
                    var item = player.MoveSet.LegacyControl.animations[i];

                    if (clipName != item.clipName)
                    {
                        continue;
                    }

                    item.clip.wrapMode = wrapMode;

                    break;
                }
            }
            else
            {
                int length = player.MoveSet.MecanimControl.animations.Length;
                for (int i = 0; i < length; i++)
                {
                    var item = player.MoveSet.MecanimControl.animations[i];

                    if (clipName != item.clipName)
                    {
                        continue;
                    }

                    item.clip.wrapMode = wrapMode;

                    break;
                }
            }
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
                UFE.FireAlert("UFE Alert " + UnityEngine.Random.Range(0, int.MaxValue).ToString(), UFE.p1ControlsScript);
                UFE.FireAlert("UFE Alert " + UnityEngine.Random.Range(0, int.MaxValue).ToString(), UFE.p2ControlsScript);
            }
        }
    }
}

/*
 * 
 *  // In that case, we can process pause menu events
     //UFE.PauseGame(!UFE.IsPaused());

     if (player1CurrentStartButton && !player1PreviousStartButton)
     {
    UFE2FTE.UFE2FTEPlayerPausedManager.PlayerPaused = UFE2FTE.UFE2FTEPlayerPausedManager.Player.Player1Paused;
    UFE.PauseGame(!UFE.IsPaused());
}
     else if (player2CurrentStartButton && !player2PreviousStartButton)
{
    UFE2FTE.UFE2FTEPlayerPausedManager.PlayerPaused = UFE2FTE.UFE2FTEPlayerPausedManager.Player.Player2Paused;
    UFE.PauseGame(!UFE.IsPaused());
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
