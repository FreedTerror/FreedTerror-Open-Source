using System;
using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class UFE_2ChallengeModeManager : MonoBehaviour
    {
        public static UFE_2ChallengeModeManager Instance { get; private set; }

        [HideInInspector]
        public DefaultChallengeModeGUI defaultChallengeModeGUI;

        private bool onRoundBeginsEventSubscribed;

        private bool onBasicMoveEventSubscribed;

        [Serializable]
        public class ChallengeModeBasicMove
        {
            public int challengeNumber;
            [HideInInspector]
            public bool player1ChallengeComplete;
            public BasicMoveChallengeType basicMoveChallengeType;
            public BasicMoveReference basicMove;
            [HideInInspector]
            public float player1BasicMovePlayingTimer;
            public int player1BasicMovePlayingTimeSuccess;
            [HideInInspector]
            public int player1BasicMoveTimesPlayed;
            public int player1BasicMoveTimesPlayedSuccess;         
        }

        public ChallengeModeBasicMove[] challengeModeBasicMoves;

        [Serializable]
        public class ChallengeModeRuleBasedAI
        {
            public int challengeNumber;
            public AIInfo aiInfo;
        }
                
        public ChallengeModeRuleBasedAI[] challengeModeRuleBasedAis;

        [HideInInspector]
        public RuleBasedAI player2RuleBasedAi;

        [Serializable]
        public class ChallengeModeGauge
        {
            public int challengeNumber;
            [Range(0, 100)]
            public int player1GaugePercent;           
            [Range(0, 100)]
            public int player2GaugePercent;
            public bool player1InhibitGainWhileDraining;
            public bool player2InhibitGainWhileDraining;
            public GaugeId[] player1GaugeIds;
            public GaugeId[] player2GaugeIds;
            public bool useAllPlayer1GaugeIds;
            public bool useAllPlayer2GaugeIds;
        }

        public ChallengeModeGauge[] challengeModeGauges;

        public bool resetGaugesOnUnusedChallenge;

        void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            RuleBasedAI[] ruleBasedAis = UFE.UFEInstance.gameObject.GetComponents<RuleBasedAI>();

            int ruleBasedAisLength = ruleBasedAis.Length;
            for (int i = 0; i < ruleBasedAisLength; i++)
            {
                switch (ruleBasedAis[i].player)
                {
                    case 2:
                        player2RuleBasedAi = ruleBasedAis[i];
                        break;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (UFE.gameMode == GameMode.ChallengeMode
                && UFE.p1ControlsScript != null
                && UFE.p2ControlsScript != null)
            {
                if (defaultChallengeModeGUI == null)
                {
                    defaultChallengeModeGUI = UFE.challengeMode.GetComponent<DefaultChallengeModeGUI>();
                }

                SubscribeToUFE_2Events();

                TestChallengeModeBasicMoveBasicMovePlaying();

                SetChallengeModeGauges();           
            }
            else
            {
                UnsubscribeFromUFE_2Events();
            }
        }

        #region Test Challenge Mode Basic Move Basic Move Playing Functions

        public void TestChallengeModeBasicMoveBasicMovePlaying()
        {
            if (UFE.challengeMode != null)
            {
                float deltaTime = (float)UFE.fixedDeltaTime;

                int challengeModeBasicMovesLength = challengeModeBasicMoves.Length;
                for (int i = 0; i < challengeModeBasicMovesLength; i++)
                {
                    if (UFE.challengeMode.currentChallenge == challengeModeBasicMoves[i].challengeNumber)
                    {
                        switch (challengeModeBasicMoves[i].basicMoveChallengeType)
                        {
                            case BasicMoveChallengeType.BasicMovePlaying:
                                switch (challengeModeBasicMoves[i].player1ChallengeComplete)
                                {
                                    case false:
                                        switch (challengeModeBasicMoves[i].basicMove)
                                        {
                                            case BasicMoveReference.MoveForward:
                                                //Some GC created from IsBasicMovePlaying
                                                /*switch (UFE.p1ControlsScript.MoveSet.IsBasicMovePlaying(UFE.p1ControlsScript.MoveSet.basicMoves.moveForward))
                                                {
                                                    case true:
                                                        challengeModeBasicMoves[i].player1BasicMovePlayingTimer += deltaTime;

                                                        if (challengeModeBasicMoves[i].player1BasicMovePlayingTimer >= challengeModeBasicMoves[i].player1BasicMovePlayingTimeSuccess)
                                                        {
                                                            challengeModeBasicMoves[i].player1ChallengeComplete = true;
                                                            challengeModeBasicMoves[i].player1BasicMovePlayingTimer = challengeModeBasicMoves[i].player1BasicMovePlayingTimeSuccess;
                                                            defaultChallengeModeGUI.CompleteChallenge();
                                                        }
                                                        break;

                                                    case false:
                                                        challengeModeBasicMoves[i].player1BasicMovePlayingTimer = 0;
                                                        break;
                                                }*/

                                                if (UFE.p1ControlsScript.MoveSet.MecanimControl.currentAnimationData.clipName == "moveForward")
                                                {
                                                    challengeModeBasicMoves[i].player1BasicMovePlayingTimer += deltaTime;

                                                    if (challengeModeBasicMoves[i].player1BasicMovePlayingTimer >= challengeModeBasicMoves[i].player1BasicMovePlayingTimeSuccess)
                                                    {
                                                        challengeModeBasicMoves[i].player1ChallengeComplete = true;
                                                        challengeModeBasicMoves[i].player1BasicMovePlayingTimer = challengeModeBasicMoves[i].player1BasicMovePlayingTimeSuccess;
                                                        //defaultChallengeModeGUI.CompleteChallenge();
                                                    }
                                                }
                                                else
                                                {
                                                    challengeModeBasicMoves[i].player1BasicMovePlayingTimer = 0;
                                                }
                                                break;

                                            case BasicMoveReference.MoveBack:
                                                if (UFE.p1ControlsScript.MoveSet.MecanimControl.currentAnimationData.clipName == "moveBack")
                                                {
                                                    challengeModeBasicMoves[i].player1BasicMovePlayingTimer += deltaTime;

                                                    if (challengeModeBasicMoves[i].player1BasicMovePlayingTimer >= challengeModeBasicMoves[i].player1BasicMovePlayingTimeSuccess)
                                                    {
                                                        challengeModeBasicMoves[i].player1ChallengeComplete = true;
                                                        challengeModeBasicMoves[i].player1BasicMovePlayingTimer = challengeModeBasicMoves[i].player1BasicMovePlayingTimeSuccess;
                                                        //defaultChallengeModeGUI.CompleteChallenge();
                                                    }
                                                }
                                                else
                                                {
                                                    challengeModeBasicMoves[i].player1BasicMovePlayingTimer = 0;
                                                }
                                                break;

                                            case BasicMoveReference.Crouching:
                                                if (UFE.p1ControlsScript.MoveSet.MecanimControl.currentAnimationData.clipName == "crouching")
                                                {
                                                    challengeModeBasicMoves[i].player1BasicMovePlayingTimer += deltaTime;

                                                    if (challengeModeBasicMoves[i].player1BasicMovePlayingTimer >= challengeModeBasicMoves[i].player1BasicMovePlayingTimeSuccess)
                                                    {
                                                        challengeModeBasicMoves[i].player1ChallengeComplete = true;
                                                        challengeModeBasicMoves[i].player1BasicMovePlayingTimer = challengeModeBasicMoves[i].player1BasicMovePlayingTimeSuccess;
                                                        //defaultChallengeModeGUI.CompleteChallenge();
                                                    }
                                                }
                                                else
                                                {
                                                    challengeModeBasicMoves[i].player1BasicMovePlayingTimer = 0;
                                                }
                                                break;
                                        }
                                        break;
                                }
                                break;
                        }

                        break;
                    }
                }
            }
        }

        #endregion

        #region Set Challenge Mode Gauges Functions

        public void SetChallengeModeGauges()
        {
            int challengeModeGaugesLength = challengeModeGauges.Length;
            int p1Length = UFE.p1ControlsScript.currentGaugesPoints.Length;
            int p2Length = UFE.p2ControlsScript.currentGaugesPoints.Length;
            for (int i = 0; i < challengeModeGaugesLength; i++)
            {
                if (UFE.challengeMode.currentChallenge == challengeModeGauges[i].challengeNumber)
                {
                    switch (challengeModeGauges[i].useAllPlayer1GaugeIds)
                    {
                        case true:
                            for (int a = 0; a < p1Length; a++)
                            {
                                UFE.p1ControlsScript.currentGaugesPoints[a] = UFE.p1ControlsScript.myInfo.maxGaugePoints * challengeModeGauges[i].player1GaugePercent / 100;
                            }
                            break;

                        case false:
                            int player1GaugeIdsLength = challengeModeGauges[i].player1GaugeIds.Length;
                            for (int a = 0; a < p1Length; a++)
                            {
                                for (int b = 0; b < player1GaugeIdsLength; b++)
                                {
                                    UFE.p1ControlsScript.currentGaugesPoints[(int)challengeModeGauges[i].player1GaugeIds[b]] = UFE.p1ControlsScript.myInfo.maxGaugePoints * challengeModeGauges[i].player1GaugePercent / 100;
                                }
                            }
                            break;
                    }

                    switch (challengeModeGauges[i].useAllPlayer2GaugeIds)
                    {
                        case true:
                            for (int a = 0; a < p2Length; a++)
                            {
                                UFE.p2ControlsScript.currentGaugesPoints[a] = UFE.p2ControlsScript.myInfo.maxGaugePoints * challengeModeGauges[i].player2GaugePercent / 100;
                            }
                            break;

                        case false:
                            int player2GaugeIdsLength = challengeModeGauges[i].player2GaugeIds.Length;
                            for (int a = 0; a < p2Length; a++)
                            {
                                for (int b = 0; b < player2GaugeIdsLength; b++)
                                {
                                    UFE.p2ControlsScript.currentGaugesPoints[(int)challengeModeGauges[i].player2GaugeIds[b]] = UFE.p2ControlsScript.myInfo.maxGaugePoints * challengeModeGauges[i].player2GaugePercent / 100;
                                }
                            }
                            break;
                    }
                }
            }
        }

        #endregion

        #region Subscribe To And Unsubscribe From UFE_2 Events Functions

        public void SubscribeToUFE_2Events()
        {
            switch (onRoundBeginsEventSubscribed)
            {
                case false:
                    UFE.OnRoundBegins += this.OnRoundBegins;
                    onRoundBeginsEventSubscribed = true;
                    break;
            }

            switch (onBasicMoveEventSubscribed)
            {
                case false:
                    UFE.OnBasicMove += this.OnBasicMove;
                    onBasicMoveEventSubscribed = true;
                    break;
            }
        }

        public void UnsubscribeFromUFE_2Events()
        {
            switch (onRoundBeginsEventSubscribed)
            {
                case true:
                    UFE.OnRoundBegins -= this.OnRoundBegins;
                    onRoundBeginsEventSubscribed = false;
                    break;
            }

            switch (onBasicMoveEventSubscribed)
            {
                case true:
                    UFE.OnBasicMove -= this.OnBasicMove;
                    onBasicMoveEventSubscribed = false;
                    break;
            }
        }

        #endregion

        #region On Round Begins Functions

        void OnRoundBegins(int newInt)
        {
            int challengeModeBasicMovesLength = challengeModeBasicMoves.Length;
            for (int i = 0; i < challengeModeBasicMovesLength; i++)
            {
                challengeModeBasicMoves[i].player1ChallengeComplete = false;
                challengeModeBasicMoves[i].player1BasicMoveTimesPlayed = 0;
                challengeModeBasicMoves[i].player1BasicMovePlayingTimer = 0;
            }

            int challengeModeRuleBasedAisLength = challengeModeRuleBasedAis.Length;
            for (int i = 0; i < challengeModeRuleBasedAisLength; i++)
            {
                if (UFE.challengeMode.currentChallenge == challengeModeRuleBasedAis[i].challengeNumber)
                {
                    player2RuleBasedAi.SetAIInformation(challengeModeRuleBasedAis[i].aiInfo);
                }
            }

            int challengeModeGaugesLength = challengeModeGauges.Length;
            int p1CurrentGaugesPointsLength = UFE.p1ControlsScript.currentGaugesPoints.Length;
            int p2CurrentGaugesPointsLength = UFE.p2ControlsScript.currentGaugesPoints.Length;
            for (int i = 0; i < challengeModeGaugesLength; i++)
            {
                if (UFE.challengeMode.currentChallenge == challengeModeGauges[i].challengeNumber)
                {
                    switch (challengeModeGauges[i].player1InhibitGainWhileDraining)
                    {
                        case true:
                            UFE.p1ControlsScript.inhibitGainWhileDraining = true;
                            break;

                        case false:
                            UFE.p1ControlsScript.inhibitGainWhileDraining = false;
                            break;
                    }

                    switch (challengeModeGauges[i].player2InhibitGainWhileDraining)
                    {
                        case true:
                            UFE.p2ControlsScript.inhibitGainWhileDraining = true;
                            break;

                        case false:
                            UFE.p2ControlsScript.inhibitGainWhileDraining = false;
                            break;
                    }
                }
                else
                {
                    switch (resetGaugesOnUnusedChallenge)
                    {
                        case true:
                            for (int a = 0; a < p1CurrentGaugesPointsLength; a++)
                            {
                                UFE.p1ControlsScript.currentGaugesPoints[a] = 0;
                            }

                            for (int a = 0; a < p2CurrentGaugesPointsLength; a++)
                            {
                                UFE.p2ControlsScript.currentGaugesPoints[a] = 0;
                            }
                            break;
                    }

                    UFE.p1ControlsScript.inhibitGainWhileDraining = false;
                    UFE.p2ControlsScript.inhibitGainWhileDraining = false;
                }
            }       
        }

        #endregion

        #region On Basic Move Functions

        void OnBasicMove(BasicMoveReference basicMove, ControlsScript player)
        {
            int challengeModeBasicMovesLength = challengeModeBasicMoves.Length;
            for (int i = 0; i < challengeModeBasicMovesLength; i++)
            {             
                if (UFE.challengeMode.currentChallenge == challengeModeBasicMoves[i].challengeNumber)
                {
                    switch (challengeModeBasicMoves[i].basicMoveChallengeType)
                    {
                        case BasicMoveChallengeType.BasicMoveTimesPlayed:
                            switch (challengeModeBasicMoves[i].player1ChallengeComplete)
                            {
                                case false:
                                    if (basicMove == challengeModeBasicMoves[i].basicMove)
                                    {
                                        challengeModeBasicMoves[i].player1BasicMoveTimesPlayed++;

                                        if (challengeModeBasicMoves[i].player1BasicMoveTimesPlayed >= challengeModeBasicMoves[i].player1BasicMoveTimesPlayedSuccess)
                                        {
                                            challengeModeBasicMoves[i].player1ChallengeComplete = true;
                                            //defaultChallengeModeGUI.CompleteChallenge();
                                        }

                                        break;
                                    }
                                    break;
                            }
                            break;
                    }                               
                }
            }
        }

        #endregion
    }
}
