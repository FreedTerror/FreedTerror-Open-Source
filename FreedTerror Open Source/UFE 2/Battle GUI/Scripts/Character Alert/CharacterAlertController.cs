using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FPLibrary;
using UFE3D;

namespace FreedTerror.UFE2
{
    public class CharacterAlertController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Fix64 alertDuration = 1;
        [SerializeField]
        private bool allowUFEAlerts;
        [SerializeField]
        private bool useWakeUpAlertOnMove = true;
        [SerializeField]
        private bool useCounterHitAlertOnHit = true;
        [SerializeField]
        private bool usePunishAlertOnHit = true;
        [SerializeField]
        private bool useArmorAlertOnHit = true;
        [SerializeField]
        private bool useArmorBreakerAlertOnHit = true;
        [SerializeField]
        private bool useArmorBrokenAlertOnHit = true;
        [SerializeField]
        private bool useUnblockableAlertOnHit = true;
        [SerializeField]
        private bool useForceStandAlertOnHit = true;
        [SerializeField]
        private bool useOtgAlertOnHit = true;
        [SerializeField]
        private bool useParryAlertOnParry = true;

        [System.Serializable]
        public class CharacterAlert
        {
            public GameObject alertGameObject;
            public Transform alertTransform;
            public Text alertText;
            [HideInInspector]
            public Fix64 alertDurationElapsedTime;
        }
        [SerializeField]
        private List<CharacterAlert> characterAlertList = new List<CharacterAlert>();

        private CharacterData player1CharacterData = new CharacterData();
        private CharacterData player2CharacterData = new CharacterData();
        public CharacterData GetCharacterData(UFE2Manager.Player player)
        {
            if (player == UFE2Manager.Player.Player1)
            {
                return player1CharacterData;
            }
            else if (player == UFE2Manager.Player.Player2)
            {
                return player2CharacterData;
            }

            return null;
        }
        [SerializeField]
        private CharacterData.OverrideOptions characterDataOverrideOptions = new CharacterData.OverrideOptions();

        private void OnEnable()
        {
            UFE.OnNewAlert += OnNewAlert;
            UFE.OnMove += OnMove;
            UFE.OnHit += OnHit;
            UFE.OnParry += OnParry;
        }

        private void Start()
        {
            ResetCharacterAlerts();
        }

        private void Update()
        {
            CharacterData.SetCharacterData(UFE.p1ControlsScript, player1CharacterData, player2CharacterData, characterDataOverrideOptions);
            CharacterData.SetCharacterData(UFE.p2ControlsScript, player2CharacterData, player1CharacterData, characterDataOverrideOptions);

            Fix64 deltaTime = UFE.fixedDeltaTime;

            UpdateCharacterAlerts(deltaTime);
        }

        private void OnDisable()
        {
            UFE.OnNewAlert -= OnNewAlert;
            UFE.OnMove -= OnMove;
            UFE.OnHit -= OnHit;
            UFE.OnParry -= OnParry;
        }

        #region Character Alert Methods

        private void StartCharacterAlert(string message)
        {
            if (message == "")
            {
                return;
            }

            int index = characterAlertList.Count - 1;
            CharacterAlert characterAlert = characterAlertList[index];
            if (characterAlert == null
                || characterAlert.alertGameObject == null
                || characterAlert.alertTransform == null
                || characterAlert.alertText == null)
            {
                return;
            }
            characterAlert.alertText.text = message;
            characterAlert.alertDurationElapsedTime = 0;
            characterAlertList.RemoveAt(index);
            characterAlertList.Insert(0, characterAlert);
        }

        private void UpdateCharacterAlerts(Fix64 deltaTime)
        {
            int count = characterAlertList.Count;
            for (int i = 0; i < count; i++)
            {
                if (characterAlertList[i] == null
                    || characterAlertList[i].alertGameObject == null
                    || characterAlertList[i].alertTransform == null)
                {
                    continue;
                }

                characterAlertList[i].alertDurationElapsedTime += deltaTime;
                if (characterAlertList[i].alertDurationElapsedTime < alertDuration)
                {
                    characterAlertList[i].alertGameObject.SetActive(true);
                }
                else
                {
                    characterAlertList[i].alertGameObject.SetActive(false);
                }

                characterAlertList[i].alertTransform.SetSiblingIndex(i);
            }
        }

        private void ResetCharacterAlert(CharacterAlert characterAlert)
        {
            if (characterAlert == null
                || characterAlert.alertGameObject == null
                || characterAlert.alertTransform == null
                || characterAlert.alertText == null)
            {
                return;
            }

            characterAlert.alertGameObject.SetActive(false);
            characterAlert.alertText.text = "";
            characterAlert.alertDurationElapsedTime = alertDuration;
        }

        private void ResetCharacterAlerts()
        {
            int count = characterAlertList.Count;
            for (int i = 0; i < count; i++)
            {
                ResetCharacterAlert(characterAlertList[i]);
            }
        }

        #endregion

        #region On New Alert Methods

        private void OnNewAlert(string newString, ControlsScript player)
        {
            StartCharacterAlertOnNewAlert(player, newString);
        }

        private void StartCharacterAlertOnNewAlert(ControlsScript player, string message)
        {
            if (player == null
                || player != UFE2Manager.GetControlsScript(this.player))
            {
                return;
            }

            StartCharacterAlert(message);
        }

        #endregion

        #region On Move Methods

        private void OnMove(MoveInfo move, ControlsScript player)
        {
            StartWakeUpAlertOnMove(player, GetCharacterData(this.player), move);
        }

        private void StartWakeUpAlertOnMove(ControlsScript player, CharacterData characterData, MoveInfo moveInfo)
        {
            if (useWakeUpAlertOnMove == false
                || moveInfo == null
                || player == null
                || player != UFE2Manager.GetControlsScript(this.player)
                || characterData == null
                || characterData.standUp == false)
            {
                return;
            }

            StartCharacterAlert("Wake Up");
        }

        #endregion

        #region On Hit Methods

        private void OnHit(HitBox strokeHitBox, MoveInfo move, Hit hit, ControlsScript player)
        {
            StartOtgAlertOnHit(player, hit);

            StartForceStandAlertOnHit(player, hit);

            StartUnblockableAlertOnHit(player, hit);

            StartArmorBrokenAlertOnHit(player, GetCharacterData(this.player), hit);

            StartArmorAlertOnHit(player, GetCharacterData(this.player));

            StartArmorBreakerAlertOnHit(player, hit);

            StartPunishAlertOnHit(player, GetCharacterData(this.player));

            StartCounterHitAlertOnHit(player, GetCharacterData(this.player));
        }

        private void StartCounterHitAlertOnHit(ControlsScript player, CharacterData characterData)
        {
            //characterData.opponentCharacterData.armor = false;
            //characterData.opponentCharacterData.currentFrameData = CurrentFrameData.StartupFrames;

            if (useCounterHitAlertOnHit == false
                || player == null
                || player != UFE2Manager.GetControlsScript(this.player)
                || player.opControlsScript == null
                || player.opControlsScript.comboHits > 0
                || characterData == null
                || characterData.opponentCharacterData == null
                || characterData.opponentCharacterData.currentFrameData != CurrentFrameData.StartupFrames
                || characterData.opponentCharacterData.armor == true)
            {
                return;
            }

            StartCharacterAlert("Counter Hit");
        }

        private void StartPunishAlertOnHit(ControlsScript player, CharacterData characterData)
        {
            //characterData.opponentCharacterData.armor = false;
            //characterData.opponentCharacterData.currentFrameData = CurrentFrameData.RecoveryFrames;

            if (usePunishAlertOnHit == false
                || player == null
                || player != UFE2Manager.GetControlsScript(this.player)
                || player.opControlsScript == null
                || player.opControlsScript.comboHits > 0
                || characterData == null
                || characterData.opponentCharacterData == null
                || characterData.opponentCharacterData.currentFrameData != CurrentFrameData.RecoveryFrames
                || characterData.opponentCharacterData.armor == true)
            {
                return;
            }

            StartCharacterAlert("Punish");
        }

        private void StartArmorAlertOnHit(ControlsScript player, CharacterData characterData)
        {
            //characterData.opponentCharacterData.armor = true;

            if (useArmorAlertOnHit == false
                || player == null
                || player == UFE2Manager.GetControlsScript(this.player)
                || characterData == null
                || characterData.opponentCharacterData == null
                || characterData.opponentCharacterData.armor == false)
            {
                return;
            }

            StartCharacterAlert("Armored Hit");
        }

        private void StartArmorBreakerAlertOnHit(ControlsScript player, Hit hit)
        {
            //hit.armorBreaker = true;

            if (useArmorBreakerAlertOnHit == false
                || player == null
                || player != UFE2Manager.GetControlsScript(this.player)
                || player.opControlsScript == null
                || player.opControlsScript.comboHits > 0
                || hit == null
                || hit.armorBreaker == false)
            {
                return;
            }

            StartCharacterAlert("Armor Breaker");
        }

        private void StartArmorBrokenAlertOnHit(ControlsScript player, CharacterData characterData, Hit hit)
        {
            //hit.armorBreaker = true;
            //characterData.opponentCharacterData.armor = true;

            if (useArmorBrokenAlertOnHit == false
                || player == null
                || player == UFE2Manager.GetControlsScript(this.player)
                || characterData == null
                || characterData.opponentCharacterData == null
                || characterData.opponentCharacterData.armor == false
                || hit == null
                || hit.armorBreaker == false)
            {
                return;
            }

            StartCharacterAlert("Armor Broken");
        }

        private void StartUnblockableAlertOnHit(ControlsScript player, Hit hit)
        {
            //hit.unblockable = true;

            if (useUnblockableAlertOnHit == false
                || player == null
                || player != UFE2Manager.GetControlsScript(this.player)
                || player.opControlsScript == null
                || player.opControlsScript.comboHits > 0
                || hit == null
                || hit.unblockable == false)
            {
                return;
            }

            StartCharacterAlert("Unblockable");
        }

        private void StartForceStandAlertOnHit(ControlsScript player, Hit hit)
        {
            //hit.forceStand = true;
            //player.opControlsScript.currentState = PossibleStates.Crouch;

            if (useForceStandAlertOnHit == false
                || player == null
                || player != UFE2Manager.GetControlsScript(this.player)
                || player.opControlsScript == null
                || player.opControlsScript.currentState != PossibleStates.Crouch
                || hit == null
                || hit.forceStand == false)
            {
                return;
            }

            StartCharacterAlert("Force Stand");
        }

        private void StartOtgAlertOnHit(ControlsScript player, Hit hit)
        {
            //hit.downHit = true;
            //player.opControlsScript.currentState = PossibleStates.Down;

            if (useOtgAlertOnHit == false
                || player == null
                || player != UFE2Manager.GetControlsScript(this.player)
                || player.opControlsScript == null
                || player.opControlsScript.currentState != PossibleStates.Down
                || hit == null
                || hit.downHit == false)
            {
                return;
            }

            StartCharacterAlert("Otg");
        }

        #endregion

        #region On Parry Methods

        private void OnParry(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            StartParryAlertOnParry(player);
        }

        private void StartParryAlertOnParry(ControlsScript player)
        {
            if (useParryAlertOnParry == false
                || player == null
                || player != UFE2Manager.GetControlsScript(this.player))
            {
                return;
            }

            StartCharacterAlert("Parry");
        }

        #endregion
    }
}