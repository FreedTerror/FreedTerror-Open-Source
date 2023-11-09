using System;
using UFE3D;
using FPLibrary;

namespace UFE2FTE
{
    public static class TriggeredBehaviour
    {
        [Serializable]
        public class DelayActionTimeOptions
        {
            public Fix64 delayActionTime;
            public bool useFreezingTimeAsDelayActionTime;

            public Fix64 GetDelayActionTime()
            {
                Fix64 delayActionTime = this.delayActionTime;

                if (delayActionTime <= 0)
                {
                    delayActionTime = 0;
                }

                return delayActionTime;
            }

            public Fix64 GetDelayActionTimeOnHit(Hit hit)
            {
                Fix64 delayActionTime = this.delayActionTime;

                if (useFreezingTimeAsDelayActionTime == true)
                {
                    delayActionTime = UFE2FTE.GetFreezingTimeFromHitOnHit(hit);
                }

                if (delayActionTime <= 0)
                {
                    delayActionTime = 0;
                }

                return delayActionTime;
            }

            public Fix64 GetDelayActionTimeOnBlock(Hit hit)
            {
                Fix64 delayActionTime = this.delayActionTime;

                if (useFreezingTimeAsDelayActionTime == true)
                {
                    delayActionTime = UFE2FTE.GetFreezingTimeFromHitOnBlock(hit);
                }

                if (delayActionTime <= 0)
                {
                    delayActionTime = 0;
                }

                return delayActionTime;
            }

            public Fix64 GetDelayActionTimeOnParry(Hit hit)
            {
                Fix64 delayActionTime = this.delayActionTime;

                if (useFreezingTimeAsDelayActionTime == true)
                {
                    delayActionTime = UFE2FTE.GetFreezingTimeFromHitStrength(hit.hitStrength);
                }

                if (delayActionTime <= 0)
                {
                    delayActionTime = 0;
                }

                return delayActionTime;
            }
        }

        [Serializable]
        public class TargetOptions
        {
            public string[] excludedCharacterNameArray;
            public bool usePlayer;
            public bool usePlayerAssist;
            public bool useAllPlayerAssists;
            public bool useOpponent;
            public bool useOpponentAssist;
            public bool useAllOpponentAssists;
        }

        public static bool IsIntMatch(int comparingInt, int matchingInt)
        {
            if (comparingInt == matchingInt)
            {
                return true;
            }

            return false;
        }

        public static bool IsIntMatch(int comparingInt, int[] matchingIntArray)
        {
            if (matchingIntArray == null)
            {
                return false;
            }

            int length = matchingIntArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (IsIntMatch(comparingInt, matchingIntArray[i]) == false)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public static bool IsStringMatch(string comparingString, string matchingString)
        {
            if (comparingString == matchingString)
            {
                return true;
            }

            return false;
        }

        public static bool IsStringMatch(string comparingString, string[] matchingStringArray)
        {
            if (matchingStringArray == null)
            {
                return false;
            }

            int length = matchingStringArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (IsStringMatch(comparingString, matchingStringArray[i]) == false)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public static bool IsBasicMoveMatch(BasicMoveReference comparingBasicMove, BasicMoveReference matchingBasicMove)
        {
            if (comparingBasicMove == matchingBasicMove)
            {
                return true;
            }

            return false;
        }

        public static bool IsBasicMoveMatch(BasicMoveReference comparingBasicMove, BasicMoveReference[] matchingBasicMoveArray)
        {
            if (matchingBasicMoveArray == null)
            {
                return false;
            }

            int length = matchingBasicMoveArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (IsBasicMoveMatch(comparingBasicMove, matchingBasicMoveArray[i]) == false)
                {
                    continue;
                }

                return true;
            }

            return false;
        }
    }
}