using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace UFE2FTE
{
    public class UFE2FTEPaletteSwapSpritePhoton2NetworkManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        public static UFE2FTEPaletteSwapSpritePhoton2NetworkManager instance;

        [Serializable]
        private class SwapColorsData
        {
            public string characterName;
            public int playerNumber;
            public string swapColorsName;
            public Color32[] swapColors;
        }
        private List<SwapColorsData> swapColorsData;
        private List<SwapColorsData> GetSwapColorsData()
        {
            if (swapColorsData == null)
            {
                swapColorsData = new List<SwapColorsData>();
            }

            return swapColorsData;
        }

        public override void OnEnable()
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null) return;

            base.OnEnable();

            instance = this;

            photonView.ViewID = 1;
        }

        [PunRPC]
        public void SetSwapColorsDataRPC(string characterName, int playerNumber, string swapColorsName, Vector3[] swapColorsRGBColorBytes)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null
                || IsSwapColorsDataMatch(characterName, playerNumber, swapColorsName, swapColorsRGBColorBytes) == false) return;

            int length = swapColorsRGBColorBytes.Length;
            SwapColorsData newSwapColorsData = new SwapColorsData();
            newSwapColorsData.characterName = characterName;
            newSwapColorsData.playerNumber = playerNumber;
            newSwapColorsData.swapColorsName = swapColorsName;
            newSwapColorsData.swapColors = new Color32[length];       
            for (int i = 0; i < length; i++)
            {
                newSwapColorsData.swapColors[i] = new Color32((byte)swapColorsRGBColorBytes[i].x, (byte)swapColorsRGBColorBytes[i].y, (byte)swapColorsRGBColorBytes[i].z, 255);
            }

            GetSwapColorsData().Add(newSwapColorsData);
        }

        private bool IsSwapColorsDataMatch(string characterName, int playerNumber, string swapColorsName, Vector3[] swapColorsRGBColorBytes)
        {        
            int count = GetSwapColorsData().Count;
            int length = swapColorsRGBColorBytes.Length;
            for (int i = 0; i < count; i++)
            {
                if (characterName == GetSwapColorsData()[i].characterName
                    && playerNumber == GetSwapColorsData()[i].playerNumber
                    && length == GetSwapColorsData()[i].swapColors.Length)
                {
                    GetSwapColorsData()[i].characterName = characterName;

                    GetSwapColorsData()[i].playerNumber = playerNumber;

                    GetSwapColorsData()[i].swapColorsName = swapColorsName;

                    for (int a = 0; a < length; a++)
                    {
                        GetSwapColorsData()[i].swapColors[a] = new Color32((byte)swapColorsRGBColorBytes[a].x, (byte)swapColorsRGBColorBytes[a].y, (byte)swapColorsRGBColorBytes[a].z, 255);
                    }

                    return true;
                }
            }

            return false;
        }

        public Color32[] GetSwapColors(string characterName, int playerNumber)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null) return null;

            int count = GetSwapColorsData().Count;
            for (int i = 0; i < count; i++)
            {
                if (characterName != GetSwapColorsData()[i].characterName
                    && playerNumber != GetSwapColorsData()[i].playerNumber) continue;

                return GetSwapColorsData()[i].swapColors;       
            }

            return null;
        }

        public string GetSwapColorsName(string characterName, int playerNumber)
        {
            if (UFE2FTEPaletteSwapSpriteManager.instance == null) return "";

            int count = GetSwapColorsData().Count;
            for (int i = 0; i < count; i++)
            {
                if (characterName != GetSwapColorsData()[i].characterName
                    && playerNumber != GetSwapColorsData()[i].playerNumber) continue;

                return GetSwapColorsData()[i].swapColorsName;                  
            }

            return "";
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {

        }
    }
}
