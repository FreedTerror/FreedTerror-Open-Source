using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class MoveListPopulateUIController : MonoBehaviour
    {
        public delegate void OnPopulateEventHandler(MoveInfo moveInfo);
        public static event OnPopulateEventHandler OnPopulateEvent;
        public static void CallOnPopulateEvent(MoveInfo moveInfo)
        {
            if (OnPopulateEvent == null)
            {
                return;
            }

            OnPopulateEvent(moveInfo);
        }

        [SerializeField]
        private CharacterInfoReferencesScriptableObject characterInfoReferencesScriptableObject;
        [SerializeField]
        private GameObject moveInfoDisplayPrefab;
        [SerializeField]
        private Transform spawnParent;   

        private void Awake()
        {
            Populate(UFE2FTE.GetControlsScript(UFE2FTE.instance.pausedPlayer));
        }

        private void Populate(ControlsScript player)
        {
            if (characterInfoReferencesScriptableObject == null
                || moveInfoDisplayPrefab == null
                || spawnParent == null
                || player == null)
            {
                return;
            }

            if (moveInfoDisplayPrefab.activeInHierarchy == true)
            {
                moveInfoDisplayPrefab.SetActive(false);
            }

            MoveListScriptableObject moveListScriptableObject = characterInfoReferencesScriptableObject.GetMoveListScriptableObject(player.myInfo);
            if (moveListScriptableObject != null)
            {
                int length = moveListScriptableObject.moveListOptionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (UFE2FTE.IsCombatStancesMatch(player.MoveSet.currentCombatStance, moveListScriptableObject.moveListOptionsArray[i].combatStanceArray) == false)
                    {
                        continue;
                    }

                    int lengthA = moveListScriptableObject.moveListOptionsArray[i].moveInfoArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (moveListScriptableObject.moveListOptionsArray[i].moveInfoArray[a] == null)
                        {
                            continue;
                        }

                        GameObject spawnedGameObject = Instantiate(moveInfoDisplayPrefab, spawnParent);
                        spawnedGameObject.SetActive(true);

                        CallOnPopulateEvent(moveListScriptableObject.moveListOptionsArray[i].moveInfoArray[a]);
                    }

                    break;
                }
            }
        }
    }
}