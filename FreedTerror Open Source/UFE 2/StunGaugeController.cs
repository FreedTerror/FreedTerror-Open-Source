using FPLibrary;
using System;
using System.Collections.Generic;
using UFE3D;
using UnityEngine;

namespace FreedTerror.UFE2
{
    public class StunGaugeController : MonoBehaviour
    {
        [SerializeField]
        private string[] stunGaugeGameObjectNameArray;
        private List<GameObject> stunGaugeGameObjectList = new List<GameObject>();

        [System.Serializable]
        public class StunOptions
        {
            public GaugeId gaugeId;
            [Fix64Range(-100f, 0f)]
            public Fix64 passiveGaugeLossPercentAmount;
            public string moveName;

#if UNITY_EDITOR
            [NaughtyAttributes.ProgressBar("Player 1 Stun", nameof(debugPlayer1MaxGaugePoints), color: NaughtyAttributes.EColor.Blue)]
            public float debugPlayer1CurrentGaugePoints = 0;
            [HideInInspector]
            public int debugPlayer1MaxGaugePoints = 0;

            [NaughtyAttributes.ProgressBar("Player 2 Stun", nameof(debugPlayer2MaxGaugePoints), color: NaughtyAttributes.EColor.Blue)]
            public float debugPlayer2CurrentGaugePoints = 0;
            [HideInInspector]
            public int debugPlayer2MaxGaugePoints = 0;
#endif
        }
        [SerializeField]
        private StunOptions stunOptions;

        [System.Serializable]
        public class StunDelayOptions
        {
            public GaugeId gaugeId;
            [Fix64Range(-100f, 0f)]
            public Fix64 passiveGaugeLossPercentAmount;

#if UNITY_EDITOR
            [NaughtyAttributes.ProgressBar("Player 1 Stun Delay", nameof(debugPlayer1MaxGaugePoints), color: NaughtyAttributes.EColor.Blue)]
            public float debugPlayer1CurrentGaugePoints = 0;
            [HideInInspector]
            public int debugPlayer1MaxGaugePoints = 0;

            [NaughtyAttributes.ProgressBar("Player 2 Stun Delay", nameof(debugPlayer2MaxGaugePoints), color: NaughtyAttributes.EColor.Blue)]
            public float debugPlayer2CurrentGaugePoints = 0;
            [HideInInspector]
            public int debugPlayer2MaxGaugePoints = 0;
#endif
        }
        [SerializeField]
        private StunDelayOptions stunDelayOptions;

        private void OnEnable()
        {
            UFE.OnHit += OnHit;
        }

        private void FixedUpdate()
        {
            UpdateStunDecayGauge(UFE.p1ControlsScript);
            UpdateStunDecayGauge(UFE.p2ControlsScript);

            UpdateStunGauge(UFE.p1ControlsScript);
            UpdateStunGauge(UFE.p2ControlsScript);

#if UNITY_EDITOR
            UpdateDebugData(UFE.p1ControlsScript);
            UpdateDebugData(UFE.p2ControlsScript);
#endif
        }

        private void OnDisable()
        {
            UFE.OnHit -= OnHit;
        }

        private void OnHit(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            UFE2Manager.AddOrSubtractGaugePointsPercent(player.opControlsScript, stunDelayOptions.gaugeId, 100);
        }

        private void UpdateStunGauge(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            if (player.currentGaugesPoints[(int)stunOptions.gaugeId] >= player.myInfo.maxGaugePoints)
            {
                UFE2Manager.CastMoveByMoveName(player, stunOptions.moveName);
                UFE2Manager.AddOrSubtractGaugePointsPercent(player, stunOptions.gaugeId, -(Fix64)100);
            }

            if (player.currentGaugesPoints[(int)stunDelayOptions.gaugeId] <= 0)
            {
                UFE2Manager.AddOrSubtractGaugePointsPercent(player, stunOptions.gaugeId, stunOptions.passiveGaugeLossPercentAmount);
            }
        }

        private void UpdateStunDecayGauge(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            UFE2Manager.AddOrSubtractGaugePointsPercent(player, stunDelayOptions.gaugeId, stunDelayOptions.passiveGaugeLossPercentAmount);
        }

#if UNITY_EDITOR
        public void UpdateDebugData(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            if (player == UFE.p1ControlsScript)
            {
                stunOptions.debugPlayer1CurrentGaugePoints = (float)player.currentGaugesPoints[(int)stunOptions.gaugeId];
                stunOptions.debugPlayer1MaxGaugePoints = player.myInfo.maxGaugePoints;

                stunDelayOptions.debugPlayer1CurrentGaugePoints = (float)player.currentGaugesPoints[(int)stunDelayOptions.gaugeId];
                stunDelayOptions.debugPlayer1MaxGaugePoints = player.myInfo.maxGaugePoints;
            }
            else if (player == UFE.p2ControlsScript)
            {
                stunOptions.debugPlayer2CurrentGaugePoints = (float)player.currentGaugesPoints[(int)stunOptions.gaugeId];
                stunOptions.debugPlayer2MaxGaugePoints = player.myInfo.maxGaugePoints;

                stunDelayOptions.debugPlayer2CurrentGaugePoints = (float)player.currentGaugesPoints[(int)stunDelayOptions.gaugeId];
                stunDelayOptions.debugPlayer2MaxGaugePoints = player.myInfo.maxGaugePoints;
            }
        }
#endif

        [NaughtyAttributes.Button]
        private void TestGaugesPlayer1()
        {
            UFE2Manager.AddOrSubtractGaugePointsPercent(UFE.p1ControlsScript, stunOptions.gaugeId, 50);
            UFE2Manager.AddOrSubtractGaugePointsPercent(UFE.p1ControlsScript, stunDelayOptions.gaugeId, 100);
        }

        [NaughtyAttributes.Button]
        private void TestGaugesPlayer2()
        {
            UFE2Manager.AddOrSubtractGaugePointsPercent(UFE.p2ControlsScript, stunOptions.gaugeId, 50);
            UFE2Manager.AddOrSubtractGaugePointsPercent(UFE.p2ControlsScript, stunDelayOptions.gaugeId, 100);
        }

        [NaughtyAttributes.Button]
        private void ToggleStunGauge()
        {
            int length = stunGaugeGameObjectNameArray.Length;
            for (int i = 0; i < length; i++)
            {
                GameObject foundGameObject = GameObject.Find(stunGaugeGameObjectNameArray[i]);
                if (foundGameObject == null)
                {
                    continue;
                }

                AddToList(foundGameObject);
            }

            length = stunGaugeGameObjectList.Count;
            for (int i = 0; i < length; i++)
            {
                if (stunGaugeGameObjectList[i] == null)
                {
                    continue;
                }

                if (stunGaugeGameObjectList[i].activeInHierarchy == true)
                {
                    stunGaugeGameObjectList[i].SetActive(false);
                }
                else
                {
                    stunGaugeGameObjectList[i].SetActive(true);
                }
            }
        }

        private void AddToList(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }

            int count = stunGaugeGameObjectList.Count;
            for (int i = 0; i < count; i++)
            {
                if (gameObject == stunGaugeGameObjectList[i])
                {
                    return;
                }
            }

            stunGaugeGameObjectList.Add(gameObject);
        }
    }
}