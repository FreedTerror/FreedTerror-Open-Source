using UFE3D;
using UnityEngine;

namespace FreedTerror.UFE2
{
    public class ChallengeModeDisplayGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] challengeModeDisplayGameObjectArray;

        private void Update()
        {
            if (UFE.gameMode == GameMode.ChallengeMode)
            {
                UFE2Manager.SetGameObjectActive(challengeModeDisplayGameObjectArray, true);
            }
            else
            {
                UFE2Manager.SetGameObjectActive(challengeModeDisplayGameObjectArray, false);
            }  
        }
    }
}