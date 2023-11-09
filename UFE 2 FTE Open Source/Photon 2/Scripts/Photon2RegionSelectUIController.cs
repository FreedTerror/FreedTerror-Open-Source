using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace UFE2FTE
{
    public class Photon2RegionSelectUIController : MonoBehaviour
    {
        [SerializeField]
        private string playerPrefsKey = "Photon2Region";

        private enum Photon2Region
        {
            Asia,
            Australia,
            CanadaEast,
            Europe,
            India,
            Japan,
            Russia,
            RussiaEast,
            SouthAfrica,
            SouthAmerica,
            SouthKorea,
            Turkey,
            USAEast,
            USAWest
        }
        [SerializeField]
        private Photon2Region defaultPhoton2Region;
        [SerializeField]
        private Photon2Region[] photon2RegionOrderArray =
            { Photon2Region.Asia,
            Photon2Region.Australia,
            Photon2Region.CanadaEast,
            Photon2Region.Europe,
            Photon2Region.India,
            Photon2Region.Japan,
            Photon2Region.Russia,
            Photon2Region.RussiaEast,
            Photon2Region.SouthAfrica,
            Photon2Region.SouthAmerica,
            Photon2Region.SouthKorea,
            Photon2Region.Turkey,
            Photon2Region.USAEast,
            Photon2Region.USAWest };
        private int photon2RegionOrderArrayIndex;

        [Tooltip("Can't interact with these buttons if photon isn't connected.")]
        [SerializeField]
        private Button[] photon2IsConnectedButtonArray;
        [SerializeField]
        private Text photon2RegionText;

        [SerializeField]
        private string asiaName = "ASIA";
        [SerializeField]
        private string australiaName = "AUSTRALIA";
        [SerializeField]
        private string canadaEastName = "CANADA EAST";
        [SerializeField]
        private string europeName = "EUROPE";
        [SerializeField]
        private string indiaName = "INDIA";
        [SerializeField]
        private string japanName = "JAPAN";
        [SerializeField]
        private string russiaName = "RUSSIA";
        [SerializeField]
        private string russiaEastName = "RUSSIA EAST";
        [SerializeField]
        private string southAfricaName = "SOUTH AFRICA";
        [SerializeField]
        private string southAmericaName = "SOUTH AMERICA";
        [SerializeField]
        private string southKoreaName = "SOUTH KOREA";
        [SerializeField]
        private string turkeyName = "TURKEY";
        [SerializeField]
        private string usaEastName = "USA EAST";
        [SerializeField]
        private string usaWestName = "USA WEST";

        private void Start()
        {
            int photon2Region = PlayerPrefs.GetInt(playerPrefsKey);

            SetPhoton2Region(GetPhoton2RegionFromEnumIndex(photon2Region));    
        }

        private void Update()
        {
            SetButtonInteractable(photon2IsConnectedButtonArray, PhotonNetwork.IsConnectedAndReady);
        }

        public void NextPhoton2Region()
        {
            photon2RegionOrderArrayIndex++;

            if (photon2RegionOrderArrayIndex > photon2RegionOrderArray.Length - 1)
            {
                photon2RegionOrderArrayIndex = 0;
            }

            SetPhoton2Region(photon2RegionOrderArray[photon2RegionOrderArrayIndex]);
        }

        public void PreviousPhoton2Region()
        {
            photon2RegionOrderArrayIndex--;

            if (photon2RegionOrderArrayIndex < 0)
            {
                photon2RegionOrderArrayIndex = photon2RegionOrderArray.Length - 1;
            }

            SetPhoton2Region(photon2RegionOrderArray[photon2RegionOrderArrayIndex]);
        }

        private void SetPhoton2RegionOrderArrayIndex(Photon2Region photon2Region)
        {
            int length = photon2RegionOrderArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (photon2Region != photon2RegionOrderArray[i])
                {
                    continue;
                }

                photon2RegionOrderArrayIndex = i;

                break;
            }
        }

        private void SetPhoton2Region(Photon2Region photon2Region)
        {
            PhotonNetwork.Disconnect();

            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = GetFixedRegionNameFromPhoton2Region(photon2Region);

            SetPhoton2RegionOrderArrayIndex(photon2Region);

            SetTextMessage(photon2RegionText, GetPhoton2RegionNameFromPhoton2Region(GetPhoton2RegionFromFixedRegionName(PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion)));

            PlayerPrefs.SetInt(playerPrefsKey, (int)photon2Region);

            PhotonNetwork.ConnectUsingSettings();
        }

        private Photon2Region GetPhoton2RegionFromEnumIndex(int enumIndex)
        {
            if (Enum.IsDefined(typeof(Photon2Region), enumIndex) == true)
            {
                return (Photon2Region)enumIndex;
            }

            return defaultPhoton2Region;
        }

        private Photon2Region GetPhoton2RegionFromFixedRegionName(string fixedRegionName)
        {
            if (fixedRegionName == "asia")
            {
                return Photon2Region.Asia;
            }
            else if (fixedRegionName == "au")
            {
                return Photon2Region.Australia;
            }
            else if (fixedRegionName == "cae")
            {
                return Photon2Region.CanadaEast;
            }
            else if (fixedRegionName == "eu")
            {
                return Photon2Region.Europe;
            }
            else if (fixedRegionName == "in")
            {
                return Photon2Region.India;
            }
            else if (fixedRegionName == "jp")
            {
                return Photon2Region.Japan;
            }
            else if (fixedRegionName == "ru")
            {
                return Photon2Region.Russia;
            }
            else if (fixedRegionName == "rue")
            {
                return Photon2Region.RussiaEast;
            }
            else if (fixedRegionName == "za")
            {
                return Photon2Region.SouthAfrica;
            }
            else if (fixedRegionName == "sa")
            {
                return Photon2Region.SouthAmerica;
            }
            else if (fixedRegionName == "kr")
            {
                return Photon2Region.SouthKorea;
            }
            else if (fixedRegionName == "tr")
            {
                return Photon2Region.Turkey;
            }
            else if (fixedRegionName == "us")
            {
                return Photon2Region.USAEast;
            }
            else if (fixedRegionName == "usw")
            {
                return Photon2Region.USAWest;
            }

            return defaultPhoton2Region;
        }

        private string GetFixedRegionNameFromPhoton2Region(Photon2Region photon2Region)
        {
            switch (photon2Region)
            {
                case Photon2Region.Asia: 
                    return "asia";

                case Photon2Region.Australia: 
                    return "au";

                case Photon2Region.CanadaEast: 
                    return "cae";

                case Photon2Region.Europe: 
                    return "eu";

                case Photon2Region.India: 
                    return "in";

                case Photon2Region.Japan: 
                    return "jp";

                case Photon2Region.Russia: 
                    return "ru";

                case Photon2Region.RussiaEast: 
                    return "rue";

                case Photon2Region.SouthAfrica: 
                    return "za";

                case Photon2Region.SouthAmerica: 
                    return "sa";

                case Photon2Region.SouthKorea: 
                    return "kr";

                case Photon2Region.Turkey: 
                    return "tr";

                case Photon2Region.USAEast: 
                    return "us";

                case Photon2Region.USAWest: 
                    return "usw";

                default: 
                    return GetFixedRegionNameFromPhoton2Region(defaultPhoton2Region);
            }
        }

        private string GetPhoton2RegionNameFromPhoton2Region(Photon2Region photon2Region)
        {
            switch (photon2Region)
            {
                case Photon2Region.Asia: 
                    return asiaName;

                case Photon2Region.Australia: 
                    return australiaName;

                case Photon2Region.CanadaEast: 
                    return canadaEastName;

                case Photon2Region.Europe: 
                    return europeName;

                case Photon2Region.India: 
                    return indiaName;

                case Photon2Region.Japan: 
                    return japanName;

                case Photon2Region.Russia: 
                    return russiaName;

                case Photon2Region.RussiaEast: 
                    return russiaEastName;

                case Photon2Region.SouthAfrica: 
                    return southAfricaName;

                case Photon2Region.SouthAmerica: 
                    return southAmericaName;

                case Photon2Region.SouthKorea: 
                    return southKoreaName;

                case Photon2Region.Turkey: 
                    return turkeyName;

                case Photon2Region.USAEast: 
                    return usaEastName;

                case Photon2Region.USAWest: 
                    return usaWestName;

                default: 
                    return GetPhoton2RegionNameFromPhoton2Region(defaultPhoton2Region);
            }
        }

        private static void SetButtonInteractable(Button button, bool interactable)
        {
            if (button == null)
            {
                return;
            }

            button.interactable = interactable;
        }

        private static void SetButtonInteractable(Button[] buttonArray, bool interactable)
        {
            if (buttonArray == null)
            {
                return;
            }

            int length = buttonArray.Length;
            for (int i = 0; i < length; i++)
            {
                SetButtonInteractable(buttonArray[i], interactable);
            }
        }

        private static void SetTextMessage(Text text, string message)
        {
            if (text == null)
            {
                return;
            }

            text.text = message;
        }
    }
}
