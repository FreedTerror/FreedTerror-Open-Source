using FPLibrary;
using System;
using System.Collections.Generic;
using UFE3D;
using UnityEngine;

namespace UFE2FTE
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
        }
        [SerializeField]
        private StunOptions stunOptions;

        [System.Serializable]
        public class StunDecayOptions
        {
            public GaugeId gaugeId;
            [Fix64Range(-100f, 0f)]
            public Fix64 passiveGaugeLossPercentAmount;
        }
        [SerializeField]
        private StunDecayOptions stunDecayOptions;

        private void OnEnable()
        {
            UFE.OnHit += OnHit;
        }

        private void FixedUpdate()
        {
            UpdateStunDecayGauge(UFE.GetPlayer1ControlsScript());
            UpdateStunDecayGauge(UFE.GetPlayer2ControlsScript());

            UpdateStunGauge(UFE.GetPlayer1ControlsScript());
            UpdateStunGauge(UFE.GetPlayer2ControlsScript());
        }

        private void OnDisable()
        {
            UFE.OnHit -= OnHit;
        }

        private void OnHit(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            UFE2FTE.AddOrSubtractGaugePointsPercent(player.opControlsScript, stunDecayOptions.gaugeId, 100);
        }

        private void UpdateStunGauge(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            if (player.currentGaugesPoints[(int)stunOptions.gaugeId] >= player.myInfo.maxGaugePoints)
            {
                UFE2FTE.CastMoveByMoveName(player, stunOptions.moveName);
                UFE2FTE.AddOrSubtractGaugePointsPercent(player, stunOptions.gaugeId, -(Fix64)100);
            }

            if (player.currentGaugesPoints[(int)stunDecayOptions.gaugeId] <= 0)
            {
                UFE2FTE.AddOrSubtractGaugePointsPercent(player, stunOptions.gaugeId, stunOptions.passiveGaugeLossPercentAmount);
            }
        }

        [NaughtyAttributes.Button]
        private void MaxStunGauge()
        {
            UFE2FTE.AddOrSubtractGaugePointsPercent(UFE.GetPlayer1ControlsScript(), stunOptions.gaugeId, 100);
            UFE2FTE.AddOrSubtractGaugePointsPercent(UFE.GetPlayer1ControlsScript(), stunDecayOptions.gaugeId, 100);     
        }

        private void UpdateStunDecayGauge(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            UFE2FTE.AddOrSubtractGaugePointsPercent(player, stunDecayOptions.gaugeId, stunDecayOptions.passiveGaugeLossPercentAmount);
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