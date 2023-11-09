using UnityEngine;

namespace UFE2FTE
{
    public class CharacterGroundBounceTimesGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private int groundBounceTimes = 1;
        [SerializeField]
        private GameObject[] gameObjectArray;


        private void Update()
        {
            if (UFE2FTE.GetControlsScript(player) == null
                || UFE2FTE.GetControlsScript(player).opControlsScript == null
                || UFE2FTE.GetControlsScript(player).opControlsScript.Physics == null)
            {
                return;
            }

            if (UFE2FTE.GetControlsScript(player).opControlsScript.Physics.groundBounceTimes >= groundBounceTimes)
            {
                UFE2FTE.SetGameObjectActive(gameObjectArray, true);
            }
            else
            {
                UFE2FTE.SetGameObjectActive(gameObjectArray, false);
            }
        }
    }
}