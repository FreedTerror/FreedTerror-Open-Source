using System.Collections.Generic;
using UnityEngine;

namespace FreedTerror.UFE2
{   
    public class InputDisplayController : MonoBehaviour
    {
        private readonly string player1InputViewerName = "Player 1 Input Viewer";
        private readonly string player2InputViewerName = "Player 2 Input Viewer";
        private RectTransform player1InputDisplayRectTransform;
        private RectTransform player2InputDisplayRectTransform;
        private List<RectTransform> player1ButtonIconList = new List<RectTransform>();
        private List<RectTransform> player2ButtonIconList = new List<RectTransform>();
        public string inputDisplayButtonIconControllerPrefabPath = "Input Display Button Icon Controller Prefab";
        private InputDisplayButtonIconController inputDisplayButtonIconControllerPrefab;

        private void Start()
        {
            inputDisplayButtonIconControllerPrefab = UFE2Manager.instance.inputDisplayScriptableObject.GetInputDisplayButtonIconControllerPrefab();

            FindInputDisplayGameObjects();
        }

        private void Update()
        {
            SetActiveInputDisplayGameObjects();
        }

        private void FindInputDisplayGameObjects()
        {
            if (UFE2Manager.instance.inputDisplayScriptableObject.CanDisplayInputs() == false)
            {
                return;
            }

            GameObject inputDisplayGameObject = GameObject.Find(player1InputViewerName);
            if (inputDisplayGameObject != null)
            {
                player1InputDisplayRectTransform = inputDisplayGameObject.GetComponent<RectTransform>();
                if (player1InputDisplayRectTransform != null)
                {
                    foreach (RectTransform buttonPressList in player1InputDisplayRectTransform)
                    {
                        foreach (RectTransform buttonIcon in buttonPressList)
                        {
                            player1ButtonIconList.Add(buttonIcon);

                            if (inputDisplayButtonIconControllerPrefab != null)
                            {
                                Instantiate(inputDisplayButtonIconControllerPrefab, buttonIcon);
                            }
                        }
                    }
                }
            }

            inputDisplayGameObject = GameObject.Find(player2InputViewerName);
            if (inputDisplayGameObject != null)
            {
                player2InputDisplayRectTransform = inputDisplayGameObject.GetComponent<RectTransform>();
                if (player2InputDisplayRectTransform != null)
                {
                    foreach (RectTransform buttonPressList in player2InputDisplayRectTransform)
                    {
                        foreach (RectTransform buttonIcon in buttonPressList)
                        {
                            player2ButtonIconList.Add(buttonIcon);

                            if (inputDisplayButtonIconControllerPrefab != null)
                            {
                                Instantiate(inputDisplayButtonIconControllerPrefab, buttonIcon);
                            }
                        }
                    }
                }
            }

            if (player2InputDisplayRectTransform != null)
            {
                player2InputDisplayRectTransform.SetAsFirstSibling();
            }

            if (player1InputDisplayRectTransform != null)
            {
                player1InputDisplayRectTransform.SetAsFirstSibling();
            }
        }

        private void SetActiveInputDisplayGameObjects()
        {
            bool active = UFE2Manager.instance.displayInputs;

            if (player1InputDisplayRectTransform != null)
            {
                player1InputDisplayRectTransform.gameObject.SetActive(active);

                if (active == true)
                {
                    player1InputDisplayRectTransform.offsetMax = new Vector2(UFE2Manager.instance.inputDisplayScriptableObject.inputDisplayOffsetMax.x, UFE2Manager.instance.inputDisplayScriptableObject.inputDisplayOffsetMax.y);

                    int count = player1ButtonIconList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (player1ButtonIconList[i] == null)
                        {
                            continue;
                        }

                        player1ButtonIconList[i].sizeDelta = UFE2Manager.instance.inputDisplayScriptableObject.inputDisplayButtonIconSizeDelta;
                    }
                }
            }

            if (player2InputDisplayRectTransform != null)
            {
                player2InputDisplayRectTransform.gameObject.SetActive(active);

                if (active == true)
                {
                    player2InputDisplayRectTransform.offsetMax = new Vector2(UFE2Manager.instance.inputDisplayScriptableObject.inputDisplayOffsetMax.x, UFE2Manager.instance.inputDisplayScriptableObject.inputDisplayOffsetMax.y);

                    int count = player2ButtonIconList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (player2ButtonIconList[i] == null)
                        {
                            continue;
                        }

                        player2ButtonIconList[i].sizeDelta = UFE2Manager.instance.inputDisplayScriptableObject.inputDisplayButtonIconSizeDelta;
                    }
                }
            }
        }
    }
}