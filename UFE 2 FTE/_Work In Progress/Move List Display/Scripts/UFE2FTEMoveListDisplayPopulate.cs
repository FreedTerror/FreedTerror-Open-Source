using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEMoveListDisplayPopulate : MonoBehaviour
    {
        private Transform myTransform;

        [SerializeField]
        private ScrollRect scrollRect;

        [SerializeField]
        private UFE2FTEMoveListDisplayInfo moveListDisplayInfo;

        private UFE2FTEMoveListDisplayCharacterInfo currentMoveListDisplayCharacterInfo;

        void Awake()
        {
            myTransform = transform;
        }

        // Start is called before the first frame update
        void Start()
        {
            PopulateMoveList();
        }

        // Update is called once per frame
        void Update()
        {
            if (currentMoveListDisplayCharacterInfo != null)
            {
                currentMoveListDisplayCharacterInfo.scrollRectAnchoredPosition = scrollRect.content.anchoredPosition;
            }
        }

        #region Populate Methods

        private void PopulateMoveList()
        {
            if (moveListDisplayInfo == null
                || UFE.p1ControlsScript == null
                || UFE.p2ControlsScript == null) return;

            if (UFE2FTEPlayerPausedOptionsManager.playerPaused == UFE2FTEPlayerPausedOptionsManager.Player.Player1)
            {
                PopulateMoveListWithGameObjects(UFE.p1ControlsScript);
            }
            else if (UFE2FTEPlayerPausedOptionsManager.playerPaused == UFE2FTEPlayerPausedOptionsManager.Player.Player2)
            {
                PopulateMoveListWithGameObjects(UFE.p2ControlsScript);
            }
        }

        private void PopulateMoveListWithGameObjects(ControlsScript controlsScript)
        {
            if (moveListDisplayInfo.moveListDisplayCharacterInfoPrefab != null)
            {
                UFE2FTEMoveListDisplayUI characterInfoUI = Instantiate(moveListDisplayInfo.moveListDisplayCharacterInfoPrefab, scrollRect.transform);

                RectTransform characterInfoGameObjectRectTransform = characterInfoUI.GetComponent<RectTransform>();

                characterInfoUI.SetCharacterInfoUI(controlsScript, characterInfoGameObjectRectTransform);
            }

            int length = moveListDisplayInfo.moveListDisplayCharacterInfos.Length;
            for (int i = 0; i < length; i++)
            {
                if (controlsScript.myInfo.characterName != moveListDisplayInfo.moveListDisplayCharacterInfos[i].characterName) continue;

                currentMoveListDisplayCharacterInfo = moveListDisplayInfo.moveListDisplayCharacterInfos[i];

                int lengthA = currentMoveListDisplayCharacterInfo.moveInfoOptions.Length;
                for (int moveInfoOptionsIndex = 0; moveInfoOptionsIndex < lengthA; moveInfoOptionsIndex++)
                {
                    int lengthB = currentMoveListDisplayCharacterInfo.categoryInfoOptions.Length;
                    for (int b = 0; b < lengthB; b++)
                    {
                        if (moveListDisplayInfo.moveListDisplayCategoryInfoPrefab == null
                            || moveInfoOptionsIndex != currentMoveListDisplayCharacterInfo.categoryInfoOptions[b].categoryIndex) continue;

                        UFE2FTEMoveListDisplayUI categoryInfoUI = Instantiate(moveListDisplayInfo.moveListDisplayCategoryInfoPrefab, myTransform);

                        categoryInfoUI.SetCategoryInfoUI(moveListDisplayInfo.GetCategoryInfoCategoryName(currentMoveListDisplayCharacterInfo.categoryInfoOptions[b].categoryName));
                    }

                    if (moveListDisplayInfo.moveListDisplayMoveInfoPrefab == null) continue;

                    UFE2FTEMoveListDisplayUI moveInfoUI = Instantiate(moveListDisplayInfo.moveListDisplayMoveInfoPrefab, myTransform);

                    string moveName = "";

                    if (currentMoveListDisplayCharacterInfo.moveInfoOptions[moveInfoOptionsIndex].overrideOptions.useMoveNameOverride == true)
                    {
                        moveName = currentMoveListDisplayCharacterInfo.moveInfoOptions[moveInfoOptionsIndex].overrideOptions.moveName;
                    }
                    else
                    {
                        moveName = currentMoveListDisplayCharacterInfo.moveInfoOptions[moveInfoOptionsIndex].moveInfo.moveName;
                    }

                    string moveDescription = "";

                    if (currentMoveListDisplayCharacterInfo.moveInfoOptions[moveInfoOptionsIndex].overrideOptions.useMoveDescriptionOverride == true)
                    {
                        moveDescription = currentMoveListDisplayCharacterInfo.moveInfoOptions[moveInfoOptionsIndex].overrideOptions.moveDescription;
                    }
                    else
                    {
                        moveDescription = currentMoveListDisplayCharacterInfo.moveInfoOptions[moveInfoOptionsIndex].moveInfo.description;
                    }

                    bool useCustomButtonSequenceName = false;
                    string customButtonSequenceName = "";

                    if (currentMoveListDisplayCharacterInfo.moveInfoOptions[moveInfoOptionsIndex].overrideOptions.useCustomButtonSequenceName == true)
                    {
                        useCustomButtonSequenceName = true;
                        customButtonSequenceName = currentMoveListDisplayCharacterInfo.moveInfoOptions[moveInfoOptionsIndex].overrideOptions.customButtonSequenceName;
                    }
                    else
                    {
                        useCustomButtonSequenceName = false;
                    }

                    bool useCustomButtonExecutionDirectionName = false;
                    string customButtonExecutionDirectionName = "";

                    if (currentMoveListDisplayCharacterInfo.moveInfoOptions[moveInfoOptionsIndex].overrideOptions.useCustomButtonExecutionDirectionName == true)
                    {
                        useCustomButtonExecutionDirectionName = true;
                        customButtonExecutionDirectionName = currentMoveListDisplayCharacterInfo.moveInfoOptions[moveInfoOptionsIndex].overrideOptions.customButtonExecutionDirectionName;
                    }
                    else
                    {
                        useCustomButtonExecutionDirectionName = false;
                    }

                    bool useCustomButtonExecutionButtonName = false;
                    string customButtonExecutionButtonName = "";

                    if (currentMoveListDisplayCharacterInfo.moveInfoOptions[moveInfoOptionsIndex].overrideOptions.useCustomButtonExecutionButtonName == true)
                    {
                        useCustomButtonExecutionButtonName = true;
                        customButtonExecutionButtonName = currentMoveListDisplayCharacterInfo.moveInfoOptions[moveInfoOptionsIndex].overrideOptions.customButtonExecutionButtonName;
                    }
                    else
                    {
                        useCustomButtonExecutionButtonName = false;
                    }

                    moveInfoUI.SetMoveInfoUI
                        (controlsScript,
                        currentMoveListDisplayCharacterInfo.moveInfoOptions[moveInfoOptionsIndex].moveInfo.defaultInputs,
                        useCustomButtonSequenceName,
                        customButtonSequenceName,
                        useCustomButtonExecutionDirectionName,
                        customButtonExecutionDirectionName,
                        useCustomButtonExecutionButtonName,
                        customButtonExecutionButtonName,
                        moveName,
                        moveDescription);
                }
            }

            scrollRect.content.anchoredPosition = currentMoveListDisplayCharacterInfo.scrollRectAnchoredPosition;
        }

        #endregion
    }
}
