using UnityEngine;
using UFE3D;

namespace FreedTerror.UFE2
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
        private GameObject gameObjectToSpawn;
        [SerializeField]
        private Transform spawnParent;   

        private void Start()
        {
            Populate(UFE2Manager.GetControlsScript(UFE2Manager.instance.pausedPlayer));
        }

        private void Populate(ControlsScript player)
        {
            if (UFE2Manager.instance.characterInfoReferencesScriptableObject == null
                || gameObjectToSpawn == null
                || spawnParent == null
                || player == null)
            {
                return;
            }

            gameObjectToSpawn.SetActive(false);

            MoveListScriptableObject moveListScriptableObject = UFE2Manager.instance.characterInfoReferencesScriptableObject.GetMoveListScriptableObject(player.myInfo);
            if (moveListScriptableObject != null)
            {
                int length = moveListScriptableObject.moveListOptionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    var item = moveListScriptableObject.moveListOptionsArray[i];

                    if (UFE2Manager.IsCombatStancesMatch(player.MoveSet.currentCombatStance, item.combatStanceArray) == false)
                    {
                        continue;
                    }

                    int lengthA = item.moveInfoArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        var itemA = item.moveInfoArray[a];

                        if (itemA == null)
                        {
                            continue;
                        }

                        var newGameObject = Instantiate(gameObjectToSpawn, spawnParent);
                        newGameObject.SetActive(true);

                        CallOnPopulateEvent(itemA);
                    }

                    break;
                }
            }
        }
    }
}