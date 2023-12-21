using UnityEngine;
using UFENetcode;

namespace UFE2FTE
{
    public class CastMoveOnLandingOverrideController : UFEBehaviour, UFEInterface
    {
        [SerializeField]
        private string[] moveNameArray;

        public void FixedUpdate()
        {
            UFE2FTE.TryCastMoveOnLandingOverrideAllControlsScripts(moveNameArray);   
        }
    }
}