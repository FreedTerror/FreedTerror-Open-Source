using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTECharacterAlertGUIController : MonoBehaviour
    {
        [Serializable]
        private class CharacterData
        {
            public CurrentFrameData currentFrameData;
            [Serializable]
            public class CurrentFrameDataOverrideOptions
            {
                public CurrentFrameData currentFrameData;
                public string[] moveNameArray;
            }       
            public bool armor;
            public int armorHitsRemaining;
            public bool invincibility;
            public bool standUp;

            public static void SetCharacterData(ControlsScript player, CharacterData characterData)
            {
                if (player == null
                    || characterData == null)
                {
                    return;
                }

                if (player.currentMove == null)
                {
                    characterData.currentFrameData = CurrentFrameData.Any;
                }
                else
                {
                    characterData.currentFrameData = player.currentMove.currentFrameData;
                }

                if (player.currentMove != null
                    && player.currentMove.armorOptions.hitAbsorption > 0
                    && player.currentMove.armorOptions.hitsTaken < player.currentMove.armorOptions.hitAbsorption
                    && player.currentMove.currentFrame >= player.currentMove.armorOptions.activeFramesBegin
                    && player.currentMove.currentFrame <= player.currentMove.armorOptions.activeFramesEnds)
                {
                    characterData.armor = true;

                    characterData.armorHitsRemaining = player.currentMove.armorOptions.hitAbsorption - player.currentMove.armorOptions.hitsTaken;
                }
                else if (player.currentMove == null
                    || player.currentMove.armorOptions.hitAbsorption <= 0
                    || player.currentMove.armorOptions.hitsTaken >= player.currentMove.armorOptions.hitAbsorption
                    || player.currentMove.currentFrame > player.currentMove.armorOptions.activeFramesEnds)
                {
                    characterData.armor = false;

                    characterData.armorHitsRemaining = 0;
                }

                if (player.currentMove != null)
                {
                    int length = player.currentMove.invincibleBodyParts.Length;

                    if (length <= 0)
                    {
                        characterData.invincibility = false;
                    }

                    for (int i = 0; i < length; i++)
                    {
                        if (player.currentMove.currentFrame >= player.currentMove.invincibleBodyParts[i].activeFramesBegin
                            && player.currentMove.currentFrame <= player.currentMove.invincibleBodyParts[i].activeFramesEnds
                            && player.currentMove.invincibleBodyParts[i].completelyInvincible == true)
                        {
                            characterData.invincibility = true;
                        }
                        else
                        {
                            characterData.invincibility = false;
                        }
                    }
                }
                else
                {
                    characterData.invincibility = false;
                }

                switch (player.currentBasicMove)
                {
                    case BasicMoveReference.StandUpDefault:
                    case BasicMoveReference.StandUpFromAirJuggle:
                    case BasicMoveReference.StandUpFromAirWallBounce:
                    case BasicMoveReference.StandUpFromCrumple:
                    case BasicMoveReference.StandUpFromGroundBounce:
                    case BasicMoveReference.StandUpFromKnockBack:
                    case BasicMoveReference.StandUpFromStandingHighHit:
                    case BasicMoveReference.StandUpFromStandingMidHit:
                    case BasicMoveReference.StandUpFromStandingWallBounce:
                    case BasicMoveReference.StandUpFromSweep:
                        characterData.standUp = true;
                        break;

                    default:
                        characterData.standUp = false;
                        break;
                }

                if (player.MoveSet.basicMoves.standUp.useMoveFile == true
                    && player.MoveSet.basicMoves.standUp.moveInfo != null
                    && player.currentMove != null
                    && player.currentMove.moveName == player.MoveSet.basicMoves.standUp.moveInfo.moveName)
                {
                    characterData.standUp = true;
                }
                else if (player.MoveSet.basicMoves.standUpFromAirHit.useMoveFile == true
                    && player.MoveSet.basicMoves.standUpFromAirHit.moveInfo != null
                    && player.currentMove != null
                    && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromAirHit.moveInfo.moveName)
                {
                    characterData.standUp = true;
                }
                else if (player.MoveSet.basicMoves.standUpFromAirWallBounce.useMoveFile == true
                    && player.MoveSet.basicMoves.standUpFromAirWallBounce.moveInfo != null
                    && player.currentMove != null
                    && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromAirWallBounce.moveInfo.moveName)
                {
                    characterData.standUp = true;
                }
                else if (player.MoveSet.basicMoves.standUpFromCrumple.useMoveFile == true
                    && player.MoveSet.basicMoves.standUpFromCrumple.moveInfo != null
                    && player.currentMove != null
                    && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromCrumple.moveInfo.moveName)
                {
                    characterData.standUp = true;
                }
                else if (player.MoveSet.basicMoves.standUpFromGroundBounce.useMoveFile == true
                    && player.MoveSet.basicMoves.standUpFromGroundBounce.moveInfo != null
                    && player.currentMove != null
                    && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromGroundBounce.moveInfo.moveName)
                {
                    characterData.standUp = true;
                }
                else if (player.MoveSet.basicMoves.standUpFromKnockBack.useMoveFile == true
                    && player.MoveSet.basicMoves.standUpFromKnockBack.moveInfo != null
                    && player.currentMove != null
                    && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromKnockBack.moveInfo.moveName)
                {
                    characterData.standUp = true;
                }
                else if (player.MoveSet.basicMoves.standUpFromStandingHighHit.useMoveFile == true
                    && player.MoveSet.basicMoves.standUpFromStandingHighHit.moveInfo != null
                    && player.currentMove != null
                    && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromStandingHighHit.moveInfo.moveName)
                {
                    characterData.standUp = true;
                }
                else if (player.MoveSet.basicMoves.standUpFromStandingMidHit.useMoveFile == true
                    && player.MoveSet.basicMoves.standUpFromStandingMidHit.moveInfo != null
                    && player.currentMove != null
                    && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromStandingMidHit.moveInfo.moveName)
                {
                    characterData.standUp = true;
                }
                else if (player.MoveSet.basicMoves.standUpFromStandingWallBounce.useMoveFile == true
                    && player.MoveSet.basicMoves.standUpFromStandingWallBounce.moveInfo != null
                    && player.currentMove != null
                    && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromStandingWallBounce.moveInfo.moveName)
                {
                    characterData.standUp = true;
                }
                else if (player.MoveSet.basicMoves.standUpFromSweep.useMoveFile == true
                    && player.MoveSet.basicMoves.standUpFromSweep.moveInfo != null
                    && player.currentMove != null
                    && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromSweep.moveInfo.moveName)
                {
                    characterData.standUp = true;
                }
                else if (player.currentMove == null)
                {
                    characterData.standUp = false;
                }
            }

            public static void SetCharacterDataCurrentFrameDataOverrideOptions(ControlsScript player, CharacterData characterData, CurrentFrameDataOverrideOptions currentFrameDataOverrideOptions)
            {
                if (player == null
                    || characterData == null
                    || currentFrameDataOverrideOptions == null)
                {
                    return;
                }

                if (currentFrameDataOverrideOptions.moveNameArray != null
                    && player.currentMove != null)
                {
                    int length = currentFrameDataOverrideOptions.moveNameArray.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (player.currentMove.moveName != currentFrameDataOverrideOptions.moveNameArray[i])
                        {
                            continue;
                        }

                        characterData.currentFrameData = currentFrameDataOverrideOptions.currentFrameData;

                        break;
                    }
                }
            }

            public static void SetCharacterDataCurrentFrameDataOverrideOptions(ControlsScript player, CharacterData characterData, CurrentFrameDataOverrideOptions[] currentFrameDataOverrideOptionsArray)
            {
                if (player == null
                    || characterData == null
                    || currentFrameDataOverrideOptionsArray == null)
                {
                    return;
                }

                int length = currentFrameDataOverrideOptionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    SetCharacterDataCurrentFrameDataOverrideOptions(player, characterData, currentFrameDataOverrideOptionsArray[i]);
                }
            }
        }
        private CharacterData player1CharacterData = new CharacterData();
        private CharacterData player2CharacterData = new CharacterData();

        private enum Player
        {
            Player1,
            Player2
        }
        [SerializeField]
        private Player player;

        [SerializeField]
        private float alertDuration;
        [SerializeField]
        private float alertStartYPosition;
        [SerializeField]
        private float alertYOffset;

        [Serializable]
        private class CharacterAlertGUI
        {
            public GameObject alertGameObject;
            public RectTransform alertRectTransform;
            public Text alertText;
            [HideInInspector]
            public float alertDurationElapsedTime;
        }
        [SerializeField]
        private CharacterAlertGUI[] characterAlertGUIArray;
        private List<CharacterAlertGUI> activeCharacterAlertGUIList = new List<CharacterAlertGUI>();

        [SerializeField]
        private bool useFirstHitAlertOnNewAlert = true;
        [SerializeField]
        private string firstHitAlertName = "FIRST HIT";
        [SerializeField]
        private Color32 firstHitAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private bool useCounterHitAlertOnNewAlert;
        [SerializeField]
        private bool useCounterHitAlertOnHit = true;
        [SerializeField]
        private string counterHitAlertName = "COUNTER HIT";
        [SerializeField]
        private Color32 counterHitAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private bool usePunishAlertOnHit = true;
        [SerializeField]
        private string punishAlertName = "PUNISH";
        [SerializeField]
        private Color32 punishAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private bool useWakeUpAlertOnMove = true;
        [SerializeField]
        private string wakeUpAlertName = "WAKE UP";
        [SerializeField]
        private Color32 wakeUpAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private bool useArmorAlertOnHit = true;
        [SerializeField]
        private string armorAlertName = "ARMORED HIT";
        [SerializeField]
        private Color32 armorAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private bool useContinuousArmorAlert = true;
        [SerializeField]
        private Color32 continuousArmorAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private bool useContinuousInvincibilityAlert = true;
        [SerializeField]
        private string continuousInvincibilityAlertName = "INVINCIBILITY";
        [SerializeField]
        private Color32 continuousInvincibilityAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private bool useArmorBreakerAlertOnHit = true;
        [SerializeField]
        private string armorBreakerAlertName = "ARMOR BREAKER";
        [SerializeField]
        private Color32 armorBreakerAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private bool useArmorBrokenAlertOnHit = true;
        [SerializeField]
        private string armorBrokenAlertName = "ARMOR BROKEN";
        [SerializeField]
        private Color32 armorBrokenAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private bool useUnblockableAlertOnHit = true;
        [SerializeField]
        private string unblockableAlertName = "UNBLOCKABLE";
        [SerializeField]
        private Color32 unblockableAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private bool useParryAlertOnParry = true;
        [SerializeField]
        private string parryAlertName = "PARRY";
        [SerializeField]
        private Color32 parryAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private bool useContinuousPotentialParryAlert = true;
        [SerializeField]
        private string potentialParryAlertName = "POTENTIAL PARRY";
        [SerializeField]
        private Color32 potentialParryAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private bool useContinuousJuggleAlert = true;
        [SerializeField]
        private Color32 juggleAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private bool useContinuousGroundBounceAlert = true;
        [SerializeField]
        private Color32 groundBounceAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private bool useContinuousWallBounceAlert = true;
        [SerializeField]
        private Color32 wallBounceAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private bool useBlockBreakAlertOnBlockBreak = true;
        [SerializeField]
        private string blockBreakAlertName = "BLOCK BREAK";
        [SerializeField]
        private Color32 blockBreakAlertColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private UFE2FTEGCFreeStringNumbersScriptableObject gCFreeStringNumbersScriptableObject;

        [Serializable]
        private class OnNewAlertData
        {
            public string[] alertNameArray;
            public Color32 alertColor = new Color32(255, 255, 255, 255);
            [Tooltip("Checks if an active alert with the same alert name is active. If one is found a new alert will not be created.")]
            public bool useCheckMatchingActiveAlert;
        }
        [SerializeField]
        private OnNewAlertData[] onNewAlertDataArray;

        [Serializable]
        private class BasicMoveAlertData
        {
            public string alertName;
            public Color32 alertColor = new Color32(255, 255, 255, 255);
            [Tooltip("Checks if an active alert with the same alert name is active. If one is found a new alert will not be created.")]
            public bool useCheckMatchingActiveAlert;
            public BasicMoveReference[] basicMoveArray;
        }
        [SerializeField]
        private BasicMoveAlertData[] basicMoveAlertDataOnBasicMoveArray;

        [Serializable]
        private class MoveNameAlertData
        {
            public string alertName;
            public Color32 alertColor = new Color32(255, 255, 255, 255);
            [Tooltip("Checks if an active alert with the same alert name is active. If one is found a new alert will not be created.")]
            public bool useCheckMatchingActiveAlert;
            public string[] moveNameArray;
        }
        [SerializeField]
        private MoveNameAlertData[] moveNameAlertDataOnMoveArray;

        [SerializeField]
        private CharacterData.CurrentFrameDataOverrideOptions[] currentFrameDataOverrideOptionsArray;

        private void OnEnable()
        {
            UFE2FTESoulsLikeFightingGameEventsManager.OnBlockBreak += OnBlockBreak;
            UFE.OnNewAlert += OnNewAlert;
            UFE.OnBasicMove += OnBasicMove;
            UFE.OnMove += OnMove;
            UFE.OnHit += OnHit;
            UFE.OnBlock += OnBlock;
            UFE.OnParry += OnParry;
        }

        private void Start()
        {
            ResetAllCharacterAlertGUI();
        }

        private void Update()
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null
                || gCFreeStringNumbersScriptableObject == null)
            {
                return;
            }

            float deltaTime = (float)UFE.fixedDeltaTime;

            CharacterData.SetCharacterData(UFE.GetPlayer1ControlsScript(), player1CharacterData);

            CharacterData.SetCharacterData(UFE.GetPlayer2ControlsScript(), player2CharacterData);

            CharacterData.SetCharacterDataCurrentFrameDataOverrideOptions(UFE.GetPlayer1ControlsScript(), player1CharacterData, currentFrameDataOverrideOptionsArray);

            CharacterData.SetCharacterDataCurrentFrameDataOverrideOptions(UFE.GetPlayer2ControlsScript(), player2CharacterData, currentFrameDataOverrideOptionsArray);

            SetContinuousInvincibilityAlert();

            SetContinuousArmorAlert();

            SetContinuousPotentialParryAlert();

            SetContinuousWallBounceAlert();

            SetContinuousGroundBounceAlert();

            SetContinuousJuggleAlert();

            SetCharacterAlertGUI(deltaTime);
        }

        private void OnDisable()
        {
            UFE2FTESoulsLikeFightingGameEventsManager.OnBlockBreak -= OnBlockBreak;
            UFE.OnNewAlert -= OnNewAlert;
            UFE.OnBasicMove -= OnBasicMove;
            UFE.OnMove -= OnMove;
            UFE.OnHit -= OnHit;
            UFE.OnBlock -= OnBlock;
            UFE.OnParry -= OnParry;
        }

        private void OnBlockBreak(ControlsScript player)
        {
            StartBlockBreakAlertOnBlockBreak(player);
        }

        private void OnNewAlert(string newString, ControlsScript player)
        {
            StartCounterHitAlertOnNewAlert(player, newString);

            StartFirstHitAlertOnNewAlert(player, newString);

            StartOnNewAlertByMatchingAlertName(player, newString);
        }

        private void OnBasicMove(BasicMoveReference basicMove, ControlsScript player)
        {
            StartBasicMoveAlertOnBasicMove(player, basicMove);
        }

        private void OnMove(MoveInfo move, ControlsScript player)
        {
            if (player.currentMove == null)
            {
                return;
            }

            StartWakeUpAlertOnMove(player, move.moveName);

            StartMoveNameAlertOnMove(player, move.moveName);
        }

        private void OnHit(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            StartUnblockableAlertOnHit(player, hitInfo);

            StartArmorAlertOnHit(player, hitInfo);

            StartArmorBrokenAlertOnHit(player, hitInfo);

            StartArmorBreakerAlertOnHit(player, hitInfo);

            StartPunishAlertOnHit(player);

            StartCounterHitAlertOnHit(player);
        }

        private void OnBlock(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {

        }

        private void OnParry(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            StartParryAlertOnParry(player);
        }

        #region Character Alert GUI

        private void StartCharacterAlertGUI(string textMessage, Color32 textColor, string matchingString = null, string[] matchingStringArray = null)
        {
            if (matchingString != null)
            {
                int count1 = activeCharacterAlertGUIList.Count;
                for (int i = 0; i < count1; i++)
                {
                    if (activeCharacterAlertGUIList[i].alertText == null
                        || activeCharacterAlertGUIList[i].alertText.text != matchingString)
                    {
                        continue;
                    }

                    activeCharacterAlertGUIList[i].alertText.text = textMessage;

                    activeCharacterAlertGUIList[i].alertText.color = textColor;

                    activeCharacterAlertGUIList[i].alertDurationElapsedTime = 0;

                    return;
                }
            }

            if (matchingStringArray != null)
            {
                int count1 = activeCharacterAlertGUIList.Count;
                for (int i = 0; i < count1; i++)
                {
                    if (activeCharacterAlertGUIList[i].alertText == null)
                    {
                        continue;
                    }

                    int lengthA = matchingStringArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (activeCharacterAlertGUIList[i].alertText.text != matchingStringArray[a])
                        {
                            continue;
                        }

                        activeCharacterAlertGUIList[i].alertText.text = textMessage;

                        activeCharacterAlertGUIList[i].alertText.color = textColor;

                        activeCharacterAlertGUIList[i].alertDurationElapsedTime = 0;

                        return;
                    }
                }
            }

            int length = characterAlertGUIArray.Length;
            int count = activeCharacterAlertGUIList.Count;

            if (length == count)
            {
                ResetCharacterAlertGUI(activeCharacterAlertGUIList[count - 1]);

                RemoveFromActiveCharacterAlertGUIList(activeCharacterAlertGUIList[count - 1]);

                //SetCharacterAlertGUIPositions();
            }

            for (int i = 0; i < length; i++)
            {
                if (characterAlertGUIArray[i].alertGameObject == null
                    || characterAlertGUIArray[i].alertGameObject.activeInHierarchy == true)
                {
                    continue;
                }

                characterAlertGUIArray[i].alertGameObject.SetActive(true);

                characterAlertGUIArray[i].alertText.text = textMessage;

                characterAlertGUIArray[i].alertText.color = textColor;

                characterAlertGUIArray[i].alertDurationElapsedTime = 0;

                AddToActiveCharacterAlertGUIList(characterAlertGUIArray[i]);

                break;
            }
        }

        private void SetCharacterAlertGUI(float deltaTime)
        {
            int length = characterAlertGUIArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (player == Player.Player1
                    && characterAlertGUIArray[i].alertText != null)
                {
                    if (UFE.GetPlayer1ControlsScript().comboHits <= 0)
                    {
                        if (characterAlertGUIArray[i].alertText.text == armorBrokenAlertName)
                        {
                            ResetCharacterAlertGUI(characterAlertGUIArray[i]);
                        }
                    }

                    if (UFE.GetPlayer2ControlsScript().comboHits <= 0)
                    {
                        if (characterAlertGUIArray[i].alertText.text == firstHitAlertName)
                        {
                            ResetCharacterAlertGUI(characterAlertGUIArray[i]);
                        }
                        else if (characterAlertGUIArray[i].alertText.text == counterHitAlertName)
                        {
                            ResetCharacterAlertGUI(characterAlertGUIArray[i]);
                        }
                        else if (characterAlertGUIArray[i].alertText.text == punishAlertName)
                        {
                            ResetCharacterAlertGUI(characterAlertGUIArray[i]);
                        }
                        else if (characterAlertGUIArray[i].alertText.text == armorBreakerAlertName)
                        {
                            ResetCharacterAlertGUI(characterAlertGUIArray[i]);
                        }
                        else if (characterAlertGUIArray[i].alertText.text == unblockableAlertName)
                        {
                            ResetCharacterAlertGUI(characterAlertGUIArray[i]);
                        }
                    }
                }
                else if (player == Player.Player2
                    && characterAlertGUIArray[i].alertText != null)
                {
                    if (UFE.GetPlayer1ControlsScript().comboHits <= 0)
                    {
                        if (characterAlertGUIArray[i].alertText.text == firstHitAlertName)
                        {
                            ResetCharacterAlertGUI(characterAlertGUIArray[i]);
                        }
                        else if (characterAlertGUIArray[i].alertText.text == counterHitAlertName)
                        {
                            ResetCharacterAlertGUI(characterAlertGUIArray[i]);
                        }
                        else if (characterAlertGUIArray[i].alertText.text == punishAlertName)
                        {
                            ResetCharacterAlertGUI(characterAlertGUIArray[i]);
                        }
                        else if (characterAlertGUIArray[i].alertText.text == armorBreakerAlertName)
                        {
                            ResetCharacterAlertGUI(characterAlertGUIArray[i]);
                        }
                        else if (characterAlertGUIArray[i].alertText.text == unblockableAlertName)
                        {
                            ResetCharacterAlertGUI(characterAlertGUIArray[i]);
                        }
                    }

                    if (UFE.GetPlayer2ControlsScript().comboHits <= 0)
                    {
                        if (characterAlertGUIArray[i].alertText.text == armorBrokenAlertName)
                        {
                            ResetCharacterAlertGUI(characterAlertGUIArray[i]);
                        }
                    }
                }

                if (characterAlertGUIArray[i].alertDurationElapsedTime < alertDuration)
                {
                    characterAlertGUIArray[i].alertDurationElapsedTime += deltaTime;

                    if (characterAlertGUIArray[i].alertGameObject != null)
                    {
                        characterAlertGUIArray[i].alertGameObject.SetActive(true);
                    }
                }
                else
                {
                    if (characterAlertGUIArray[i].alertGameObject != null)
                    {
                        characterAlertGUIArray[i].alertGameObject.SetActive(false);
                    }
                }

                if (characterAlertGUIArray[i].alertGameObject != null)
                {
                    if (characterAlertGUIArray[i].alertGameObject.activeInHierarchy == true)
                    {
                        AddToActiveCharacterAlertGUIList(characterAlertGUIArray[i]);
                    }
                    else
                    {
                        RemoveFromActiveCharacterAlertGUIList(characterAlertGUIArray[i]);
                    }
                }

                SetCharacterAlertGUIPositions();
            }
        }

        private void SetCharacterAlertGUIPositions()
        {
            int count = activeCharacterAlertGUIList.Count;
            for (int i = 0; i < count; i++)
            {
                if (activeCharacterAlertGUIList[i].alertRectTransform == null)
                {
                    continue;
                }

                activeCharacterAlertGUIList[i].alertRectTransform.anchoredPosition = new Vector2(activeCharacterAlertGUIList[i].alertRectTransform.anchoredPosition.x, alertStartYPosition + alertYOffset * i);
            }
        }

        private void AddToActiveCharacterAlertGUIList(CharacterAlertGUI characterAlertGUI)
        {
            if (characterAlertGUI == null)
            {
                return;
            }

            int count = activeCharacterAlertGUIList.Count;
            for (int i = 0; i < count; i++)
            {
                if (characterAlertGUI == activeCharacterAlertGUIList[i])
                {
                    return;
                }
            }

            activeCharacterAlertGUIList.Insert(0, characterAlertGUI);
        }

        private void RemoveFromActiveCharacterAlertGUIList(CharacterAlertGUI characterAlertGUI)
        {
            if (characterAlertGUI == null)
            {
                return;
            }

            int count = activeCharacterAlertGUIList.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                if (characterAlertGUI != activeCharacterAlertGUIList[i])
                {
                    continue;
                }

                activeCharacterAlertGUIList.RemoveAt(i);
            }
        }

        private void ResetCharacterAlertGUI(CharacterAlertGUI characterAlertGUI)
        {
            if (characterAlertGUI == null)
            {
                return;
            }

            if (characterAlertGUI.alertGameObject != null)
            {
                characterAlertGUI.alertGameObject.SetActive(false);
            }

            if (characterAlertGUI.alertText != null)
            {
                characterAlertGUI.alertText.text = "";
            }

            characterAlertGUI.alertDurationElapsedTime = alertDuration;
        }

        private void ResetAllCharacterAlertGUI()
        {
            int length = characterAlertGUIArray.Length;
            for (int i = 0; i < length; i++)
            {
                ResetCharacterAlertGUI(characterAlertGUIArray[i]);
            }
        }

        private void ResetCharacterAlertGUIByMatchingString(string matchingString)
        {
            int length = characterAlertGUIArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterAlertGUIArray[i].alertText == null
                    || characterAlertGUIArray[i].alertText.text != matchingString)
                {
                    continue;
                }
                
                ResetCharacterAlertGUI(characterAlertGUIArray[i]);
            }
        }

        private void ResetCharacterAlertGUIByMatchingString(string[] matchingStringArray)
        {
            if (matchingStringArray == null)
            {
                return;
            }

            int length = characterAlertGUIArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (characterAlertGUIArray[i].alertText == null)
                {
                    continue;
                }

                int lengthA = matchingStringArray.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (characterAlertGUIArray[i].alertText.text != matchingStringArray[a])
                    {
                        continue;
                    }

                    ResetCharacterAlertGUI(characterAlertGUIArray[i]);
                }
            }
        }

        #endregion

        #region On Block Break Alert Methods

        private void StartBlockBreakAlertOnBlockBreak(ControlsScript player)
        {
            if (useBlockBreakAlertOnBlockBreak == false
                || player == null)
            {
                return;
            }

            if (this.player == Player.Player1
                && player == UFE.GetPlayer1ControlsScript())
            {
                StartCharacterAlertGUI(blockBreakAlertName, blockBreakAlertColor);
            }
            else if (this.player == Player.Player2
                && player == UFE.GetPlayer2ControlsScript())
            {
                StartCharacterAlertGUI(blockBreakAlertName, blockBreakAlertColor);
            }
        }

        #endregion

        #region On New Alert Methods

        private void StartOnNewAlertByMatchingAlertName(ControlsScript player, string matchingAlertName)
        {
            if (player == null)
            {
                return;
            }

            if (this.player == Player.Player1
                && player == UFE.GetPlayer1ControlsScript())
            {
                int length = onNewAlertDataArray.Length;
                for (int i = 0; i < length; i++)
                {
                    int lengthA = onNewAlertDataArray[i].alertNameArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (matchingAlertName != onNewAlertDataArray[i].alertNameArray[a])
                        {
                            continue;
                        }

                        if (onNewAlertDataArray[i].useCheckMatchingActiveAlert == true)
                        {
                            StartCharacterAlertGUI(onNewAlertDataArray[i].alertNameArray[a], onNewAlertDataArray[i].alertColor, onNewAlertDataArray[i].alertNameArray[a]);
                        }
                        else
                        {
                            StartCharacterAlertGUI(onNewAlertDataArray[i].alertNameArray[a], onNewAlertDataArray[i].alertColor);
                        }

                        break;
                    }
                }
            }
            else if (this.player == Player.Player2
                && player == UFE.GetPlayer2ControlsScript())
            {
                int length = onNewAlertDataArray.Length;
                for (int i = 0; i < length; i++)
                {
                    int lengthA = onNewAlertDataArray[i].alertNameArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (matchingAlertName != onNewAlertDataArray[i].alertNameArray[a])
                        {
                            continue;
                        }

                        if (onNewAlertDataArray[i].useCheckMatchingActiveAlert == true)
                        {
                            StartCharacterAlertGUI(onNewAlertDataArray[i].alertNameArray[a], onNewAlertDataArray[i].alertColor, onNewAlertDataArray[i].alertNameArray[a]);
                        }
                        else
                        {
                            StartCharacterAlertGUI(onNewAlertDataArray[i].alertNameArray[a], onNewAlertDataArray[i].alertColor);
                        }

                        break;
                    }
                }
            }
        }

        private void StartFirstHitAlertOnNewAlert(ControlsScript player, string firstHitMessage)
        {
            if (useFirstHitAlertOnNewAlert == false
                || player == null
                || firstHitMessage != UFE.config.selectedLanguage.firstHit)
            {
                return;
            }

            if (this.player == Player.Player1
                && player == UFE.GetPlayer1ControlsScript())
            {
                StartCharacterAlertGUI(firstHitAlertName, firstHitAlertColor);
            }
            else if (this.player == Player.Player2
                && player == UFE.GetPlayer2ControlsScript())
            {
                StartCharacterAlertGUI(firstHitAlertName, firstHitAlertColor);
            }
        }

        private void StartCounterHitAlertOnNewAlert(ControlsScript player, string counterHitMessage)
        {
            if (useCounterHitAlertOnNewAlert == false
                || player == null
                || counterHitMessage != UFE.config.selectedLanguage.counterHit)
            {
                return;
            }

            if (this.player == Player.Player1
                && player == UFE.GetPlayer1ControlsScript())
            {
                StartCharacterAlertGUI(counterHitAlertName, counterHitAlertColor);
            }
            else if (this.player == Player.Player2
                && player == UFE.GetPlayer2ControlsScript())
            {
                StartCharacterAlertGUI(counterHitAlertName, counterHitAlertColor);
            }
        }

        #endregion

        #region On Basic Move Alert Methods

        private void StartBasicMoveAlertOnBasicMove(ControlsScript player, BasicMoveReference basicMove)
        {
            if (player == null)
            {
                return;
            }

            if (this.player == Player.Player1
                && player == UFE.GetPlayer1ControlsScript())
            {
                int length = basicMoveAlertDataOnBasicMoveArray.Length;
                for (int i = 0; i < length; i++)
                {
                    int lengthA = basicMoveAlertDataOnBasicMoveArray[i].basicMoveArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (basicMove != basicMoveAlertDataOnBasicMoveArray[i].basicMoveArray[a])
                        {
                            continue;
                        }

                        if (basicMoveAlertDataOnBasicMoveArray[i].useCheckMatchingActiveAlert == true)
                        {
                            StartCharacterAlertGUI(basicMoveAlertDataOnBasicMoveArray[i].alertName, basicMoveAlertDataOnBasicMoveArray[i].alertColor, basicMoveAlertDataOnBasicMoveArray[i].alertName);
                        }
                        else
                        {
                            StartCharacterAlertGUI(basicMoveAlertDataOnBasicMoveArray[i].alertName, basicMoveAlertDataOnBasicMoveArray[i].alertColor);
                        }

                        break;
                    }
                }
            }
            else if (this.player == Player.Player2
                && player == UFE.GetPlayer2ControlsScript())
            {
                int length = basicMoveAlertDataOnBasicMoveArray.Length;
                for (int i = 0; i < length; i++)
                {
                    int lengthA = basicMoveAlertDataOnBasicMoveArray[i].basicMoveArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (basicMove != basicMoveAlertDataOnBasicMoveArray[i].basicMoveArray[a])
                        {
                            continue;
                        }

                        if (basicMoveAlertDataOnBasicMoveArray[i].useCheckMatchingActiveAlert == true)
                        {
                            StartCharacterAlertGUI(basicMoveAlertDataOnBasicMoveArray[i].alertName, basicMoveAlertDataOnBasicMoveArray[i].alertColor, basicMoveAlertDataOnBasicMoveArray[i].alertName);
                        }
                        else
                        {
                            StartCharacterAlertGUI(basicMoveAlertDataOnBasicMoveArray[i].alertName, basicMoveAlertDataOnBasicMoveArray[i].alertColor);
                        }

                        break;
                    }
                }
            }
        }

        #endregion

        #region On Move Alert Methods

        private void StartMoveNameAlertOnMove(ControlsScript player, string moveName)
        {
            if (player == null)
            {
                return;
            }

            if (this.player == Player.Player1
                && player == UFE.GetPlayer1ControlsScript())
            {
                int length = moveNameAlertDataOnMoveArray.Length;
                for (int i = 0; i < length; i++)
                {
                    int lengthA = moveNameAlertDataOnMoveArray[i].moveNameArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (moveName != moveNameAlertDataOnMoveArray[i].moveNameArray[a])
                        {
                            continue;
                        }

                        if (moveNameAlertDataOnMoveArray[i].useCheckMatchingActiveAlert == true)
                        {
                            StartCharacterAlertGUI(moveNameAlertDataOnMoveArray[i].alertName, moveNameAlertDataOnMoveArray[i].alertColor, moveNameAlertDataOnMoveArray[i].alertName);
                        }
                        else
                        {
                            StartCharacterAlertGUI(moveNameAlertDataOnMoveArray[i].alertName, moveNameAlertDataOnMoveArray[i].alertColor);
                        }

                        break;
                    }
                }
            }
            else if (this.player == Player.Player2
                && player == UFE.GetPlayer2ControlsScript())
            {
                int length = moveNameAlertDataOnMoveArray.Length;
                for (int i = 0; i < length; i++)
                {
                    int lengthA = moveNameAlertDataOnMoveArray[i].moveNameArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (moveName != moveNameAlertDataOnMoveArray[i].moveNameArray[a])
                        {
                            continue;
                        }

                        if (moveNameAlertDataOnMoveArray[i].useCheckMatchingActiveAlert == true)
                        {
                            StartCharacterAlertGUI(moveNameAlertDataOnMoveArray[i].alertName, moveNameAlertDataOnMoveArray[i].alertColor, moveNameAlertDataOnMoveArray[i].alertName);
                        }
                        else
                        {
                            StartCharacterAlertGUI(moveNameAlertDataOnMoveArray[i].alertName, moveNameAlertDataOnMoveArray[i].alertColor);
                        }

                        break;
                    }
                }
            }
        }

        private void StartWakeUpAlertOnMove(ControlsScript player, string moveName)
        {
            if (useWakeUpAlertOnMove == false
                && player == null)
            {
                return;
            }

            if (this.player == Player.Player1
                && player == UFE.GetPlayer1ControlsScript()
                && player1CharacterData.standUp == true)
            {
                StartCharacterAlertGUI(wakeUpAlertName, wakeUpAlertColor);
            }
            else if (this.player == Player.Player2
                && player == UFE.GetPlayer2ControlsScript()
                && player2CharacterData.standUp == true)
            {
                StartCharacterAlertGUI(wakeUpAlertName, wakeUpAlertColor);
            }
        }

        #endregion

        #region On Hit Alert Methods

        private void StartCounterHitAlertOnHit(ControlsScript player)
        {
            if (useCounterHitAlertOnHit == false
                || player == null)
            {
                return;
            }

            if (this.player == Player.Player1
                && player == UFE.GetPlayer1ControlsScript()
                && player2CharacterData.currentFrameData == CurrentFrameData.StartupFrames
                && player2CharacterData.armor == false)
            {
                StartCharacterAlertGUI(counterHitAlertName, counterHitAlertColor);
            }
            else if (this.player == Player.Player2
                && player == UFE.GetPlayer2ControlsScript()
                && player1CharacterData.currentFrameData == CurrentFrameData.StartupFrames
                && player1CharacterData.armor == false)
            {
                StartCharacterAlertGUI(counterHitAlertName, counterHitAlertColor);
            }
        }

        private void StartPunishAlertOnHit(ControlsScript player)
        {
            if (usePunishAlertOnHit == false
                || player == null)
            {
                return;
            }

            if (this.player == Player.Player1
                && player == UFE.GetPlayer1ControlsScript()
                && player2CharacterData.currentFrameData == CurrentFrameData.RecoveryFrames
                && player2CharacterData.armor == false)
            {
                StartCharacterAlertGUI(punishAlertName, punishAlertColor);
            }
            else if (this.player == Player.Player2
                && player == UFE.GetPlayer2ControlsScript()
                && player1CharacterData.currentFrameData == CurrentFrameData.RecoveryFrames
                && player1CharacterData.armor == false)
            {
                StartCharacterAlertGUI(punishAlertName, punishAlertColor);
            }
        }

        private void StartArmorAlertOnHit(ControlsScript player, Hit hitInfo)
        {
            if (useArmorAlertOnHit == false
                || player == null
                || hitInfo.armorBreaker == true)
            {
                return;
            }

            if (this.player == Player.Player1
                && player == UFE.GetPlayer2ControlsScript()
                && player1CharacterData.armor == true)
            {
                StartCharacterAlertGUI(armorAlertName, armorAlertColor);
            }
            else if (this.player == Player.Player2
                && player == UFE.GetPlayer1ControlsScript()
                && player2CharacterData.armor == true)
            {
                StartCharacterAlertGUI(armorAlertName, armorAlertColor);
            }
        }

        private void StartArmorBreakerAlertOnHit(ControlsScript player, Hit hitInfo)
        {
            if (useArmorBreakerAlertOnHit == false
                || player == null
                || hitInfo.armorBreaker == false)
            {
                return;
            }

            if (this.player == Player.Player1
                && player == UFE.GetPlayer1ControlsScript()
                && UFE.GetPlayer2ControlsScript().comboHits <= 0)
            {
                StartCharacterAlertGUI(armorBreakerAlertName, armorBreakerAlertColor);
            }
            else if (this.player == Player.Player2
                && player == UFE.GetPlayer2ControlsScript()
                && UFE.GetPlayer1ControlsScript().comboHits <= 0)
            {
                StartCharacterAlertGUI(armorBreakerAlertName, armorBreakerAlertColor);
            }
        }

        private void StartArmorBrokenAlertOnHit(ControlsScript player, Hit hitInfo)
        {
            if (useArmorBrokenAlertOnHit == false
                || player == null
                || hitInfo.armorBreaker == false)
            {
                return;
            }

            if (this.player == Player.Player1
                && player == UFE.GetPlayer2ControlsScript()
                && player1CharacterData.armor == true)
            {
                StartCharacterAlertGUI(armorBrokenAlertName, armorBrokenAlertColor);
            }
            else if (this.player == Player.Player2
                && player == UFE.GetPlayer1ControlsScript()
                && player2CharacterData.armor == true)
            {
                StartCharacterAlertGUI(armorBrokenAlertName, armorBrokenAlertColor);
            }
        }

        private void StartUnblockableAlertOnHit(ControlsScript player, Hit hitInfo)
        {
            if (useUnblockableAlertOnHit == false
                || player == null
                || hitInfo.unblockable == false)
            {
                return;
            }

            if (this.player == Player.Player1
                && player == UFE.GetPlayer1ControlsScript()
                && UFE.GetPlayer2ControlsScript().comboHits <= 0)
            {
                StartCharacterAlertGUI(unblockableAlertName, unblockableAlertColor);
            }
            else if (this.player == Player.Player2
                && player == UFE.GetPlayer2ControlsScript()
                && UFE.GetPlayer1ControlsScript().comboHits <= 0)
            {
                StartCharacterAlertGUI(unblockableAlertName, unblockableAlertColor);
            }
        }

        #endregion

        #region On Parry Alert Methods

        private void StartParryAlertOnParry(ControlsScript player)
        {
            if (useParryAlertOnParry == false
                || player == null)
            {
                return;
            }

            if (this.player == Player.Player1
                && player == UFE.GetPlayer1ControlsScript())
            {
                StartCharacterAlertGUI(parryAlertName, parryAlertColor);
            }
            else if (this.player == Player.Player2
                && player == UFE.GetPlayer2ControlsScript())
            {
                StartCharacterAlertGUI(parryAlertName, parryAlertColor);
            }
        }

        #endregion

        #region Continuous Alert Methods

        private void SetContinuousArmorAlert()
        {
            if (useContinuousArmorAlert == false)
            {
                return;
            }

            if (player == Player.Player1)
            {
                if (player1CharacterData.armor == true)
                {
                    StartCharacterAlertGUI(UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.armorHitsRemainingStringNumberArray, player1CharacterData.armorHitsRemaining), continuousArmorAlertColor, null, gCFreeStringNumbersScriptableObject.armorHitsRemainingStringNumberArray);
                }
                else
                {
                    ResetCharacterAlertGUIByMatchingString(gCFreeStringNumbersScriptableObject.armorHitsRemainingStringNumberArray);
                }
            }
            else if (player == Player.Player2)
            {
                if (player2CharacterData.armor == true)
                {
                    StartCharacterAlertGUI(UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.armorHitsRemainingStringNumberArray, player2CharacterData.armorHitsRemaining), continuousArmorAlertColor, null, gCFreeStringNumbersScriptableObject.armorHitsRemainingStringNumberArray);
                }
                else
                {
                    ResetCharacterAlertGUIByMatchingString(gCFreeStringNumbersScriptableObject.armorHitsRemainingStringNumberArray);
                }
            }
        }

        private void SetContinuousInvincibilityAlert()
        {
            if (useContinuousInvincibilityAlert == false)
            {
                return;
            }

            if (player == Player.Player1)
            {
                if (player1CharacterData.invincibility == true)
                {
                    StartCharacterAlertGUI(continuousInvincibilityAlertName, continuousInvincibilityAlertColor, continuousInvincibilityAlertName);
                }
                else
                {
                    ResetCharacterAlertGUIByMatchingString(continuousInvincibilityAlertName);
                }
            }
            else if (player == Player.Player2)
            {
                if (player2CharacterData.invincibility == true)
                {
                    StartCharacterAlertGUI(continuousInvincibilityAlertName, continuousInvincibilityAlertColor, continuousInvincibilityAlertName);
                }
                else
                {
                    ResetCharacterAlertGUIByMatchingString(continuousInvincibilityAlertName);
                }
            }
        }

        private void SetContinuousPotentialParryAlert()
        {
            if (useContinuousPotentialParryAlert == false)
            {
                return;
            }

            if (player == Player.Player1)
            {
                if (UFE.GetPlayer1ControlsScript().potentialParry > 0)
                {
                    StartCharacterAlertGUI(potentialParryAlertName, potentialParryAlertColor, potentialParryAlertName);
                }
                else
                {
                    ResetCharacterAlertGUIByMatchingString(potentialParryAlertName);
                }
            }
            else if (player == Player.Player2)
            {
                if (UFE.GetPlayer2ControlsScript().potentialParry > 0)
                {
                    StartCharacterAlertGUI(potentialParryAlertName, potentialParryAlertColor, potentialParryAlertName);
                }
                else
                {
                    ResetCharacterAlertGUIByMatchingString(potentialParryAlertName);
                }
            }
        }

        private void SetContinuousJuggleAlert()
        {
            if (useContinuousJuggleAlert == false)
            {
                return;
            }

            if (player == Player.Player1)
            {
                if (UFE.GetPlayer2ControlsScript().comboHits > 0
                    && UFE.GetPlayer2ControlsScript().airJuggleHits > 0)
                {
                    StartCharacterAlertGUI(UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.juggleStringNumberArray, UFE.GetPlayer2ControlsScript().airJuggleHits), juggleAlertColor, null, gCFreeStringNumbersScriptableObject.juggleStringNumberArray);
                }
                else if (UFE.GetPlayer2ControlsScript().comboHits <= 0)
                {
                    ResetCharacterAlertGUIByMatchingString(gCFreeStringNumbersScriptableObject.juggleStringNumberArray);
                }
            }
            else if (player == Player.Player2)
            {
                if (UFE.GetPlayer1ControlsScript().comboHits > 0
                    && UFE.GetPlayer1ControlsScript().airJuggleHits > 0)
                {
                    StartCharacterAlertGUI(UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.juggleStringNumberArray, UFE.GetPlayer1ControlsScript().airJuggleHits), juggleAlertColor, null, gCFreeStringNumbersScriptableObject.juggleStringNumberArray);
                }
                else if (UFE.GetPlayer1ControlsScript().comboHits <= 0)
                {
                    ResetCharacterAlertGUIByMatchingString(gCFreeStringNumbersScriptableObject.juggleStringNumberArray);
                }
            }
        }

        private void SetContinuousGroundBounceAlert()
        {
            if (useContinuousGroundBounceAlert == false)
            {
                return;
            }

            if (player == Player.Player1)
            {
                if (UFE.GetPlayer2ControlsScript().comboHits > 0
                    && UFE.GetPlayer2ControlsScript().Physics.groundBounceTimes > 0)
                {
                    StartCharacterAlertGUI(UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.groundBounceStringNumberArray, UFE.GetPlayer2ControlsScript().Physics.groundBounceTimes), groundBounceAlertColor, null, gCFreeStringNumbersScriptableObject.groundBounceStringNumberArray);
                }
                else if (UFE.GetPlayer2ControlsScript().Physics.groundBounceTimes <= 0)
                {
                    ResetCharacterAlertGUIByMatchingString(gCFreeStringNumbersScriptableObject.groundBounceStringNumberArray);
                }
            }
            else if (player == Player.Player2)
            {
                if (UFE.GetPlayer1ControlsScript().comboHits > 0
                    && UFE.GetPlayer1ControlsScript().Physics.groundBounceTimes > 0)
                {
                    StartCharacterAlertGUI(UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.groundBounceStringNumberArray, UFE.GetPlayer1ControlsScript().Physics.groundBounceTimes), groundBounceAlertColor, null, gCFreeStringNumbersScriptableObject.groundBounceStringNumberArray);
                }
                else if (UFE.GetPlayer1ControlsScript().Physics.groundBounceTimes <= 0)
                {
                    ResetCharacterAlertGUIByMatchingString(gCFreeStringNumbersScriptableObject.groundBounceStringNumberArray);
                }
            }
        }

        private void SetContinuousWallBounceAlert()
        {
            if (useContinuousWallBounceAlert == false)
            {
                return;
            }

            if (player == Player.Player1)
            {
                if (UFE.GetPlayer2ControlsScript().comboHits > 0
                    && UFE.GetPlayer2ControlsScript().Physics.wallBounceTimes > 0)
                {
                    StartCharacterAlertGUI(UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.wallBounceStringNumberArray, UFE.GetPlayer2ControlsScript().Physics.wallBounceTimes), wallBounceAlertColor, null, gCFreeStringNumbersScriptableObject.wallBounceStringNumberArray);
                }
                else if (UFE.GetPlayer2ControlsScript().Physics.wallBounceTimes <= 0)
                {
                    ResetCharacterAlertGUIByMatchingString(gCFreeStringNumbersScriptableObject.wallBounceStringNumberArray);
                }
            }
            else if (player == Player.Player2)
            {
                if (UFE.GetPlayer1ControlsScript().comboHits > 0
                    && UFE.GetPlayer1ControlsScript().Physics.wallBounceTimes > 0)
                {
                    StartCharacterAlertGUI(UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.wallBounceStringNumberArray, UFE.GetPlayer1ControlsScript().Physics.wallBounceTimes), wallBounceAlertColor, null, gCFreeStringNumbersScriptableObject.wallBounceStringNumberArray);
                }
                else if (UFE.GetPlayer1ControlsScript().Physics.wallBounceTimes <= 0)
                {
                    ResetCharacterAlertGUIByMatchingString(gCFreeStringNumbersScriptableObject.wallBounceStringNumberArray);
                }
            }
        }

        #endregion
    }
}