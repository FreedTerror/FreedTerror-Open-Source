using UFE3D;
using UnityEngine;

namespace UFE2FTE
{
    public class ChallengeModeDisplayGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] challengeModeDisplayGameObjectArray;

        private void Update()
        {
            if (UFE.gameMode == GameMode.ChallengeMode)
            {
                UFE2FTE.SetGameObjectActive(challengeModeDisplayGameObjectArray, true);
            }
            else
            {
                UFE2FTE.SetGameObjectActive(challengeModeDisplayGameObjectArray, false);
            }  
        }
    }
}